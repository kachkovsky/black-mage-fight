using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BlackMageValueProvider : UnitValueProvider
{
	public override Unit Value {
		get {
			return BlackMage.instance;
		}
	}
}
