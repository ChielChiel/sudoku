using System.Collections.Generic;
using System.Globalization;

class Bord : ICloneable
{

    public Node[] sudoku;
    public Dictionary<string, int> evaluatie_waarden;
    public int evaluatie;
    public List<List<int>> blokken;
    public object Clone()
    {
        return this.MemberwiseClone();
    }

    public Bord DeepClone() {
        Node[] sdk = new Node[81];
        for (int i = 0; i < this.sudoku.Length; i++)
        {
            sdk[i] = this.sudoku[i];
        }
        
        Bord clone = new Bord(sudoku_: sdk, evaluatie_waarden_: this.evaluatie_waarden, evaluatie_: evaluatie, blokken_: this.blokken);
        return clone;
    }

    // Used for deepcloning the object
    public Bord(Node[] sudoku_, Dictionary<string, int> evaluatie_waarden_, int evaluatie_, List<List<int>> blokken_) {
        this.sudoku = sudoku_;
        // Deep-clone (dc) the evaluation values dictionary
        Dictionary<string, int> dc_eval = new Dictionary<string, int>();
        foreach(KeyValuePair<string, int> row_eval in evaluatie_waarden_) {
            dc_eval.Add(row_eval.Key, row_eval.Value);
        }
        this.evaluatie_waarden = dc_eval;
        this.evaluatie = evaluatie_;
        this.blokken = blokken_;
    }


    public Bord(int[] sudoku_array) {
        this.sudoku = this.Create_Board(sudoku_array);

        this.evaluatie = this.CalculateEvaluatie();

        // foreach(Node sd in this.sudoku) {
        //     Console.Write("(" + "[" + sd.Row + ";" + sd.Column + "] "  + sd.Getal + " " + sd.Verplaatsbaar + "),");
        // }

        // Test to show that GetCoordinate and GetFlatPosition are working.
        Coordinate start = this.GetCoordinate(80);
        Console.WriteLine(start);
        int end = this.GetFlatPosition(start);
        Console.WriteLine(end);

        this.updateBlokken(alleenSwappebleGetallen: false);
        this.Print();
        // Console.WriteLine("filling sudoku");
        this.fillSudoku();
        Console.WriteLine("filled sudoku");
        this.Print();
    }
    //TODO
    public void updateBlokken(bool alleenSwappebleGetallen = false)
    {
        this.blokken = new List<List<int>>();
        int aantalRijen = (int)Math.Sqrt(this.sudoku.Length);

        int nummerBlok = -1;

        for (int i = 0; i < aantalRijen; i++) {
            this.blokken.Add(new List<int>());
        }
        
        for (int i = 0; i < this.sudoku.Length; i++)
        {

            nummerBlok = (int)((this.sudoku[i].Row / 3) * 3) + (int)((this.sudoku[i].Column) / 3);
            // Console.WriteLine("i: " + i + "; nummerblok: " + nummerBlok + "; first: " + (int)((this.sudoku[i].Row / 3) * 3) + "; second: " + (int)((this.sudoku[i].Column) / 3));
            if (alleenSwappebleGetallen) {
                if (this.sudoku[i].Verplaatsbaar) {
                    this.blokken[nummerBlok].Add(i);
                }
            } else {
                this.blokken[nummerBlok].Add(i);
            }
        }
    }

    public void fillSudoku() // function for filling the sudoku with random numbers
     {
         int numberOfRows = (int)Math.Sqrt(this.sudoku.Length);
         int[] arrayA = new int[9];
         int[] arrayB = new int[9] {1,2,3,4,5,6,7,8,9};

        // Console.WriteLine(numberOfRows);

         for (int j = 0; j < numberOfRows; j++)
         {
             for (int i = 0; i < numberOfRows; i++) {
                // Console.WriteLine("niet kaas: " + blokken[j][i]);
                arrayA[i] = sudoku[blokken[j][i]].Getal; // fill a temporary array with the values of a block
             }

             IEnumerable<int> difference = arrayB.Except(arrayA); // Checks the difference between a given block and a full block

             int nextElement = 0;
             foreach (var g in arrayA) // looping through the array
             {
                 if (g == 0) // if zero then replace it
                 {
                     arrayA[nextElement] = difference.ElementAt(0); // fills the temporary array with the missing numbers
                     sudoku[blokken[j][nextElement]].Getal = arrayA[nextElement]; // fills the flat array with the temporary array
                 }
                 nextElement++;
             }
         }
     }

