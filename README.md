# sudoku

4 klassen:

dillema: Houden we het algoritme dom door niet bij te houden welke swaps er al zijn geweest.


```csharp
Class Node()
{
properties: getal, verplaatsbaar
}
```

```csharp
Class Bord()
{
//geeft een representatie van het bord voor debugging
Print()
//vult het bord (maar Vul() vonden we geen mooie naam)
Read()
}
````

```csharp
Class Solver()
{
4x Swap methode (noord, oost, zuid, west)
HillClimb()
}
````
```csharp
Class Program()
{
CreateBord()
Solver(Bord)
}
```