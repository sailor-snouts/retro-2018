using UnityEngine;
using System.Collections;

public class PlayerPrefsManager : MonoBehaviour {

	const string MASTER_VOLUME_KEY = "master_volume";
	const string DIFFICULTY_KEY = "difficulty";

	public static void Initialize() {
        if(GetMasterVolume() <= Mathf.Epsilon)
			SetMasterVolume (0.8f);
        if(GetDifficulty() <= Mathf.Epsilon)
			SetDifficulty (2.0f);
	}

	public static void SetMasterVolume(float volume) {
        if (volume >= Mathf.Epsilon && volume <= 1.0f) {
			PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
		} else {
			Debug.LogError ("Master Volume set out of range: " + volume);
		}
	}

	public static float GetMasterVolume() {
        // TODO: Address once player can change settings
        return 1.0f;
		//return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
	}

	public static void SetDifficulty(float difficulty) {
		if (difficulty > 1.0f && difficulty <= 3.0f) {
			PlayerPrefs.SetFloat (DIFFICULTY_KEY, difficulty);
		} else {
			Debug.LogError ("Difficulty set out of range: " + difficulty);
		}
	}

	public static float GetDifficulty() {
		return PlayerPrefs.GetFloat (DIFFICULTY_KEY);
	}

}
