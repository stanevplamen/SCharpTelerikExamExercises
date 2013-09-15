using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeaturingWithGrisko
{
    class PermutationWithRepetition
    {
        private static List<List<int>> combos = new List<List<int>>();
        private static int counter = 0;

        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            int[] numbers = ConvertToArray(input);
            PermutationsWithRepetition(numbers);
            Console.WriteLine(counter);
        }

        private static int[] ConvertToArray(string input)
        {
            int[] numbs = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                numbs[i] = (int)(input[i]);
            }
            return numbs;
        }

        static void PermutationsWithRepetition(int[] numbersSet)
        {
            Array.Sort(numbersSet);
            Permute(numbersSet, 0, numbersSet.Length);
        }

        static void Permute(int[] numbersSet, int start, int end)
        {
            CheckNumbers(numbersSet);

            int swapValue = 0;

            if (start < end)
            {
                for (int i = end - 2; i >= start; i--)
                {
                    for (int j = i + 1; j < end; j++)
                    {
                        if (numbersSet[i] != numbersSet[j])
                        {
                            swapValue = numbersSet[i];
                            numbersSet[i] = numbersSet[j];
                            numbersSet[j] = swapValue;

                            Permute(numbersSet, i + 1, end);
                        }
                    }

                    swapValue = numbersSet[i];
                    for (int k = i; k < end - 1; k++)
                    {
                        numbersSet[k] = numbersSet[k + 1];
                    }
                    numbersSet[end - 1] = swapValue;
                }
            }
        }

        static void CheckNumbers(int[] numbers)
        {
            bool isCounted = true;
            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i] == numbers[i + 1])
                {
                    isCounted = false;
                    break;
                }
            }
            if (isCounted == true)
            {
                counter++;
            }
        }
    }
}