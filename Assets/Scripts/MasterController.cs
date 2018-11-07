using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SaveLoad.Load();
		if (SaveLoad.instance == null) {
			
			SaveLoad.instance = new SaveData();
		}
		Debug.Log(SaveLoad.instance.musicVolume);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
