using System;
using System.Numerics;

class CoordinateCamel
{
    static void Main()
    {
        int numbersAmountN = int.Parse(Console.ReadLine());
        int variationsAmountK = numbersAmountN;

        BigInteger meters = 0;
        for (int i = 0; i < numbersAmountN; i++)
        {
            string notUsed = Console.ReadLine();
            string[] twoNumbers = Console.ReadLine().Split();
            int x = int.Parse(twoNumbers[0]);
            int y = int.Parse(twoNumbers[1]);

            meters = meters + Math.Abs(x) + Math.Abs(y);
        }

        BigInteger result = 0;
        for (int k = 1; k <= variationsAmountK; k++)
        {

            BigInteger factorialKminus = 1;
            for (int j = 1; j <= k - 1; j++)
            {
                factorialKminus = factorialKminus * j;
            }

            BigInteger factorialNminusK = 1;
            for (int j = 1; j <= numbersAmountN - k; j++)
            {
                factorialNminusK = factorialNminusK * j;
            }

            BigInteger factorialN = 1;
            for (int j = 1; j <= numbersAmountN; j++)
            {
                factorialN = factorialN * j;
            }

            BigInteger repeatingTimes = factorialN / (factorialNminusK * factorialKminus * numbersAmountN );
            if (k == numbersAmountN)
            {
                repeatingTimes = 1;
            }

            result = result + repeatingTimes * meters;
        }
        Console.WriteLine(result);       
    }
}

