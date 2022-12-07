using System;
using System.Collections.Generic;
using System.Diagnostics;


class Solver {

    public Solver(Bord initial) {
        initial.CalculateEvaluatie();

        // Time how long it takes to solve the given sudoku
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        // We will have to vary these parameters to see what works best to get the best result overall.
        Bord result = this.HillClimb(problem: initial, plateau_length: 10, plateau_height: 3, random_walk_length: 5, max_steps: 1000);
        stopWatch.Stop();

        TimeSpan diff = stopWatch.Elapsed;
        Console.WriteLine("This problem took: " + diff.TotalSeconds);
        Console.WriteLine("The final state, with evaluation value " + result.evaluatie + " being: ");
        result.Print();
    }


    public Bord HillClimb(Bord problem, int plateau_length = 10, int plateau_height = 3, int random_walk_length = 5, int max_steps = 1000) {
        List<int> past_states = new List<int>();
        int steps = 0;
        bool stop_criterea = false;

        bool alleenSwappebleGetallen = true;
        problem.updateBlokken(alleenSwappebleGetallen);
        Bord current = problem;
        Bord neighbour = problem;
        Bord rnd_walk_temp;
        while (!stop_criterea)
        {
            steps += 1;

            // Check if the algorithm is on a plateau
            if(past_states.Count >= plateau_length && new HashSet<int>(past_states.Skip(past_states.Count - plateau_length).Take(plateau_length)).Count < plateau_height) {
                // In the past `plateau_length` states there are only circulating less than `plateau_height` 
                // different numbers, so it is on a plateau

                Console.WriteLine("----Random Walk----");
                current = this.RandomSwap(current, random_walk_length);
                
            }
            else { // Not on a plateau
                neighbour = this.Swap(current);
            }

            // Compare evaluation values
            if(neighbour.evaluatie <= current.evaluatie) {

            current = neighbour;
            past_states.Add(current.evaluatie);
            }
            Console.WriteLine("Iteration: " + steps + "; Evaluatie: " + current.evaluatie);


            // Determine if stop criterea is met
            if(steps == max_steps || current.evaluatie == 0) {
                stop_criterea = true;
            }
        }

        // After hillclimb is finished, return the state which it ended with.
        return current;
    }


    private Bord Swap(Bord problem, bool random_walk = false) {

        Bord beste_tot_nu_toe = (Bord)problem.Clone();
        // bijhouden beste_tot_nu_toe
        // voordat swap, copy sudoku maken
        // dan swappen
        // bord.UpdateEvaluatie() aanroepen
        // dan chekcen of beter dan beste_tot_nu_toe
        // na alle swaps, beste_tot_nu_toe returnen
        if(random_walk) {
            // Just swap, dont look at evaluation values.

            // return random_swap;
        }


        Random rnd = new Random();
        int bloknummer = rnd.Next(0, problem.blokken.Count- 1);
        List<int> blok = problem.blokken[bloknummer];
        List<int[]> result = new List<int[]>();


        // BETER OM HIERMEE ER DOORHEEN TE LOPEN @Kars
        // https://stackoverflow.com/questions/18863047/c-sharp-iterate-over-all-possible-pairwise-combinations-of-array-contents

        for (int i = 0; i < blok.Count; i++)
        {
            for (int j = i + 1; j < blok.Count; j++)
            {
                // Take a copy of the original problem, change that.
                Bord tempbord = (Bord)problem.Clone();
                (tempbord.sudoku[blok[i]], tempbord.sudoku[blok[j]]) = (tempbord.sudoku[blok[j]], tempbord.sudoku[blok[i]]);
                // Console.WriteLine("blok i: " + blok[i] + "; blok j: " + blok[j]);
                
                Coordinate a = tempbord.GetCoordinate(blok[i]);
                Coordinate b = tempbord.GetCoordinate(blok[j]);
                //Wat moet ik hier aan UpdateEvaluatie() meegeven? De coordinaten van de geswapte dingen?
                if (tempbord.UpdateEvaluatie(a,b) < beste_tot_nu_toe.evaluatie) {
                    beste_tot_nu_toe = tempbord;
                }
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

    private Bord RandomSwap(Bord problem, int random_walk_length)
    {
        Random rnd = new Random();
        int bloknummer = rnd.Next(0, problem.blokken.Count - 1);
        int Cijfer1;
        int Cijfer2;
        List<int> blok = problem.blokken[bloknummer];
        for (int i = 0; i < random_walk_length; i++)
        {
            Cijfer1 = rnd.Next(0, problem.blokken[bloknummer].Count - 1);
            Cijfer2 = rnd.Next(0, problem.blokken[bloknummer].Count - 1);
            (problem.sudoku[blok[Cijfer1]], problem.sudoku[blok[Cijfer2]]) = (problem.sudoku[blok[Cijfer2]], problem.sudoku[blok[Cijfer1]]);

            Coordinate a = problem.GetCoordinate(blok[Cijfer1]);
            Coordinate b = problem.GetCoordinate(blok[Cijfer2]);
            problem.UpdateEvaluatie(a, b);
        }
       

        return problem;
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