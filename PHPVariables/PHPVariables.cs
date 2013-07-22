using System;
using System.Collections.Generic;
using System.Text;

class PHPVariables
{
    static void Main()
    {
        bool inCommentMulti = false;
        List<string> variableNames = new List<string>();
        while (true)
        {
            bool inCommentLine = false;
            string currentLine = Console.ReadLine();
            if (currentLine == "?>")
            {
                break;
            }
            for (int i = 0; i < currentLine.Length; i++)
            {
                if ( (currentLine[i] == '#')||(i+1< currentLine.Length && currentLine[i] == '/' && currentLine[i + 1] == '/')  )
                {
                    inCommentLine = true;
                }
                else if ((i + 1 < currentLine.Length && currentLine[i] == '/' && currentLine[i + 1] == '*') )
                {
                    inCommentMulti = true;
                }
                if ((inCommentMulti == true) && (i + 1 < currentLine.Length && currentLine[i] == '*' && currentLine[i + 1] == '/'))
                {
                    inCommentMulti = false;
                }

                if (inCommentLine == false && inCommentMulti == false)
                {
                    StringBuilder sb = new StringBuilder();
                    if (currentLine[i] == '$')
                    {
                        if (i - 1 >= 0 && currentLine[i - 1] == '\\' && i - 2 >= 0 && currentLine[i - 2] != '\\')
                        {
                            continue;
                        }
                        for (int j = i + 1; j < currentLine.Length; j++)
                        {
                            if ( ((int)(currentLine[j]) >= 65 && (int)(currentLine[j]) <= 90) ||
                                ((int)(currentLine[j]) >= 48 && (int)(currentLine[j]) <= 57) ||
                                ((int)(currentLine[j]) >= 97 && (int)(currentLine[j]) <= 122) || (int)(currentLine[j]) == 95)
                            {
                                sb.Append(currentLine[j]);
                            }
                            else
                            {
                                i = j;
                                variableNames.Add(sb.ToString());
                                break;
                            }
                        }
                    }
                }

            }
        }
        variableNames = QuickSortFunction(variableNames);
        string current = String.Empty;
        int counterVars = 0;
        StringBuilder newSB = new StringBuilder();
        for (int i = 0; i < variableNames.Count; i++)
        {
            if (current == variableNames[i])
            {
                continue;
            }
            counterVars++;
            current = variableNames[i];
            newSB.AppendLine(current);
        }
        Console.WriteLine(counterVars);
        Console.WriteLine(newSB.ToString());
    }

    static List<string> QuickSortFunction(List<string> currentArrayList)
    {
        if (currentArrayList.Count <= 1)
        {
            return currentArrayList;
        }

        int measuringSign = currentArrayList.Count / 2;
        string valueSign = currentArrayList[measuringSign];
        currentArrayList.RemoveAt(measuringSign);

        List<string> smaller = new List<string>();
        List<string> bigger = new List<string>();

        int counter = 0;
        foreach (var item in currentArrayList)
        {
            if (true)
            {
                if (string.Compare(item, valueSign, StringComparison.Ordinal) < 0)//((int)item[0] < (int)valueSign[0])
                {
                    smaller.Add(item);
                }
                else
                {
                    bigger.Add(item);
                }
            }
            counter++;
        }
        List<string> resultArrayList = new List<string>();
        resultArrayList.AddRange(QuickSortFunction(smaller));
        resultArrayList.Add(valueSign);
        resultArrayList.AddRange(QuickSortFunction(bigger));
        return resultArrayList;
    }
}

