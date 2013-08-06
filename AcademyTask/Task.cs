using System;
using System.Collections.Generic;

class Task
{
    static int[,] matrixSums;
    static int[] numbersArr;
    static int rowIndex;
    static int ColIndex;
    static List<Tuple<int, int>> impIndexes;

    static void Main()
    {
        string allNumbers = Console.ReadLine();
        string[] splitedInputNumbs = allNumbers.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        int numberToCompare = int.Parse(Console.ReadLine());

        LoadTheNumbers(splitedInputNumbs);

        LoadTheSums();

        FindTheResultIndexes(numberToCompare);

        CalculateAndPrintResult();
    }

    private static void CalculateAndPrintResult()
    {
        int tempResult = 0;
        int minResult = int.MaxValue;

        foreach (var pair in impIndexes)
        {
            int rowSign = pair.Item1 % 2;
            int colSign = pair.Item2 % 2;

            int rowDiv = pair.Item1 / 2;
            int colDiv = pair.Item2 / 2;

            if (rowSign == 1 && colSign == 1)
            {
                tempResult = Math.Max(rowDiv, colDiv) + 1;
            }
            else if (rowSign == 0 && colSign == 0)
            {
                tempResult = Math.Max(rowDiv, colDiv) + 1;
            }
            else if (rowSign == 1 && colSign == 0)
            {
                tempResult = Math.Max(rowDiv, colDiv) + 1;
            }
            else if (rowSign == 0 && colSign == 1 && pair.Item1 > pair.Item2)
            {
                tempResult = Math.Max(rowDiv, colDiv) + 1;
            }
            else if (rowSign == 0 && colSign == 1 && pair.Item1 < pair.Item2)
            {
                tempResult = Math.Max(rowDiv, colDiv) + 2;
            }
            if (tempResult < minResult)
            {
                minResult = tempResult;
            }
        }
        if (minResult < int.MaxValue)
        {
            Console.WriteLine(minResult);
        }
        else
        {
            Console.WriteLine(numbersArr.Length);
        }
    }

    private static void FindTheResultIndexes(int numberToCompare)
    {
        impIndexes = new List<Tuple<int, int>>();
        for (int row = 1; row < matrixSums.GetLength(0); row++)
        {
            for (int col = 1; col < matrixSums.GetLength(1); col++)
            {
                if (matrixSums[row, col] >= numberToCompare)
                {
                    rowIndex = row;
                    ColIndex = col;
                    impIndexes.Add(new Tuple<int, int>(rowIndex, ColIndex));
                }
            }
        }
    }

    private static void LoadTheSums()
    {
        matrixSums = new int[numbersArr.Length + 1, numbersArr.Length + 1];

        for (int row = 1; row < matrixSums.GetLength(0); row++)
        {
            for (int col = 1; col < matrixSums.GetLength(1); col++)
            {
                matrixSums[row, col] = (numbersArr[row - 1] - numbersArr[col - 1]);
            }
        }
    }

    private static void LoadTheNumbers(string[] splitedInputNumbs)
    {
        numbersArr = new int[splitedInputNumbs.Length];

        for (int i = 0; i < numbersArr.Length; i++)
        {
            numbersArr[i] = int.Parse(splitedInputNumbs[i].Trim());
        }
    }
}