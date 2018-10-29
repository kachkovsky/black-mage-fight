using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using System;

public class Periodic : Trigger
{
    public int period = 5;
    public int phase = 0;
	public List<int> runPhases = new List<int>();

	[Serializable]
	public class MultipleRun {
		public int phase = 0;
		public int times = 1;

		public MultipleRun(int phase, int times) {
			this.phase = phase;
			this.times = times;
		}
	}

	public List<MultipleRun> multipleRunPhases = new List<MultipleRun>() {
		new MultipleRun(0, 1)
	};

    public UnityEvent tick;

    public void Tick() {
        phase += 1;
        if (runPhases.Contains(phase % period)) {
            Run();
        } 
		var multipleRun = multipleRunPhases.FirstOrDefault(r => r.phase == phase % period);
		if (multipleRun != null) {
			for (int i = 0; i < multipleRun.times; i++) {
				Run();
			}
		} 
        tick.Invoke();
    }
}
