﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour {



	GameObject canvas;
	GameObject levelCanvas;

	// Use this for initialization
	void Start () {
		canvas = GameObject.Find ("Canvas");
		levelCanvas = transform.Find ("LevelSelectorCanvas").gameObject;
		for (int i = 0; i < 25; i++) {
			var g = Instantiate (blob, this.gameObject.transform);
			g.transform.position = new Vector2 (Random.Range (-8f, 8f), Random.Range (-4.2f, 1.2f));
			g.GetComponent<SpriteRenderer> ().color = Random.ColorHSV ();
		}
	}

	IEnumerator Pause()
	{
		yield return new WaitForSeconds(0.25f);
		canvas.SetActive (false);
		levelCanvas.SetActive (true);
	}
/***********************************************************
PUBLIC Variables
************************************************************/
	public GameObject titleText;
	public GameObject blob;
/***********************************************************
PUBLIC METHODS 
************************************************************/
	public void playBtnPressed(){
		StartCoroutine (Pause());

	}
	public void changeColor(){
		titleText.GetComponent<Text> ().color = new Color ((float)Random.Range (50, 255)/255f, (float)Random.Range (50, 255)/255f, (float)Random.Range (50, 255)/255f);
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