    public void Print()
    {
        string Rij;
        int aantalRijen = (int) Math.Sqrt(sudoku.Length);
        for (int i = 0; i < aantalRijen; i++)
        {
            Rij = "";
            for (int j = 0; j < aantalRijen; j++)
            {
                if ((j + (i * aantalRijen)) % 3 == 0) {
                    Rij += "|";
                }
                if(sudoku[j+(i*aantalRijen)].Verplaatsbaar == false) {
                    // "\x1b[36m" geeft de kleur blauw aan niet verplaatsbare getallen
                    Rij += "\x1b[36m" + sudoku[j+(i*aantalRijen)].Getal.ToString() + "\x1b[0m ";
                } else {
                    Rij += sudoku[j+(i*aantalRijen)].Getal.ToString() + " ";
                }
                
            }
            if ((i % 3) == 0)
                Console.WriteLine("--------------------");
           
            Console.WriteLine(Rij);
        }
    }

    // Just creates a starting sudoku.
    public Node[] Create_Board(int[] sudoku_array) {
        Node[] bord = new Node[sudoku_array.Length];

        for (int i = 0; i < sudoku_array.Length; i++)
        {
            Node vakje = new Node();
            vakje.Verplaatsbaar = (sudoku_array[i] == 0);
            vakje.Getal = sudoku_array[i];
            
            Coordinate positie = GetCoordinate(i);
            vakje.Row = positie.Y;
            vakje.Column = positie.X;

            bord[i] = vakje;
        }
        return bord;
    } 


    // Bepaal de evaluatie waarde van een sudoku
    // We gebruiken hashsets omdat hier alleen unieke elementen inzitten. Bij lengte 9 zullen dus alle getallen 1-9 hierin voorkomen.
    public int CalculateEvaluatie() {
        Dictionary<string, int> evaluaties =  new Dictionary<string, int>(); // Dictionary bevat de evaluatiewaarden voor elke row en column.
        
        int row = 0; // duidt de huidige row aan
        int column = -1; //duidt de huidige column aan. -1 door de werking van het algoritme
        int node_getal; // De variabele die het getal van het desbetreffende vakje onthoud

        // We gaan in feite horizontaal door de sudoku heen, dus er is maar 1 hashset voor de rows nodig.
        HashSet<int> row_content = new HashSet<int>();

        // Initialise de hashsets voor elk van de colulmns
        HashSet<int>[] cols = new HashSet<int>[9];
        for (int j = 0; j < cols.Length; j++)
        {
            cols[j] = new HashSet<int>();
        }

        // Loop door de hele sudoku heen
        for (int i = 1; i <= this.sudoku.Length; i++)
        {
            // Console.WriteLine("row: " + row + "; " + sudoku.sudoku[i - 1].Getal);
            node_getal = this.sudoku[i - 1].Getal; // Pak het huidige vakje met getal
            row_content.Add(node_getal); // Voeg het getal toe aan de hashset voor deze row.
            column = column + 1; // Verplaats de column pointer 1 naar rechts

            // Als de positie in de array deelbaar is door 9 dan zit het aan het begin van een nieuwe row
            if((i + 0) % 9 == 0) {
                row = row + 1;
                evaluaties.Add("r" + (row - 1), 9 - row_content.Count); // Zet het aantal missende getallen voor de desbetreffende row
                row_content = new HashSet<int>(); // Maak de hashset leeg voor de nieuwe row
            } 

            // Als de vorige positie in de array deelbaar is door 9, dan zit het weer aan de eerste column
            if((i - 1) % 9 == 0) { 
                column = 0;
            }
            cols[column].Add(node_getal); // Update de desbetreffende hashset voor deze column met het getal in dit vakje voor deze column
        }
        
        // Loop door alle hashsets van de columns heen
        for (int j = 0; j < cols.Length; j++)
        {
            evaluaties.Add("c"+ j ,9 - cols[j].Count); // Zet het aantal missende getallen voor de desbetreffende column
        }

        // Loop door alle rows en columns met bijbehorende aantal missende getallen en tel deze bij elkaar op.
        // Console.WriteLine("print waardes van evaluaties");
        int evaluatie_waarde = 0;
        foreach(KeyValuePair<string, int> row_eval in evaluaties) {
            // Console.WriteLine(row_eval.Key + ": " + row_eval.Value);
            evaluatie_waarde += row_eval.Value;
        }
        
        // Console.WriteLine("Evaluatie waarde: " + evaluatie_waarde);
        this.evaluatie_waarden = evaluaties;
        this.evaluatie = evaluatie_waarde;

        return evaluatie_waarde;
    }

