using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        while (true)
        {
            Bord neighbour = this.Swap(current);

            if(neighbour.evaluatie <= current.evaluatie) {
                current = neighbour;
            }
        }
    }


    private Bord Swap(Bord problem, bool random_walk = false) {
        // bijhouden beste_tot_nu_toe
        // voordat swap, copy sudoku maken
        // dan swappen
        // bord.UpdateEvaluatie() aanroepen
        // dan chekcen of beter dan beste_tot_nu_toe
        // na alle swaps, beste_tot_nu_toe returnen



        Random rnd = new Random();
        Bord swapped = problem;
        // Swap() | @We zien wel.
        int []blok = GetBlok();
        //  try swaps
        // new_sudoku.update(swap_from, swap_to);
        //  Update evaluatiefunctie
        // return swaps
        int first = rnd.Next(0, blok.Length-1);
        int second = rnd.Next(0, blok.Length-1);
        (swapped.sudoku[blok[first]], swapped.sudoku[blok[second]]) = (swapped.sudoku[blok[second]], swapped.sudoku[blok[first]]);

        // swapped.UpdateEvaluatie()
        return swapped;
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