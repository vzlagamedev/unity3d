using UnityEngine;
using System.Collections;

public class CameraSingleton : MonoBehaviour {

	public static CameraSingleton cameraSingleton;

	void Awake ()
	{

		//Debug.Log (fileDataGame);
		if (cameraSingleton == null) {
			cameraSingleton = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start () {

		adjustVolume ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void adjustVolume(){
		AudioSource sfx = this.GetComponent<AudioSource> ();
		sfx.volume = (float)GameData.gameData.getAllDataGame ().getMusicLevel ();
	}
}
