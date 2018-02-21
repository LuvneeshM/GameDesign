using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class timeScript : MonoBehaviour {

	public GameObject snakeManHead;
	public GameObject timeText;
	public GameObject tailLength;
	public double timeLeft;

	double originlTime;

	// Use this for initialization
	void Start () {
		originlTime = 10f;
		timeLeft = originlTime;

		timeText.GetComponent<Text> ().text = "Time Left: " + timeLeft;
	}
	
	// Update is called once per frame
	void Update () {
		if (snakeManHead.GetComponent<SnakeMan> ().gameOn) {
			timeLeft -= Time.deltaTime;
			if (timeLeft <= 0) {
				ZeroTime ();
			} else if (timeLeft <= 5.0f) {
				snakeManHead.GetComponent<SnakeMan> ().indicateLosingTai (true);
			}
			updateText ();

		}
	}

	void updateText(){
		timeText.GetComponent<Text> ().text = "Time Left: " + System.Math.Round (timeLeft, 1);
		tailLength.GetComponent<Text> ().text = "Tail Length: " + snakeManHead.GetComponent<SnakeMan> ().tailLength ();
	}

	void ZeroTime(){
		snakeManHead.GetComponent<SnakeMan> ().loseATail ();
		resetTime ();
		snakeManHead.GetComponent<SnakeMan> ().indicateLosingTai (false);
	}

	//resets the timer to initial time
	public void resetTime(){
		timeLeft = originlTime;
	}

	public double getTimeLeft(){
		return timeLeft;
	}


}
