using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public int force_const = 50;
	public int vel_cont = 10;
	public bool shouldIForce = true;

	public GameObject startScriptObj;

	// Use this for initialization
	void Start () {
		
	}


	void OnTriggerExit2D(Collider2D other) {
		Debug.Log ("GG");
		startScriptObj.GetComponent<StartScript> ().gameOver ();
	}

	void OnCollisionEnter2D(Collision2D collider){
		//bounce
		this.GetComponent<Rigidbody2D> ().velocity *= -0.6f;


	}

	// Update is called once per frame
	void FixedUpdate () {
		if (this.GetComponent<Rigidbody2D> ().velocity.magnitude < 2.8f && startScriptObj.GetComponent<StartScript> ().getStartTime()) {
			if (shouldIForce) {
				if (Input.GetButton (KeyCode.UpArrow.ToString ())) {
					this.GetComponent<Rigidbody2D> ().AddForce (force_const * Vector2.up * Time.deltaTime);
				}
				if (Input.GetButton (KeyCode.DownArrow.ToString ())) {
					this.GetComponent<Rigidbody2D> ().AddForce (force_const * Vector2.down * Time.deltaTime);
				}
				if (Input.GetButton (KeyCode.LeftArrow.ToString ())) {
					this.GetComponent<Rigidbody2D> ().AddForce (force_const * Vector2.left * Time.deltaTime);
				}
				if (Input.GetButton (KeyCode.RightArrow.ToString ())) {
					this.GetComponent<Rigidbody2D> ().AddForce (force_const * Vector2.right * Time.deltaTime);
				}
			} else {
				if (Input.GetButton (KeyCode.UpArrow.ToString ())) {
					this.GetComponent<Rigidbody2D> ().velocity += (vel_cont * Vector2.up * Time.deltaTime);
				}
				if (Input.GetButton (KeyCode.DownArrow.ToString ())) {
					this.GetComponent<Rigidbody2D> ().velocity += (vel_cont * Vector2.down * Time.deltaTime);
				}
				if (Input.GetButton (KeyCode.LeftArrow.ToString ())) {
					this.GetComponent<Rigidbody2D> ().velocity += (vel_cont * Vector2.left * Time.deltaTime);
				}
				if (Input.GetButton (KeyCode.RightArrow.ToString ())) {
					this.GetComponent<Rigidbody2D> ().velocity += (vel_cont * Vector2.right * Time.deltaTime);
				}
			}
		}


	}
}
