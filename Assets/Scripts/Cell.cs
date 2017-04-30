using UnityEngine;
using System.Collections;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Collections.Generic;
#endif

[ExecuteInEditMode]
public class Cell : MonoBehaviour {
    public int x, y;

    public List<Figure> figures = new List<Figure>();

    public List<Figure> Figures {
        get {
            return figures.Where(f => f.gameObject.activeInHierarchy).ToList();
        }
    }

    public MeshRenderer meshRenderer;

    Color baseColor;

    public void Init() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Awake() {
        Init();
        baseColor = meshRenderer.sharedMaterial.color;
    }

    public void ChangeColor(Color c) {
        meshRenderer.material.color = c;
    }

    public void RestoreColor() { 
        meshRenderer.material.color = baseColor;
    }

    public void MoveHere(Figure f) {
        f.transform.position = transform.position;
        f.SetPosition(this); 
        figures.ForEach(f2 => {
            f2.Collide(f);
            f.Collide(f2);
        });
    }

    public Cell ToDirection(IntVector2 direction) {
        return Board.instance.GetCell(x + direction.x, y + direction.y);
    }

    public Cell Right() {
        return Board.instance.GetCell(x, y + 1);
    }
    public Cell Left() {
        return Board.instance.GetCell(x, y - 1);
    }
    public Cell Up() {
        return Board.instance.GetCell(x - 1, y);
    }
    public Cell Down() {
        return Board.instance.GetCell(x + 1, y);
    }
}
