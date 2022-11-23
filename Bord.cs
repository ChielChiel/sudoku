class Bord {

    public Node[] sudoku;

    public Bord(int[] sudoku_array) {
        this.Create_Board(sudoku_array);
    }


    // Just creates a starting sudoku.
    public void Create_Board(int[] sudoku_array) {
        Node[] bord = new Node[sudoku_array.Length];

        for (int i = 0; i < sudoku_array.Length; i++)
        {
            Node vakje = new Node();
            if (sudoku_array[i] == 0)
            {
                vakje.verplaatsbaar = true;
            } else {
                vakje.verplaatsbaar = false;
            }
            // vakje.verplaatsbaar = sudoku_array[i] == 0
            vakje.getal = sudoku_array[i];
            bord[i] = vakje;
        }
        this.sudoku = bord;
    }

}