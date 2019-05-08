using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class CounterProxy : MonoBehaviour
{
	public Mark mark;

	public void Decrement() {
		Counter.counters[mark].Decrement();
	}
}
