using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class GameLevels : MonoBehaviour {
    public List<Difficulty> difficulties;

    public List<Level> commonLevels;

    public GameObject commonObjects;

	public Transform commonLevelsFolder;

    public static GameLevels instance;

    void Awake() {
        instance = this;
		commonLevels = commonLevelsFolder.Children().Select(c => c.GetComponent<Level>()).ToList();
        transform.Children().ForEach(c => c.gameObject.SetActive(false));
    }
}
