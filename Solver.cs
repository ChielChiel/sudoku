using System.Diagnostics;


class Solver {

    public Solver(Board initial) {
        initial.CalculateEvaluatie();

        // Time how long it takes to solve the given sudoku
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        // We will have to vary these parameters to see what works best to get the best result overall.
        Board result = this.HillClimb(problem: initial, plateau_length: 10, plateau_height: 1, random_walk_length: 2, max_steps: 100000);
        stopWatch.Stop();

        TimeSpan diff = stopWatch.Elapsed;

        Console.WriteLine("This problem took: " + diff.TotalSeconds + " seconds to complete");
        Console.WriteLine("The final state, with evaluation value " + result.Evaluation + " being: ");
        result.Print();
    }


    public Board HillClimb(Board problem, int plateau_length = 10, int plateau_height = 1, int random_walk_length = 2, int max_steps = 100000) {
        List<int> past_states = new List<int>();
        int steps = 0;
        bool stop_criterea = false;

        bool onlySwappableNumbers = true;
        problem.UpdateBlocks(onlySwappableNumbers);
        Board current = problem;
        Board neighbour = problem;
        while (!stop_criterea)
        {
            steps += 1;
            current = current.DeepClone();
            neighbour = null;
            // Check if the algorithm is on a plateau
            if(past_states.Count >= plateau_length && new HashSet<int>(past_states.Skip(past_states.Count - plateau_length).Take(plateau_length)).Count <= plateau_height) 
            {
                // In the past `plateau_length` states there are only circulating less than `plateau_height` 
                // different numbers, so it is on a plateau
                current = this.RandomSwap(current.DeepClone(), random_walk_length);
                past_states.Add(current.Evaluation);
                
            }
            else 
            { // Not on a plateau
                neighbour = this.Swap(current);
                 // Compare evaluation values
                if(neighbour.Evaluation <= current.Evaluation) {
                    current = neighbour;
                    past_states.Add(current.Evaluation);
                }
            }
            
            // Determine if stop criterea is met
            if(current.Evaluation == 0 || steps == max_steps) {
                stop_criterea = true;
                return current;
            }

            
        }

        // After hillclimb is finished, return the state which it ended with.
        return current;
    }


    private Board Swap(Board problem) {
        Board bestUntilNow = problem.DeepClone();

        Random rnd = new Random();
        int bloknummer = rnd.Next(0, problem.blocks.Count);
        List<int> block = problem.blocks[bloknummer];
        List<int[]> result = new List<int[]>();

        // Save the initial problem in a separate variable
        Board init = problem.DeepClone();
        
        for (int i = 0; i < block.Count; i++)
        {
            for (int j = i + 1; j < block.Count; j++)
            {
                // Take a deepclone of the original problem, change that.
                Board tempbord = init.DeepClone();

                (tempbord.sudoku[block[i]], tempbord.sudoku[block[j]]) = (tempbord.sudoku[block[j]], tempbord.sudoku[block[i]]);
                
                Coordinate a = tempbord.GetCoordinate(block[i]);
                Coordinate b = tempbord.GetCoordinate(block[j]);

                //Update the board if it has a lower evaluation
                if (tempbord.UpdateEvaluation(a,b) < bestUntilNow.Evaluation) {
                    bestUntilNow = tempbord;
                }
               
            }
        }
       
        return bestUntilNow;
    }
    
    //Selects a random block and swaps (random_walk_lenght) numbers
    private Board RandomSwap(Board problem, int random_walk_length)
    {
        Random rnd = new Random();
        int blockNumber = rnd.Next(0, problem.blocks.Count);
        int IndexNumber1;
        int IndexNumber2;
        List<int> block = problem.blocks[blockNumber];
        for (int i = 0; i < random_walk_length; i++)
        {
            IndexNumber1 = rnd.Next(0, problem.blocks[blockNumber].Count);
            IndexNumber2 = rnd.Next(0, problem.blocks[blockNumber].Count);
            (problem.sudoku[block[IndexNumber1]], problem.sudoku[block[IndexNumber2]]) = (problem.sudoku[block[IndexNumber2]], problem.sudoku[block[IndexNumber1]]);

            Coordinate a = problem.GetCoordinate(block[IndexNumber1]);
            Coordinate b = problem.GetCoordinate(block[IndexNumber2]);
            problem.UpdateEvaluation(a, b);

        }       

        return problem;
    }

}