using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class SaveData {
	public float masterVolume, musicVolume, sfxVolume;


	public SaveData() {
		masterVolume = 0.5f;
		musicVolume = 0.5f;
		sfxVolume = 0.5f;
	}
}

public static class SaveLoad {
	public static SaveData instance;
	public static void Save() {
		BinaryFormatter bf = new BinaryFormatter();
		Debug.Log(Application.persistentDataPath);
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
		bf.Serialize(file, instance);
		file.Close();
	}

	public static void Load() {
    if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
        instance = (SaveData)bf.Deserialize(file);
        file.Close();
    }
}
}
