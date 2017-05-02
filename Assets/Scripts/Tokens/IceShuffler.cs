using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class IceShuffler : Token
{
    public void PlaceBack(Unit hero, Cell from, Cell to, IntVector2 direction) {
        from.MoveHere(FindObjectsOfType<Ice>().ToList().Rnd());
    }

    public void Shuffle(int cnt = 1) {
        for (int i = 0; i < cnt; i++) {
            Board.instance.RandomEmptyCell().MoveHere(FindObjectsOfType<Ice>().ToList().Rnd());
        }
    }
}
