using System;
using UnityEngine;

public class Grid
{

    public int rows;
    public int columns;

    public Grid(int rows, int columns)
    {
        this.rows = rows;
        this.columns = columns;
    }

    public static implicit operator Grid(string v)
    {
       string[] numbers = v.Split("x");
       return new Grid(int.Parse(numbers[0]), int.Parse(numbers[1]));
    }
}
