using System;
using System.Collections.Generic;
class Solver {
    


    public Solver(Bord initial) {
        this.evalutie(initial);


    }
    // oplositeratie()
    // Kies random blok
    // probeer alle swaps
    //      res = Swap(sudoku.copy())
    //      Bereken de evaluatiewaarde(res)
    //      Onthou de evaluatiewaarde
    //      If evalutie = same_for_past(10):
    //          random_walk()


    // random_walk():
    //  random blok
    //  random swap
    // 

    // update array(res)

    // Methode moet nog uitgewerkt worden. Maar de basis staat.
    public Bord HillClimb(Bord problem) {
        Bord current = problem;
        while (true)
        {
            Bord neighbour = this.Swap(current);
            if(this.evalutie(neighbour) <= this.evalutie(current)) {
                current = neighbour;
            }
        }
    }


    // Bepaal de evaluatie waarde van een sudoku
    // We gebruiken hashsets omdat hier alleen unieke elementen inzitten. Bij lengte 9 zullen dus alle getallen 1-9 hierin voorkomen.
    public int evalutie(Bord sudoku) {
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
        for (int i = 1; i <= sudoku.sudoku.Length; i++)
        {
            // Console.WriteLine("row: " + row + "; " + sudoku.sudoku[i - 1].Getal);
            node_getal = sudoku.sudoku[i - 1].Getal; // Pak het huidige vakje met getal
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
        Console.WriteLine("print waardes van evaluaties");
        int evaluatie_waarde = 0;
        foreach(KeyValuePair<string, int> row_eval in evaluaties) {
            Console.WriteLine(row_eval.Key + ": " + row_eval.Value);
            evaluatie_waarde += row_eval.Value;
        }
        
        Console.WriteLine("Evaluatie waarde: " + evaluatie_waarde);
        
        return evaluatie_waarde;
    }

    // In plaats van voor een veranderde sudoku alle evaluaties opnieuw te berekenen, kunnen ook alleen de desbetreffende evaluaties voor
    // de verandere rows en columns herberekend worden.
    public int evalutie_update(Bord sudoku, Dictionary<string, int> current_evaluatie, Coordinate swap_1, Coordinate swap_2) {
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
            col_1_content.Add(sudoku.sudoku[column_1].Getal);
            row_1_content.Add(sudoku.sudoku[row_1].Getal);
            col_2_content.Add(sudoku.sudoku[column_2].Getal);
            row_2_content.Add(sudoku.sudoku[row_2].Getal);
        }

        // Herberekend het aantal missende getallen.
        current_evaluatie["c" + start_c_1] = 10 - col_1_content.Count;
        current_evaluatie["r" + start_r_1] = 10 - row_1_content.Count;
        current_evaluatie["c" + start_c_2] = 10 - col_2_content.Count;
        current_evaluatie["r" + start_r_2] = 10 - row_2_content.Count;

        // Herbereken de totale evaluatiewaarde
        int updated_evaluatie_waarde = 0;
        foreach(KeyValuePair<string, int> row_eval in current_evaluatie) {
            // Console.WriteLine(row_eval.Key + ": " + row_eval.Value);
            updated_evaluatie_waarde += row_eval.Value;
        }

        return updated_evaluatie_waarde;
    }

    private Bord Swap(Bord problem) {
        Bord swapped = problem;
        // Swap() | We zien wel.
        //  blok = GetBlok()
        //  try swaps
        // return swaps

        return swapped;
    }




    //Class bord:
    // Invullen van sudoku met ranodom getallen. | @ardjuhh


}