using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Lightning : MonoBehaviour {

    List<LineRenderer> lines;
    public Vector3 from;
    public Vector3 to;
    public int n = 8;
    public float curvy = 0.1f;

    Vector3 Curvy(Vector3 v) {
        return v + UnityEngine.Random.insideUnitSphere * curvy;
    }

    void Awake() {
        lines = GetComponentsInChildren<LineRenderer>().ToList();  
        for (int i = 0; i < lines.Count; i++) {
            lines[i].numPositions = n+1;
            List<Vector3> positions = new List<Vector3>();
            for (int j = 0; j <= n; j++) {
                Vector3 p = from + (to-from)*j/n;
                positions.Add(p);
            }
            lines[i].SetPositions(positions.ToArray());
        }
    }

    void UpdateLine(LineRenderer line, int fromIndex, int toIndex) {
        if (toIndex - fromIndex < 2) {
            return;
        }
        int mid = (fromIndex + toIndex) / 2;
        line.SetPosition(mid, Curvy((line.GetPosition(fromIndex) + line.GetPosition(toIndex))/2));
        UpdateLine(line, fromIndex, mid);
        UpdateLine(line, mid, toIndex);
    }

    public void Update() {
        Debug.LogFormat("Update");
        for (int i = 0; i < lines.Count; i++) {
            UpdateLine(lines[i], 0, n);
        }
    }
}
