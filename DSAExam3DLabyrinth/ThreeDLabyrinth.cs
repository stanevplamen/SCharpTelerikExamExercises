using System;
using System.Collections.Generic;

class ThreeDLabyrinth
{
    class Point
    {
        public int xCoordinate { get; set; }
        public int yCoordinate { get; set; }
        public int zCoordinate { get; set; }
        public int counter { get; set; }

        public Point(int x, int y, int z, int c)
        {
            xCoordinate = x;
            yCoordinate = y;
            zCoordinate = z;
            counter = c;
        }
    }

    static int startXlevel;
    static int startYrow;
    static int startZcol;
    static int Xlevels;
    static int Yrows;
    static int Zcols;
    static char[, ,] theLabyrinth;

    static void Main()
    {
        ReadTheInput();
        Point startPoint = new Point(startXlevel, startYrow, startZcol, 0);
        ProvideBFS(startPoint);
    }

    private static void ProvideBFS(Point startPoint)
    {
        Queue<Point> labPoints = new Queue<Point>();
        if (theLabyrinth[startXlevel, startYrow, startZcol] == '.')
        {
            theLabyrinth[startXlevel, startYrow, startZcol] = '@'; 
            labPoints.Enqueue(startPoint);
        }
        else if (theLabyrinth[startXlevel, startYrow, startZcol] == 'U')
        {
            theLabyrinth[startXlevel + 1, startYrow, startZcol] = 'u'; 
            labPoints.Enqueue(new Point(startXlevel + 1, startYrow, startZcol, 1)); 
        }
        else if (theLabyrinth[startXlevel, startYrow, startZcol] == 'D')
        {
            theLabyrinth[startXlevel - 1, startYrow, startZcol] = 'd'; 
            labPoints.Enqueue(new Point(startXlevel - 1, startYrow, startZcol, 1));
        }

        int value = 0;
        while (true)
        {
            Point currentPoint = labPoints.Dequeue();
            value = currentPoint.counter + 1;

            if (!XInRange(currentPoint.xCoordinate)) 
            {
                Console.WriteLine(value - 1);
                return;
            }

            #region Three cases '.' 'D' 'U'
            if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate] == '@' || theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate] == '.')
            {
                if (currentPoint.yCoordinate - 1 >= 0)
                {
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate - 1, currentPoint.zCoordinate] == '.')
                    {
                        theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate - 1, currentPoint.zCoordinate] = '@';
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate, currentPoint.yCoordinate - 1, currentPoint.zCoordinate, value));
                    }
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate - 1, currentPoint.zCoordinate] == 'U')
                    {
                        theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate - 1, currentPoint.zCoordinate] = 'u';
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate + 1, currentPoint.yCoordinate - 1, currentPoint.zCoordinate, value + 1));
                    }
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate - 1, currentPoint.zCoordinate] == 'D')
                    {
                        theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate - 1, currentPoint.zCoordinate] = 'd';
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate - 1, currentPoint.yCoordinate - 1, currentPoint.zCoordinate, value + 1));
                    }
                }

                if (currentPoint.yCoordinate + 1 < Yrows)
                {
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate + 1, currentPoint.zCoordinate] == '.')
                    {
                        theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate + 1, currentPoint.zCoordinate] = '@';
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate, currentPoint.yCoordinate + 1, currentPoint.zCoordinate, value));
                    }
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate + 1, currentPoint.zCoordinate] == 'U')
                    {
                        theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate + 1, currentPoint.zCoordinate] = 'u';
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate + 1, currentPoint.yCoordinate + 1, currentPoint.zCoordinate, value + 1));
                    }
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate + 1, currentPoint.zCoordinate] == 'D')
                    {
                        theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate + 1, currentPoint.zCoordinate] = 'd';
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate - 1, currentPoint.yCoordinate + 1, currentPoint.zCoordinate, value + 1));
                    }
                }

                if (currentPoint.zCoordinate - 1 >= 0)
                {
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate - 1] == '.')
                    {
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate - 1, value));
                    }
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate - 1] == 'U')
                    {
                        theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate - 1] = 'u';
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate + 1, currentPoint.yCoordinate, currentPoint.zCoordinate - 1, value + 1));
                    }
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate - 1] == 'D')
                    {
                        theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate - 1] = 'd';
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate - 1, currentPoint.yCoordinate, currentPoint.zCoordinate - 1, value + 1));
                    }
                }

                if (currentPoint.zCoordinate + 1 < Zcols)
                {
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate + 1] == '.')
                    {
                        theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate + 1] = '@';
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate + 1, value));
                    }
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate + 1] == 'U')
                    {
                        theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate + 1] = 'u';
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate + 1, currentPoint.yCoordinate, currentPoint.zCoordinate + 1, value + 1));
                    }
                    if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate + 1] == 'D')
                    {
                        theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate + 1] = 'd';
                        labPoints.Enqueue(new Point(currentPoint.xCoordinate - 1, currentPoint.yCoordinate, currentPoint.zCoordinate + 1, value + 1));
                    }
                }
            }
            else if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate] == 'u' || theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate] == 'U')
            {
                if (currentPoint.xCoordinate + 1 <= Xlevels)
                {
                     //theLabyrinth[currentPoint.xCoordinate + 1, currentPoint.yCoordinate, currentPoint.zCoordinate] = 'u';
                     labPoints.Enqueue(new Point(currentPoint.xCoordinate + 1, currentPoint.yCoordinate, currentPoint.zCoordinate, value));
                }
                else
                {
                    Console.WriteLine(value);
                    return;
                }
            }
            else if (theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate] == 'd' || theLabyrinth[currentPoint.xCoordinate, currentPoint.yCoordinate, currentPoint.zCoordinate] == 'D')
            {
                if (currentPoint.xCoordinate - 1 >= 0)
                {
                    //theLabyrinth[currentPoint.xCoordinate - 1, currentPoint.yCoordinate, currentPoint.zCoordinate] = 'd';
                    labPoints.Enqueue(new Point(currentPoint.xCoordinate - 1, currentPoint.yCoordinate, currentPoint.zCoordinate, value));
                }
                else
                {
                    Console.WriteLine(value);
                    return;
                }
            }
            #endregion
        }
    }

    static bool XInRange(int coordx)
    {
        bool xInRange = coordx >= 0 && coordx < Xlevels;
        return (xInRange);
    }

    private static void ReadTheInput()
    {
        string firstLineRead = Console.ReadLine();
        string[] splitedLine = firstLineRead.Split();
        startXlevel = int.Parse(splitedLine[0]);
        startYrow = int.Parse(splitedLine[1]);
        startZcol = int.Parse(splitedLine[2]);

        string secondLineRead = Console.ReadLine();
        string[] splitedLineTwo = secondLineRead.Split();
        Xlevels = int.Parse(splitedLineTwo[0]);
        Yrows = int.Parse(splitedLineTwo[1]);
        Zcols = int.Parse(splitedLineTwo[2]);

        theLabyrinth = new char[Xlevels,Yrows,Zcols];

        for (int lev = 0; lev < Xlevels; lev++)
        {
            for (int row = 0; row < Yrows; row++)
            {
                string currentLine = Console.ReadLine();
                for (int col = 0; col < Zcols; col++)
                {
                    theLabyrinth[lev,row,col] = currentLine[col];
                }
            }
        }
    }
}

