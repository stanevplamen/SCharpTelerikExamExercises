using System;
using System.Collections.Generic;

class TwoIsBetterThanOne
{
    static void Main()
    {
        string inputNumbes = Console.ReadLine();
        string[] splitedInput = inputNumbes.Split();

        long firstBoarder = long.Parse(splitedInput[0]);
        long secondBoarder = long.Parse(splitedInput[1]);

        int luckyCounter = FindLuckyNumbers(firstBoarder, secondBoarder);
        Console.WriteLine(luckyCounter);

        string inputList = Console.ReadLine(); 
        string[] splitInput = inputList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        int[] sortedNumbers = new int[splitInput.Length];
        for (int i = 0; i < sortedNumbers.Length; i++)
        {
            sortedNumbers[i] = int.Parse(splitInput[i]);
        }

        Array.Sort(sortedNumbers);

        string percentage = Console.ReadLine(); 
        int percentNumber = int.Parse(percentage);

        int index = FindPergentageArrayElement(sortedNumbers, percentNumber);
        Console.WriteLine(sortedNumbers[index]);
    }

    #region FirstPart
    private static int FindLuckyNumbers(long firstBoarder, long secondBoarder)
    {
        long maxNumber = (long)Math.Pow(10,18);

        int maxLength = secondBoarder.ToString().Length;
        List<string> allCases = new List<string>();

        allCases.Add("3");
        allCases.Add("5");
        int startCycle = 0;
        while (allCases[allCases.Count - 1].Length < maxLength)
        {
            int tempEnd = allCases.Count;
            for (int i = startCycle; i < tempEnd; i++)
            {
                allCases.Add(allCases[i] + "3");
                allCases.Add(allCases[i] + "5");
            }
            startCycle = tempEnd;
        }

        int counterPalindromes = 0;
        for (int i = 0; i < allCases.Count; i++)
        {
            long currentNumbParsed = long.Parse(allCases[i]);
            if (currentNumbParsed >= firstBoarder && currentNumbParsed <= secondBoarder && CheckIsPalindrome(allCases[i]) == true)
            {
                counterPalindromes++;
            }
        }
        return counterPalindromes;
    }

    private static bool CheckIsPalindrome(string stringToCheck)
    {
        bool isPalindrome = true;
        for (int i = 0; i < stringToCheck.Length; i++)
        {
            if (stringToCheck[i] != stringToCheck[stringToCheck.Length - 1 - i])
            {
                isPalindrome = false;
                break;
            }
        }
        return isPalindrome;
    }
    #endregion

    #region SecondPart
    private static int FindPergentageArrayElement(int[] sortedNumbers, int percentNumber)
    {
        if (sortedNumbers.Length % 2 == 0 && percentNumber % 10 > 5)
        {
            int allElements = sortedNumbers.Length;
            int checkNumber = (int)(sortedNumbers.Length * percentNumber / 100);
            return checkNumber;
        }
        else if (sortedNumbers.Length % 2 == 0)
        {
            int allElements = sortedNumbers.Length;
            int checkNumber = (int)(sortedNumbers.Length * percentNumber / 100 - 1);
            return checkNumber;
        }
        else
        {
            int allElements = sortedNumbers.Length;
            int checkNumber = (int)(sortedNumbers.Length * percentNumber / 100.0);
            return checkNumber;
        }
    }
    #endregion
}

