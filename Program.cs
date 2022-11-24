// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// 5 and 81 can be defined globally
int[,] sudoku = new int[5,81];
sudoku = ReadFromFile.ReadTXT(@"Sudoku_puzzels_5.txt");

// Testing purposes only
for (int i = 0; i < 5; i++)
{        
    for (int j = 0; j < 81; j++)
    {
        System.Console.Write(sudoku[i,j]);
    }
    System.Console.WriteLine("");
}
