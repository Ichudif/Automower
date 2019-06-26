using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automower
{
    class Program
    {
        static Feld[,] Bereich;
        static Mawer MyMawer;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            initialize(30, 30);

            MyMawer = new Mawer(15, 15, ref Bereich, rnd);

            startmovin();
        }

        private static void startmovin()
        {
            while (true)
            {
                reset(Bereich);
                for (int i = 0; i < 1000; i++)
                {
                    MyMawer.move2();
                    //output();
                    //System.Threading.Thread.Sleep(500);
                }

                output();

                //System.Threading.Thread.Sleep(1000);
                Console.ReadKey();
            }
        }

        private static void output()
        {
            Console.Clear();
            for (int x = 0; x < Bereich.GetLength(0); x++)
            {
                for (int y = 0; y < Bereich.GetLength(1); y++)
                {
                    //if (Bereich[x, y].maehanzahl > 0)
                    //{
                    //    Console.WriteLine("X:" + x + ", Y:" + y);
                    //}

                    //Console.WriteLine("Koordinaten: x: " + x + ", y: " + y + ", Anzahl der Mähvorgänge: " + Bereich[x, y].maehanzahl);

                    if (Bereich[x, y].maehanzahl < 9)
                    {
                        Console.Write(Bereich[x, y].maehanzahl + "");
                    }
                    else
                    {
                        Console.Write("X");
                    }
                }
                Console.WriteLine();
            }
        }

        private static void initialize(int hoehe, int breite)
        {
            Bereich = new Feld[hoehe, breite];
            for (int x = 0; x < Bereich.GetLength(0); x++)
            {
                for (int y = 0; y < Bereich.GetLength(1); y++)
                {
                    Bereich[x, y] = new Feld(((x == 0 || y == 0) || (x == Bereich.GetLength(0) - 1 || y == Bereich.GetLength(1) - 1)) ? true : false, true);
                }
            }
        }

        private static void reset(Feld[,] Bereich)
        {
            for (int x = 0; x < Bereich.GetLength(0); x++)
            {
                for (int y = 0; y < Bereich.GetLength(1); y++)
                {
                    Bereich[x, y].maehanzahl = 0;
                }
            }
        }
    }
}
