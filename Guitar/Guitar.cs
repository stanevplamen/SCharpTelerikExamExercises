using System;
using System.Collections.Generic;

class Guitar
{
    static int maxVolume;
    static int minVolume;
    static int startVolume;
    static int sum = 0;

    static void Main()
    {
        string notUSed = Console.ReadLine();                       
        string firstLineInput = Console.ReadLine();                      
        startVolume = int.Parse(Console.ReadLine());                      
        maxVolume = int.Parse(Console.ReadLine());                     
        minVolume = 0;

        string[] numbsStr = firstLineInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int[] numbs = ConvertToInt(numbsStr);
        if (sum + startVolume <= maxVolume)
        {
            Console.WriteLine(sum + startVolume);
        }
        else
        {
            BFSVolume(numbs);
        }
    }

    private static int[] ConvertToInt(string[] numbsStr)
    {
        int[] numbs = new int[numbsStr.Length];
        for (int i = 0; i < numbsStr.Length; i++)
        {
            numbs[i] = int.Parse(numbsStr[i].Trim());
            sum = sum + numbs[i];
        }
        return numbs;
    }

    private static void BFSVolume(int[] numbs)
    {
        Queue<int> volumesQ = new Queue<int>();
        int maxAskedVolume = -1;
        volumesQ.Enqueue(startVolume);
        int i = 0;
        int counter = 0;
        int cycles = 0;
        int firstCount = 0;
        while (volumesQ.Count > 0)
        {
            cycles++;
            int currentVolume = volumesQ.Dequeue();
            if (i == numbs.Length)
            {
                if (maxAskedVolume < currentVolume)
                {
                    maxAskedVolume = currentVolume;
                }
            }

            if (i < numbs.Length && currentVolume + numbs[i] >= minVolume && currentVolume + numbs[i] <= maxVolume)
            {
                volumesQ.Enqueue(currentVolume + numbs[i]);
                counter++;
            }

            if (i < numbs.Length && currentVolume - numbs[i] >= minVolume && currentVolume - numbs[i] <= maxVolume)
            {
                volumesQ.Enqueue(currentVolume - numbs[i]);
                counter++;
            }

            if (i != 0 && cycles == firstCount || i == 0)
            {
                i++;
                firstCount = counter; 
                cycles = 0;
                counter = 0;
            }
        }
        Console.WriteLine(maxAskedVolume);
    }
}

