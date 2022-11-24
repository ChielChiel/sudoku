class Node
{
    private bool verplaatsbaar;
    private int getal;
    private int row;
    private int column;
    public Node()
    { 
    }

    public bool Verplaatsbaar
    {
        get { return verplaatsbaar; }
        set { verplaatsbaar = value; }
    }

    public int Getal
    {
        get { return getal; }
        set { getal = value; }
    }

    public int Row
    {
        get { return row; }
        set { row = value; }
    }

    public int Column
    {
        get { return column; }
        set { column = value; }
    }


}