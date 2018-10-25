using System;
using UnityEngine;

public abstract class Condition<T> : MonoBehaviour
{
	public abstract bool Satisfied(T obj);
}

