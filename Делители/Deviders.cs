using System;
using System.Collections.Generic;
using System.Text;

class Permute
{
    private void swap(ref char a, ref char b)
    {
        if (a == b) return;
        a ^= b;
        b ^= a;
        a ^= b;
    }

    public void Set_Permutation(char[] list)
    {
        int arrayLength = list.Length - 1;
        Permutation_Method(list, 0, arrayLength);
    }

    private int CheckNumberOfDivisions(int currentNumber)
    {
        int upperBoarder = currentNumber / 2;
        int counter = 0;
        for (int i = 1; i <= upperBoarder; i++)
        {
            if (currentNumber % i == 0)
            {
                counter++;
            }
        }
        return counter + 1;
    }

    public static List<int> numbers = new List<int>();
    public static List<int> dividers = new List<int>();

    private void Permutation_Method(char[] list, int k, int m)
    {
        StringBuilder sb = new StringBuilder();
        // k intial index passed 
        // m size of char array
        int i;
        if (k == m)  
        {
            sb.Append(list);
            string current = sb.ToString();
            int currentNumber = int.Parse(current);
            numbers.Add(currentNumber);
            int currentDivisions = CheckNumberOfDivisions(currentNumber);
            dividers.Add(currentDivisions);
        }
        else
        {
            for (i = k; i <= m; i++) 
            {
                swap(ref list[k], ref list[i]);  

                //recursive call
                Permutation_Method(list, k + 1, m);

                swap(ref list[k], ref list[i]);  
            }
        }
    }


}

class Deviders
{
    static void Main()
    {
        int numberOfDigits = int.Parse(Console.ReadLine());
        string[] inputArr = new string[numberOfDigits];
        for (int i = 0; i < numberOfDigits; i++)
        {
            inputArr[i] = Console.ReadLine();
        }
        //string[] inputArr = { "1","2" };
        string str = String.Empty;
        foreach (string digit in inputArr)
        {
            str = str + digit;
        }

        Permute objPermutation = new Permute();
        //string str = "123";
        char[] mycharArray = str.ToCharArray();
        /*calling the permute*/
        objPermutation.Set_Permutation(mycharArray);
        int minPosition = 0;
        int minValue = int.MaxValue;

        List<int> result = new List<int>();
        for (int i = 0; i < Permute.numbers.Count; i++)
        {
            if (minValue > Permute.dividers[i])
            {
                minValue = Permute.dividers[i];
                minPosition = i;
            }
        }

        for (int i = 0; i < Permute.dividers.Count; i++)
        {
            if (minValue == Permute.dividers[i])
            {
                result.Add(Permute.numbers[i]);
            }
        }
        result.Sort();
        Console.WriteLine(result[0]);
    }
}

