using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

namespace AlgorithmInsteadDijkstraAndPrimPoints
{
    /// <summary>
    /// Solution if we must destroy all points 
    /// *this solution is working in BG-coder for 70pts because all the word cases between tower&tower are not coverred
    /// </summary>
    class Towers
    {
        private static Dictionary<int, Tuple<int, int>> towersCoords;      /// number , x coordinate - y coordinate 
        private static Dictionary<int, Tuple<int, int>> pointsCoords;

        private static Dictionary<int, Tuple<double, int>> towersDists;    /// numberFrom , distance - numberTo  /// each tower keeps its own min distance
        private static Dictionary<int, Tuple<double, int>> pointsDists;    /// each point records its min distance to each tower distance
        static double tempMinResult = Double.MaxValue;
        static int keyTwo = 0;
        static bool[] visitedPoints;

        static void Main()
        {
            ReadInput();
            if (towersCoords.Count > 1)
            {
                LoadTheDistancesTowers();
                LoadTheDistancesPoints();
                CalculateResult();
            }
            else
            {
                CalculateResultOneTower();
            }
        }

        #region Calculate 
        private static void CalculateResult()
        {
            OrderedBag<double> allMinimums = new OrderedBag<double>();
            int counter = 0;
            visitedPoints = new bool[towersCoords.Count + pointsCoords.Count + 1];
            foreach (var pair in pointsDists)
            {
                counter++;
                visitedPoints[pair.Key] = true;
                double currentPointDist = pair.Value.Item1;
                double currentTowerDist = towersDists[pair.Value.Item2].Item1;
                if (counter == pointsDists.Count)
                {
                    allMinimums.Add(currentPointDist);
                    break;
                }
                else
                {
                    allMinimums.Add(currentPointDist);
                }

                #region not used
                /// not used
                //double currentTowerDist = towersDists[pair.Value.Item2].Item1;

                //int nextTower = towersDists[pair.Value.Item2].Item2;

                //double distBetweenNextAndPoint = CalculateDistAgain(towersCoords[nextTower], pointsCoords[pair.Key]);

                //if (currentPointDist >= currentTowerDist)
                //{
                //    allMinimums.Add(currentPointDist);
                //}
                //else
                //{
                //    double min = Math.Min(distBetweenNextAndPoint, currentPointDist);
                //    allMinimums.Add(min);
                //}
                #endregion
            }
            Console.WriteLine("{0:0.000000}", allMinimums.GetLast());
        }

        private static void CalculateResultOneTower()
        {
            pointsDists = new Dictionary<int, Tuple<double, int>>();
            foreach (var pairOne in pointsCoords)
            {
                keyTwo = -1;
                foreach (var pairTwo in towersCoords)
                {
                    CalculateDistanceTowersPoints(pairOne.Value, pairTwo.Value, pairOne.Key, pairTwo.Key);
                }
                Tuple<double, int> tempTup = new Tuple<double, int>(tempMinResult, keyTwo);
                pointsDists.Add(pairOne.Key, tempTup);
                tempMinResult = double.MaxValue;
            }

            OrderedBag<double> allMinimums = new OrderedBag<double>();
            foreach (var pair in pointsDists) /// take the min distance of the tower to other tower
            {
                allMinimums.Add(pair.Value.Item1);
            }
            Console.WriteLine("{0:0.000000}", allMinimums.GetLast());
        }

        private static void CalculateDistanceTowersPoints(Tuple<int, int> start, Tuple<int, int> next, int numb1, int numb2)
        {
            double result = 0.0;
            result = Math.Abs(((start.Item1 - next.Item1) * (start.Item1 - next.Item1) + (start.Item2 - next.Item2) * (start.Item2 - next.Item2)) * 1.0);
            result = Math.Sqrt(result);

            if (tempMinResult > result)
            {
                tempMinResult = result;
                keyTwo = numb2;
            }
        }

