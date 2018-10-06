using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

	public AudioClip[] levelMusicChangeArray;
    public int maxScene = 2;

	private AudioSource audioSource;

	void Awake () {
		DontDestroyOnLoad (gameObject);
        audioSource = GetComponentInParent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
    }

    void Start() {
	}

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
        SceneManager.sceneUnloaded += OnSceneFinishedUnloading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }

    void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene Loaded, scene: " + scene.buildIndex + ", mode: " + mode);

        if( scene.buildIndex > maxScene ) {
            Debug.Log("Not a primary scene, no music change");
            return;
        }

        AudioClip sceneMusic = levelMusicChangeArray[scene.buildIndex];

        if (sceneMusic)
        {
            if(!audioSource) {
                Debug.LogError("Uh oh no AudioSource");
            }
            audioSource.clip = sceneMusic;
            audioSource.loop = true;
            audioSource.Play();
        }

    }

    void OnSceneFinishedUnloading(Scene scene)
    {
        Debug.Log("Scene unloaded: " + scene.buildIndex);
        //audioSource.Stop();
    }

	public void SetVolume(float volume) {
		audioSource.volume = volume;
	}



}
