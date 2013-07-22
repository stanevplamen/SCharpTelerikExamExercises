using System;

class KukataDancing
{
    static string[,] theMatrix;

    static int length = 3;

    static void LoadTheMatrix()
    {
        theMatrix = new string[3, 3];
        for (int row = 0; row < length; row++)
        {
            for (int col = 0; col < length; col++)
            {
                // edge
                if ((col == 0 && row == 0) || (col == 0 && row == length - 1) || (col == length - 1 && row == 0) || (col == length - 1 && row == length - 1))
                {
                    theMatrix[row, col] = "RED";
                }
                // center
                else if (col == 1 && row == 1)
                {
                    theMatrix[row, col] = "GREEN";
                }
                // other
                else
                {
                    theMatrix[row, col] = "BLUE";
                }
            }          
        }
    }

    //static string tempString;

    static void DanceImplementation(string currentDance, int currentCol, int currentRow, int currentDirCol, int currentDirRow, int lastStep, bool col, int i)
    {
        if (currentRow >= length)
        {
            currentRow = 0;
        }
        else if (currentRow < 0)
        {
            currentRow = length - 1;
        }
        if (currentCol >= length)
        {
            currentCol = 0;
        }
        else if (currentCol < 0)
        {
            currentCol = length - 1;
        }

        string tempString = theMatrix[currentRow, currentCol];

        if (i >= currentDance.Length )
        {
           Console.WriteLine(tempString);
           return;
        }

            if (currentDance[i] == 'L')
            {
                if (col == true)
                {
                    if (currentDirCol == 1)
                    {
                        lastStep = i;
                        currentDirCol = 0;
                        currentDirRow = -1;
                        DanceImplementation(currentDance, currentCol, currentRow, currentDirCol, currentDirRow, lastStep, false, i + 1);
                    }
                    else if (currentDirCol == -1)
                    {
                        lastStep = i;
                        currentDirCol = 0;
                        currentDirRow = 1;
                        DanceImplementation(currentDance, currentCol, currentRow, currentDirCol, currentDirRow, lastStep, false, i + 1);
                    }
                    
                }
                else if (col == false)
                {
                    if (currentDirRow == 1)
                    {
                        lastStep = i;
                        currentDirCol = 1;
                        currentDirRow = 0;
                        DanceImplementation(currentDance, currentCol, currentRow, currentDirCol, currentDirRow, lastStep, true, i + 1);
                    }
                    else if (currentDirRow == -1)
                    {
                        lastStep = i;
                        currentDirCol = -1;
                        currentDirRow = 0;
                        DanceImplementation(currentDance, currentCol, currentRow, currentDirCol, currentDirRow, lastStep, true, i + 1);
                    }
                    
                }
            }
            else if (currentDance[i] == 'R')
            {
                if (col == true)
                {
                    if (currentDirCol == 1)
                    {
                        lastStep = i;
                        currentDirCol = 0;
                        currentDirRow = 1;
                        DanceImplementation(currentDance, currentCol, currentRow, currentDirCol, currentDirRow, lastStep, false, i + 1);
                    }
                    else if (currentDirCol == -1)
                    {
                        lastStep = i;
                        currentDirCol = 0;
                        currentDirRow = -1;
                        DanceImplementation(currentDance, currentCol, currentRow, currentDirCol, currentDirRow, lastStep, false, i + 1);
                    }
                    
                }
                else if (col == false)
                {
                    if (currentDirRow == 1)
                    {
                        lastStep = i;
                        currentDirCol = -1;
                        currentDirRow = 0;
                        DanceImplementation(currentDance, currentCol, currentRow, currentDirCol, currentDirRow, lastStep, true, i + 1);
                    }
                    else if (currentDirRow == -1)
                    {
                        lastStep = i;
                        currentDirCol = 1;
                        currentDirRow = 0;
                        DanceImplementation(currentDance, currentCol, currentRow, currentDirCol, currentDirRow, lastStep, true, i + 1);
                    }
                    
                }
            }
            else if (currentDance[i] == 'W')
            {
                if (col == true)
                {
                    lastStep = i;
                    DanceImplementation(currentDance, currentCol + currentDirCol, currentRow, currentDirCol, currentDirRow, lastStep, true, i + 1);                   
                }
                else if (col == false)
                {
                    lastStep = i;
                    DanceImplementation(currentDance, currentCol, currentRow + currentDirRow, currentDirCol, currentDirRow, lastStep, false, i + 1);                    
                }
            }
            return;
    }

    static string[] dances;

    static void Main()
    {
        string input = Console.ReadLine(); //"5"; //
        int numberOfDances = int.Parse(input);
        dances = new string[numberOfDances];
        for (int i = 0; i < dances.Length; i++)
        {
            dances[i] = Console.ReadLine();
        }

        LoadTheMatrix();

        for (int i = 0; i < dances.Length; i++)
        {
            DanceImplementation(dances[i], 1, 1, 0, 1, -1, false, 0);          
        }
    }
}

