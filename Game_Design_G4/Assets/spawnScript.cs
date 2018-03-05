using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class spawnScript : MonoBehaviour {

	public GameObject gg;

	public GameObject enemyPrefab;
	public Transform enemyPosition;

	public GameObject playerPrefab;
	public Transform playerPosition;

	public int count = 1;

	public int again = 1;

	public int lives = 8;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lives <= 0) {
			gg.SetActive (true); 
			Time.timeScale = 0;
		}
	}

	public void spawnPlayer(bool upgrade){
		
		var pl = (GameObject)Instantiate(playerPrefab,
			playerPosition.position,
			playerPosition.rotation);

		again++;
		lives--;
		if (upgrade == true) {
			again -= 2;
			lives++;
		}

		if (again < 2) {
			pl.GetComponent<playerScript> ().pos = 0;
			pl.GetComponent<playerScript> ().shotRate = 2f;
			pl.GetComponent<playerScript> ().health = 4;
			again = 1;
		} else if (again == 2) {
			pl.GetComponent<Transform> ().localScale = new Vector3 (1.2f, 1.2f, 1.2f);
			pl.GetComponent<playerScript> ().shotRate = 3f;
			pl.GetComponent<playerScript> ().health = 4;
			pl.GetComponent<playerScript> ().pos = 1;
		} else {
			pl.GetComponent<Transform> ().localScale = new Vector3 (0.8f, 0.8f, 0.8f);
			pl.GetComponent<playerScript> ().shotRate = 4f;
			pl.GetComponent<playerScript> ().health = 3;
			pl.GetComponent<playerScript> ().pos = 2;
			again = 3;
		}


	}

	public void summonEnemy(){
		var enemy = (GameObject)Instantiate(enemyPrefab,
			enemyPosition.position,
			enemyPosition.rotation);



		count++;
		if (count < 2) {
			enemy.GetComponent<enemyScript> ().health = 5;
			enemy.GetComponent<enemyScript> ().type = "e";
			count = 2;
		} else if (count == 2) {
			enemy.GetComponent<Transform> ().localScale = new Vector3 (2, 2, 2);
			enemy.GetComponent<enemyScript> ().health = 6;
			enemy.GetComponent<enemyScript> ().type = "m";
		} else {
			enemy.GetComponent<Transform> ().localScale = new Vector3 (3, 3, 3);
			enemy.GetComponent<enemyScript> ().health = 10;
			enemy.GetComponent<enemyScript> ().type = "h";
		}

	}

}