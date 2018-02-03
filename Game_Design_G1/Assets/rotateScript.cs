using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class rotateScript : MonoBehaviour {

	float meter = 0;

	public GameObject slider;
	public GameObject player;

	public Vector3 pastPlayerPos;
	public Vector3 currPlayerPos;

	// Use this for initialization
	void Start () {
		pastPlayerPos = player.transform.position;
		currPlayerPos = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		currPlayerPos = player.GetComponent<playerScript> ().getPlayerPos ();
		if (Vector3.Distance(pastPlayerPos, currPlayerPos) > 0.01 && pastPlayerPos.x != currPlayerPos.x) {
			pastPlayerPos = currPlayerPos;
			inputTilt ();
		}

		//tease the rotate
		if (meter >= 0.9f || meter <= -0.9f) {
			Camera.main.GetComponent<CameraShake> ().shakeDuration = 1f;
		} else {
			Camera.main.GetComponent<CameraShake> ().shakeDuration = 0f;
		}

		//do the rotate
		if (meter >= 1f) {
			meter = 0f;
			this.gameObject.transform.Rotate (0f, 0f, 90f);
		} else if (meter <= -1f) {
			meter = 0f;
			this.gameObject.transform.Rotate (0f, 0f, 90f);
		}
	}

	void inputTilt(){
		if (Input.GetButton (KeyCode.LeftArrow.ToString ())) {
			meter -= 0.015f;
		}
		if (Input.GetButton (KeyCode.RightArrow.ToString ())) {
			meter += 0.015f;
		}
		slider.GetComponent<Slider> ().value = meter;
	}

}
