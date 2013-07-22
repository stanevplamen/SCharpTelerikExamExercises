using System;

class Program
{
    static void Main()
    {
        int arrayLength = int.Parse(Console.ReadLine());//5;
        string inputLine = Console.ReadLine(); //  "1 2 3 4 5";

        string[] numbsStr = inputLine.Split();

        int indicatorNumb = int.Parse(Console.ReadLine()); //3;

        int[] numbersToCompare = new int[numbsStr.Length];

        for (int i = 0; i < numbsStr.Length; i++)
        {
            numbersToCompare[i] = int.Parse(numbsStr[i]);
        }
        
        int[,] resultMatrix = new int[arrayLength, arrayLength];

        for (int col = 0; col < resultMatrix.GetLength(1); col++)
        {
            for (int row = col+1; row < resultMatrix.GetLength(0); row++)
            {
                resultMatrix[row, col] = Math.Abs(numbersToCompare[col] - numbersToCompare[row]);
            }
        }

        int minRow = 0;
        int minCol = 0;
        bool isBreak = false;
        for (int row = 1; row < resultMatrix.GetLength(0); row++)
        {
            for (int col = 0; col <= row; col++)
            {
                if (resultMatrix[row, col] >= indicatorNumb)
                {
                    minRow = row;
                    minCol = col;
                    isBreak = true;
                    break;
                }
            }
            if (isBreak == true)
            {
                break;
            }
        }
        int sum = 1;
        if (true)
        {
            int divideOne = minCol / 2;
            int remainOne = minCol % 2;
            if (remainOne == 0)
            {
                sum = sum + divideOne;
            }
            else
            {
                sum = sum + divideOne + 1;
            }

            int substractOne = minRow - minCol;
            int divideTwo = substractOne / 2;
            int remainTwo = substractOne % 2;

            if (remainTwo == 0)
            {
                sum = sum + divideTwo;
            }
            else
            {
                sum = sum + divideTwo + 1;
            }
        }
        if (sum == 1)
        {
            Console.WriteLine(arrayLength);
        }
        else
        {
            Console.WriteLine(sum);
        }
    }
}

