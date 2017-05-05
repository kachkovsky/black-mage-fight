﻿using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Board : MonoBehaviour {
    public int n = 8;

    public static Board instance;

    public GameObject sample;

    public GameObject[] rows;
    public Cell[,] cells;

    public List<Cell> cellsList;

    public bool toroid = false;

    public static IntVector2 up = new IntVector2(-1, 0);
    public static IntVector2 down = new IntVector2(1, 0);
    public static IntVector2 left = new IntVector2(0, -1);
    public static IntVector2 right = new IntVector2(0, 1);

    void Awake() {
        instance = this;
        cells = new Cell[n,n];
        cellsList = FindObjectsOfType<Cell>().ToList();
        cellsList.ForEach(cell => {
            cells[cell.x, cell.y] = cell;
        });
    }

    public bool Inside(int x, int y) {
        return 0 <= x && x < n && 0 <= y && y < n;
    }

    public Cell GetCell(int x, int y) {
        if (toroid) {
            x = x.modulo(n);
            y = y.modulo(n);
        }
        if (Inside(x, y)) {
            return cells[x, y];
        }
        return null;
    }

    public Cell RandomEmptyCell(Func<Figure, bool> occupies = null) {
        occupies = occupies ?? (f => f.Occupies());
        var emptyCells = cellsList.Where(c => !c.figures.Any(occupies)).ToList();
        if (emptyCells.Count > 0) {
            return emptyCells.ToList().Rnd();
        }
        return cellsList.Rnd();
    }

    [ContextMenu("Generate")]
    public void Generate() {
        transform.Children().ForEach(c => DestroyImmediate(c.gameObject));
        rows = new GameObject[n];
        cells = new Cell[n,n];
        for (int i = 0; i < n; i++) {
            var row = new GameObject(string.Format("Row {0}", i));
            row.transform.SetParent(transform, worldPositionStays: false);
            row.transform.localPosition = new Vector3(0, -i, 0);
            rows[i] = row;
            for (int j = 0; j < n; j++) {
                var cellObject = GameObject.Instantiate(sample);
                cellObject.name = string.Format("Cell {0} {1}", i, j);
                cellObject.transform.SetParent(row.transform, worldPositionStays: false);
                cellObject.transform.localPosition = new Vector3(j, 0, 0);
                cellObject.transform.localScale = Vector3.one * 0.9f;
                var cell = cellObject.GetComponent<Cell>();
                cell.x = i;
                cell.y = j;
                cells[i, j] = cell;
            }
        }
        Camera.main.transform.position = cells[n / 2, n / 2].transform.position + Vector3.back * 10f;
    }

    [ContextMenu("Restore")]
    public void Restore() {
        FindObjectsOfType<Cell>().ForEach(c => {
            c.figures = FindObjectsOfType<Figure>().Where(f => f.Position == c && f.gameObject.activeInHierarchy).ToList();
            if (!EditorApplication.isPlaying) {
                EditorUtility.SetDirty(c);
            }
        });
        if (!EditorApplication.isPlaying) {
            UnityEditor.SceneManagement.EditorSceneManager.MarkAllScenesDirty();
        }
    }
}
