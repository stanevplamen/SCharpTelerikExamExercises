using System;
using System.Collections.Generic;
using System.Text;
using Wintellect.PowerCollections;

public class Times : IComparable
{
    public DateTime currentTime { get; set; }
    public int orderID { get; set; }

    public Times(DateTime current, int id)
    {
        this.currentTime = current;
        this.orderID = id;
    }

    public int CompareTo(object obj)
    {
        return this.orderID.CompareTo((obj as Times).orderID);
    }
}

class Metro
{
    /// <summary>
    /// The task for now is working for 40p in BG coder
    /// The program shoud be optimized and test against input cameras order only
    /// The speed should be optimzed also
    /// </summary>
    private static Dictionary<string,int> stations;               /// number - station
    private static int[,] stationsDistances;        /// station number - station number - distance between
    private static SortedDictionary<Times, Tuple<string, int, int, int, int>> cameraRecords; /// <DateTime>, <station out, in, direction, order>
    private static bool[] dateUsed;
    private static SortedDictionary<DateTime, Tuple<string, int, int, int, int>> currentTrainDates;
    private static int peak;
    private static StringBuilder resultSB;
    private static bool hasPeak = false;

    static void Main()
    {
//        //This section is compiled in Debug Mode only.
//#if DEBUG
//        Console.SetIn(new System.IO.StreamReader("input.txt"));
//#endif
        ReadTheStations();
        ReadCameraRecords();
        peak = int.Parse(Console.ReadLine());
        resultSB = new StringBuilder();
        SearchInAllTrains();
        if (hasPeak == true)
        {
            Console.WriteLine(resultSB.ToString());
        }
        else
        {
            Console.WriteLine("OK");
        }
    }

    #region Train search
    private static void SearchInAllTrains()
    {
        dateUsed = new bool[cameraRecords.Count];
        foreach (var pair in cameraRecords)
        {
            if (dateUsed[pair.Key.orderID] == false)
            {
                currentTrainDates = new SortedDictionary<DateTime, Tuple<string, int, int, int, int>>();
                dateUsed[pair.Key.orderID] = true;
                currentTrainDates.Add(pair.Key.currentTime, pair.Value);
                LoadOneTrain(pair.Key, pair.Value);
                SolveOneTrain();
            }
        }
    }

    private static void SolveOneTrain()
    {
        int currentPeak = 0;
        foreach (var pair in currentTrainDates)
        {
            currentPeak = currentPeak + pair.Value.Item2 - pair.Value.Item3;
            if (currentPeak > peak)
            {
                string currentdiir = string.Empty;
                if (pair.Value.Item4 == 1)
	            {
		            currentdiir = ">>";
	            }
                else
	            {
                    currentdiir = "<<";
	            }
                resultSB.AppendFormat("{0:d.M.yyyy H:mm} | {1} | {2} | {3}\n", pair.Key, pair.Value.Item1, currentdiir, currentPeak);
                hasPeak = true;
            }
            else if (currentPeak < 0)
            {
                currentPeak = 0;
            }
            else if (stations[pair.Value.Item1] == 0 && pair.Value.Item4 == 2)
            {
                currentPeak = 0;
            }
            else if (stations[pair.Value.Item1] == stations.Count - 1 && pair.Value.Item4 == 1)
            {
                currentPeak = 0;
            }
            else
            {
                continue;
            }
        }
    }

