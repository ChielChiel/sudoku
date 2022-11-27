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


    public int evalutie(Bord sudoku) {
        Dictionary<string, int> evaluaties =  new Dictionary<string, int>();
        
        int row = 0;
        int column = -1;
        int node_getal;
        HashSet<int> row_content = new HashSet<int>();

        HashSet<int>[] cols = new HashSet<int>[9];
        for (int j = 0; j < cols.Length; j++)
        {
            cols[j] = new HashSet<int>();
        }


        for (int i = 1; i <= sudoku.sudoku.Length; i++)
        {
            // Console.WriteLine("row: " + row + "; " + sudoku.sudoku[i - 1].Getal);
            node_getal = sudoku.sudoku[i - 1].Getal;
            row_content.Add(node_getal);
            column = column + 1;

            if((i + 0) % 9 == 0) {
                row = row + 1;
                evaluaties.Add("r" + (row - 1), 10 - row_content.Count);
                row_content = new HashSet<int>();
            } 

            if((i - 1) % 9 == 0) {
                column = 0;
            }
            cols[column].Add(node_getal);
            // Console.WriteLine("column " + column + ": " + node_getal);
            
        }
        
        for (int j = 0; j < cols.Length; j++)
        {
            evaluaties.Add("c"+ j ,10 - cols[j].Count);
        }


        Console.WriteLine("print waardes van evaluaties");
        int evaluatie_waarde = 0;
        foreach(KeyValuePair<string, int> row_eval in evaluaties) {
            Console.WriteLine(row_eval.Key + ": " + row_eval.Value);
            evaluatie_waarde += row_eval.Value;
        }
        
        Console.WriteLine("Evaluatie waarde: " + evaluatie_waarde);
        
        return evaluatie_waarde;
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