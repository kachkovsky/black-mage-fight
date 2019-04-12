using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeroValueProvider : UnitValueProvider
{
	public override Unit Value {
		get {
			return Hero.instance;
		}
	}
}
