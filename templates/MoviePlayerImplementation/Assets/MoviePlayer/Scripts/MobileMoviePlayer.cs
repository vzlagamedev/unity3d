using UnityEngine;
using System.Collections;
using System.IO;

public class MobileMoviePlayer : MonoBehaviour
{
	public string movieFileName;
	public Color backgroundColor = Color.black;
	public string urlvideo = "http://192.168.0.103/startreck.mp4";
	public string downloadedVideo = "";
	#if UNITY_ANDROID || UNITY_IPHONE
		public FullScreenMovieControlMode controlMod = FullScreenMovieControlMode.Hidden;
		public FullScreenMovieScalingMode scalingMod = FullScreenMovieScalingMode.Fill;
		#endif

	public bool playOnStart = true;

	void Start (){
		downloadedVideo = Application.persistentDataPath + "/video.mp4";
		Debug.Log (downloadedVideo);
		StartCoroutine (downloadVideo(urlvideo));
	}

	IEnumerator StartVideo ()
	{
		Debug.Log (Application.streamingAssetsPath + "/MoviePlayer/StreamingAssets/" + movieFileName);

		if (playOnStart) {
			Play ();
		}
		yield return 0;
	}

	/// <summary>
	/// Play the movie
	/// </summary>
	public void Play ()
	{
		if (string.IsNullOrEmpty (downloadedVideo)) {
			Debug.Log ("movieFileName is undefined");
			return;
		}
		#if UNITY_ANDROID || UNITY_IPHONE
					//Play full screen only
		Handheld.PlayFullScreenMovie (downloadedVideo,  backgroundColor, controlMod,scalingMod);
		#endif
	}

	IEnumerator downloadVideo (string url){
		WWW www = new WWW (url);
		yield return www;
		File.WriteAllBytes (downloadedVideo, www.bytes);
		StartCoroutine (StartVideo());
	}
}