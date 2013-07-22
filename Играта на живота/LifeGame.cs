using System;

class LifeGame
{
    public static int[,] lifeMatrix;

    static void Main()
    {
        int numberOfCycles = int.Parse(Console.ReadLine());
        string input = Console.ReadLine();
        string[] splitedInput = input.Split();

        int allRows = int.Parse(splitedInput[0]);
        int allCols = int.Parse(splitedInput[1]);

        lifeMatrix = new int[allRows + 2, allCols + 2];

        for (int rows = 1; rows < allRows+1; rows++)
        {
            string tempInput = Console.ReadLine();
            string[] splitedTempInput = tempInput.Split();
            for (int cols = 1; cols < allCols + 1; cols++)
            {
                int currentNumber = int.Parse(splitedTempInput[cols - 1]);
                if (currentNumber == 1)
                {
                    lifeMatrix[rows, cols] = 1;
                }
                else
                {
                    lifeMatrix[rows, cols] = 2; 
                }
            }
        }
        // 0 neutral, 1 life, 2 dead (round of neutrals)
        ImplementLifeCycle(numberOfCycles);
    }

    private static void ImplementLifeCycle(int numberOfCycles)
    {
        int cycleLiveCells = 0;
        bool[,] toChange = new bool[lifeMatrix.GetLength(0), lifeMatrix.GetLength(1)];
        for (int i = 1; i <= numberOfCycles; i++)
        {           
            cycleLiveCells = 0;
            for (int row = 1; row < lifeMatrix.GetLength(0) - 1; row++)
            {
                for (int col = 1; col < lifeMatrix.GetLength(1) - 1; col++)
                {
                    int lifeNeighbors = 0;

                    if (lifeMatrix[row-1, col-1] == 1) { lifeNeighbors++; }
                    if (lifeMatrix[row-1, col] == 1) { lifeNeighbors++; }
                    if (lifeMatrix[row-1, col+1] == 1) { lifeNeighbors++; }
                    if (lifeMatrix[row, col-1] == 1) { lifeNeighbors++; }
                    if (lifeMatrix[row, col+1] == 1) { lifeNeighbors++; }
                    if (lifeMatrix[row+1, col-1] == 1) { lifeNeighbors++; }
                    if (lifeMatrix[row+1, col] == 1) { lifeNeighbors++; }
                    if (lifeMatrix[row+1, col+1]  == 1) { lifeNeighbors++; }

                    if (lifeMatrix[row, col] == 2 && lifeNeighbors != 3)
                    {
                        continue;
                    }
                    else if (lifeMatrix[row, col] == 2 && lifeNeighbors == 3)
                    {
                        //lifeMatrix[row, col] = 1;
                        toChange[row, col] = true;
                    }
                    else if (lifeMatrix[row, col] == 1 && (lifeNeighbors == 2 || lifeNeighbors == 3))
                    {
                        cycleLiveCells++;
                    }
                    else 
                    {
                        toChange[row, col] = true;
                    }                  
                }
            }

            for (int row = 1; row < lifeMatrix.GetLength(0) - 1; row++)
            {
                for (int col = 1; col < lifeMatrix.GetLength(1) - 1; col++)
                {
                    if (toChange[row, col] == true)
                    {
                        if (lifeMatrix[row, col] == 1)
                        {
                            lifeMatrix[row, col] = 2;
                        }
                        else 
                        {
                            lifeMatrix[row, col] = 1;
                            cycleLiveCells++;
                        }
                        toChange[row, col] = false;
                    }
                }
            }
        }
        Console.WriteLine(cycleLiveCells);
    }
}

