using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
/***********************************************************
PRIVATE Variables
************************************************************/
	GameObject AnswerManager;
	GameObject ScoreManager;
	GameObject panel;
	GameObject canvas;

	GameObject headerHolder;

	GameObject userGuessBtn = null;
	GameObject answerBtn = null;

	int ansRed;
	int ansGreen;
	int ansBlue;
	int thisButtonIsTheAnswer;

	bool watchAdOnce;

/***********************************************************
PUBLIC VARIABLES 
************************************************************/
	public int buffer;

		
/***********************************************************
PRIVATE METHODS
************************************************************/
	// Use this for initialization
	void Start () {
		AnswerManager = GameObject.Find ("AnswerManager");
		ScoreManager = GameObject.Find ("ScoreManager");
		panel = GameObject.Find ("Panel");
		canvas = GameObject.Find ("Canvas");

		headerHolder = GameObject.Find ("HeaderHolder");

		thisButtonIsTheAnswer = -1;

		watchAdOnce = false;

		pickTheAnswerButton ();
		newAnswerColor ();
		randomizeBtnColor ();
		//softReset ();
	}

	void pickTheAnswerButton(){
		//pick a random button to be the answer button
		int theRangeToPickFrom = panel.transform.childCount;
		thisButtonIsTheAnswer = Random.Range (0, theRangeToPickFrom);
	}

	void newAnswerColor(){
		ansRed = Random.Range (0, 256);
		ansGreen = Random.Range (0, 256);
		ansBlue = Random.Range (0, 256);

		setTextForUserToKnowColor ();
	}

	void setTextForUserToKnowColor(){
		string toWrite = "Guess the color";
		toWrite += "\nRGB(" + ansRed + "," + ansGreen + "," + ansBlue + ")";

		headerHolder.transform.GetChild(0).GetComponent<Text> ().text = toWrite;
	}

	//gives every button a random color
	//also give the answer button its right color
	void randomizeBtnColor(){
		for (int i = 0; i < panel.transform.childCount; i++) {
			GameObject gO = panel.transform.GetChild(i).gameObject;
			if (i != thisButtonIsTheAnswer) {
				int r = Random.Range (0, 256);
				int g = Random.Range (0, 256);
				int b = Random.Range (0, 256);
				while ((r > ansRed - buffer) && (r < ansRed + buffer)) {
					r = Random.Range (0, 256);
				}
				while ((g > ansGreen - buffer) && (g < ansGreen + buffer)) {
					g = Random.Range (0, 256);
				}
				while ((b > ansBlue - buffer) && (b < ansBlue + buffer)) {
					b = Random.Range (0, 256);
				}
				Debug.Log ("fakes " + r + " " + g + " " + b);
				var a = gO.GetComponent<Image> ();
				a.color = new Color (((float)r)/255.0f, ((float)g)/255.0f, ((float)b/255.0f));
				gO.GetComponent<Image> ().color = a.color;
			} else {
				Debug.Log ("REAL " + ansRed + " " + ansGreen + " " + ansBlue);
				answerBtn = gO;
				var a = gO.GetComponent<Image> ();
				a.color = new Color (((float)ansRed)/255.0f, ((float)ansGreen)/255.0f, ((float)ansBlue/255.0f));
				gO.GetComponent<Image> ().color = a.color;
			}
		}
	}

	//soft reset
	//user got answer so keep the game going
	void softReset(){
		userGuessBtn.transform.FindChild ("wrong").gameObject.SetActive (false);
		userGuessBtn.transform.FindChild ("correct").gameObject.SetActive (false);
		answerBtn.transform.FindChild ("correct").gameObject.SetActive (false);

		pickTheAnswerButton ();
		newAnswerColor ();
		randomizeBtnColor ();
	}
	//hard reset
	//user decided from pop-up to play again, so they play same level again
	//instead of reloading error just reset the score and do a soft reset
	void hardReset(){
		ScoreManager.SendMessage ("hardReset");
		softReset ();
	}

	void setAnswerButtons(bool value){
		for (int i = 0; i < panel.transform.childCount; i++) {
			panel.transform.GetChild (i).GetComponent<Button> ().interactable = value;
		}
	}

	void gameOver(){
		//if did not watch an Ad
		if (watchAdOnce == false) {
			//disable the buttons from being clicked on
			setAnswerButtons (false);
			//enable the popup
			canvas.transform.FindChild ("PopupHolder").gameObject.SetActive (true);
		}
		//watched an ad
		//go home
		else {
			SceneManager.LoadScene ("homeScene");
		}
	}

	IEnumerator showTheAnswer(bool result){
		if (result == true) {
			userGuessBtn.transform.FindChild ("correct").gameObject.SetActive (true);
		}
		if (result == false) {
			userGuessBtn.transform.FindChild ("wrong").gameObject.SetActive (true);
			answerBtn.transform.FindChild ("correct").gameObject.SetActive (true);
		}
		yield return new WaitForSeconds(1.0f);
		if (result == true) {
			//TO DO: ADD IN SOUND FOR RIGHT ANSWER
			ScoreManager.SendMessage ("correctAnswer");
			softReset ();
		} else if (result == false) {
			//TO DO: ADD IN SOUND FOR WRONG ANSWER 
			//show the game over popup
			gameOver ();
		}
	}

/***********************************************************
PUBLIC METHODS 
************************************************************/
	//tell the answer manager the correct colors
	public void passingAnsColors(){
		List<int> colors = new List<int>{ansRed, ansGreen, ansBlue};
		AnswerManager.SendMessage ("answerColorsFromGM", colors);
	}
	//user pressed this btn
	public void buttonPressed(GameObject btnGuessed){
		userGuessBtn = btnGuessed;
	}
	//answer manager tells gm if the answer is right
	//if answer is right, +1 and next question
	public void isGuessCorrect(bool result){
		//show right/wrong answer
		StartCoroutine(showTheAnswer(result));
	}
	//return to home scene
	public void returnHome(){
		//TO DO: SEND USER SCORE FOR TO LEADERBOARD FOR CURRENT LEVEL
		SceneManager.LoadScene ("homeScene");

	}
	//show an ad and keep playing
	public void watchAdToContinue(){
		//TO DO: ADD WATCHING AN AD
		canvas.transform.FindChild("PopupHolder").gameObject.SetActive(false);
		setAnswerButtons (true);

		watchAdOnce = true;

		//DECIDE IF USER CAN KEEP PLAYING FROM SAME QUESTION OR START WITH A NEW ONE
		//FOR NOW I PICK START WITH A NEW ONE
		softReset ();
	}
	public void playAgain(){
		canvas.transform.FindChild("PopupHolder").gameObject.SetActive(false);
		setAnswerButtons (true);
		hardReset ();
	}

}
