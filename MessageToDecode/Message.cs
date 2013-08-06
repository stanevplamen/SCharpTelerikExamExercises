using System;
using System.Collections.Generic;
using System.Text;

class Message
{
    static List<string> numbsList;
    static List<char> charList;
    static List<string> finalOutput;

    static void Main()
    {
        string inputNumbersString = Console.ReadLine();
        string textToDecode = Console.ReadLine();

        SplitTheINput(textToDecode);

        finalOutput = new List<string>();
        FindCodeMatches(inputNumbersString, String.Empty);

        Print();    
    }

    private static void FindCodeMatches(string cipher, string current)
    {
        if (cipher.Length == 0)
        {
            finalOutput.Add(current);
            return;
        }
        for (int i = 0; i < numbsList.Count; i++)
        {
            if (cipher.StartsWith(numbsList[i]))
            {
                FindCodeMatches(cipher.Substring(numbsList[i].Length), current + charList[i]);
            }
        }
    }

    private static void SplitTheINput(string text)
    {
        charList = new List<char>();
        numbsList = new List<string>();
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] >= 65 && text[i] <= 90 && i == 0)
            {
                charList.Add(text[i]);
            }
            else if (text[i] >= 65 && text[i] <= 90)
            {
                charList.Add(text[i]);
                numbsList.Add(sb.ToString());
                sb.Clear();
            }
            else
            {
                sb.Append(text[i]);
            }
        }
        numbsList.Add(sb.ToString());
        sb.Clear();
    }

    private static void Print()
    {
        finalOutput.Sort();
        Console.WriteLine(finalOutput.Count);
        foreach (var decode in finalOutput)
        {
            Console.WriteLine(decode);
        }
    }
}


