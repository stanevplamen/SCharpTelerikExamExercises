using System;

class ThreeInOne
{
    static void Main()
    {
        // #1
        string firstInput = Console.ReadLine();
        sbyte taskOneResult = SolveTaskOne(firstInput);
        Console.WriteLine(taskOneResult);

        // #2
        string inputSecond = Console.ReadLine();
        int friends = int.Parse(Console.ReadLine());
        int taskTwoResult = SolveTaskTwo(inputSecond, friends);
        Console.WriteLine(taskTwoResult);

        // #3
        string inputThree = Console.ReadLine();
        int taskThreeResult = SolveTaskThree(inputThree);
        Console.WriteLine(taskThreeResult);
    }

    #region TaskOne
    static sbyte SolveTaskOne(string line)
    {
        string[] ponits = line.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        sbyte maxNumber = -1;
        sbyte counter = 0;
        sbyte index = -1;
        for (sbyte i = 0; i < ponits.Length; i++)
        {
            sbyte currentNumber = sbyte.Parse(ponits[i]);

            if (currentNumber < 22 && maxNumber < currentNumber)
            {
                maxNumber = currentNumber;
                counter = 0;
                index = i;
            }
            if (maxNumber == currentNumber)
            {
                counter++;
                if (counter > 1)
                {
                    index = -1;
                }
            }
        }
        return index;
    }
    #endregion

    #region TaskTwo
    private static int SolveTaskTwo(string inputSecond, int friends)
    {
        string[] bytesStr = inputSecond.Split(',');
        byte[] allBytes = new byte[bytesStr.Length];

        for (int i = 0; i < allBytes.Length; i++)
        {
            allBytes[i] = byte.Parse(bytesStr[i]);
        }
        Array.Sort(allBytes);
        int myBytes = 0;
        byte counter = (byte)(friends + 1);

        for (int i = allBytes.Length - 1; i >= 0; i--)
        {
            counter--;
            if (counter == friends)
            {
                myBytes = myBytes + allBytes[i];
            }
            if (counter == 0)
            {
                counter = (byte)(friends + 1);
            }
        }
        return myBytes;
    }
    #endregion

    #region TaskThree
    private static int SolveTaskThree(string inputThree)
    {
        string[] splitedInputThree = inputThree.Split();
        int g1 = int.Parse(splitedInputThree[0]);
        int s1 = int.Parse(splitedInputThree[1]);
        int b1 = int.Parse(splitedInputThree[2]);

        int g2 = int.Parse(splitedInputThree[3]);
        int s2 = int.Parse(splitedInputThree[4]);
        int b2 = int.Parse(splitedInputThree[5]);
        int counterOperations = 0;
        while (g1 < g2)
        {
            g1++;
            s2 += 11;
            counterOperations++;
        }

        while (s1 < s2)
        {
            if (g1 > g2)
            {
                g1--;
                s1 += 9;
                counterOperations++;
            }
            else
            {
                b2 += 11;
                s1++;
                counterOperations++;
            }
        }

        while (b1 < b2)
        {
            if (s1 > s2)
            {
                s1--;
                b1 += 9;
                counterOperations++;
            }
            else if (g1 > g2)
            {
                --g1;
                s1 += 9;
                counterOperations++;
            }
            else
            {
                counterOperations = -1;
                break;
            }
        }
        return counterOperations;
    }
    #endregion
}

