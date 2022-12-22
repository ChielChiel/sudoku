using System.Collections.Generic;
using System.Globalization;

class Board 
{

    public Node[] sudoku;
    public Dictionary<string, int> evaluationValues;
    public int Evaluation;
    public List<List<int>> blocks;

    //DeepClone creates an exact copy of the Board
    public Board DeepClone() {
        Node[] sdk = new Node[81];
        for (int i = 0; i < this.sudoku.Length; i++)
        {
            sdk[i] = this.sudoku[i];
        }
        
        Board clone = new Board(sudoku_: sdk, evaluatie_waarden_: this.evaluationValues, evaluatie_: Evaluation, blokken_: this.blocks);
        return clone;
    }

    // Used for deepcloning the object
    public Board(Node[] sudoku_, Dictionary<string, int> evaluatie_waarden_, int evaluatie_, List<List<int>> blokken_) {
        this.sudoku = sudoku_;
        // Deep-clone (dc) the evaluation values dictionary
        Dictionary<string, int> dc_eval = new Dictionary<string, int>();
        foreach(KeyValuePair<string, int> row_eval in evaluatie_waarden_) {
            dc_eval.Add(row_eval.Key, row_eval.Value);
        }
        this.evaluationValues = dc_eval;
        this.Evaluation = evaluatie_;
        this.blocks = blokken_;
    }


    public Board(int[] sudoku_array) {
        this.sudoku = this.Create_Board(sudoku_array);

        this.Evaluation = this.CalculateEvaluatie();

        // Test to show that GetCoordinate and GetFlatPosition are working.
        Coordinate start = this.GetCoordinate(80);
        Console.WriteLine(start);
        int end = this.GetFlatPosition(start);
        Console.WriteLine(end);

        this.UpdateBlocks(onlySwappableNumbers: false);
        this.fillSudoku();
        Console.WriteLine("filled start sudoku");
        this.Print();
    }

    //returns from the flat array arrays with the indexes, sorted in blocks. FI: the numbers  [0, 1, 2, 9, 10, 11, 18, 19, 20] are in block 0
    public void UpdateBlocks(bool onlySwappableNumbers = false)
    {
        this.blocks = new List<List<int>>();
        int numberOfRows = (int)Math.Sqrt(this.sudoku.Length);

        int blockNumber;

        for (int i = 0; i < numberOfRows; i++) 
        {
            this.blocks.Add(new List<int>());
        }
        
        for (int i = 0; i < this.sudoku.Length; i++)
        {

            blockNumber = (int)((this.sudoku[i].Row / 3) * 3) + (int)((this.sudoku[i].Column) / 3);
          
            if (onlySwappableNumbers) {
                if (this.sudoku[i].Swappable) {
                    this.blocks[blockNumber].Add(i);
                }
            } else {
                this.blocks[blockNumber].Add(i);
            }
        }
    }

    public void fillSudoku() // function for filling the sudoku with random numbers
     {
         int numberOfRows = (int)Math.Sqrt(this.sudoku.Length);
         int[] arrayA = new int[9];
         int[] arrayB = new int[9] {1,2,3,4,5,6,7,8,9};


         for (int j = 0; j < numberOfRows; j++)
         {
             for (int i = 0; i < numberOfRows; i++) {
                arrayA[i] = sudoku[blocks[j][i]].Number; // fill a temporary array with the values of a block
             }

             IEnumerable<int> difference = arrayB.Except(arrayA); // Checks the difference between a given block and a full block

             int nextElement = 0;
             foreach (var g in arrayA) // looping through the array
             {
                 if (g == 0) // if zero then replace it
                 {
                     arrayA[nextElement] = difference.ElementAt(0); // fills the temporary array with the missing numbers
                     sudoku[blocks[j][nextElement]].Number = arrayA[nextElement]; // fills the flat array with the temporary array
                 }
                 nextElement++;
             }
         }
     }

    public void Print()
    {
        string row;
        int numberOfRows = (int) Math.Sqrt(sudoku.Length);
        for (int i = 0; i < numberOfRows; i++)
        {
            row = "";
            for (int j = 0; j < numberOfRows; j++)
            {
                if ((j + (i * numberOfRows)) % 3 == 0) {
                    row += "|";
                }
                if(sudoku[j+(i*numberOfRows)].Swappable == false) {
                    row += "\x1b[36m" + sudoku[j+(i*numberOfRows)].Number.ToString() + "\x1b[0m ";
                } else {
                    row += sudoku[j+(i*numberOfRows)].Number.ToString() + " ";
                }
                
            }
            if ((i % 3) == 0)
                Console.WriteLine("--------------------");
           
            Console.WriteLine(row);
        }
    }

    // Just creates a starting sudoku.
    public Node[] Create_Board(int[] sudoku_array) {
        Node[] board = new Node[sudoku_array.Length];

        for (int i = 0; i < sudoku_array.Length; i++)
        {
            Node number = new Node();
            number.Swappable = (sudoku_array[i] == 0);
            number.Number = sudoku_array[i];
            
            Coordinate positie = GetCoordinate(i);
            number.Row = positie.Y;
            number.Column = positie.X;

            board[i] = number;
        }
        return board;
    } 


