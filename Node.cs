class Node
{
    private bool swappable;
    private int number;  
    private int row;
    private int column;
    public Node()
    { 
    }

    public bool Swappable
    {
        get { return swappable; }
        set { swappable = value; }
    }

    public int Number
    {
        get { return number; }
        set { number = value; }
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