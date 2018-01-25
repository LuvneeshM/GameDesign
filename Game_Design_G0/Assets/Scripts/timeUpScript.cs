using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class timeUpScript : MonoBehaviour {

	public double currentTime=0;
	public GameObject timeText;


	// Use this for initialization
	void Start () {
		timeText.GetComponent<Text> ().text = "Time:\n" + currentTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.GetComponent<StartScript> ().getStartTime ()) {
			currentTime += Time.deltaTime;
			updateText ();
		}

	}

	void updateText(){
        timeText.GetComponent<Text>().text = "Time:\n" + System.Math.Round(currentTime, 1).ToString("F2");
	}

    public double getCurrentTime()
    {
        return currentTime;
    }
}
