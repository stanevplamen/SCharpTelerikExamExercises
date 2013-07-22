using System;

class Guitar
{
    static int maxVolume;
    static int minVolume;
    static int startVolume;

    static void Main()
    {
        string notUsed = Console.ReadLine();                  
        string firstLineInput =  Console.ReadLine();                 
        startVolume = int.Parse(Console.ReadLine());              
        maxVolume = int.Parse(Console.ReadLine());                
        minVolume = 0;

        string[] numbsStr = firstLineInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int[] numbs = ConvertToInt(numbsStr);

        DynamicProgrammingSolution(numbs);
    }

    private static void DynamicProgrammingSolution(int[] numbs)
    {
        int[,] possibleResults = new int[numbs.Length + 1, maxVolume + 1];
        possibleResults[0, startVolume] = 1;

        for (int row = 1; row < possibleResults.GetLength(0); row++)
        {
            //int lowerBoargerCol = -1;
            //int biggerBoarderCol = maxVolume;
            for (int col = 0; col < possibleResults.GetLength(1); col++)
            {
                if (possibleResults[row - 1, col] == 1)
                {
                    if (col + numbs[row - 1] <= maxVolume)
                    {
                        possibleResults[row, col + numbs[row - 1]] = 1;
                    }
                    if (col - numbs[row - 1] >= minVolume)
                    {
                        possibleResults[row, col - numbs[row - 1]] = 1;
                    }
                }
            }
        }

        int biggerCurrenVolume = -1;
        for (int col = 0; col < possibleResults.GetLength(1); col++)
        {
            if (possibleResults[possibleResults.GetLength(0) - 1, col] == 1)
            {
                if (biggerCurrenVolume < col)
                {
                    biggerCurrenVolume = col;
                }
            }
        }
        Console.WriteLine(biggerCurrenVolume);
    }

    private static int[] ConvertToInt(string[] numbsStr)
    {
        int[] numbs = new int[numbsStr.Length];
        for (int i = 0; i < numbsStr.Length; i++)
        {
            numbs[i] = int.Parse(numbsStr[i].Trim());
        }
        return numbs;
    }
}