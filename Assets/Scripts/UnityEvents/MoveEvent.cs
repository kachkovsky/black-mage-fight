using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

[Serializable]
public class MoveEvent : UnityEvent<Unit, Cell, Cell, IntVector2> {
}
