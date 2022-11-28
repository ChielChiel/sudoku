using System;
using System.Linq;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// 5 and 81 can be defined globally
Console.WriteLine(0 % 9);
Console.WriteLine(1 % 9);
Console.WriteLine(2 % 9);
Console.WriteLine(3 % 9);
Console.WriteLine(4 % 9);
Console.WriteLine(5 % 9);
Console.WriteLine(8 % 9);
Console.WriteLine(9 % 9);
Console.WriteLine(10 % 9);
int[,] sudoku = new int[5,81];
sudoku = ReadFromFile.ReadTXT(@"Sudoku_puzzels_5.txt");

// Testing purposes only
// for (int i = 0; i < 5; i++)
// {        
//     for (int j = 0; j < 81; j++)
//     {
//         System.Console.Write(sudoku[i,j]);
//     }
//     System.Console.WriteLine("");
// }


Bord testbord = new Bord(sudoku.GetRow(0));
new Solver(testbord);


// Aangepast van https://stackoverflow.com/a/1183086/8902440
// & https://stackoverflow.com/a/51241629/8902440
// Om rows en columns makkelijk te kunnen pakken van 2d arrays
public static class Extension
{
    public static T[] GetColumn<T>(this T[,] matrix, int columnNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
    }

    public static T[] GetRow<T>(this T[,] matrix, int rowNumber)
    {
        return Enumerable.Range(0, matrix.GetLength(1))
                .Select(x => matrix[rowNumber, x])
                .ToArray();
    }
}