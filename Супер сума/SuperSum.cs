using System;

class SuperSum
{
    static void Main()
    {
        string input = Console.ReadLine(); //"2 3";
        string[] splitedInput = input.Split();
        int K = int.Parse(splitedInput[0]);
        int N = int.Parse(splitedInput[1]);

        int[,] resultMatrix = new int[K + 1, N + 1];

        for (int coll = 1; coll <= N; coll++)
		{
			 resultMatrix[1, coll] = coll;
		}

        int row = 0;
        int col = 0;
        for (row = 2; row <= K; row++)
        {
            for (col = 1; col<= N; col++)
            {
                resultMatrix[row, col] = resultMatrix[row - 1, col] + resultMatrix[row, col - 1];
            }
        }
        if (K > 1)
        {
            int sum = 0;
            for (int i = 1; i <= col - 1; i++)
            {
                sum = sum + resultMatrix[row - 1, i];
            }
            Console.WriteLine(sum);
        }
        else if (K == 1)
        {
            int sum = 0;
            for (int i = 1; i <= N; i++)
            {
                sum = sum + resultMatrix[1, i];
            }
            Console.WriteLine(sum);
        }
    }
}

