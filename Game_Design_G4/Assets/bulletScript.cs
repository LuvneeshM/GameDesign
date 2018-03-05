using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other){
		if (other.collider.name.Contains("en_ball")){
			Destroy (other.gameObject);
			Destroy (this.gameObject);

		}
	}

	public void loadScene(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("game");
	}

	public void returnHomeScene(){
		SceneManager.LoadScene ("start");
	}

}
