using UnityEngine;
using System.Collections;
using RSG;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

public class Controls : MonoBehaviour {
    public static Controls instance;
    const int N = 9;

    public Cell hovered;
    public Cell selected;

    public Unit activeUnit;

    public Button up;
    public Button down;
    public Button left;
    public Button right;

	public List<MonoBehaviour> lockers;

	public UnityEvent ready;

	public void Lock(MonoBehaviour locker) {
		lockers.Add(locker);
	}

	public void Unlock(MonoBehaviour locker) {
		lockers.Remove(locker);
	}

	public bool Locked() {
		return lockers.Count > 0;
	}

    void Awake() {
        instance = this;
		Ready();
    }

    private void RefreshHovered() {
        hovered = null;
        var cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(new Ray(cursor, Vector3.forward), out hit)) {
            var cell = hit.collider.gameObject.GetComponent<Cell>();
            if (cell != null) {
                hovered = cell;
            }
        }
    }

    IPromise commands = Promise.Resolved();
	Promise after;

	void OnReady() {
		ready.Invoke();
	}

	public void Ready() {
		Command(Promise.Resolved);
	}

	void UpdateAfter(Promise newAfter) {
		if (after != null && after.CurState == PromiseState.Pending) {
			after.Reject(new Exception("Interrupted by new command"));
		}
		after = newAfter;
		after.Then(() => OnReady()).Done();
	}

    public void Command(Func<IPromise> command) {
		var ready = new Promise();
		UpdateAfter(ready);
		commands = commands.Then(command).Then(() => {
			if (ready.CurState == PromiseState.Pending) {
				ready.Resolve();
			}
		});
        commands.Done();
    }

    public void Move(IntVector2 direction) {
        if (Hero.instance.Dead || BlackMage.instance.Dead) {
            return;
        }
        if (Intermission.active) {
            return;
        }
        if (activeUnit == null) {
            return;
        }
		if (Locked()) {
			Debug.LogFormat("Locked");
			return;
		}
        Command(() => activeUnit.MoveTo(direction).Untyped());
    }

    public void Restart() {
        GameManager.instance.Restart();
    }

	public void Start() {
		#if UNITY_EDITOR
		Cheats.on = true;
		#endif 
	}

    void Update() {
        if (Input.GetKeyDown(KeyCode.R) && Input.GetKey(KeyCode.LeftShift)) {
            GameManager.instance.DropSaveFile();
        }
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.GameOver()) {
            if (GameManager.instance.Won()) {
                GameManager.instance.ConfirmWin();
            } else {
                GameManager.instance.ConfirmLose();
            }
        }
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0) {
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.F5)) {
            Cheats.on ^= true;
        }
        if (Cheats.on) {
            if (Input.GetKeyDown(KeyCode.W)) {
                GameManager.instance.Win();
            }
            if (Input.GetKeyDown(KeyCode.L)) {
                GameManager.instance.Lose();
			}
			if (Input.GetKeyDown(KeyCode.R)) {
				GameManager.instance.Restart();
			}
            if (Input.GetKeyDown(KeyCode.LeftBracket)) {
                GameManager.instance.gameState.CurrentRun.levelsCompleted--;
                GameManager.instance.UpdateState();
            }
            if (Input.GetKeyDown(KeyCode.RightBracket)) {
                GameManager.instance.gameState.CurrentRun.levelsCompleted++;
                GameManager.instance.UpdateState();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            UI.instance.Escape();
        }
    }
}
