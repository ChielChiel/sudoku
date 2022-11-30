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

        int[] arrayA = new int[9];
        int[] arrayB = new int[9] {1,2,3,4,5,6,7,8,9};

        // per graph
        for (int k = 0; k < numberOfGraphs; k++)
        {        
            const int maxStepSize = 21;
            int begin = 0;
            int end = maxStepSize;
            int position = 0;
            int counter = 0;
            

            while(true)
            {
                // 3x3 block found and reset to get a new one
                if (begin + counter == end) 
                {
                    // compare two arrays on difference
                    IEnumerable<int> difference = arrayB.Except(arrayA);

                    int nextElement = 0;
                    foreach (var g in arrayA)
                    {
                        if (g == 0)
                        {
                            arrayA[nextElement] = difference.ElementAt(0);
                        }
                        nextElement++;
                        
                    }

                    // test purposes //////////
                    // System.Console.Write(" ");
                    // foreach (var h in arrayA)
                    // {
                    //     Console.Write(h);
                    // }
                    // System.Console.WriteLine(" ");
                    ////////////////////////////
                    
                    if (begin + counter == (numberOfValues/3)) // upper 3 blocks cleared
                    {
                        begin = (numberOfValues/3);
                        end = begin + maxStepSize;
                    }
                    else if(begin + counter == ((numberOfValues/3)*2)) // middle 3 blocks cleared
                    {
                        begin = ((numberOfValues/3)*2);
                        end = begin + maxStepSize;
                    }
                    else if(begin + counter == numberOfValues) // lower 3 blocks cleared and break 
                    {
                        break;
                    }
                    else // blocks aren't done yet
                    {
                        end = end + 3; 
                        begin = end - maxStepSize;  
                    }
                    counter = 0; 
                    position = 0;
                }
                // if 3 numbers in a block are done then skip to the next line to continue
                if (counter == 3) {begin = begin+9; counter = 0;}
                
                // place flat array in a block
                arrayA[position] = sudoku[k,begin+counter];
                // System.Console.Write(arrayA[position]);
                
                counter++;
                position++;
            }
            //System.Console.WriteLine("");
        }

        return sudoku;
    }
}