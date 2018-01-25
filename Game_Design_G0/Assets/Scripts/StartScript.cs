using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour {

	public GameObject fireFlake;
	public GameObject gamePieces;
	public GameObject canvas;

	Color originalColor;
	Color currColor;
	bool startLearpingColorBack = false;
	bool gameOn = true;
	bool startTime = false;
	float rad = 0;
	float timeTrack = 0;
	int makeMore = 2;
	int spawnTime = 5;

	// Use this for initialization
	void Start () {

		originalColor = fireFlake.GetComponent<SpriteRenderer> ().color;

		fireFlake.GetComponent<SpriteRenderer> ().color = Color.red;
		StartCoroutine("changeColor");
	}

	IEnumerator changeColor(){
		yield return new WaitForSeconds (1.0f);
		fireFlake.GetComponent<SpriteRenderer> ().color = new Color(0.8f,0.3f,0.6f);
		yield return new WaitForSeconds (1.0f);
		fireFlake.GetComponent<SpriteRenderer> ().color = Color.cyan;
		startLearpingColorBack = true;

		//start
		enemyScript[] enScripts = gamePieces.GetComponentsInChildren<enemyScript> ();
		foreach (enemyScript e in enScripts) {
			e.setStart (true);
		}
		startTime = true;
	}

	// Update is called once per frame
	void Update () {
		if (startLearpingColorBack) {
			fireFlake.GetComponent<SpriteRenderer> ().color = Color.Lerp (fireFlake.GetComponent<SpriteRenderer> ().color, originalColor, Mathf.PingPong (Time.time, 1));
		}

		//pulse background
		rad += 0.01f;
		currColor = this.GetComponent<SpriteRenderer>().color;
		currColor.a = 1.0f/6.0f*(Mathf.Sin (rad)+3.0f);
		this.GetComponent<SpriteRenderer> ().color = currColor;


		//split and make more 
		timeTrack += Time.deltaTime;
		if (timeTrack > spawnTime) {
			for (int i = 0; i < makeMore; i++) {
				int r = Random.Range (1, 4);
				GameObject enemyToCopy = GameObject.FindGameObjectWithTag ("e" + r);
				if (enemyToCopy != null) {
					enemyToCopy.GetComponent<enemyScript> ().makeMoreEnemies ();
				}
			}
			timeTrack = 0f;
			if (spawnTime < 10) {
				spawnTime++;
			}
			makeMore++;
		}
			
	}

	public bool getStartTime(){
		return startTime;
	}

	public void gameOver(){
		enemyScript[] enScripts = gamePieces.GetComponentsInChildren<enemyScript> ();
		foreach (enemyScript e in enScripts) {
			e.setStart (false);
		}
		gamePieces.SetActive (false);
		canvas.SetActive (true);
		this.gameObject.SetActive (false);
	}
}
