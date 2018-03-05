using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class showText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		updatePHealthText ();
		updateEHealthText ();
		updateLives ();
	}

	public void updatePHealthText(){
		var player = GameObject.FindWithTag ("Player");
		this.transform.GetChild (0).GetComponent<Text> ().text = "Player Health: " + player.GetComponent<playerScript> ().health;
		this.transform.GetChild (2).GetComponent<Text> ().text = "Rate of Fire: " + player.GetComponent<playerScript> ().shotRate;
	}
	public void updateEHealthText(){
		var en = GameObject.FindWithTag ("en");
		this.transform.GetChild (1).GetComponent<Text> ().text = "Enemy Health: " + en.GetComponent<enemyScript> ().health;
	}
	public void updateLives(){
		var livesCounter = GameObject.Find ("enemySpawner");
		this.transform.GetChild (3).GetComponent<Text> ().text = "Lives: " + livesCounter.GetComponent<spawnScript>().lives;
	}

}
