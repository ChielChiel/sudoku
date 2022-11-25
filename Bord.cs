class Bord {

    public Node[] sudoku;

    public Bord(int[] sudoku_array) {
        this.sudoku = this.Create_Board(sudoku_array);
        // foreach(Node sd in this.sudoku) {
        //     Console.Write("(" + "[" + sd.Row + ";" + sd.Column + "] "  + sd.Getal + " " + sd.Verplaatsbaar + "),");
        // }

        // Test to show that GetCoordinate and GetFlatPosition are working.
        Coordinate start = this.GetCoordinate(80);
        Console.WriteLine(start);
        int end = this.GetFlatPosition(start);
        Console.WriteLine(end);
        this.Print();
        // int[] test_sdk = new int[81];
        // for (int i = 0; i < 81; i++)
        // {
        //     test_sdk[i] = i + 1;
        // }
        // Node[] test_sudoku = this.Create_Board(test_sdk);
        // foreach(Node sd in test_sudoku) {
        //     Console.Write("(" + "[" + sd.Row + ";" + sd.Column + "] "  + sd.Getal + " " + sd.Verplaatsbaar + "),");
        // }



    }
    public void Print()
    {
        string Rij;
        int aantalRijen = (int) Math.Sqrt(sudoku.Length);
        for (int i = 0; i < aantalRijen; i++)
        {
            Rij = "";
            for (int j = 0; j < aantalRijen; j++)
            {
                if ((j + (i * aantalRijen)) % 3 == 0)
                    Rij += "|";
                Rij += sudoku[j+(i*aantalRijen)].Getal.ToString() + " ";
            }
            if ((i % 3) == 0)
                Console.WriteLine("--------------------");
           
            Console.WriteLine(Rij);
        }
    }

    // Just creates a starting sudoku.
    public Node[] Create_Board(int[] sudoku_array) {
        Node[] bord = new Node[sudoku_array.Length];

        for (int i = 0; i < sudoku_array.Length; i++)
        {
            Node vakje = new Node();
            vakje.Verplaatsbaar = (sudoku_array[i] == 0);
            vakje.Getal = sudoku_array[i];
            
            Coordinate positie = GetCoordinate(i + 1);
            vakje.Row = positie.Y;
            vakje.Column = positie.X;

            bord[i] = vakje;
        }
        return bord;
    } 


    // Maps the flattened position of sudoku array to a coordinate in the sudoku.
    // Coordinates from (0,0) top-left to (8,8) bottom right
    public Coordinate GetCoordinate(int flat_position) {
        int x = 0;
        int y = 0;
        y = (flat_position - 1) / 9;
        x = flat_position - (y * 9);
        return new Coordinate(x, y);
    }

    public int GetFlatPosition(Coordinate position) {
        return 9 * position.Y + position.X;
    }

}

// Helper class to project a coordinate in our sudoku
class Coordinate {

    // The X coordinate, also the column
    public int X;

    // The Y coordinate, also the row
    public int Y;

    public Coordinate(int x, int y) {
        this.X = x;
        this.Y = y;
    }

    public override string ToString()
    {
        return "X = " + this.X + ", Y = " + this.Y;
    }

}
