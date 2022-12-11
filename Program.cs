using System;
using System.Diagnostics;
using System.Linq;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, \x1b[1mWorld!\x1b[0m");

// 5 and 81 can be defined globally
int[,] sudoku = new int[5,81];
sudoku = ReadFromFile.ReadTXT(@"Sudoku_puzzels_5.txt");
Board testBoard = new Board(sudoku.GetRow(2));


int runs = 100;
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
for (int i = 0; i < runs; i++)
    new Solver(testBoard);
stopWatch.Stop();

TimeSpan diff = stopWatch.Elapsed;
Console.WriteLine("Average time:" + diff.TotalSeconds / runs);
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