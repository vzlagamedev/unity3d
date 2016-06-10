using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.UI;

public class GameData : MonoBehaviour {

	// Use this for initialization
	public static GameData gameData;
	private string fileDataGame;
	private AllDataGame allDataGame;

	void Awake(){
		fileDataGame = Application.persistentDataPath+"/worldLevelDatav1.1.dat";
		if (gameData == null) {
			gameData = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy(gameObject);
		}
		LoadDataGame ();
	}

	
	// Use this for initialization
	void Start () {
		LoadDataGame ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void simpleSave(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file;
		if (!File.Exists (fileDataGame)) {
			file = File.Create (fileDataGame);
		}else{
			file = File.Open (fileDataGame, FileMode.Open);
		}
		bf.Serialize (file, allDataGame);
		file.Close ();
	}

	void LoadDataGame (){
		//Inicia Carga, sino existe lo crea
		if (File.Exists (fileDataGame)) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (fileDataGame, FileMode.Open);
			allDataGame = (AllDataGame)bf.Deserialize (file);
			file.Close ();
		} else {
			allDataGame = new AllDataGame ();
		}
	}

	public void setVolumeMusic(float volume){
		allDataGame.setMusicLevel (volume);
		simpleSave ();

	}

	public void setVolumeSfx(float volume){
		allDataGame.setSfxLevel (volume);
		simpleSave ();
	}

	public double getVolumeMusic(){
		double ml = 0;
		try {
			ml = allDataGame.getMusicLevel ();
		} catch (Exception e){
			Debug.Log("Vm "+e.Message);
		}
		return ml;
	}

	public double getVolumeSfx(){
		double ml = 0;
		try{
			ml = allDataGame.getSfxLevel ();
		} catch (Exception e){
			Debug.Log("VS "+e.Message);
		}
		return ml;
	}

	public void toogleMusic(){
		allDataGame.setMusic (!allDataGame.getMusic());
		mainCameraAudioSet ();
		simpleSave();
	}

	public void toogleSfx(){
		allDataGame.setSfx (!allDataGame.getSfx());
		sfxAudioSet ();
		simpleSave ();
	}

	public bool getMusic(){
		bool msc = false;
		try{
			msc = allDataGame.getMusic ();
		} catch (Exception e){

		}
		return msc;
	}

	public bool getSfx (){
		bool sfx = false;
		try{
			sfx = allDataGame.getSfx ();
		} catch (Exception e){

		}
		return sfx;
	}

	private void sfxAudioSet(){
		GameObject btnSfx =GameObject.Find(Constants.GO_BTNSFX);
		if (btnSfx != null) {
			if(getSfx()==false){ 
				btnSfx.GetComponent<Image>().color = Color.gray;
			}else{
				btnSfx.GetComponent<Image>().color = Color.white;
			}		
		}
	}

	private void mainCameraAudioSet(){
		GameObject camera=GameObject.Find(Constants.GO_CAMERA);
		camera.GetComponent<AudioSource> ().volume = (float)getVolumeMusic ();
		GameObject btnMusic =GameObject.Find(Constants.GO_BTNMUSIC);
		if (btnMusic != null) {
			if(getMusic()==false){ 
				btnMusic.GetComponent<Image>().color = Color.gray;
			}else{
				btnMusic.GetComponent<Image>().color = Color.white;
			}		
		}
	}


	public AllDataGame getAllDataGame(){
		return  allDataGame;
	}
}

[Serializable]
public class AllDataGame{


	private bool music = true;
	private double musicLevel = 0.6f;
	private double musicLevelLast = 0.6f;
	private bool sfx = true;
	private double sfxLevel = 0.6f;
	private double sfxLevelLast  = 0.6f;
	private string language = "es";

	public AllDataGame(){

	}

	public void setMusic (bool isEnabled){
		if (isEnabled == false) {
			musicLevel = 0f;
		} else {
			musicLevel=musicLevelLast;
		}
		music = isEnabled;
	}

	public bool getMusic (){
		return music;
	}

	public void setSfx (bool isEnabled){
		if(isEnabled==false){
			sfxLevel = 0f;
		}else{
			sfxLevel = sfxLevelLast;
		}
		sfx = isEnabled;
	}

	public bool getSfx (){
		return sfx;
	}

	public void setMusicLevel (double amount){
		musicLevel = amount;
	}

	public double getMusicLevel (){
		return musicLevel;
	}

	public void setSfxLevel (double amount){
		sfxLevel = amount;
	}

	public double getSfxLevel (){
		return sfxLevel;
	}

	public void setLanguage (string _language){
		language = _language;
	}

	public string getLanguage (){
		return language;
	}

}
