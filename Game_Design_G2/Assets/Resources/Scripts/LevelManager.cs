using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

/***********************************************************
PUBLIC METHODS 
************************************************************/
	public void easyBtnPressed(){
		SceneManager.LoadScene ("gameScene_Easy");
	}
	public void mediumBtnPressed(){
		SceneManager.LoadScene ("gameScene_Medium");
	}
	public void hardBtnPressed(){
		SceneManager.LoadScene ("gameScene_Hard");
	}
}
