using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using UnityEditor;

[ExecuteInEditMode]
public class Board : MonoBehaviour {
    public const int N = 8;

    public static Board instance;

    public GameObject sample;

    public GameObject[] rows;
    public Cell[,] cells;

    public bool toroid = false;

    void Awake() {
        instance = this;
        cells = new Cell[N,N];
        FindObjectsOfType<Cell>().ToList().ForEach(cell => cells[cell.x, cell.y] = cell);
    }

    public bool Inside(int x, int y) {
        return 0 <= x && x < N && 0 <= y && y < N;
    }

    public Cell GetCell(int x, int y) {
        if (toroid) {
            x = x.modulo(N);
            y = y.modulo(N);
        }
        if (Inside(x, y)) {
            return cells[x, y];
        }
        return null;
    }

    public Cell RandomEmptyCell(Func<Figure, bool> occupies = null) {
        occupies = occupies ?? (f => f.Occupies());
        for (int i = 0; i < 1000; i++) {
            var c = cells.Rand();
            if (!FindObjectsOfType<Figure>().Any(f => f.Position == c && occupies(f))) {
                return c;
            }
        }
        return null;
    }

    [ContextMenu("Generate")]
    public void Generate() {
        transform.Children().ForEach(c => DestroyImmediate(c.gameObject));
        rows = new GameObject[N];
        cells = new Cell[N,N];
        for (int i = 0; i < N; i++) {
            var row = new GameObject(string.Format("Row {0}", i));
            row.transform.SetParent(transform, worldPositionStays: false);
            row.transform.localPosition = new Vector3(0, -i, 0);
            rows[i] = row;
            for (int j = 0; j < N; j++) {
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
        Camera.main.transform.position = cells[N / 2, N / 2].transform.position + Vector3.back * 10f;
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
