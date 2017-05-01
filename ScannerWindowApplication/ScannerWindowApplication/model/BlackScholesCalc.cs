using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerWindowApplication.model
{
    public class BlackScholesCalc
    {
        private const double P = 0.2316419;
        private const double B1 = 0.319381530;
        private const double B2 = -0.356563782;
        private const double B3 = 1.781477937;
        private const double B4 = -1.821255978;
        private const double B5 = 1.330274429;

        public double bs_price(string cp_flag, double S, double K, double T, double r, double v, double q = 0.0)
        {
            double d1 = (Math.Log(S / K) + (r + v * v / 2.0) * T) / (v * Math.Sqrt(T));
            double d2 = d1 - v * Math.Sqrt(T);
            double price = 0;

            if (cp_flag == "c")
                price = S * Math.Exp(-q * T) * cumulativeDistribution(d1) - K * Math.Exp(-r * T) * cumulativeDistribution(d2);
            else
                price = K * Math.Exp(-r * T) * cumulativeDistribution(-d2) - S * Math.Exp(-q * T) * cumulativeDistribution(-d1);

            return price;
        }

        public double bs_vega(string cp_flag, double S, double K, double T, double r, double v, double q = 0.0)
        {
            double d1 = (Math.Log(S / K) + (r + v * v / 2.0) * T) / (v * Math.Sqrt(T));
            return S * Math.Sqrt(T) * standardNormalDistribution(d1);
        }


        public double cumulativeDistribution(double x)
        {
            double t = 1 / (1 + P * Math.Abs(x));
            double t1 = B1 * Math.Pow(t, 1);
            double t2 = B2 * Math.Pow(t, 2);
            double t3 = B3 * Math.Pow(t, 3);
            double t4 = B4 * Math.Pow(t, 4);
            double t5 = B5 * Math.Pow(t, 5);
            double b = t1 + t2 + t3 + t4 + t5;
            double cd = 1 - standardNormalDistribution(x) * b;
            return x < 0 ? 1 - cd : cd;
        }

        public double standardNormalDistribution(double x)
        {
            double top = Math.Exp(-0.5 * Math.Pow(x, 2));
            double bottom = Math.Sqrt(2 * Math.PI);
            return top / bottom;
        }

        public double find_vol(double target_value, string call_put, double S, double K, double T, double r)
        {
            int MAX_ITERATIONS = 100;
            //double PRECISION = 1.0e-5;
            double PRECISION = 0.00001;

            double sigma = 0.5;
            int i = 0;
            for (i = 0; i < MAX_ITERATIONS; i++)
            {
                double price = bs_price(call_put, S, K, T, r, sigma);
                double vega = bs_vega(call_put, S, K, T, r, sigma);

                double diff = target_value - price;  // our root

                //Console.WriteLine(i + " " + sigma + " " + diff);

                if (Math.Abs(diff) < PRECISION)
                {
                    return sigma;
                }
                sigma = sigma + diff / vega; // f(x) / f'(x)
            }
            Console.WriteLine(" i = " + i);
            //value wasn't found, return best guess so far
            return sigma;
        }

        public double[] getAllGreeks(string CallPutFlag, double S, double K, double T, double r, double v)
        {
            double[] Greeks = new double[4];

            double d1, d2;

            d1 = (Math.Log(S / K) + v * v * T / 2) / (v * Math.Sqrt(T));
            d2 = d1 - v * Math.Sqrt(T);

            //Options Price
            if (CallPutFlag == "c")
            {
                //OptionPrice
                //Greeks[0] = S * cumulativeDistribution(d1) - K * Math.Exp(-r * T) * cumulativeDistribution(d2);
                Greeks[0] = bs_price(CallPutFlag, S, K, T, r, v);
                //Delta
                Greeks[1] = Math.Exp(-r * T) * cumulativeDistribution(d1);
                //Gamma    
                Greeks[2] = Math.Exp(-r * T) * d1 / (S * v * Math.Sqrt(T));
                //Vega    
                Greeks[3] = 0.01 * S * Math.Exp(-r * T) * d1 * Math.Sqrt(T);
                Console.WriteLine("D " + Greeks[1] + " G " + Greeks[2] + " v " + Greeks[3]);
            }
            else
            {
                //OptionPrice
                Greeks[0] = bs_price(CallPutFlag, S, K, T, r, v);
                //Delta
                Greeks[1] = Math.Exp(-r * T) * (cumulativeDistribution(d1) - 1);
                //Gamma    
                Greeks[2] = Math.Exp(-r * T) * d1 / (S * v * Math.Sqrt(T));
                //Vega    
                Greeks[3] = 0.01 * S * Math.Exp(-r * T) * d1 * Math.Sqrt(T);
                Console.WriteLine("D " + Greeks[1] + " G " + Greeks[2] + " v " + Greeks[3]);
            }

            return Greeks;
        }

    }

}
