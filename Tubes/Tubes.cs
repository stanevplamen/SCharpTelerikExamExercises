using System;

class Program
{
    static void Main()
    {
        int pipesN = int.Parse(Console.ReadLine());
        int friendsM = int.Parse(Console.ReadLine());
        int[] pipesArr = new int[pipesN];

        int sum = 0;
        for (int i = 0; i < pipesN; i++)
        {
            pipesArr[i] = int.Parse(Console.ReadLine());
            sum = sum + pipesArr[i];
        }

        int biggestNumber = sum / friendsM;
        int currentSections = 0;

        for (int i = biggestNumber; i >= 0; i--)
        {
            for (int j = 0; j < pipesArr.Length; j++)
            {
                int helpTempNumber = pipesArr[j] / i;
                currentSections = currentSections + helpTempNumber;
            }
            if (currentSections == friendsM)
            {
                Console.WriteLine(i);
                break;
            }
            else
            {
                currentSections = 0;
            }
        }
    }
}

