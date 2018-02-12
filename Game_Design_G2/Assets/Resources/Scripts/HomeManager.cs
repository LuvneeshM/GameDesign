using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour {

	GameObject canvas;
	GameObject levelCanvas;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("Canvas");
		levelCanvas = transform.Find ("LevelSelectorCanvas").gameObject;
	}
	
/***********************************************************
PUBLIC METHODS 
************************************************************/
	public void playBtnPressed(){
		canvas.SetActive (false);
		levelCanvas.SetActive (true);
	}
	public void settingsBtnPressed(){

	}
	public void fbBtnPressed(){
		Application.OpenURL ("https://facebook.com/works.gilly");
	}
	public void rateUs(){
		/*
		#if UNITY_ANDROID
 			Application.OpenURL("market://details?id=YOUR_ID");
 		#elif UNITY_IPHONE
 			Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_ID");
 		#endif
 		*/
	}
	public void leaderBoard(){

	}
}
