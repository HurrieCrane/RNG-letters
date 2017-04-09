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
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // The default are for the letter to be saved

            SVG SVGBuilder = new SVG(); // An object, of SVG, used to build the SVGs

            Random r = new Random(); // An object used to generate the random numbers

            for (int i = 0; i < 26; i++) // This for loop allows the process to be done for each letter of the alphabet
            {
                int[,] points = new int[5, 2]; //The points used the 5 corresponding to 5 points per letter and the 2 the X and Y value

                for (int row = 0; row < points.GetLength(0); row++)
                {
                    for (int col = 0; col < points.GetLength(1); col++)
                    {
                        points[row, col] = r.Next(1, 100); // Random numbers are generated to be used to make a polyline
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
            // ^ If there's more than an X and Y coordinate given an exception is thrown

            StringBuilder builder = new StringBuilder(); // String builder is used to construct the SVG
            builder.Append(@"<svg width='1000' height='1000' viewPort='0 0 120 120' xmlns='http://www.w3.org/2000/svg'> <polyline fill='none' stroke='black' points='");
            // ^ This is the basics of the SVG and is used to formulate the basis for it

            for (int row = 0; row < points.GetLength(0); row++)
            {
                string temp = ""; // This is temp storage for the coordiantes to operate on them

                for (int col = 0; col < points.GetLength(1); col++)
                {
                    temp += points[row, col];
                }

                temp = temp.Insert(2, ","); // A comma is added between the coordinates however currently if either coord is just one number then the coordinates are uneven
                temp += " "; // A space is added for the next coordinate
                builder.Append(temp); // The coordinates are added to builder
            }
            builder.Append("'/> </svg>"); // This adds the final closing tags to the SVG

            return builder.ToString(); // The SVG string is returned
        }
    }
}
