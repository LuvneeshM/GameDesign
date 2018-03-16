using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	IEnumerator Pause()
	{
		yield return new WaitForSeconds (1.4f);
		SceneManager.LoadScene ("gameScene_Fusion");
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
	public void fusionBtnPressed(){
		StartCoroutine (Pause());
	}
}
