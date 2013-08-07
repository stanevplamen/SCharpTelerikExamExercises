using System;
using System.Collections.Generic;
using System.Text;

class PermitationsWithRepetitionsSet
{
    static string[] allCombosStrait;
    static string[] allCombosReversed;
    static string[] allCombosUsed;
    static int lengthNumber;
    static int numberOfLoops;
    static int numberOfIterations;
    static SortedSet<string> sequences;
    static int[] loops;

    static void Main()
    {
        lengthNumber = int.Parse(Console.ReadLine());
        GetTheInput(lengthNumber);
        sequences = new SortedSet<string>();
        SetLoopCycle();
        PrintGeneralOutput();
    }

    #region Generating vectors 0-1
    private static void SetLoopCycle()
    {
        numberOfLoops = lengthNumber;
        numberOfIterations = 2;
        loops = new int[numberOfLoops];
        NestedLoops(0);
    }

    static void NestedLoops(int currentLoop)
    {
        int test = numberOfLoops;
        int test2 = numberOfIterations;
        if (currentLoop == numberOfLoops)
        {
            DoCurrentLoop();
            return;
        }
        for (int counter = 1; counter <= numberOfIterations; counter++)
        {
            loops[currentLoop] = counter;
            NestedLoops(currentLoop + 1);
        }
    }

    static void DoCurrentLoop()
    {
        allCombosUsed = new string[lengthNumber];
        for (int i = 0; i < numberOfLoops; i++)
        {
            if (loops[i] == 1)
            {
                allCombosUsed[i] = allCombosStrait[i];
            }
            else
            {
                allCombosUsed[i] = allCombosReversed[i];
            }
        }
        CountSequencesViaPermitations();
    }
    #endregion Generating vectors 0-1

    #region Generating Permutations
    private static void CountSequencesViaPermitations()
    {
        GeneratePermutations(0);
        return;
    }

    private static void GeneratePermutations(int start)
    {
        if (start == allCombosUsed.Length - 1)
        {
            LoadOutput();
            return;
        }
        GeneratePermutations(start + 1);
        for (int i = start; i < allCombosUsed.Length; i++)
        {
            if (allCombosUsed[start] != allCombosUsed[i]) // if true for all the perminations
            {
                Swap(ref allCombosUsed[start], ref allCombosUsed[i]);
                GeneratePermutations(start + 1);
                Swap(ref allCombosUsed[start], ref allCombosUsed[i]);
            }
        }
    }

    private static void Swap<T>(ref T first, ref T second)
    {
        T old = first;
        first = second;
        second = old;
    }

    private static void LoadOutput()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < allCombosUsed.Length; i++)
        {
            if (i == 0)
            {
                sb.AppendFormat("{0} ", allCombosUsed[i]);
            }
            else if (i == allCombosUsed.Length - 1)
            {
                sb.AppendFormat("| {0}", allCombosUsed[i]);
            }
            else
            {
                sb.AppendFormat("| {0} ", allCombosUsed[i]);
            }
        }
        sequences.Add(sb.ToString());
    }
    #endregion Generating Permutations

    private static void GetTheInput(int lengthNumber)
    {
        allCombosStrait = new string[lengthNumber];
        allCombosReversed = new string[allCombosStrait.Length];
        for (int i = 0; i < lengthNumber; i++)
        {
            string lineRead = (Console.ReadLine());
            char firstToSwap = (lineRead[0]);
            char secondToSwap = (lineRead[2]);
            string currentNew = String.Format("({0}, {1})", firstToSwap, secondToSwap);
            allCombosStrait[i] = currentNew;
        }

        for (int i = 0; i < allCombosReversed.Length; i++)
        {
            char firstToSwap = (allCombosStrait[i][1]);
            char secondToSwap = (allCombosStrait[i][4]);
            string currentNew = String.Format("({0}, {1})", secondToSwap, firstToSwap);
            allCombosReversed[i] = currentNew;
        }
    }

    private static void PrintGeneralOutput()
    {
        Console.WriteLine(sequences.Count);
        foreach (var item in sequences)
        {
            Console.WriteLine(item);
        }
    }
}
