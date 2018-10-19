using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using RSG;

public class EventManager : Singletone<EventManager>
{
	public event Action<Figure, IntVector2, EventController> beforeFigureMove = (f, d, e) => { };

	public EventController BeforeFigureMove(Figure figure, IntVector2 direction) {
		var e = new EventController();
		beforeFigureMove(figure, direction, e);
		return e;
	}
}
