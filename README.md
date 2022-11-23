# sudoku

4 klassen:

Class Node()
{
properties: getal, verplaatsbaar
}

Class Bord()
{
//geeft een representatie van het bord voor debugging
Print()
//vult het bord (maar Vul() vonden we geen mooie naam)
Read()
}

Class Solver()
{
4x Swap methode (noord, oost, zuid, west)
HillClimb()
}

Class Program()
{
CreateBord()
Solver(Bord)
}