Notes
1. drmemory.exe -show_reachable -logdir "C:\logs" Publisher.exe -- runnng debugger
2. save dll and lib in the folder c:\s2trading\zmqhubresource

Run the FeedPublisher using the below two steps
cd C:\App\db\src\FeedHandler\x64\Release
FeedPublisher.exe 10.223.105.91:5551 158.69.193.253:5551

Run the FillPublisher to publish the fills received from the FixServer
C:\App\db\src\FeedHandler\x64\Release\FillPublisher.exe