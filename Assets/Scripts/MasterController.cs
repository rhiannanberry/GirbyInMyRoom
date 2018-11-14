using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour {
	AudioController ac;

	// Use this for initialization
	void Start () {
		SaveLoad.Load();
		if (SaveLoad.instance == null) {
			
			SaveLoad.instance = new SaveData();
		}
		ac = GetComponent<AudioController>();

		ac.Initialize();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
