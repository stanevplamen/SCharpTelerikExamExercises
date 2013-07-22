using System;
using System.Collections.Generic;
using System.Text;

class ConsoleJustification
{
    static string WordsExtract(string[] theText)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < theText.Length; i++)
        {
            string[] wordsArray = theText[i].Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in wordsArray)
            {
                sb.Append(word.Trim());
                sb.Append(' ');
            }
        }
        string result = sb.ToString();
        return result;
    }

    static List<string> JustifyText(string allWords, int stringLength)
    {
        //string[] wordsArray = allWords.Split();
        string[] wordsArray = allWords.Split(new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        List<string> tempWords = new List<string>();
        List<string> finalWords = new List<string>();
        StringBuilder sb = new StringBuilder();

        int counterLetters = 0;
        int counterWords = 0;
        int localCount = 0;
        bool sign = false;

        for (int i = 0; i < wordsArray.Length; i++)
        {
            counterWords++;
            tempWords.Add(wordsArray[i]);
            foreach (char letter in wordsArray[i])
            {
                counterLetters++;

                if (counterWords - 1 + counterLetters > stringLength)
                {
                    sign = true;
                    tempWords.RemoveAt(tempWords.Count - 1);
                    //tempWords.Remove(wordsArray[i]);

                    for (int w = 0; w < tempWords.Count; w++)
                    {
                        foreach (char ch in tempWords[w])
                        {
                            localCount++;
                        }
                    }
                    break;
                }
            }
            if (sign == true)
            {
                int currentWords = counterWords - 1;
                int currentSpaces = counterWords - 1 - 1;
                int difference = 0;
                int remainder = 0;
                if (currentSpaces > 0)
                {
                    difference = (stringLength - (localCount + currentSpaces)) / currentSpaces;
                    remainder = (stringLength - (localCount + currentSpaces)) % currentSpaces;
                }
                else
                {
                    difference = (stringLength - (localCount + currentSpaces)) / currentWords;
                    remainder = (stringLength - (localCount + currentSpaces)) % currentWords;
                }


                for (int w = 0; w < tempWords.Count; w++)
                {
                    if (w < tempWords.Count - 1)
                    {
                        sb.Append(tempWords[w]);
                        sb.Append(' ');
                        if (difference > 0)
                        {
                            for (int d = 1; d <= difference; d++)
                            {
                                sb.Append(' ');
                            }
                        }
                        if (remainder > 0)
                        {
                            sb.Append(' ');
                            remainder--;
                        }
                    }
                    else if (w == tempWords.Count - 1)
                    {
                        sb.Append(tempWords[w]);
                    }


                }
                i--;
                finalWords.Add(sb.ToString());
                counterLetters = 0;
                counterWords = 0;
                localCount = 0;
                tempWords = new List<string>();
                sb = new StringBuilder();
                sign = false;
            }
        }

        if (sign == false)
        {
            int currentWords = counterWords;
            int currentSpaces = counterWords - 1;
            for (int w = 0; w < tempWords.Count; w++)
            {
                foreach (char ch in tempWords[w])
                {
                    localCount++;
                }
            }

            int difference = 0;
            int remainder = 0;
            if (currentSpaces > 0)
            {
                difference = (stringLength - (localCount + currentSpaces)) / currentSpaces;
                remainder = (stringLength - (localCount + currentSpaces)) % currentSpaces;
            }
            else
            {
                difference = (stringLength - (localCount + currentSpaces)) / currentWords;
                remainder = (stringLength - (localCount + currentSpaces)) % currentWords;
            }

            if (currentWords == 1)
            {
                finalWords.Add(tempWords[0]);
            }
            else
            {
                for (int w = 0; w < tempWords.Count; w++)
                {
                    if (w < tempWords.Count - 1)
                    {
                        sb.Append(tempWords[w]);
                        sb.Append(' ');
                        if (difference > 0)
                        {
                            for (int d = 1; d <= difference; d++)
                            {
                                sb.Append(' ');
                            }
                        }
                        if (remainder > 0)
                        {
                            sb.Append(' ');
                            remainder--;
                        }
                    }
                    else if (w == tempWords.Count - 1)
                    {
                        sb.Append(tempWords[w]);
                    }
                }
            }
            finalWords.Add(sb.ToString());
        }

        return finalWords;

    }

    static void Main()
    {
        int lines = int.Parse(Console.ReadLine()); //10;
        int stringLength = int.Parse(Console.ReadLine()); //18;
        string[] theText = new string[lines];

        for (int i = 0; i < theText.Length; i++)
        {
            theText[i] = Console.ReadLine();
        }

        //string[] theText = new string[lines];
        //theText[0] = "can Joro needs";
        //theText[1] = "that functionality";
        //theText[2] = "and";
        //theText[3] = "he";
        //theText[4] = "needs";
        //theText[5] = "it";
        //theText[6] = "fast";



        // първо разделяме и обединяваме всички думи в един стринг
        string allWords = WordsExtract(theText);

        // форматиране на изxода в justify
        List<string> formatedSents = JustifyText(allWords, stringLength);

        foreach (var item in formatedSents)
        {
            Console.WriteLine(item);
        }

    }
}