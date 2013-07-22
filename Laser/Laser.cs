using System;

class Laser
{
    static void Main()
    {
        string cubeInput = Console.ReadLine(); // "5 10 5"; //
        string[] splitedArray = cubeInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        int width = int.Parse(splitedArray[0]);
        int heigth = int.Parse(splitedArray[1]);
        int depth = int.Parse(splitedArray[2]);

        string start = Console.ReadLine(); // "2 6 3"; //
        string[] startPosition = start.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int startWidth = int.Parse(startPosition[0]) - 1;
        int startHeigth = int.Parse(startPosition[1]) - 1;
        int startDepth = int.Parse(startPosition[2]) - 1;

        string direction = Console.ReadLine(); // "1 0 1"; //
        string[] dirPosition = direction.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        dirWidth = int.Parse(dirPosition[0]);
        dirHeigth = int.Parse(dirPosition[1]);
        dirDepth = int.Parse(dirPosition[2]);

        byte[, ,] theMatrix = LoadTheMatrix(width, heigth, depth);

        FindPathToExit(startWidth, startHeigth, startDepth);
    }

    static int dirWidth;
    static int dirHeigth;
    static int dirDepth;

    static byte[, ,] theMatrix;

    static byte[, ,] LoadTheMatrix(int width, int heigth, int depth)
    {
        theMatrix = new byte[width, heigth, depth];
        for (int w = 0; w < theMatrix.GetLength(0); w++)
        {
            for (int h = 0; h < theMatrix.GetLength(1); h++)
            {
                for (int d = 0; d < theMatrix.GetLength(2); d++)
                {
                    if ((w == 0 && h == 0) || (h == 0 && d == 0) || (d == 0 && w == 0) ||
                        (w == theMatrix.GetLength(0) - 1 && h == theMatrix.GetLength(1) - 1) ||
                        (h == theMatrix.GetLength(1) - 1 && d == theMatrix.GetLength(2) - 1) ||
                        (d == theMatrix.GetLength(2) - 1 && w == theMatrix.GetLength(0) - 1))
                    {
                        theMatrix[w, h, d] = 1;
                    }
                    else
                    {
                        theMatrix[w, h, d] = 0;
                    }
                }
            }
        }
        return theMatrix;
    }

    static int tempW; static int tempH; static int tempD;

     static void FindPathToExit(int currentWidth, int currentHeigth, int currentDepth)
     {
         // check if we can move
         if (theMatrix[currentWidth, currentHeigth, currentDepth] == 1)
         {
             Console.WriteLine("{0} {1} {2}", tempW + 1, tempH + 1, tempD + 1);
             Environment.Exit(0);
         }
         // check if we need to change direction
         if (currentWidth <= 0 || currentWidth >= theMatrix.GetLength(0) - 1)
         {
             dirWidth *= (-1);
         }
         if (currentHeigth <= 0 || currentHeigth >= theMatrix.GetLength(1) - 1)
         {
             dirHeigth *= (-1);
         }
         if (currentDepth <= 0 || currentDepth >= theMatrix.GetLength(2) - 1)
         {
             dirDepth *= (-1);
         }

         // moving
         if (theMatrix[currentWidth, currentHeigth, currentDepth] == 0)
         {
             tempW = currentWidth; tempH = currentHeigth; tempD = currentDepth;
             theMatrix[currentWidth, currentHeigth, currentDepth] = 1;
             FindPathToExit(currentWidth + dirWidth, currentHeigth + dirHeigth, currentDepth + dirDepth);
         }

     }
}

