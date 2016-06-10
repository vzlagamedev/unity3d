using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class UINavigatorManager : MonoBehaviour {
	private float timeScaleSpeed = 1f;
	string sceneLoading = "";
	public void LoadScene(string scene){
		sceneLoading = scene;		
		AudioSource sfx = this.GetComponent<AudioSource> ();
		if (sfx != null) {
			sfx.volume = (float)GameData.gameData.getAllDataGame ().getSfxLevel ();
			sfx.Play ();
			Invoke ("loadAfterTime", sfx.clip.length);
		} else {
			loadAfterTime ();	
		}
	}

	void loadAfterTime(){
		UnityEngine.SceneManagement.SceneManager.LoadScene (sceneLoading);
	}

	public string CurrentScene(){
		return UnityEngine.SceneManagement.SceneManager.GetActiveScene ().ToString();
	}

	public string getActiveScene(){
		string scene = SceneManager.GetActiveScene ().name;
		return scene;
	}

	public bool sceneIsActive(string scene){
		return (SceneManager.GetActiveScene ().name == scene);
	}

	public void OnRateButtonClick(){
		#if UNITY_ANDROID
		Application.OpenURL("market://details?id=YOUR_APP_ID");
		#elif UNITY_IPHONE
		Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_APP_ID");
		#endif
	}

	public void twiiter(){
		Application.OpenURL("https://twitter.com/");
	}

	public void facebook(){
		Application.OpenURL("https://facebook.com/");
	}

	//#############################################################
	public void Home(){
		LoadScene (Constants.SCENE_HOME);
	}

	public void Config(){
		LoadScene (Constants.SCENE_CONFIG);
	}

	public void Achievements(){
		LoadScene (Constants.SCENE_ACHIVEMENTS);
	}

	public void LeaderBoard(){
		LoadScene (Constants.SCENE_LEADERBOARD);
	}

	public void Store(){
		LoadScene (Constants.SCENE_STORE);
	}

	public void Level(){
		LoadScene (Constants.SCENE_LEVEL);
	}

	public void Help(){
		LoadScene (Constants.SCENE_HELP);
	}

	public void QuitGame(){
		if (Application.platform != RuntimePlatform.IPhonePlayer) {
			Application.Quit();		
		}			
	}

	//#############################################

	public void Continue(){
		//Destroy
		GameObject target =  GameObject.Find(Constants.GO_SCREENPAUSE);
		if(target!=null){
			Destroy(target);
		}
		Time.timeScale =timeScaleSpeed;
	}



	public void PauseGame(){
		GameObject target =  GameObject.Find(Constants.GO_SCREENPAUSE);
		if(target==null){
			Instantiate(Resources.Load("Prefab/"+Constants.GO_SCREENPAUSE));		
			Time.timeScale =0;
		}
	}

	public void Restart(){
		LoadScene (getActiveScene());
	}

	//#######################################

	public void toogleMusic(){
		GameData.gameData.toogleMusic ();
	}

	public void toogleSfx(){
		GameData.gameData.toogleSfx ();
	}
}
