using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AnswerManager : MonoBehaviour {

	int ansRed;
	int ansGreen;
	int ansBlue;

	GameObject gM;

	public GameObject plusOne;

	// Use this for initialization
	void Start () {
		gM = GameObject.Find ("GameManager");
	}
	

	public void answerColorsFromGM(List<int> theColors){
		try{
			if(theColors.Count == 3){
				ansRed = theColors [0];
				ansGreen = theColors [1];
				ansBlue = theColors [2];
			}
		}
		catch (Exception e){
			string ee = e.ToString();
			Debug.Log ("WE FKED UP GETTING THE COLORS FROM GAME MANAGER");
		}
	}

	//user clicked on a button
	//lets check to see if he is right
	public void checkAnswer(GameObject btnGuess){
		gM.SendMessage ("buttonPressed", btnGuess);

		bool isAnswerCorrect = false;
		gM.SendMessage ("passingAnsColors");
		int guessRed = (int)(btnGuess.GetComponent<Image> ().color.r * 255);
		int guessGreen = (int)(btnGuess.GetComponent<Image> ().color.g * 255);
		int guessBlue = (int)(btnGuess.GetComponent<Image> ().color.b * 255);
		Debug.Log ("User guess is " + guessRed + " " + guessGreen + " " + guessBlue);
		if (guessRed == ansRed) {
			if (guessGreen == ansGreen) {
				if (guessBlue == ansBlue) {
					isAnswerCorrect = true;
					//plusOne.SetActive (true);
				}
			}
		}

		gM.SendMessage ("isGuessCorrect", isAnswerCorrect);
	}
}
