using System;

class Wizard
{
    static void Main()
    {
        string inputNumbs = Console.ReadLine(); //"4 4";
        string inputLine = Console.ReadLine(); //"GGGGBBBBBGG";

        string[] splitedInput = inputNumbs.Split();

        int maxRights = int.Parse(splitedInput[0]); 
        int maxWrongs = int.Parse(splitedInput[1]);

        int[] arrayOfSum = new int[inputLine.Length + 1];
        int counter = 0;
        arrayOfSum[0] = 0;
        bool gCycle = false;
        bool bCycle = false;

        for (int i = 0; i < inputLine.Length ; i++)
        {
            counter++;
            if (inputLine[i] == 'G')
            {
                if (bCycle == true)
                {
                    counter = 1;
                }
                gCycle = true;
                bCycle = false;
                if (counter <= maxRights)
                {
                    arrayOfSum[i + 1] = arrayOfSum[i] + 1;
                }
                else
                {
                    arrayOfSum[i + 1] = arrayOfSum[i];
                    counter = 0;
                }
            }
            else // (inputLine[i] == 'B')
            {
                if (gCycle == true)
                {
                    counter = 1;
                }
                bCycle = true;
                gCycle = false;
                if (counter <= maxWrongs)
                {
                    arrayOfSum[i + 1] = arrayOfSum[i] + 1;
                }
                else
                {
                    arrayOfSum[i + 1] = arrayOfSum[i];
                    counter = 0;
                }
            }
        }
        Console.WriteLine(arrayOfSum[arrayOfSum.Length - 1]);
    }
}

