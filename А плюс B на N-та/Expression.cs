using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

class Expression
{
    static void Main()
    {
        string input = Console.ReadLine(); //"(c+y)";
        string notUsed = Console.ReadLine();
        int n = int.Parse(Console.ReadLine()); //50;
        char first = input[1];
        char second = input[3];
        int k = 0;
        BigInteger factorialN = 1;
        for (int i = 1; i <= n; i++)
        {
            factorialN = factorialN * i;
        }

        StringBuilder sb = new StringBuilder();
        int currentPowerfirst = n;
        int currentPowersecond = 0;
        for (int i = 1; i <= n + 1; i++)
        {
            BigInteger factorialK = 1;
            for (int j = 1; j <= k; j++)
            {
                factorialK = factorialK * j;
            }
            BigInteger factorialNminusK = 1;
            for (int j = 1; j <= n - k; j++)
            {
                factorialNminusK = factorialNminusK * j;
            }

            // za vs. k: N! / ((N-K)! * K!)

            BigInteger tempResult = factorialN / (factorialK * factorialNminusK);

            if (k == 0)
            {
                sb.AppendFormat("({0}^{1})+", first, currentPowerfirst);
            }
            else if (k == n)
            {
                sb.AppendFormat("({0}^{1})", second, currentPowersecond);
            }
            else
            {
                sb.AppendFormat("{0}({1}^{2})({3}^{4})+", tempResult, first, currentPowerfirst, second, currentPowersecond);
            }
            currentPowerfirst--;
            currentPowersecond++;
            k++;
        }
        string result = sb.ToString();
        Console.WriteLine(result);
    }
}
