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
        Bord current = problem;
        while (true)
        {
            Bord neighbour = this.Swap(current);

            if(neighbour.evaluatie <= current.evaluatie) {
                current = neighbour;
            }
        }
    }


    private Bord Swap(Bord problem) {

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


        return swapped;
    }

    private int[] GetBlok()
    {
        //hier moet eigenlijk een array komen van elk blok met de indexen van getallen die geswapt kunnen worden
        int[] blok = new int[] { 0, 1, 2, 9, 10, 11, 18, 19, 20 };
        return blok;
    }


    //Class bord:
    // Invullen van sudoku met ranodom getallen. | @ardjuhh


}