    //determines the evaluation value
    public int CalculateEvaluatie() {
        Dictionary<string, int> evaluations =  new Dictionary<string, int>(); // Dictionary bevat de evaluatiewaarden voor elke row en column.
        
        int row = 0; // current row
        int column = -1; //current column
        int nodeNumber; // number of the current node 

        // we loop through the sudoku horizontally
        HashSet<int> rowContent = new HashSet<int>();

        // Initialise hashset for every column
        HashSet<int>[] cols = new HashSet<int>[9];
        for (int j = 0; j < cols.Length; j++)
        {
            cols[j] = new HashSet<int>();
        }

        // Loop door de hele sudoku heen
        for (int i = 1; i <= this.sudoku.Length; i++)
        {
            nodeNumber = this.sudoku[i - 1].Number; // Pak het huidige vakje met getal
            rowContent.Add(nodeNumber); // Voeg het getal toe aan de hashset voor deze row.
            column = column + 1; // Verplaats de column pointer 1 naar rechts

            // Als de positie in de array deelbaar is door 9 dan zit het aan het begin van een nieuwe row
            if((i + 0) % 9 == 0) {
                row = row + 1;
                evaluations.Add("r" + (row - 1), 9 - rowContent.Count); // Zet het aantal missende getallen voor de desbetreffende row
                rowContent = new HashSet<int>(); // Maak de hashset leeg voor de nieuwe row
            } 

            // Als de vorige positie in de array deelbaar is door 9, dan zit het weer aan de eerste column
            if((i - 1) % 9 == 0) { 
                column = 0;
            }
            cols[column].Add(nodeNumber); 
        }
        
        // loops through all hashsets
        for (int j = 0; j < cols.Length; j++)
        {
            evaluations.Add("c"+ j ,9 - cols[j].Count); // sets the amount of missing values
        }

        //loops through the rows and columns with missing correct values and counts these

        int evaluationValue = 0;
        foreach(KeyValuePair<string, int> row_eval in evaluations) {
            evaluationValue += row_eval.Value;
        }
        
        this.evaluationValues = evaluations;
        this.Evaluation = evaluationValue;

        return evaluationValue;
    }

    // UpdateEvaluation differs from CalculateEvalution, because it only calculates the updates evaluation values for the changed rows and columns
    public int UpdateEvaluation(Coordinate swap_1, Coordinate swap_2, bool verbose = false) {
        // Initialise the start step values for rows and columns
        
        int startColumn1 = swap_1.X;
        int startRow1 = swap_1.Y;
        int startColumn2 = swap_2.X;
        int startRow2 = swap_2.Y;

 
        int column1;
        int row1;
        int column2;
        int row2;

        // Hashset for different numbers for rows and columns
        HashSet<int> column1Content = new HashSet<int>();
        HashSet<int> row1Content = new HashSet<int>();
        HashSet<int> col2Content = new HashSet<int>();
        HashSet<int> row2Content = new HashSet<int>();

        //rows and columns are of lenght 9, so we loop on those values
        for (int i = 0; i < 9; i++)
        {
            // Tge positions of the sudoku array in the changed row/column
            column1 = startColumn1 + (i * 9);
            row1 = (startRow1 * 9) + i;
            column2 = startColumn2 + (i * 9);
            row2 = (startRow2 * 9) + i;

            //Add the number to the relevant row/column
          
            column1Content.Add(this.sudoku[column1].Number);
            row1Content.Add(this.sudoku[row1].Number);
            col2Content.Add(this.sudoku[column2].Number);
            row2Content.Add(this.sudoku[row2].Number);

            if(verbose) {
                Console.WriteLine("\t c1: " + column1 + "; r1: " + row1 + "; c2: " + column2 + "; r2: " + row2);
                Console.WriteLine($"lengts: {column1Content.Count} {row1Content.Count} {col2Content.Count} {row2Content.Count}");
            }
        }

        // recalculates the values per column/row
        if (startColumn1 == startColumn2)
        {
            this.evaluationValues["c" + startColumn1] = 9 - column1Content.Count;
        } else {
            this.evaluationValues["c" + startColumn1] = 9 - column1Content.Count;
            this.evaluationValues["c" + startColumn2] = 9 - col2Content.Count;
        }

        if(startRow1 == startRow2) {
            this.evaluationValues["r" + startRow1] = 9 - row1Content.Count;
        } else {
            this.evaluationValues["r" + startRow1] = 9 - row1Content.Count;
            this.evaluationValues["r" + startRow2] = 9 - row2Content.Count;
        }


        // recalculates the evaluation_valuation
        int updated_evaluatie_waarde = 0;
        foreach(KeyValuePair<string, int> row_eval in this.evaluationValues) {
           
            updated_evaluatie_waarde += row_eval.Value;
        }

        // Set the evaluatie of this object to the new evaluatie waarde. 
        // Else the comparison in Solver.HillClimb won't work
        this.Evaluation = updated_evaluatie_waarde;
        return updated_evaluatie_waarde;
    }





    // Maps the flattened position of sudoku array to a coordinate in the sudoku.
    // Coordinates from (0,0) top-left to (8,8) bottom right
    public Coordinate GetCoordinate(int flat_position) {
        int x = 0;
        int y = 0;
        y = (int)Math.Floor(flat_position / 9.0);
        x = flat_position - (y * 9);
        return new Coordinate(x, y);
    }

    public int GetFlatPosition(Coordinate position) {
        return 9 * position.Y + position.X;
    }

}

// Helper class to project a coordinate in our sudoku
class Coordinate {

    // The X coordinate, also the column
    public int X;

    // The Y coordinate, also the row
    public int Y;

    public Coordinate(int x, int y) {
        this.X = x;
        this.Y = y;
    }

    public override string ToString()
    {
        return "X = " + this.X + ", Y = " + this.Y;
    }

}
