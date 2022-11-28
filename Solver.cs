using System;
using System.Collections.Generic;
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
        Bord swapped = problem;
        // Swap() | @We zien wel.
        //  blok = GetBlok()
        //  try swaps
        // new_sudoku.update(swap_from, swap_to);
        //  Update evaluatiefunctie
        // return swaps

        return swapped;
    }




    //Class bord:
    // Invullen van sudoku met ranodom getallen. | @ardjuhh


}