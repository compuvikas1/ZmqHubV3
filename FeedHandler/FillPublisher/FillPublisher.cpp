// FillPublisher.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include "zmq.hpp"

//  We wait for 10 subscribers
#define SUBSCRIBERS_EXPECTED  10


//typedef void(*sig_t)(int);

//  Provide random number from 0..(num-1)
#define randof(num)  (int) ((float) (num) * random () / (RAND_MAX + 1.0))

int gotSignalToStop;

//  Receive 0MQ string from socket and convert into C string
//  Caller must free returned string. Returns NULL if the context
//  is being terminated.
static char * s_recv(void *socket) {
	char buffer[256];
	int size = zmq_recv(socket, buffer, 255, 0);
	if (size == -1)
		return NULL;
	//return strndup(buffer, sizeof(buffer) - 1);
	return _strdup(buffer);
	// remember that the strdup family of functions use malloc/alloc for space for the new string.  It must be manually
	// freed when you are done with it.  Failure to do so will allow a heap attack.
}

//  Convert C string to 0MQ string and send to socket
int s_send(void *socket, char *string) {
	int size = zmq_send(socket, string, strlen(string), 0);
	//std::cout << "ZMQ ["<< size <<"]" << std::endl;
	return size;
}

//  Sends string as 0MQ string, as multipart non-terminal
static int s_sendmore(void *socket, char *string) {
	int size = zmq_send(socket, string, strlen(string), ZMQ_SNDMORE);
	return size;
}

//  Receives all message parts from socket, prints neatly
//
static void s_dump(void *socket)
{
	int rc;

	zmq_msg_t message;
	rc = zmq_msg_init(&message);
	assert(rc == 0);

	puts("----------------------------------------");
	//  Process all parts of the message
	do {
		int size = zmq_msg_recv(&message, socket, 0);
		assert(size >= 0);

		//  Dump the message as text or binary
		char *data = (char*)zmq_msg_data(&message);
		assert(data != 0);
		int is_text = 1;
		int char_nbr;
		for (char_nbr = 0; char_nbr < size; char_nbr++) {
			if ((unsigned char)data[char_nbr] < 32
				|| (unsigned char)data[char_nbr] > 126) {
				is_text = 0;
			}
		}

		printf("[%03d] ", size);
		for (char_nbr = 0; char_nbr < size; char_nbr++) {
			if (is_text) {
				printf("%c", data[char_nbr]);
			}
			else {
				printf("%02X", (unsigned char)data[char_nbr]);
			}
		}
		printf("\n");
	} while (zmq_msg_more(&message));

	rc = zmq_msg_close(&message);
	assert(rc == 0);
}

static void s_set_id(void *socket, intptr_t id)
{
	char identity[10];
	sprintf_s(identity, "%04X", (int)id);
	zmq_setsockopt(socket, ZMQ_IDENTITY, identity, strlen(identity));
}

//  Sleep for a number of milliseconds
static void s_sleep(int msecs)
{
	Sleep(msecs);
}

//  Return current system clock as milliseconds
static int64_t s_clock(void)
{
	SYSTEMTIME st;
	GetSystemTime(&st);
	return (int64_t)st.wSecond * 1000 + st.wMilliseconds;
}

int main() {
	zmq::context_t context(1);
	zmq::socket_t publisher(context, ZMQ_PUB);

	int sndhwm = 0;
	publisher.setsockopt(ZMQ_SNDHWM, &sndhwm, sizeof(sndhwm));
	publisher.bind("tcp://158.69.193.253:5571");
	zmq::socket_t syncservice(context, ZMQ_REP);
	syncservice.bind("tcp://158.69.193.253:5572");
	std::cout << "REP:5572 , PUB:5571" << std::endl;
	while (1) 
	{
		char *fillResponse = s_recv(syncservice);		
		fillResponse[strlen(fillResponse) - 1] = '\0';
		std::cout << " Received [" << fillResponse << "]" << std::endl;
		s_send(publisher, fillResponse);
		free(fillResponse);
		s_send(syncservice, "OK");
	}
	zmq_close(publisher);
	zmq_close(syncservice);
	zmq_ctx_destroy(context);
	return 0;
}
