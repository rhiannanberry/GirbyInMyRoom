using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour {

	public Slider master,music,sfx;
	private AudioSource asMusic;

	void Start() {
		asMusic = GetComponent<AudioSource>();
	}

	public void Initialize() {
		UpdateSlider();
		asMusic.volume = master.value * music.value;
	}


	public void UpdateMaster() {
		SaveLoad.instance.masterVolume = master.value;
	}

	public void UpdateMusic() {
		SaveLoad.instance.musicVolume = music.value;
	}

	public void UpdateSFX() {
		SaveLoad.instance.sfxVolume = sfx.value;
	}
	public void UpdateVolumeInstance() {
		GetComponent<AudioSource>().volume = master.value * music.value;
		SaveLoad.Save();
		Debug.Log(SaveLoad.instance.musicVolume);
	}

	public void UpdateSlider() {
		if(SaveLoad.instance != null ) {
			master.value = Mathf.Clamp01(SaveLoad.instance.masterVolume);
			music.value = Mathf.Clamp01(SaveLoad.instance.musicVolume);
			sfx.value = Mathf.Clamp01(SaveLoad.instance.sfxVolume);
		}
	}

	
}