    // In plaats van voor een veranderde sudoku alle evaluaties opnieuw te berekenen, kunnen ook alleen de desbetreffende evaluaties voor
    // de verandere rows en columns herberekend worden.
    public int UpdateEvaluatie(Coordinate swap_1, Coordinate swap_2, bool verbose = false) {
        // Initialise de start stap waarden voor beide rows en columns
        // Console.WriteLine(swap_1 + " : " + swap_2);
        
        int start_c_1 = swap_1.X;
        int start_r_1 = swap_1.Y;
        int start_c_2 = swap_2.X;
        int start_r_2 = swap_2.Y;

        // Console.WriteLine("start c1: " + start_c_1 + "; r1: " + start_r_1 + "; c2: " + start_c_2 + "; r2: " + start_r_2);

        int column_1;
        int row_1;
        int column_2;
        int row_2;

        // Hashset die de verschillende getallen van beide rows en columns gaan bevatten
        HashSet<int> col_1_content = new HashSet<int>();
        HashSet<int> row_1_content = new HashSet<int>();
        HashSet<int> col_2_content = new HashSet<int>();
        HashSet<int> row_2_content = new HashSet<int>();

        // rows en columns zijn elk van lengte 9, dus loopen we alleen over de waarden die veranderd zijn.
        for (int i = 0; i < 9; i++)
        {
            // De posities in de sudoku-array die in de veranderde rows en columns zitten
            column_1 = start_c_1 + (i * 9);
            row_1 = (start_r_1 * 9) + i;
            column_2 = start_c_2 + (i * 9);
            row_2 = (start_r_2 * 9) + i;

            // Voeg het getal van de betreffende vakjes toe aan de betreffende row of column hashset
            col_1_content.Add(this.sudoku[column_1].Getal);
            row_1_content.Add(this.sudoku[row_1].Getal);
            col_2_content.Add(this.sudoku[column_2].Getal);
            row_2_content.Add(this.sudoku[row_2].Getal);

            if(verbose) {
                Console.WriteLine("\t c1: " + column_1 + "; r1: " + row_1 + "; c2: " + column_2 + "; r2: " + row_2);
                Console.WriteLine($"lengts: {col_1_content.Count} {row_1_content.Count} {col_2_content.Count} {row_2_content.Count}");
            }
        }

        // Herberekend het aantal missende getallen.
        if (start_c_1 == start_c_2)
        {
            this.evaluatie_waarden["c" + start_c_1] = 9 - col_1_content.Count;
        } else {
            this.evaluatie_waarden["c" + start_c_1] = 9 - col_1_content.Count;
            this.evaluatie_waarden["c" + start_c_2] = 9 - col_2_content.Count;
        }

        if(start_r_1 == start_r_2) {
            this.evaluatie_waarden["r" + start_r_1] = 9 - row_1_content.Count;
        } else {
            this.evaluatie_waarden["r" + start_r_1] = 9 - row_1_content.Count;
            this.evaluatie_waarden["r" + start_r_2] = 9 - row_2_content.Count;
        }


        // this.evaluatie_waarden["c" + start_c_1] = 9 - col_1_content.Count;
        // this.evaluatie_waarden["r" + start_r_1] = 9 - row_1_content.Count;
        // this.evaluatie_waarden["c" + start_c_2] = 9 - col_2_content.Count;
        // this.evaluatie_waarden["r" + start_r_2] = 9 - row_2_content.Count;

        // Herbereken de totale evaluatiewaarde
        int updated_evaluatie_waarde = 0;
        foreach(KeyValuePair<string, int> row_eval in this.evaluatie_waarden) {
            // Console.WriteLine(row_eval.Key + ": " + row_eval.Value);
            updated_evaluatie_waarde += row_eval.Value;
        }

        // Set the evaluatie of this object to the new evaluatie waarde. 
        // Else the comparison in Solver.HillClimb won't work
        this.evaluatie = updated_evaluatie_waarde;
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
