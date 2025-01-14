﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using RSG;

public class SoundManager : Singletone<SoundManager>
{
	public Transform undestroyableSounds;

	public AudioSource explosion;
	public AudioSource doorOpenSound;
	public AudioSource doorCrackSound;
}
