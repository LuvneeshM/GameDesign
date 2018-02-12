using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	GameObject txtScore;

	int score;

	// Use this for initialization
	void Start () {
		txtScore = GameObject.Find ("ScoreTxt");
		score = 0;
	}
	
	public void hardReset(){
		score = 0;
		txtScore.GetComponent<Text> ().text = "Score: " + score;
	}
	//user got the answer correct
	//update their score by 1
	public void correctAnswer(){
		score++;
		txtScore.GetComponent<Text> ().text = "Score: " + score;
	}
}
