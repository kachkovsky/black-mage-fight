using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Bonus
{
    public int x, y;
    public bool isBlackMage;
    public Cell cell;

    public Bonus(int x, int y, bool isBlackMage, Cell cell) {
        this.x = x;
        this.y = y;
        this.isBlackMage = isBlackMage;
        this.cell = cell;
    }
}
