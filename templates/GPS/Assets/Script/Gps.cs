using UnityEngine;
using System.Collections;

public class Gps : MonoBehaviour {
	float myLat = 0f;
	float myLong= 0f;
	// Use this for initialization
	void Start () {
		StartCoroutine (getLocation ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator getLocation ()
	{

		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser){ 
			yield break;
		} else {
			// Start service before querying location
			Input.location.Start ();

			// Wait until service initializes
			int maxWait = 180;
			while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0) {
				yield return new WaitForSeconds (1);
				maxWait--;
			}

			// Service didn't initialize in 20 seconds
			if (maxWait < 1) {
				Debug.Log ("Timed out");
				yield break;
			}

			// Connection has failed
			if (Input.location.status == LocationServiceStatus.Failed) {				
				yield break;
			} else {
				// Access granted and location value could be retrieved
				myLat = Input.location.lastData.latitude;
				myLong = Input.location.lastData.longitude;					
			}

			// Stop service if there is no need to query location updates continuously
			Input.location.Stop ();

		}
	}
}
