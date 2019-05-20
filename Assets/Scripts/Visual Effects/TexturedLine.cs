using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class TexturedLine : MonoBehaviour
{
	public LineRenderer line;

	public Vector2 moveSpeed = Vector2.zero;

	public Vector2 tilesPerUnit = Vector2.one;

	public void Awake() {
		line = GetComponent<LineRenderer>();
	}

	public float Length() {
		return (line.GetPosition(1) - line.GetPosition(0)).magnitude;
	}

	public void Update() {
		line.material.SetTextureScale("_MainTex", new Vector2(Length()/line.widthMultiplier, 1).Scaled(tilesPerUnit));
		line.material.SetTextureOffset("_MainTex", -moveSpeed * Time.time);
	}
}
