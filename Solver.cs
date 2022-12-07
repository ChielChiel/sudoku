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
        Bord result = this.HillClimb(problem: initial, plateau_length: 50, plateau_height: 1, random_walk_length: 2, max_steps: 1000);
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
        while (!stop_criterea)
        {
            steps += 1;

            current = current.DeepClone();
            neighbour = null;
            // Check if the algorithm is on a plateau
            if(past_states.Count >= plateau_length && new HashSet<int>(past_states.Skip(past_states.Count - plateau_length).Take(plateau_length)).Count <= plateau_height) {
                // In the past `plateau_length` states there are only circulating less than `plateau_height` 
                // different numbers, so it is on a plateau

                Console.WriteLine("----Random Walk----");
                current = this.RandomSwap(current, random_walk_length);
                past_states.Add(current.evaluatie);
                
            }
            else { // Not on a plateau
                neighbour = this.Swap(current);
                 // Compare evaluation values
                if(neighbour.evaluatie <= current.evaluatie) {
                    current = neighbour;
                    past_states.Add(current.evaluatie);
                }
            }

           
            Console.WriteLine("Iteration: " + steps + "; Evaluatie: " + current.evaluatie);


            // Determine if stop criterea is met
            if(current.evaluatie <= 10) {
                stop_criterea = true;
            }
        }

        // After hillclimb is finished, return the state which it ended with.
        return current;
    }


    private Bord Swap(Bord problem) {

        Bord beste_tot_nu_toe = problem.DeepClone();
        // bijhouden beste_tot_nu_toe
        // voordat swap, copy sudoku maken
        // dan swappen
        // bord.UpdateEvaluatie() aanroepen
        // dan chekcen of beter dan beste_tot_nu_toe
        // na alle swaps, beste_tot_nu_toe returnen


        Random rnd = new Random();
        int bloknummer = rnd.Next(0, problem.blokken.Count);
        List<int> blok = problem.blokken[bloknummer];
        List<int[]> result = new List<int[]>();

        // Save the initial problem in a separate variable
        Bord init = problem.DeepClone();
        
        for (int i = 0; i < blok.Count; i++)
        {
            for (int j = i + 1; j < blok.Count; j++)
            {
                // Take a deepclone of the original problem, change that.
                Bord tempbord = init.DeepClone();

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
       
        return beste_tot_nu_toe;
    }

    private Bord RandomSwap(Bord problem, int random_walk_length)
    {
        Random rnd = new Random();
        int bloknummer = rnd.Next(0, problem.blokken.Count);
        int Cijfer1;
        int Cijfer2;
        List<int> blok = problem.blokken[bloknummer];
        for (int i = 0; i < random_walk_length; i++)
        {
            Cijfer1 = rnd.Next(0, problem.blokken[bloknummer].Count);
            Cijfer2 = rnd.Next(0, problem.blokken[bloknummer].Count);
            (problem.sudoku[blok[Cijfer1]], problem.sudoku[blok[Cijfer2]]) = (problem.sudoku[blok[Cijfer2]], problem.sudoku[blok[Cijfer1]]);

            Coordinate a = problem.GetCoordinate(blok[Cijfer1]);
            Coordinate b = problem.GetCoordinate(blok[Cijfer2]);
            problem.UpdateEvaluatie(a, b);
        }
       

        return problem;
    }

}