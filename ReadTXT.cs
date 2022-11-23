public class ReadFromFile
{
    public static void ReadTXT()
    {
        // line to read file
        string[] lines = System.IO.File.ReadAllLines(@"Sudoku_puzzels_5.txt");
        int[,] sudoku = new int[5,81];
        int i = 0;
        int j = 0;

        foreach (string line in lines)
        {
            // first char of the sudoku line is a space
            if (line[0] == ' ')
            {
                // split the spaces in txt file
                string[] numbers = line.Split(' ');
            
                foreach (var number in numbers)
                {
                    // for every input thats not null, add to sudoku array
                    if (number != "")
                    {
                        sudoku[j,i] = int.Parse(number);
                        i++;
                    }
                }
                
                i = 0;
                j++;
            }
        }

        // Testing purposes only
        for (int k = 0; k < 5; k++)
        {        
            for (int s = 0; s < 81; s++)
            {
                System.Console.Write(sudoku[k,s]);
            }
            System.Console.WriteLine("");
        }
    }
}