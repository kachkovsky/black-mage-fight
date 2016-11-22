using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
    public int x, y;

    MeshRenderer meshRenderer;

    public Material baseMaterial;
    public Material highlightMaterial;
    public Material selectedMaterial;

    public void Init() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Awake() {
        Init();
    }

    void Update() {
        if (Controls.instance.selected == this) {
            meshRenderer.material = selectedMaterial;
        } else if (Controls.instance.hovered == this) {
            meshRenderer.material = highlightMaterial;
        } else {
            meshRenderer.material = baseMaterial;
        }
    }

    [ContextMenu("MoveHere")]
    public void MoveHeroHere() {
        Hero.instance.transform.SetParent(transform, worldPositionStays: false);
        Hero.instance.transform.localPosition = new Vector3(0, 0, -1);
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
