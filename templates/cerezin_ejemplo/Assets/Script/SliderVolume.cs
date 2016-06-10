using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderVolume : MonoBehaviour {
	public bool isMusic = true;

	Slider sliderValue;
	// Use this for initialization
	void Start () {
		sliderValue = this.GetComponent<Slider> ();
		if (isMusic == true) {
			sliderValue.value = (float)GameData.gameData.getVolumeMusic ();
		} else {
			sliderValue.value = (float)GameData.gameData.getVolumeSfx ();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isMusic == true) {
			if(GameData.gameData.getMusic()==true){
				GameData.gameData.setVolumeMusic (sliderValue.value);
				GameObject.Find (Constants.GO_CAMERA).GetComponent<CameraSingleton> ().adjustVolume ();
			}
		} else {
			if(GameData.gameData.getSfx()==true){
				GameData.gameData.setVolumeSfx (sliderValue.value);
			}
		}
	}
}
