using System;
using System.Collections.Generic;

class Stars
{
    static void Main()
    {
        string inputCubeStr = Console.ReadLine();  // "7 4 3";           
        string[] parcedDimensions = inputCubeStr.Split();
        int cubeWidth = int.Parse(parcedDimensions[0]);
        int cubeHeigth = int.Parse(parcedDimensions[1]);
        int cubeDepth = int.Parse(parcedDimensions[2]);

        string[] textCubeLines = new string[cubeHeigth];

        for (int i = 0; i < textCubeLines.Length; i++)
        {
            textCubeLines[i] = Console.ReadLine();
        }

        char[, ,] generalCube = WriteCubeColors(textCubeLines, cubeWidth, cubeHeigth, cubeDepth);

        ChechNumberOfStars(generalCube);
    }

    static bool InRange(int w, int h, int d, char[, ,] generalCube)
    {
        bool weigthInRange = w - 1 >= 0 && w + 1 < generalCube.GetLength(0);
        bool heithInRange = h - 1 >= 0 && h + 1 < generalCube.GetLength(1);
        bool depthInRange = d - 1 >= 0 && d + 1 < generalCube.GetLength(2);

        return weigthInRange && heithInRange && depthInRange;
    }


    static bool CheckweHaveStar(int w, int h, int d, bool inRange, char[, ,] generalCube)
    {
        if (inRange == true)
        {
            bool weHaveStar = false;
            char c = generalCube[w, h, d];
            if (c == generalCube[w - 1, h, d] && c == generalCube[w + 1, h, d] && c == generalCube[w, h-1, d]
                && c == generalCube[w, h + 1, d] && c == generalCube[w, h, d - 1] && c == generalCube[w, h, d + 1])
            {
                weHaveStar = true;
            }
            return weHaveStar;
        }
        else
        {
            return false;
        }
    }


    private static void ChechNumberOfStars(char[, ,] generalCube)
    {
        SortedDictionary<char, int> colorsDict = new SortedDictionary<char, int>();
        int counterStars = 0;
        for (int w = 0; w < generalCube.GetLength(0); w++)
        {
            for (int h = 0; h < generalCube.GetLength(1); h++)
            {
                for (int d = 0; d < generalCube.GetLength(2); d++)
                {
                    int temp = 0;
                    bool inRange = InRange(w, h, d, generalCube);
                    bool weHaveStar = CheckweHaveStar(w, h, d, inRange, generalCube);

                    if (weHaveStar == true)
                    {
                        counterStars++;
                        bool exists = false;
                        foreach (KeyValuePair<char, int> kvp in colorsDict)
                        {
                            if (kvp.Key == generalCube[w, h, d])
                            {
                                exists = true;
                                temp = kvp.Value + 1;
                            }
                        }
                        if (exists == false)
                        {
                            colorsDict.Add(generalCube[w, h, d], 1);
                        }
                        else if (exists == true)
                        {
                            colorsDict[generalCube[w, h, d]] = temp;
                        }
                    }
                }
            }
        }

        Print(counterStars, colorsDict);
    }

    private static void Print(int counterStars, SortedDictionary<char, int> colorsDict)
    {
        Console.WriteLine(counterStars);
        foreach (KeyValuePair<char, int> kvp in colorsDict)
        {
            Console.WriteLine("{0} {1}", kvp.Key, kvp.Value);
        }                                                 
    }

    private static char[, ,] WriteCubeColors(string[] textCubeLines, int cubeWidth, int cubeHeigth, int cubeDepth)
    {
        char[, ,] generalCube = new char[cubeWidth, cubeHeigth, cubeDepth];

        for (int h = 0; h < cubeHeigth; h++)
        {
            string[] currentRow = textCubeLines[h].Split();
            for (int d = 0; d < cubeDepth; d++)
            {
                char[] currentChars = currentRow[d].ToCharArray();
                for (int w = 0; w < cubeWidth; w++)
                {
                    generalCube[w, h, d] = currentChars[w];
                }
            }
        }
        return generalCube;
    }
}

