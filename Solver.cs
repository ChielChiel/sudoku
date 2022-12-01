using System;
using System.Diagnostics;


class Solver {
    


    public Solver(Bord initial) {
        initial.CalculateEvaluatie();

        // Time how long it takes to solve the given sudoku
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        // We will have to vary these parameters to see what works best to get the best result overall.
        Bord result = this.HillClimb(problem: initial, plateau_length: 10, plateau_height: 3, random_walk_length: 5, max_steps: 100);
        stopWatch.Stop();

        TimeSpan diff = stopWatch.Elapsed;
        Console.WriteLine("This problem took: " + diff.TotalSeconds);

    }


    public Bord HillClimb(Bord problem, int plateau_length = 10, int plateau_height = 3, int random_walk_length = 5, int max_steps = 1000) {
        List<int> past_states = new List<int>();
        int steps = 0;
        bool stop_criterea = false;

        Bord current = problem;
        Bord neighbour;
        Bord rnd_walk_temp;

        while (!stop_criterea)
        {
            steps += 1;

            // Check if the algorithm is on a plateau
            if(past_states.Count >= plateau_length && new HashSet<int>(past_states.Skip(past_states.Count - plateau_length).Take(plateau_length)).Count < plateau_height) {
                // In the past `plateau_length` states there are only circulating less than `plateau_height` 
                // different numbers, so it is on a plateau
                rnd_walk_temp = current;
                for (int s = 0; s < random_walk_length; s++) // Take s random steps
                {
                    this.Swap(problem: rnd_walk_temp, random_walk: true);
                }
                neighbour = rnd_walk_temp;
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