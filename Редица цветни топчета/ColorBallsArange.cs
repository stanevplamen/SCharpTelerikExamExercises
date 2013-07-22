using System;
using System.Collections.Generic;
using System.Numerics;

class ColorBallsArange
{
    static void Main()
    {
        string input = Console.ReadLine(); //"RYYRYBY";
        Dictionary<char, int> ballsDict = new Dictionary<char, int>();
        int temp = 0;
        char currentKey = ' ';
        for (int i = 0; i < input.Length; i++)
        {
            bool exists = false;
            foreach (KeyValuePair<char, int> kvp in ballsDict)
            {
                if (kvp.Key == input[i])
                {
                    exists = true;
                    temp = kvp.Value + 1;
                    currentKey = kvp.Key;
                }
            }
            if (exists == false)
            {
                ballsDict.Add(input[i], 1);
            }
            else if (exists == true)
            {
                ballsDict[input[i]] = temp;
            }
        }
        BigInteger nominator = 1;
        BigInteger denominator = 1;
        int counter = 1;
        foreach (KeyValuePair<char, int> kvp in ballsDict)
        {
            temp = kvp.Value;
            for (int i = 1; i <= temp; i++)
            {
                denominator = denominator * i;
            }
            counter++;
        }

        for (int i = 1; i <= input.Length ; i++)
        {
             nominator = nominator * i;
        }

        BigInteger result = nominator / denominator;
        Console.WriteLine(result);
    }
}

