using System;
using System.Collections.Generic;
using System.Diagnostics;


class Solver {
    


    public Solver(Bord initial) {
        initial.CalculateEvaluatie();

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
        // /int[] past_ten_toestand = {evaluatie_waarden_van_vroeger}
        
        Bord current = problem;
        Bord neighbour;
        Bord rnd_walk_temp;
        bool alleenSwappebleGetallen = true;
        problem.updateBlokken(alleenSwappebleGetallen);
        while (!stop_criterea)
        {
            Bord neighbour = this.Swap(current);

            if(neighbour.evaluatie <= current.evaluatie) {
                current = neighbour;
            }
        }
    }


    private Bord Swap(Bord problem, bool random_walk = false) {

        Bord beste_tot_nu_toe = (Bord)problem.Clone();
        // bijhouden beste_tot_nu_toe
        // voordat swap, copy sudoku maken
        // dan swappen
        // bord.UpdateEvaluatie() aanroepen
        // dan chekcen of beter dan beste_tot_nu_toe
        // na alle swaps, beste_tot_nu_toe returnen



        Random rnd = new Random();
        int bloknummer = rnd.Next(0, problem.blokken.Count- 1);
        List<int> blok = problem.blokken[bloknummer];
        List<int[]> result = new List<int[]>();

        foreach (int i in blok)
            Console.WriteLine(i);
        for (int i = 0; i < blok.Count - 1; i++)
        {
            for (int j = i + 1; j < blok.Count; j++)
            {
                (problem.sudoku[blok[i]], problem.sudoku[blok[j]]) = (problem.sudoku[blok[j]], problem.sudoku[blok[i]]);
                Coordinate a = problem.GetCoordinate(blok[j]);
                Coordinate b = problem.GetCoordinate(blok[i]);
                //Wat moet ik hier aan UpdateEvaluatie() meegeven? De coordinaten van de geswapte dingen?
                  if (problem.UpdateEvaluatie(a,b) < beste_tot_nu_toe.evaluatie)
                       beste_tot_nu_toe = problem;
            }
        }
       
        // Swap() | @We zien wel.


        //  try swaps
        // new_sudoku.update(swap_from, swap_to);
        //  Update evaluatiefunctie
        // return swaps

        //(swapped.sudoku[blok[first]], swapped.sudoku[blok[second]]) = (swapped.sudoku[blok[second]], swapped.sudoku[blok[first]]);

        // swapped.UpdateEvaluatie()
        return beste_tot_nu_toe;
    }

    private int[] GetBlok(bool returnAll = false)
    {
        //hier moet eigenlijk een array komen van elk blok met de indexen van getallen die geswapt kunnen worden
        int[] blok = new int[] { 0, 1, 2, 9, 10, 11, 18, 19, 20 };
        
        
        // Node node.Verplaatsbaar == true
        return blok;
    }


    //Class bord:
    // Invullen van sudoku met ranodom getallen. | @ardjuhh


}