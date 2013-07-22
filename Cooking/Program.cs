using System;
using System.Collections.Generic;

class Program
{
    static double[] firstQuantities;
    static string[] firstMeasurement;
    static string[] firstProduct;

    static double[] secondQuantities;
    static string[] secondMeasurement;
    static string[] secondProduct;

    static void Main()
    {
        AddDictionary();
        int n = int.Parse(Console.ReadLine());                                      
        string[] inputLinesFirst = new string[n];
        for (int i = 0; i < n; i++)
        {
            inputLinesFirst[i] = Console.ReadLine();
        }
  
        int m = int.Parse(Console.ReadLine());                                     
        string[] inputLinesSecond = new string[m];
        for (int i = 0; i < m; i++)
        {
            inputLinesSecond[i] = Console.ReadLine();
        }     

        firstQuantities = new double[n];
        firstMeasurement = new string[n];
        firstProduct = new string[n];

        for (int i = 0; i < inputLinesFirst.Length; i++)
        {
            string[] parceOneStr = inputLinesFirst[i].Split(':');
            firstQuantities[i] = double.Parse(parceOneStr[0].Trim());
            firstMeasurement[i] = parceOneStr[1].Trim();
            firstProduct[i] = parceOneStr[2].Trim();
        }

        secondQuantities = new double[m];
        secondMeasurement = new string[m];
        secondProduct = new string[m];

        for (int i = 0; i < inputLinesSecond.Length; i++)
        {
            string[] parceTwoStr = inputLinesSecond[i].Split(':');
            secondQuantities[i] = double.Parse(parceTwoStr[0].Trim());
            secondMeasurement[i] = parceTwoStr[1].Trim();
            secondProduct[i] = parceTwoStr[2].Trim();
        }
        CompareTwoInputs();
        Print();
    }

    private static void Print()
    {
        for (int i = 0; i < firstMeasurement.Length; i++)
        {
            if (firstMeasurement[i] != null && firstQuantities[i] > 0)
            {
                Console.WriteLine("{0:F2}:{1}:{2}", firstQuantities[i], firstMeasurement[i], firstProduct[i]);
            }
        }
    }

    private static void CompareTwoInputs()
    {
        for (int i = 0; i < firstProduct.Length; i++)
        {
            for (int j = i + 1; j < firstProduct.Length; j++)
            {
                if (firstProduct[i] != null && firstProduct[j] != null && firstProduct[i].ToLower() == firstProduct[j].ToLower())
                {
                    // quantity i = i + j
                    firstQuantities[i] = CompareProduct(firstQuantities[i], firstQuantities[j],
                        firstMeasurement[i], firstMeasurement[j]);
                    firstQuantities[j] = 0.0;
                    firstMeasurement[j] = null;
                    firstProduct[j] = null;

                }
            }
        }

        for (int i = 0; i < secondProduct.Length; i++)
        {
            for (int j = i + 1; j < secondProduct.Length; j++)
            {
                if (secondProduct[i] != null && secondProduct[j] != null &&  secondProduct[i].ToLower() == secondProduct[j].ToLower())
                {
                    secondQuantities[i] = CompareProduct(secondQuantities[i], secondQuantities[j],
                        secondMeasurement[i], secondMeasurement[j]);
                    secondQuantities[j] = 0.0;
                    secondMeasurement[j] = null;
                    secondProduct[j] = null;

                }
            }
        }


        for (int i = 0; i < firstProduct.Length; i++)
        {
            for (int j = 0; j < secondProduct.Length; j++)
            {
                if (firstProduct[i] != null &&  secondProduct[j] != null && firstProduct[i].ToLower() == secondProduct[j].ToLower())
                {
                    firstQuantities[i] = CompareDiffProduct(firstQuantities[i], secondQuantities[j],
                        firstMeasurement[i], secondMeasurement[j]);
                    secondQuantities[j] = 0.0;
                    secondMeasurement[j] = null;
                    secondProduct[j] = null;
                    break;
                }
            }
        }

    }

    private static double CompareDiffProduct(double toSum1, double toSum2, string measure1, string measure2)
    {
        int firstMesNumber = CheckNumber(measure1);
        int secondMesNumber = CheckNumber(measure2);
        if (firstMesNumber != 9 && secondMesNumber != 9)
        {
            double addedMes = toSum1 - ((globalValues[firstMesNumber] / globalValues[secondMesNumber]) * toSum2);
            return addedMes;
        }
        else
        {
            return toSum1;
        }
    }

    private static double CompareProduct(double toSum1, double toSum2, string measure1, string measure2)
    {
        int firstMesNumber = CheckNumber(measure1);
        int secondMesNumber = CheckNumber(measure2);
        if (firstMesNumber != 9 && secondMesNumber != 9)
        {
            double addedMes = toSum1 + ((globalValues[firstMesNumber] / globalValues[secondMesNumber]) * toSum2);
            return addedMes;
        }
        else
        {
            return toSum1;
        }

    }

    private static int CheckNumber(string measure)
    {
        switch (measure)
        {
            case "cups":
                return 0;
            case "tsps":
                return 1;
            case "tbsps":
                return 2;
            case "pts":
                return 3;
            case "qts":
                return 4;
            case "fl ozs":
                return 5;
            case "gals":
                return 6;
            case "mls":
                return 7;
            case "ls":
                return 8;
            case "teaspoons":
                return 1;
            case "tablespoons":
                return 2;
            case "pints":
                return 3;
            case "quarts":
                return 4;
            case "fluid ounces":
                return 5;
            case "gallons":
                return 6;
            case "milliliters":
                return 7;
            case "liters":
                return 8;
            default:
                return 9;
        }
    }

    static List<double> globalValues = new List<double>();

    static void AddDictionary()
    {
        globalValues.Add(1.0);              // "cup"
        globalValues.Add(48.0);             // "tsps",
        globalValues.Add(48.0/3);           // "tbsps", 
        globalValues.Add(0.5);              // pts", 
        globalValues.Add(0.25);             // "qts", 
        globalValues.Add(8.0);              // "fl ozs", 
        globalValues.Add(0.25 / 4);         // gals
        globalValues.Add(48.0 * 5);         // mls",
        globalValues.Add(48.0 * 5 / 1000);  // "ls",
    }
}

