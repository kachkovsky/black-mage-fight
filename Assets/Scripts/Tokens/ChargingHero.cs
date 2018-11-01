using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using RSG;
using System.Collections.Generic;

public class ChargingHero : Hero
{
	public Trail trailSample;

	public List<Mark> meleeOnly;

	public Spawner crateSpawner;

	protected override void Awake() {
		base.Awake();
		if (crateSpawner == null) {
			crateSpawner = FindObjectOfType<Spawner>();
		}
	}

	IPromise PlayMoveAnimation(Cell from, Cell to) {
		Vector3 move = (to.transform.position - from.transform.position);
		int steps = 2 * from.Distance(to);
		Vector3 step = move / steps;

		for (int i = 1; i < steps; i++) {
			var j = i;
			TimeManager.Wait(0.05f * i / steps).Then(() => {
				var trail = Instantiate(trailSample);
				trail.gameObject.SetActive(true);
				trail.transform.position = from.transform.position + j * step + Vector3.back;
				trail.ChangeAlpha(1f * j / steps);
				Destroy(trail.gameObject, 0.1f * j / steps);
			});
		}

		return Promise.Resolved();
	}

	public override IPromise<bool> MoveTo(IntVector2 direction) {
		See(direction);
		var cell = Position;
		var next = cell;
		int distance = 0;
		for (int i = 0; i < 100; i++) {
			next = cell.ToDirection(direction);
			if (next != null && next.figures.Count == 0) {
				cell = next;
				distance = i + 1;
			}
		}
		bool chargeHitted = false;
		if (next != null) {
			chargeHitted |= ChargeHit(next.figures[0], distance, direction);
		} 
		if (!chargeHitted && cell != Position) {
			crateSpawner.Spawn();
			Position.MoveHere(crateSpawner.spawnedObjects.Last().GetComponent<Crate>());
		}
		if (cell != Position || next != null) {
			var oldPosition = Position;
			cell.MoveHere(this);
			moveSound.Play();
			PlayMoveAnimation(oldPosition, Position);
			GameManager.instance.HeroMoved(this, oldPosition, Position, direction);
			return Promise<bool>.Resolved(true);
		} else {
			return Promise<bool>.Resolved(false);
		}
	}

	bool ChargeHit(Figure f, int distance, IntVector2 direction) {
		if (distance <= 0 || !f.Marked(meleeOnly)) {
			f.Collide(this);
			return true;
		}
		return false;
	}
}
