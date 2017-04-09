using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;

namespace Fibonacci_letters
{
    class Program
    {
        static void Main(string[] args)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            SVG SVGBuilder = new SVG();

            //const double GoldenRatio = 1.61803398874989484820458683436;
            Random r = new Random();

            for (int i = 0; i < 26; i++)
            {
                int[,] points = new int[5, 2] /*{
                    {0, 0},
                    {1, 1 },
                    {1, 1 },
                    {2, 2},
                    {9 ,2 },
                    }*/;


                for (int row = 0; row < points.GetLength(0); row++)
                {
                    for (int col = 0; col < points.GetLength(1); col++)
                    {
                        points[row, col] = r.Next(1, 100);
                    }
                }

                Directory.CreateDirectory(desktop + "/RNGLetters/");
                File.WriteAllText(desktop + "/RNGLetters/" + i + ".svg", SVGBuilder.polyline(points));


            }

            Console.WriteLine("The letters generated are in " + desktop + "/RNGLetters");
        }
    }

    class SVG
    {
        public string polyline(int[,] points)
        {
            if (points.GetLength(1) != 2) { throw new ArgumentException("Only one x and one y coordinate can be given per point"); }

            StringBuilder builder = new StringBuilder();
            builder.Append(@"<svg width='1000' height='1000' viewPort='0 0 120 120' xmlns='http://www.w3.org/2000/svg'> <polyline fill='none' stroke='black' points='");

            for (int row = 0; row < points.GetLength(0); row++)
            {
                string temp = "";

                for (int col = 0; col < points.GetLength(1); col++)
                {
                    temp += points[row, col];

                }
                temp = temp.Insert(2, ",");
                temp += " ";
                builder.Append(temp);
            }
            builder.Append("'/> </svg>");

            return builder.ToString();
        }
    }
}
