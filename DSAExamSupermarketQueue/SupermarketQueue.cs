using System;
using System.Collections.Generic;
using System.Text;
using Wintellect.PowerCollections;

class SupermarketQueue
{
    static BigList<string> supermarketQueue;
    static StringBuilder outputSB;
    static Bag<string> onlyNames;

    static void Main()
    {
        supermarketQueue = new BigList<string>();
        outputSB = new StringBuilder();
        onlyNames = new Bag<string>();

        LoadTheQuques();
        Console.WriteLine(outputSB.ToString());
    }

    private static void LoadTheQuques()
    {
        while (true)
        {
            string currentToken = Console.ReadLine();
            if (currentToken[0] == 'A')
            {
                string name = currentToken.Substring(7);
                supermarketQueue.Add(name);
                onlyNames.Add(name);
                outputSB.AppendLine("OK");
            }
            else if (currentToken[0] == 'I')
            {
                string possitionAndName = currentToken.Substring(7);
                int index = possitionAndName.IndexOf(' ');
                int position = int.Parse(possitionAndName.Substring(0,index));
                string name = (possitionAndName.Substring(index+1));

                try
                {
                    supermarketQueue.Insert(position, name);
                    onlyNames.Add(name);
                    outputSB.AppendLine("OK");
                }
                catch (Exception)
                {
                    outputSB.AppendLine("Error");
                }
            }
            else if (currentToken[0] == 'F')
            {
                string name = currentToken.Substring(5);
                int count = onlyNames.NumberOfCopies(name);
                outputSB.AppendLine(count.ToString());
            }
            else if (currentToken[0] == 'S')
            {
                string theStrNumb = currentToken.Substring(6);
                int servedNumber = int.Parse(theStrNumb);

                if (supermarketQueue.Count >= servedNumber)
                {
                    for (int i = 0; i < servedNumber; i++)
                    {
                        outputSB.AppendFormat("{0} ", supermarketQueue[0]);
                        onlyNames.Remove(supermarketQueue[0]);
                        supermarketQueue.RemoveAt(0);
                    }
                    outputSB.AppendLine();
                }
                else
                {
                    outputSB.AppendLine("Error");
                }             
            }
            else if (currentToken[0] == 'E')
            {
                break;
            }
        }
    }
}

