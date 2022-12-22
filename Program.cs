using System;
using System.Diagnostics;
using System.Linq;

namespace sudoku
{
    class Progrm
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\x1b[1mHill climb sudoku solver\x1b[0m");
            
            string path_to_file;
            if(args.Length > 0)
            {
                path_to_file = args[0];
                Console.WriteLine($"Sudoku's will be read from the file '{path_to_file}'.");  
            }  
            else
            {
                path_to_file = "Sudoku_puzzels_5.txt";
                Console.WriteLine($"No sudoku file specified. Sudoku's will be read from the default file '{path_to_file}'."); 
            }
            
            int[,] all_sudokus = ReadFromFile.ReadTXT(@$"{path_to_file}");
            int number_of_sudokus = all_sudokus.GetLength(0);
            Board sudoku;
            for (int i = 0; i < number_of_sudokus; i++)
            {
                Console.WriteLine($"\n\x1b[1mSudoku number: {i + 1}\x1b[0m");
                sudoku = new Board(all_sudokus.GetRow(i));
                new Solver(sudoku);
            }
        }
    }

    // Reference: https://stackoverflow.com/a/1183086/8902440
    // & https://stackoverflow.com/a/51241629/8902440
    public static class Extension
    {
        public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                    .Select(x => matrix[rowNumber, x])
                    .ToArray();
        }
    }
}