    private static void LoadOneTrain(Times startKey, Tuple<string, int, int, int, int> startValue)
    {
        foreach (var pair in cameraRecords)
        {
            if (dateUsed[pair.Key.orderID] == true)
            {
                continue;
            }
            TimeSpan currentSpan = new TimeSpan();
            if (startKey.currentTime > pair.Key.currentTime)
            {
                currentSpan = startKey.currentTime - pair.Key.currentTime;
            }
            else if (startKey.currentTime < pair.Key.currentTime)
            {
                currentSpan = pair.Key.currentTime - startKey.currentTime;
            }
            else // for sure its not the same trane
	        {
                continue;
	        }
            int spanMinutes = (int)currentSpan.TotalMinutes;

            int startMinutes = 0;
            if (startKey.currentTime < pair.Key.currentTime)
            {
                int currentDir = startValue.Item4;
                int currentStationNumber = stations[startValue.Item1];

                while (startMinutes < spanMinutes)
                {
                    if (currentDir == 1 && InRange(currentStationNumber + 1))
                    {
                        currentDir = 1;
                    }
                    else if (currentDir == 1 && !InRange(currentStationNumber + 1))
                    {
                        currentDir = 2;
                    }
                    else if (currentDir == 2 && InRange(currentStationNumber - 1))
                    {
                        currentDir = 2;
                    }
                    else if (currentDir == 2 && !InRange(currentStationNumber - 1))
                    {
                        currentDir = 1;
                    }
                    if (currentDir == 1)
                    {
                        startMinutes = startMinutes + stationsDistances[currentStationNumber, currentStationNumber + 1];
                        currentStationNumber++;
                    }
                    else // currentDir == 2
                    {
                        startMinutes = startMinutes + stationsDistances[currentStationNumber - 1, currentStationNumber];
                        currentStationNumber--;
                    }
                }
                if (startMinutes == spanMinutes && currentDir == pair.Value.Item4 && currentStationNumber == stations[pair.Value.Item1])
                {
                    dateUsed[pair.Key.orderID] = true;
                    currentTrainDates.Add(pair.Key.currentTime, pair.Value);
                }
                else
                {
                    continue;
                }
            }
            else
            {
                int currentDir = pair.Value.Item4;
                int currentStationNumber = stations[pair.Value.Item1];

                while (startMinutes < spanMinutes)
                {
                    if (currentDir == 1 && InRange(currentStationNumber + 1))
                    {
                        currentDir = 1;
                    }
                    else if (currentDir == 1 && !InRange(currentStationNumber + 1))
                    {
                        currentDir = 2;
                    }
                    else if (currentDir == 2 && InRange(currentStationNumber - 1))
                    {
                        currentDir = 2;
                    }
                    else if (currentDir == 2 && !InRange(currentStationNumber - 1))
                    {
                        currentDir = 1;
                    }
                    if (currentDir == 1)
                    {
                        startMinutes = startMinutes + stationsDistances[currentStationNumber, currentStationNumber + 1];
                        currentStationNumber++;
                    }
                    else // currentDir == 2
                    {
                        startMinutes = startMinutes + stationsDistances[currentStationNumber - 1, currentStationNumber];
                        currentStationNumber--;
                    }
                }
                if (startMinutes == spanMinutes && currentDir == startValue.Item4 && currentStationNumber == stations[startValue.Item1])
                {
                    dateUsed[pair.Key.orderID] = true;
                    currentTrainDates.Add(pair.Key.currentTime, pair.Value);
                }
                else
                {
                    continue;
                }
            }
            
        }
    }

    private static bool InRange(int index)
    {
        bool inRange = index < stationsDistances.GetLength(0) && index >= 0;
        return inRange;
    }
    #endregion

    #region Stations Input Matrix
    private static void ReadTheStations()
    {
        int allStations = int.Parse(Console.ReadLine());
        stations = new Dictionary<string, int>();
        string stationsToken = Console.ReadLine();
        string[] splitedToken = stationsToken.Split(new string[] { "-->" }, StringSplitOptions.RemoveEmptyEntries);

        CopyAllStations(splitedToken);
        int[] onlyDists = CopyDistances(splitedToken, allStations - 1);
        LoadTheDistances(onlyDists, allStations);
    }

    private static void LoadTheDistances(int[] onlyDists, int allStations) // TODO check
    {
        stationsDistances = new int[allStations, allStations];
        for (int i = 0; i < allStations; i++)
        {
            for (int j = i + 1; j < allStations; j++)
            {
                stationsDistances[i, j] = stationsDistances[i, j - 1] + onlyDists[j - 1];
            }
        }
        for (int i = 0; i < allStations; i++)
        {
            for (int j = i + 1; j < allStations; j++)
            {
                stationsDistances[j, i] = stationsDistances[j - 1, i] + onlyDists[j - 1];
            }
        } 
    }

    private static int[] CopyDistances(string[] splitedToken, int length)
    {
        int j = 0;
        int[] onlyDists = new int[length];
        for (int i = 1; i < splitedToken.Length - 1; i += 2) /// always odd number
        {
            onlyDists[j] = int.Parse(splitedToken[i].Trim());
            j++;
        }
        return onlyDists;
    }

    private static void CopyAllStations(string[] splitedToken)
    {
        int j = 0;
        for (int i = 0; i < splitedToken.Length; i += 2) /// always even number
        {
            stations.Add(splitedToken[i].Trim(), j);
            j++;
        }
    }
    #endregion

    #region Camera Information Data Structure
    private static void ReadCameraRecords()
    {
        cameraRecords = new SortedDictionary<Times, Tuple<string, int, int, int, int>>();
        int allRecords = int.Parse(Console.ReadLine());
        for (int i = 0; i < allRecords; i++)
        {
            string currentToken = Console.ReadLine();
            string[] splitedSecondToken = currentToken.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            DateTime currentDateTime = new DateTime();
            currentDateTime = DateTime.ParseExact(splitedSecondToken[0].TrimEnd(),  "d.M.yyyy H:mm", null);
            Times newT = new Times(currentDateTime, i);
            string station = splitedSecondToken[1].Trim();
            int peopleOut = int.Parse(splitedSecondToken[2].Trim());
            int peopleIn = int.Parse(splitedSecondToken[3].Trim());
            int direction = GetDirection(splitedSecondToken[4].Trim());

            Tuple<string, int, int, int, int> currentPoint = new Tuple<string, int, int, int, int>(station, peopleOut, peopleIn, direction, i);
            cameraRecords.Add(newT, currentPoint); /// <DateTime>, <station, out, in, direction, order>
        }
    }

    private static int GetDirection(string toRead)
    {
        switch (toRead)
        {
            case ">>":
                return 1;
            case "<<":
                return 2;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    #endregion
}

