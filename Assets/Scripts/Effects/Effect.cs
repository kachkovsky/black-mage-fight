using UnityEngine;
using System.Collections;

public abstract class Effect : MonoBehaviour
{
    public abstract void Run();

	[ContextMenu("Run")]
	public void RunFromEditor() {
        Board.instance.Restore();
		Run();
	}
}
