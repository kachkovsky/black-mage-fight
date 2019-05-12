using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Marks : Singletone<Marks>
{
	public Mark monster;
	public static Mark Monster { get { return instance.monster; } }
}
