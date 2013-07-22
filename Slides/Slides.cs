using System;

class Slides
{
    static int[] mark = new int[3];

    static void Main()
    {
        string cubeInput = Console.ReadLine(); 
        string[] splitedArray = cubeInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        int width = int.Parse(splitedArray[0]);
        int heigth = int.Parse(splitedArray[1]);
        int depth = int.Parse(splitedArray[2]);
        string[] inputs = new string[heigth]; //
        for (int i = 0; i < heigth; i++)
        {
            inputs[i] = Console.ReadLine();
        }

        string start = Console.ReadLine(); 
        string[] startPosition = start.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        int startWidth = int.Parse(startPosition[0]);
        int startDepth = int.Parse(startPosition[1]);
        int startHeigth = 0;


        string[, ,] theMatrix = LoadTheMatrix(inputs, width, heigth, depth);

        FindPathToExit(theMatrix, startWidth, startHeigth, startDepth);
    }

    static string[, ,] LoadTheMatrix(string[] inputs, int width, int heigth, int depth)
    {
        string[, ,] theMatrix = new string[width, heigth, depth];
        for (int h = 0; h < heigth; h++)
        {
            string[] splitedH = inputs[h].Trim().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            for (int d = 0; d < depth; d++)
            {
                string[] splitedD = splitedH[d].Trim().Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                for (int w = 0; w < width; w++)
                {
                    theMatrix[w, h, d] = splitedD[w];
                }
            }
        }
        return theMatrix;
    }

    static void FindPathToExit(string[, ,] theMatrix, int widthCurrent, int heigthCurrent, int depthCurrent)
    {

        // следват две проверки дали не сме задънили
        if (!InRange(theMatrix, widthCurrent, depthCurrent))
        {
            Console.WriteLine("No\n{0} {1} {2}", mark[0], mark[1], mark[2]);
            Environment.Exit(0);
        }
        if (theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "B")
        {
            Console.WriteLine("No\n{0} {1} {2}", widthCurrent, heigthCurrent, depthCurrent);
            Environment.Exit(0);
        }
        
        //  проверяване дали не сме намерили изход
        if (Exit(theMatrix, heigthCurrent) && 
            (theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S L" || 
            theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S R" || 
            theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S B" || 
            theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S F" ||
            theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S FL" ||
            theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S FR" ||
            theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S BL" ||
            theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S BR" ||
            theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "E" ))
        {
            Console.WriteLine("Yes\n{0} {1} {2}", widthCurrent, heigthCurrent, depthCurrent);
            Environment.Exit(0);
        }

        // следва проветка дали няма директно задаване на ново координати
        string transCheck = theMatrix[widthCurrent, heigthCurrent, depthCurrent];
        string[] splitedTrans = transCheck.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (splitedTrans[0].Trim() == "T")
        {
            widthCurrent = int.Parse(splitedTrans[1].Trim());
            depthCurrent = int.Parse(splitedTrans[2].Trim());
            FindPathToExit(theMatrix, widthCurrent, heigthCurrent, depthCurrent);
        }


        mark[0] = widthCurrent; mark[1] = heigthCurrent; mark[2] = depthCurrent;

        // следва проверка ако вървим надолу - влизаме в рекурсия
        if (theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "E")
        {
            FindPathToExit(theMatrix, widthCurrent, heigthCurrent + 1, depthCurrent);
        }
        if (theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S R")
        {
            FindPathToExit(theMatrix, widthCurrent + 1, heigthCurrent + 1, depthCurrent);
        }
        if (theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S L")
        {
            FindPathToExit(theMatrix, widthCurrent - 1, heigthCurrent + 1, depthCurrent);
        }
        if (theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S B")
        {
            FindPathToExit(theMatrix, widthCurrent, heigthCurrent + 1, depthCurrent + 1);
        }
        if (theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S F")
        {
            FindPathToExit(theMatrix, widthCurrent, heigthCurrent + 1, depthCurrent - 1);
        }
        if (theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S FL")
        {
            FindPathToExit(theMatrix, widthCurrent - 1, heigthCurrent + 1, depthCurrent - 1);
        }
        if (theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S FR")
        {
            FindPathToExit(theMatrix, widthCurrent + 1, heigthCurrent + 1, depthCurrent - 1);
        }
        if (theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S BL")
        {
            FindPathToExit(theMatrix, widthCurrent - 1, heigthCurrent + 1, depthCurrent + 1);
        }
        if (theMatrix[widthCurrent, heigthCurrent, depthCurrent] == "S BR")
        {
            FindPathToExit(theMatrix, widthCurrent + 1, heigthCurrent + 1, depthCurrent + 1);
        }


    }
    static bool Exit(string[, ,] theMatrix, int heigthCurrent)
    {
        bool heigtInRange = heigthCurrent >= theMatrix.GetLength(1) - 1;
        return heigtInRange;
    }

    static bool InRange(string[, ,] theMatrix, int widthCurrent, int depthCurrent)
    {
        bool widthInRange = widthCurrent >= 0 && widthCurrent < theMatrix.GetLength(0);
        bool depthInRange = depthCurrent >= 0 && depthCurrent < theMatrix.GetLength(2);
        return widthInRange && depthInRange;
    }
}




