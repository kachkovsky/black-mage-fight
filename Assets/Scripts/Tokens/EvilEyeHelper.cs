﻿using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine.Events;
using System.Collections.Generic;

public class EvilEyeHelper : Token
{
    public Color highlightColor;

    void See(Cell cell, IntVector2 direction) {
        cell = cell.ToDirection(direction);
        List<Cell> path = new List<Cell>();
        while (cell != null && !cell.figures.Any(f => f is EvilEye)) {
            path.Add(cell);
            cell = cell.ToDirection(direction);
        }
        if (cell != null) {
            path.ForEach(c => c.ChangeColor(highlightColor));
        }
    }

    public void Run() {
        Debug.LogFormat("helper");
        Board.instance.cellsList.ForEach(cell => cell.RestoreColor());
        See(Hero.instance.Position, new IntVector2(1, 0));
        See(Hero.instance.Position, new IntVector2(-1, 0));
        See(Hero.instance.Position, new IntVector2(0, 1));
        See(Hero.instance.Position, new IntVector2(0, -1));
    }
}