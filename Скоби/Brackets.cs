using System;

class Brackets
{
    static void Main()
    {
        string inputExpression = Console.ReadLine(); //"";
        int lastIndex = inputExpression.LastIndexOf("(");
        int allLength = inputExpression.Length;

        if (lastIndex == allLength - 1 || inputExpression[0] == ')' || allLength % 2 != 0)
        {
            Console.WriteLine(0);
            Environment.Exit(0);
        }

        long[,] resultMatrix = new long[allLength + 1, allLength + 1];
        resultMatrix[0,0] = 1;

        for (int row = 1; row < resultMatrix.GetLength(0); row++)
        {
            for (int col = 0; col < resultMatrix.GetLength(1); col++)
            {
                if (inputExpression[row-1] == '?')
                {
                    if (col - 1 < 0)
                    {
                        resultMatrix[row, col] = resultMatrix[row - 1, col + 1];
                    }
                    else if (col + 1 >= resultMatrix.GetLength(1))
                    {
                        resultMatrix[row, col] = resultMatrix[row - 1, col - 1];
                    }
                    else
                    {
                        resultMatrix[row, col] = resultMatrix[row - 1, col - 1] + resultMatrix[row - 1, col + 1];
                    }
                }
                else if (inputExpression[row - 1] == '(')
                {
                    if (col - 1 < 0)
                    {
                        resultMatrix[row, col] = 0;
                    }
                    else
                    {
                        resultMatrix[row, col] = resultMatrix[row - 1, col - 1];
                    }
                }
                else if (inputExpression[row - 1] == ')')
                {

                    if (col + 1 >= resultMatrix.GetLength(1))
                    {
                        resultMatrix[row, col] = 0;
                    }
                    else
                    {
                        resultMatrix[row, col] = resultMatrix[row - 1, col + 1];
                    }
                }
            }
        }
        Console.WriteLine(resultMatrix[resultMatrix.GetLength(0)-1, 0]);
    }
}

