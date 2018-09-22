using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
	public Slider difficultySlider;
	public Navigation navigation;

	private MusicManager musicManager;

	void Start() {
		musicManager = FindObjectOfType<MusicManager> ();
	
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume ();
	    difficultySlider.value = PlayerPrefsManager.GetDifficulty ();
	}

	public void VolumeChanged() {
        if (musicManager)
        {
            musicManager.SetVolume(volumeSlider.value);
        } else {
            Debug.LogWarning("Expected MusicManager while loading Options!");
        }
	}

	public void DifficultyChanged() {
	}

	public void SaveAndExit() {
		PlayerPrefsManager.SetMasterVolume (volumeSlider.value);
		PlayerPrefsManager.SetDifficulty (difficultySlider.value);
		navigation.GoBack ("Options");
	}

    public void MainMenu()
    {
        navigation.Title();
    }

    public void QuitGame()
    {
        navigation.QuitGame();
    }

    public void Defaults() {
		volumeSlider.value = 0.8f;
		difficultySlider.value = 2.0f;

		PlayerPrefsManager.SetMasterVolume (volumeSlider.value);
		PlayerPrefsManager.SetDifficulty (difficultySlider.value);
	}
}
