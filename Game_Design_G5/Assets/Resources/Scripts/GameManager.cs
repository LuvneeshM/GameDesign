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
	GameObject cam;

	GameObject headerHolder;

	GameObject userGuessBtn = null;
	GameObject answerBtn = null;

	int ansRed;
	int ansGreen;
	int ansBlue;
	int thisButtonIsTheAnswer;

	bool watchAdOnce;

	List<GameObject> blobList;

/***********************************************************
PUBLIC VARIABLES 
************************************************************/
	public int buffer;
	public GameObject cheater;
	public GameObject plusOne;
	public GameObject blob;
		
/***********************************************************
PRIVATE METHODS
************************************************************/
	// Use this for initialization
	void Start () {
		AnswerManager = GameObject.Find ("AnswerManager");
		ScoreManager = GameObject.Find ("ScoreManager");
		panel = GameObject.Find ("Panel");
		canvas = GameObject.Find ("Canvas");
		cam = GameObject.Find ("Main Camera");
		headerHolder = GameObject.Find ("HeaderHolder");

		thisButtonIsTheAnswer = -1;

		watchAdOnce = false;

		if (SceneManager.GetActiveScene ().name == "gameScene_Fusion") {
			blobList = new List<GameObject> ();
			for (int i = 0; i < 40; i++) {
				var g = Instantiate (blob, canvas.gameObject.transform);
				blobList.Add (g);
			}
		}
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

		if (SceneManager.GetActiveScene ().name != "gameScene_Fusion") {
			headerHolder.transform.GetChild (0).GetComponent<Text> ().text = toWrite;
		} else {
			//pick 2 colors for fusion
			//color 1
			int tempR = Random.Range(25, 200);
			int tempG = Random.Range(25, 200);
			int tempB = Random.Range(25, 200);
			//color2
			int tempR2 = Random.Range(25, 200);
			int tempG2 = Random.Range(25, 200);
			int tempB2 = Random.Range(25, 200);

			Color colorLeft = new Color();
			Color colorRight = new Color();

			bool left = true;
			for (int i = 0; i < 2; i++) {
				GameObject gO = headerHolder.transform.GetChild (i).gameObject;
				var a = gO.GetComponent<Image>();
				if (left) {
				//	Debug.Log ("Left " + tempR + " " + tempG + " " + tempB);
					colorLeft = new Color (((float)tempR) / 255.0f, ((float)tempG) / 255.0f, ((float)tempB / 255.0f), 0.75f);
					a.color = colorLeft;
					left = false;
				} else {
				//	Debug.Log ("Right " + (ansRed - tempR) + " " + (ansGreen - tempG) + " " + (ansBlue - tempB));
					colorRight = new Color ( ((float)(tempR2)) / 255.0f, ((float)(tempG2)) / 255.0f, ((float)(tempB2)) / 255.0f, .75f );
					a.color = colorRight;
				}
				gO.GetComponent<Image> ().color = a.color;
			}
				
			float c1H, c1S, c1L;
			Color.RGBToHSV (colorLeft, out c1H, out c1S, out c1L);
			float c2H, c2S, c2L;
			Color.RGBToHSV (colorRight, out c2H, out c2S, out c2L);
			var avgH = ((c1H + c2H) / 2.0f);
			var avgS = ((c1S + c2S) / 2.0f);
			var avgL = ((c1L + c2L) / 2.0f);
			Debug.Log ("AVGH " + avgH);
			var rr = Color.HSVToRGB(avgH, avgS, avgL);

			//var r = mix (colorLeft, colorRight);
			Debug.Log (rr);
			ansRed = (int)(rr.r * 255);
			ansGreen = (int)(rr.g * 255);
			ansBlue = (int)(rr.b * 255);

			cheater.GetComponent<Text>().color = new Color (((float)ansRed)/255.0f, ((float)ansGreen)/255.0f, ((float)ansBlue/255.0f));

		}
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
				if (SceneManager.GetActiveScene ().name == "gameScene_Fusion") {
					plusOne.transform.position = answerBtn.transform.position;
					plusOne.SetActive (false);
				}
			}
		}
	}

	//soft reset
	//user got answer so keep the game going
	void softReset(){
		if (SceneManager.GetActiveScene ().name == "gameScene_Fusion") {
			plusOne.GetComponent<moveToScore> ().softReset ();
			for (int i = 0; i < blobList.Count; i++) {
				blobList[i].transform.position = blob.transform.position;
				blobList [i].GetComponent<SpriteRenderer> ().color = Color.white;
			}
		}
		cam.GetComponent<CameraShake> ().softReset ();
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
			//SOUND FOR RIGHT ANSWER
			cam.GetComponent<playSound> ().PlaySound ();
			yield return new WaitForSeconds(0.75f);
			userGuessBtn.transform.FindChild ("correct").gameObject.SetActive (true);
			if (SceneManager.GetActiveScene ().name == "gameScene_Fusion") {
				this.gameObject.GetComponent<playSound> ().splatSound ();
				yield return new WaitForSeconds (.750f);
				plusOne.GetComponent<moveToScore> ().sendScore = true;
				plusOne.SetActive (true);
				//Shots
				for (int i = 0; i < blobList.Count; i++) {
					blobList[i].transform.position = new Vector2 (Random.Range (-8f, 8f), Random.Range (-4.2f, 3.2f));
					blobList[i].GetComponent<SpriteRenderer> ().color = new Color (((float)ansRed)/255.0f, ((float)ansGreen)/255.0f, ((float)ansBlue/255.0f));
				}
			}
		}
		if (result == false) {
			//SOUND FOR WRONG ANSWER
			cam.GetComponent<playSound> ().PlayLevelSelectSound ();
			yield return new WaitForSeconds (0.75f);
			cam.GetComponent<CameraShake> ().shackShake = true;
			userGuessBtn.transform.FindChild ("wrong").gameObject.SetActive (true);
			answerBtn.transform.FindChild ("correct").gameObject.SetActive (true);
			if (SceneManager.GetActiveScene ().name == "gameScene_Fusion") {
				plusOne.SetActive (false);
			}
		}

		yield return new WaitForSeconds(3.0f);
		if (result == true) {
			if (SceneManager.GetActiveScene ().name != "gameScene_Fusion") {
				ScoreManager.SendMessage ("correctAnswer");
			} 
			softReset ();
		} else if (result == false) {
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
		this.gameObject.GetComponent<playSound>().take2Sound();
		canvas.transform.FindChild("PopupHolder").gameObject.SetActive(false);
		setAnswerButtons (true);

		watchAdOnce = true;

		//DECIDE IF USER CAN KEEP PLAYING FROM SAME QUESTION OR START WITH A NEW ONE
		//FOR NOW I PICK START WITH A NEW ONE
		softReset ();
	}
	public void playAgain(){
		this.gameObject.GetComponent<playSound>().restartingTheLevel();
		canvas.transform.FindChild("PopupHolder").gameObject.SetActive(false);
		setAnswerButtons (true);
		hardReset ();
	}

}