        private static double CalculateDistAgain(Tuple<int, int> start, Tuple<int, int> next) /// not used in current solution
        {
            double result = 0.0;
            result = Math.Abs(((start.Item1 - next.Item1) * (start.Item1 - next.Item1) + (start.Item2 - next.Item2) * (start.Item2 - next.Item2)) * 1.0);
            result = Math.Sqrt(result);
            return result;
        }
        #endregion

        #region Load the points with distances
        private static void LoadTheDistancesPoints()
        {
            pointsDists = new Dictionary<int, Tuple<double, int>>();
            foreach (var pairOne in pointsCoords)
            {
                keyTwo = -1;
                foreach (var pairTwo in towersDists)
                {
                    CalculateDistancePoints(pairOne.Key, pairOne.Value, pairTwo.Key, pairTwo.Value);

                }
                Tuple<double, int> tempTup = new Tuple<double, int>(tempMinResult, keyTwo);
                pointsDists.Add(pairOne.Key, tempTup);
                tempMinResult = double.MaxValue;
            }
        }

        private static void CalculateDistancePoints(int pointNumber, Tuple<int, int> start, int towerNumber, Tuple<double, int> secondValue)
        {
            Tuple<int, int> next = new Tuple<int, int>(towersCoords[towerNumber].Item1, towersCoords[towerNumber].Item2);
            double result = 0.0;
            result = Math.Abs(((start.Item1 - next.Item1) * (start.Item1 - next.Item1) + (start.Item2 - next.Item2) * (start.Item2 - next.Item2)) * 1.0);
            result = Math.Sqrt(result);

            if (tempMinResult > result)
            {
                tempMinResult = result;
                keyTwo = towerNumber;
            }
        }
        #endregion

        #region Load The towers with distances
        private static void LoadTheDistancesTowers()
        {
            towersDists = new Dictionary<int, Tuple<double, int>>();
            foreach (var pairOne in towersCoords)
            {
                keyTwo = -1;
                foreach (var pairTwo in towersCoords)
                {
                    CalculateDistanceTowers(pairOne.Value, pairTwo.Value, pairOne.Key, pairTwo.Key);
                }

                Tuple<double, int> tempTup = new Tuple<double, int>(tempMinResult, keyTwo);
                towersDists.Add(pairOne.Key, tempTup);
                tempMinResult = double.MaxValue;
            }
        }

        private static void CalculateDistanceTowers(Tuple<int, int> start, Tuple<int, int> next, int numb1, int numb2) /// Gets the distance between points (X1,Y1) and (X2,Y2)
        {
            if (numb1 == numb2)
            {
                return;
            }
            else
            {
                double result = 0.0;
                result = Math.Abs(((start.Item1 - next.Item1) * (start.Item1 - next.Item1) + (start.Item2 - next.Item2) * (start.Item2 - next.Item2)) * 1.0);
                result = Math.Sqrt(result);

                if (tempMinResult > result)
                {
                    tempMinResult = result;
                    keyTwo = numb2;
                }
            }
        }
        #endregion

        #region Load the start towers and points
        private static void ReadInput()
        {
            string firstToken = Console.ReadLine();
            string[] splitedToken = firstToken.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int rows = int.Parse(splitedToken[0]);
            int cols = int.Parse(splitedToken[1]);

            towersCoords = new Dictionary<int, Tuple<int, int>>();
            pointsCoords = new Dictionary<int, Tuple<int, int>>();
            int counter = 1;
            for (int i = 0; i < rows; i++)
            {
                string currentToken = Console.ReadLine();
                for (int j = 0; j < cols; j++)
                {
                    if (currentToken[j] == '.')
                    {
                        continue;
                    }
                    else if (currentToken[j] == '*')
                    {
                        Tuple<int, int> pointXY = new Tuple<int, int>(i, j);
                        towersCoords.Add(counter, pointXY);
                        counter++;
                    }
                    else // if (currentToken[j] == 'x')
                    {
                        Tuple<int, int> pointXY = new Tuple<int, int>(i, j);
                        pointsCoords.Add(counter, pointXY);
                        counter++;
                    }
                }
            }
        }
        #endregion
    }
}

