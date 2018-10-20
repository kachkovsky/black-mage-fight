using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public class Edge : Token
{
    [Serializable]
    public class Position : IEquatable<Position>
    {
        public Cell a;
        public Cell b;

        public bool Equals(Position other) {
            return a == other.a && b == other.b;
        }

        public override int GetHashCode() {
            return 37 * (a == null ? 0 : a.GetHashCode()) + (b == null ? 0 : b.GetHashCode());
        }

        public Position(Cell a, Cell b) {
            this.a = a;
            this.b = b;
        }

        public Position Reversed() {
            return new Position(b, a);
        }
    }
    public Position position;

    [ContextMenu("Place")]
    public void Place() {
		transform.position = ((position.a.transform.position + position.b.transform.position) / 2).Change(z: transform.position.z);
        transform.eulerAngles = Vector3.forward * (position.b.transform.position - position.a.transform.position).xy().Direction();
    }

    public virtual void Pick(Unit hero) {
    }

    public virtual void ReversePick(Unit hero) {
    }

    void Check(HashSet<Position> positions, Cell a, Cell b) {
        if (b != null) {
            positions.Add(new Position(a, b));
        }
    }

    public void Blink() {
        HashSet<Position> positions = new HashSet<Position>();
        for (int i = 0; i < Board.instance.n; i++) {
            for (int j = 0; j < Board.instance.n; j++) {
                var a = Board.instance.cells[i,j];
                Check(positions, a, a.Right());
                Check(positions, a, a.Down());
            }
        }
        foreach (Edge e in FindObjectsOfType<Edge>()) {
            positions.Remove(e.position);
            positions.Remove(e.position.Reversed());
        }
        position = positions.ToList().Rnd();
        if (UnityEngine.Random.Range(0, 2) == 1) {
            position = position.Reversed();
        }
        Place();
    }
}
