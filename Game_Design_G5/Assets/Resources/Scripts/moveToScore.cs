using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToScore : MonoBehaviour {

	public GameObject score;
	public bool sendScore = false;

	GameObject ScoreManager; 
	float targetScale = 0.1f;
 	float shrinkSpeed = 0.5f;

	public void softReset(){
		sendScore = false;
		gameObject.transform.localScale = new Vector2(2.0f,2.0f);
	}

	void Start () {
		ScoreManager = GameObject.Find ("ScoreManager");

		softReset ();
	}

	// Update is called once per frame
	void Update () {
		if (this.gameObject.transform.position.x <= score.transform.position.x+0.1f 
		&&  this.gameObject.transform.position.x >= score.transform.position.x-0.1f 
		&&  this.gameObject.transform.position.y <= score.transform.position.y+0.1f 
		&&  this.gameObject.transform.position.y >= score.transform.position.y-0.1f 
		) {
			if (sendScore == true) {
				Debug.Log ("Score sent");
				ScoreManager.SendMessage ("correctAnswer");
				sendScore = false;
			}

		} 
		gameObject.transform.position = Vector2.MoveTowards (this.transform.position, new Vector2(score.transform.position.x-0.05f, score.transform.position.y), 0.25f);

		gameObject.transform.localScale = Vector2.Lerp(gameObject.transform.localScale, new Vector2(targetScale, targetScale), Time.deltaTime*shrinkSpeed);
	}
}
