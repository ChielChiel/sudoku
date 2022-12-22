public class ReadFromFile
{

    public static int[,] ReadTXT(System.String link)
    {
        // line to read file
        string[] lines = System.IO.File.ReadAllLines(link);
        int numberOfGraphs = 0;
        const int numberOfValues = 81;
        int i = 0;
        int j = 0;

        // count number of graphs present in txt doc
        foreach (string line in lines)
        {
            if (line[0] == ' ')
            {
                numberOfGraphs++;
            }
        }

        int[,] sudoku = new int[numberOfGraphs,numberOfValues];

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

        return sudoku;
    }
}