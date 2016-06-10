using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenPause : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.transform.SetParent(GameObject.Find("Canvas").transform);
		this.transform.localPosition = new Vector3 (0,0,0);
		this.transform.name=Constants.GO_SCREENPAUSE;
		this.transform.localScale = new Vector3 (1,1,1);

		GameObject uiManager =  GameObject.Find(Constants.GO_UINAVIGATORMANAGER);
		UINavigatorManager uinm = uiManager.GetComponent<UINavigatorManager> ();

		
		GameObject buttonCont =  GameObject.Find(Constants.GO_BTNCONTINUE);
		Button a = buttonCont.GetComponent<Button>();
		a.onClick.AddListener(() => uinm.Continue());
		
		
		GameObject buttonRest =  GameObject.Find(Constants.GO_BTNRESTART);
		Button b = buttonRest.GetComponent<Button>();
		b.onClick.AddListener(() => uinm.Restart());
		
		
		GameObject buttonExit =  GameObject.Find(Constants.GO_BTNSURRENDER);
		Button c = buttonExit.GetComponent<Button>();
		c.onClick.AddListener(() => uinm.Home());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
