using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automower
{
    class Mawer
    {
        private int X { get; set; }
        private int Y { get; set; }
        public int Directionx { get; set; }
        public int Directiony { get; set; }
        private Feld[,] Bereich { get; set; }
        Random rnd;

        public Mawer(int x, int y, ref Feld[,] bereich, Random rand)
        {
            Directionx = 1;
            Directiony = 0;
            X = x;
            Y = y;
            Bereich = bereich;
            rnd = rand;
        }

        public void move()      //Version 1, nicht so genau, läuft später nur am rand und diagonal
        {
            X += Directionx;
            Y += Directiony;
            if ((X < Bereich.GetLength(0) && Y < Bereich.GetLength(1)) && (X >= 0 && Y >= 0))
            {
                Feld Actualfield = Bereich[X, Y];
                Actualfield.maehen();

                if (((((X + Directionx) == Bereich.GetLength(0) || (Y + Directiony) == Bereich.GetLength(1)) || ((X + Directionx) == -1 || (Y + Directiony == -1))) || !Bereich[X + Directionx, Y + Directiony].isinside) && Actualfield.isrand)
                {
                    List<int[]> PossibleNeighbours = new List<int[]>();
                    PossibleNeighbours.Add(new int[2] { X + 1, Y + 1 });
                    PossibleNeighbours.Add(new int[2] { X + 1, Y - 1 });
                    PossibleNeighbours.Add(new int[2] { X + 0, Y + 1 });
                    PossibleNeighbours.Add(new int[2] { X + 0, Y - 1 });
                    PossibleNeighbours.Add(new int[2] { X - 1, Y + 1 });
                    PossibleNeighbours.Add(new int[2] { X - 1, Y - 1 });
                    PossibleNeighbours.Add(new int[2] { X - 1, Y + 0 });
                    //PossibleNeighbours.Add(new int[2] { X + 1, Y + 0 });

                    List<int[]> posneigh = new List<int[]>();

                    foreach (int[] item in PossibleNeighbours)
                    {
                        if (item[0] < Bereich.GetLength(0) && item[1] < Bereich.GetLength(1))
                        {
                            if (item[0] > -1 && item[1] > -1)
                            {
                                if (Bereich[item[0], item[1]].isinside)
                                {
                                    posneigh.Add(item);
                                }
                            }
                        }
                    }

                    int[] newField = posneigh[rnd.Next(0, posneigh.Count)];

                    Directionx = newField[0] - X;
                    Directiony = newField[1] - Y;
                }

            }
        }

        public void move2()         //Version 2, genauer, fährt überall 
        {
                                        //Ohne winkel
            //List<KeyValuePair<int, int>> possibleneighbours = new List<KeyValuePair<int, int>>();
            //for (int x = 0; x < Bereich.GetLength(0); x++)
            //{
            //    for (int y = 0; y < Bereich.GetLength(1); y++)
            //    {
            //        if (Bereich[x, y].isrand)
            //        {
            //            if (x == X || y == Y)
            //            {
            //                continue;
            //            }
            //            possibleneighbours.Add(new KeyValuePair<int, int>(x, y));
            //        }
            //    }
            //}

            KeyValuePair<int, int> newField = get_new_endpoint(X, Y);                   //possibleneighbours[rnd.Next(0, possibleneighbours.Count - 1)];

            int newx = newField.Key;
            int newy = newField.Value;

            drawLine(X, Y, newx, newy);
            X = newx;
            Y = newy;
        }

        public void drawLine(int startx, int starty, int endx, int endy)        //Bresenham´s Algorithmus
        {
            int dx = Math.Abs(endx - startx), sx = startx < endx ? 1 : -1;
            int dy = -Math.Abs(endy - starty), sy = starty < endy ? 1 : -1;
            int err = dx + dy, e2;

            while (true)
            {
                Bereich[startx, starty].maehen();
                if (startx == endx && starty == endy) break;
                e2 = 2 * err;
                if (e2 >= dy)
                {
                    err += dy;
                    startx += sx;
                }

                if (e2 <= dx)
                {
                    err += dx;
                    starty += sy;
                }
            }
        }

        private KeyValuePair<int, int> get_new_endpoint(int actualx, int actualy)       //mit winkeln
        {
            double y2 = 0;
            double x = 0;
            do
            {
                double winkel = rnd.Next(0, 360);
                double steigung = Math.Tan(winkel * Math.PI / 180);

                double d = Math.Abs(actualy);

                double h2 = actualx;
                if (actualx == Bereich.GetLength(0) - 1)
                {
                    x = 0;
                }
                else
                {
                    x = Bereich.GetLength(0) - 1;
                }

                y2 = steigung * (x - h2) + d;
            }
            while (y2 >= 30 || y2 < 0);

            return new KeyValuePair<int, int>((int)x, Math.Abs((int)y2));
        }
    }
}

