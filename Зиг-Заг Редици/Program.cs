using System;
using System.Collections.Generic;

class NumberVariations
{
    static void Variations(int index, bool[] used, int[] vector, int numbersLength)
    {
        if (index == -1)
        {
            CheckZigZag(vector);
        }
        else
        {
            for (int i = 0; i < numbersLength; i++)
            {
                vector[index] = i;
                if (used[i]) continue;

                used[i] = true;
                Variations(index - 1, used, vector, numbersLength);
                used[i] = false;
            }
        }
    }

    static int counter = 0;

    static void CheckZigZag(int[] vector)
    {
        bool isCount = true;
        for (int i = 0; i < vector.Length; i++)
        {
            if (i % 2 == 0)
            {
                if (i - 1 >= 0 && vector[i] <= vector[i - 1])
                {
                    isCount = false;
                }
                if (i + 1 < vector.Length && vector[i] <= vector[i + 1])
                {
                    isCount = false;
                }

            }
        }
        if (isCount == true)
        {
            for (int i = 0; i < vector.Length; i++)
            {
                //Console.Write("{0} ", vector[i]);
                counter++;
            }
            //Console.WriteLine();
        }
    }

    static void Main()
    {
        string[] twoNumbers = Console.ReadLine().Split();
        int numbersAmount = int.Parse(twoNumbers[0]);
        int variationsAmount = int.Parse(twoNumbers[1]);
        //Console.Write("Please enter the amount of the numbers 1...N, N = ");
        //int numbersAmount = 4;// int.Parse(Console.ReadLine());
        //Console.Write("Please enter the amount of the variations K = ");
        //int variationsAmount = 1; //int.Parse(Console.ReadLine());
        int[] vector = new int[variationsAmount];
        bool[] used = new bool[numbersAmount];
        Variations(variationsAmount - 1, used, vector, numbersAmount);
        Console.WriteLine(counter / variationsAmount);
    }
}
