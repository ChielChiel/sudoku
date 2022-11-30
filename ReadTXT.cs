public class ReadFromFile
{

    // Inlezen
    // Createbord(ingelezen)
    // sudoku_temp.getblocks 
    //  (9 blokken)
    //      array (0,1,2,8,)
    //      sudoku_array[0] = 3
    // 3x3 units gaat vullen


    public static int[,] ReadTXT(System.String link)
    {
        // line to read file
        string[] lines = System.IO.File.ReadAllLines(link);
        // can be made global. But size needs to defined.
        int numberOfGraphs = 5;
        int numberOfValues = 81;
        int[,] sudoku = new int[numberOfGraphs,numberOfValues];
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

        return sudoku;
    }
}