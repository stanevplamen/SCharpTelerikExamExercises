using System;
using System.Collections.Generic;

class ThreeDLines
{
    static char[, ,] theCube;

    static void Main()
    {
        string inputMain = Console.ReadLine();
        string[] mainParse = inputMain.Split();

        int weigth = int.Parse(mainParse[0]);
        int heigth = int.Parse(mainParse[1]);
        int depth = int.Parse(mainParse[2]);
        theCube = new char[weigth, heigth, depth];
        string[] secondInput = new string[heigth];

        for (int i = 0; i < heigth; i++)
        {
            secondInput[i] = Console.ReadLine();
        }

        for (int h = 0; h < secondInput.Length; h++)
        {
            string[] temp = secondInput[h].Split();

            for (int d = 0; d < temp.Length; d++)
            {
                string currentW = temp[d];
                for (int w = 0; w < currentW.Length; w++)
                {
                    theCube[w, h, d] = currentW[w];
                }
            }
        }

        for (int w = 0; w < theCube.GetLength(0); w++)
        {
            for (int h = 0; h < theCube.GetLength(1); h++)
            {
                for (int d = 0; d < theCube.GetLength(2); d++)
                {
                    currentLine = 1;
                    if ((int)theCube[w, h, d] >= 65 && (int)theCube[w, h, d] <= 90)
                    {
                        currentLine = DFSForPoint1(w + 1, h, d, theCube[w, h, d] );
                        LinesDictionaryFill(currentLine); currentLine = 1;

                        currentLine = DFSForPoint2(w, h + 1, d, theCube[w, h, d] );
                        LinesDictionaryFill(currentLine); currentLine = 1;

                        currentLine = DFSForPoint3(w, h, d + 1, theCube[w, h, d] );
                        LinesDictionaryFill(currentLine); currentLine = 1;

                        currentLine = DFSForPoint4(w + 1, h + 1, d, theCube[w, h, d]);
                        LinesDictionaryFill(currentLine); currentLine = 1;

                        currentLine = DFSForPoint5(w - 1, h + 1, d, theCube[w, h, d]);
                        LinesDictionaryFill(currentLine); currentLine = 1;

                        currentLine = DFSForPoint6(w + 1, h, d + 1, theCube[w, h, d]);
                        LinesDictionaryFill(currentLine); currentLine = 1;

                        currentLine = DFSForPoint7(w - 1, h, d + 1, theCube[w, h, d]);
                        LinesDictionaryFill(currentLine); currentLine = 1;

                        currentLine = DFSForPoint8(w, h + 1, d + 1, theCube[w, h, d]);
                        LinesDictionaryFill(currentLine); currentLine = 1;

                        currentLine = DFSForPoint9(w, h + 1, d - 1, theCube[w, h, d]);
                        LinesDictionaryFill(currentLine); currentLine = 1;


                        //==

                        currentLine = DFSForPoint10(w + 1, h + 1, d + 1, theCube[w, h, d] );
                        LinesDictionaryFill(currentLine); currentLine = 1;

                        currentLine = DFSForPoint11(w - 1, h + 1, d + 1, theCube[w, h, d] );
                        LinesDictionaryFill(currentLine); currentLine = 1;

                        currentLine = DFSForPoint12(w + 1, h - 1, d + 1, theCube[w, h, d] );
                        LinesDictionaryFill(currentLine); currentLine = 1;

                        currentLine = DFSForPoint13(w - 1, h - 1, d + 1, theCube[w, h, d] );
                        LinesDictionaryFill(currentLine); currentLine = 1;
                    }
                }
            }
        }
        int counter = 1;
        foreach (KeyValuePair<int, int> kvp in longestLines)
        {
            if (counter == longestLines.Count && kvp.Key > 1)
            {
                Console.WriteLine("{0} {1}", kvp.Key, kvp.Value);
            }
            else if (counter == longestLines.Count && kvp.Key == 1)
            {
                Console.WriteLine(-1);
            }
            counter++;
        }


        //Console.WriteLine("{0} {1}", longestLine, lineCount);
    }
    static SortedDictionary<int, int> longestLines = new SortedDictionary<int, int>();

    static int longestLine = 0;

    private static void LinesDictionaryFill(int currentLine)
    {
        int temp = 0;
        if (longestLine <= currentLine)
        {
            longestLine = currentLine;
            bool exists = false;
            foreach (KeyValuePair<int, int> kvp in longestLines)
            {
                if (kvp.Key == longestLine)
                {
                    exists = true;
                    temp = kvp.Value + 1;
                }
            }
            if (exists == false)
            {
                longestLines.Add(longestLine, 1);
            }
            else if (exists == true)
            {
                longestLines[longestLine] = temp;
            }
        }
    }

    private static char GetReplaceChar(char currentChar)
    {
        string newChar = currentChar.ToString().ToLower();
        return newChar[0];
    }

    static int currentLine = 1;

    private static bool InRange(int w, int h, int d)
    {
        bool wInRange = w >= 0 && w < theCube.GetLength(0);
        bool hInRange = h >= 0 && h < theCube.GetLength(1);
        bool dInRange = d >= 0 && d < theCube.GetLength(2);
        return wInRange && hInRange && dInRange;
    }

    private static int DFSForPoint1(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint1(w + 1, h, d, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint2(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint2(w, h + 1, d, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint3(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint3(w, h, d + 1, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint4(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint4(w + 1, h + 1, d, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint5(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint5(w - 1, h + 1, d, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint6(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint6(w + 1, h, d + 1, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint7(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint7(w - 1, h, d + 1, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint8(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint8(w, h + 1, d + 1, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint9(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint9(w, h + 1, d - 1, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint10(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint10(w + 1, h + 1, d + 1, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint11(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint11(w - 1, h + 1, d + 1, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint12(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint12(w + 1, h - 1, d + 1, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }

    private static int DFSForPoint13(int w, int h, int d, char currentChar )
    {
        if (!InRange(w, h, d))
        {
            return currentLine;
        }
        else if (theCube[w, h, d] == currentChar  )
        {
     
            currentLine++;
            DFSForPoint13(w - 1, h - 1, d + 1, currentChar );
        }
        else
        {
            return currentLine;
        }
 
        return currentLine;
    }
}

