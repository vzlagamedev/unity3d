using UnityEngine;
using System.Collections;

public class ParallaxLayer : MonoBehaviour {

	public float speed=0f;
	public float limitX = 0f;
	public bool leftToRight= true;
	private int horizontalDirection = 1;
	// Use this for initialization
	void Start () {
		if(leftToRight==true){
			horizontalDirection = 1;
		}else{
			horizontalDirection = -1;
		}
		limitX = Mathf.Abs (limitX) * horizontalDirection;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		foreach (Transform child in transform) {
			float xPosition = child.localPosition.x + Time.deltaTime * speed * horizontalDirection;
			child.localPosition = new Vector3 (xPosition,child.localPosition.y,child.localPosition.z);

			if(	child.localPosition.x<=limitX&&leftToRight==false ||
				child.localPosition.x>=limitX&&leftToRight==true
			){
				child.localPosition = new Vector3 (limitX*-1,child.localPosition.y,child.localPosition.z);
			}
		}

	}


}
