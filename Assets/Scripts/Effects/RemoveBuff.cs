﻿using UnityEngine;
using System.Collections;

public class RemoveBuff : Effect
{
	public Buff buff;
	public Unit target;

	public override void Run() {
		if (target.buffs.Contains(buff)) {
			target.buffs.Remove(buff);
		}
	}
}