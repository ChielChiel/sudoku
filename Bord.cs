using System.Collections.Generic;
using System.Globalization;

class Bord {

    public Node[] sudoku;
    public Dictionary<string, int> evaluatie_waarden;
    public int evaluatie;
    public List<List<int>> blokken;

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

        updateBlokken();
        fillSudoku();
        this.Print();




        

       // int[] test_sdk = new int[81];
       // for (int i = 0; i < 81; i++)
       // {
       //     test_sdk[i] = i + 1;
       // }
       // Node[] test_sudoku = this.Create_Board(test_sdk);



        // foreach(Node sd in test_sudoku) {
        //     Console.Write("(" + "[" + sd.Row + ";" + sd.Column + "] "  + sd.Getal + " " + sd.Verplaatsbaar + "),");
        // }



    }
    //TODO
    public void updateBlokken(bool alleenSwappebleGetallen = false)
    {
        blokken = new List<List<int>>();
        int aantalRijen = (int)Math.Sqrt(sudoku.Length);

        int nummerBlok = -1;
        for (int i = 0; i < aantalRijen; i++)
            blokken.Add(new List<int>());
        
        for (int i = 0; i < sudoku.Length; i++)
        {
            nummerBlok = (sudoku[i].Row / 3) * 3 + (sudoku[i].Column-1) / 3;
            if (alleenSwappebleGetallen)
                if (sudoku[i].Verplaatsbaar)
                    blokken[nummerBlok].Add(i);
                else
                    blokken[nummerBlok].Add(i);
        }
    }

    public void fillSudoku() // function for filling the sudoku with random numbers
    {
        int numberOfRows = (int)Math.Sqrt(sudoku.Length);
        int[] arrayA = new int[9];
        int[] arrayB = new int[9] {1,2,3,4,5,6,7,8,9};

        for (int j = 0; j < numberOfRows; j++)
        {
            for (int i = 0; i < numberOfRows; i++)
                arrayA[i] = sudoku[blokken[j][i]].Getal; // fill a temporary array with the values of a block

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
                if ((j + (i * aantalRijen)) % 3 == 0)
                    Rij += "|";
                Rij += sudoku[j+(i*aantalRijen)].Getal.ToString() + " ";
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
            
            Coordinate positie = GetCoordinate(i + 1);
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
                evaluaties.Add("r" + (row - 1), 10 - row_content.Count); // Zet het aantal missende getallen voor de desbetreffende row
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
            evaluaties.Add("c"+ j ,10 - cols[j].Count); // Zet het aantal missende getallen voor de desbetreffende column
        }

        // Loop door alle rows en columns met bijbehorende aantal missende getallen en tel deze bij elkaar op.
        // Console.WriteLine("print waardes van evaluaties");
        int evaluatie_waarde = 0;
        foreach(KeyValuePair<string, int> row_eval in evaluaties) {
            // Console.WriteLine(row_eval.Key + ": " + row_eval.Value);
            evaluatie_waarde += row_eval.Value;
        }
        
        Console.WriteLine("Evaluatie waarde: " + evaluatie_waarde);
        this.evaluatie_waarden = evaluaties;


        return evaluatie_waarde;
    }

    // In plaats van voor een veranderde sudoku alle evaluaties opnieuw te berekenen, kunnen ook alleen de desbetreffende evaluaties voor
    // de verandere rows en columns herberekend worden.
    public int UpdateEvaluatie(Coordinate swap_1, Coordinate swap_2) {
        // Initialise de start stap waarden voor beide rows en columns
        int start_c_1 = swap_1.X;
        int start_r_1 = swap_1.Y;
        int start_c_2 = swap_2.X;
        int start_r_2 = swap_2.Y;

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
        }

        // Herberekend het aantal missende getallen.
        this.evaluatie_waarden["c" + start_c_1] = 10 - col_1_content.Count;
        this.evaluatie_waarden["r" + start_r_1] = 10 - row_1_content.Count;
        this.evaluatie_waarden["c" + start_c_2] = 10 - col_2_content.Count;
        this.evaluatie_waarden["r" + start_r_2] = 10 - row_2_content.Count;

        // Herbereken de totale evaluatiewaarde
        int updated_evaluatie_waarde = 0;
        foreach(KeyValuePair<string, int> row_eval in this.evaluatie_waarden) {
            // Console.WriteLine(row_eval.Key + ": " + row_eval.Value);
            updated_evaluatie_waarde += row_eval.Value;
        }

        return updated_evaluatie_waarde;
    }





    // Maps the flattened position of sudoku array to a coordinate in the sudoku.
    // Coordinates from (0,0) top-left to (8,8) bottom right
    public Coordinate GetCoordinate(int flat_position) {
        int x = 0;
        int y = 0;
        y = (flat_position - 1) / 9;
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
