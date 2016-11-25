using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

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

#if UNITY_EDITOR
    [ContextMenu("Move Hero Here")]
    public void MoveHeroHere() {
        FindObjectOfType<Hero>().MoveTo(this);
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
    [ContextMenu("Move Black Mage Here")]
    public void MoveBlackMageHere() {
        FindObjectOfType<BlackMage>().MoveTo(this);
        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
    }
#endif

    public void MoveHere(Figure unit) {
        unit.transform.position = transform.position;
        unit.position = this;
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
