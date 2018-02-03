using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerScript : MonoBehaviour {

	public int force_const = 100;
	public int vel_cont = 20;
	public bool shouldIForce = true;

	bool isJump = false;

	public Vector3 getPlayerPos(){
		return this.transform.position;
	}

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter(){
		isJump = false;
	}

	void OnTriggerEnter(Collider obj){
		if (obj.name == "win") {
			SceneManager.LoadScene (1);
		} else if (obj.name == "lose") {
			SceneManager.LoadScene (2);
		}
	}

	// Update is called once per frame
	void FixedUpdate () {
		//if (this.GetComponent<Rigidbody> ().velocity.magnitude < 2.8f) {
		//shouldIForce is false
		if (shouldIForce) {
			if (Input.GetButton (KeyCode.Space.ToString ())) { //if space key is pressed
				print ("up");
				Jump (); //adds a force in Vector3.up [0,1,0]
			}
			if (Input.GetButton (KeyCode.LeftArrow.ToString ())) { //if left arrow is pressed
				print ("left");
				this.GetComponent<Rigidbody> ().AddForce (force_const * Vector3.left * Time.deltaTime); //add a force in the left dir
			} else if (Input.GetButton (KeyCode.RightArrow.ToString ())) { //if right arrow is pressed
				print ("right");
				this.GetComponent<Rigidbody> ().AddForce (force_const * Vector3.right * Time.deltaTime); //add a force in the right dir
			} else {
				//nothing is pressed, set the velocity.x to 0, should leave y,z the same
				this.GetComponent<Rigidbody> ().velocity = new Vector3 (0,this.GetComponent<Rigidbody> ().velocity.y, this.GetComponent<Rigidbody> ().velocity.z); 
			}
		} 
		//WE ARE DOING THE ELSE BELOW
		else {
			print ("before jump" + this.GetComponent<Rigidbody> ().velocity);
			if (Input.GetButton (KeyCode.Space.ToString ())) { //if space key is pressed
				print ("upV");
				Jump (); //adds a force in Vector3.up [0,1,0]
			}
			print ("after jump" + this.GetComponent<Rigidbody> ().velocity);
			if (Input.GetButton (KeyCode.LeftArrow.ToString ())) { //if left arrow is pressed
			//	print ("leftV");
				Vector3 temp = this.GetComponent<Rigidbody> ().velocity; //grab the velocity
				print("temp " + temp);
				temp.x = vel_cont * -1f * Time.deltaTime; //change the x of that velocity
				print("temp after " + temp);
				this.GetComponent<Rigidbody> ().velocity += temp; //set the object velocity to the new modified temp
			} else if (Input.GetButton (KeyCode.RightArrow.ToString ())) { //if right arrow is pressed
			//	print ("rightV");
				//this.GetComponent<Rigidbody> ().velocity = new Vector3 (vel_cont * 1 * Time.deltaTime, this.GetComponent<Rigidbody> ().velocity.y, this.GetComponent<Rigidbody> ().velocity.z);
				Vector3 temp = this.GetComponent<Rigidbody> ().velocity; //change the x of that velocity
				temp.x = vel_cont * 1f * Time.deltaTime;//set the object velocity to the new modified temp
				this.GetComponent<Rigidbody> ().velocity += temp;//set the object velocity to the new modified temp
			} else { //nothing is pressed
				print ("stopV");
				//nothing is pressed, set the velocity.x to 0, should leave y,z the same
				this.GetComponent<Rigidbody> ().velocity = new Vector3 (0, this.GetComponent<Rigidbody> ().velocity.y, this.GetComponent<Rigidbody> ().velocity.z); 
			}

		}
		//}


	}

	void Jump(){
		if (!isJump) {
			this.GetComponent<Rigidbody> ().AddForce (force_const * Vector3.up * 30 * Time.deltaTime);
			isJump = true;
		}
	}
	void JumpV(){
		if (!isJump) {
			this.GetComponent<Rigidbody> ().velocity += (vel_cont * Vector3.up * 300 * Time.deltaTime);
			isJump = true;
		}

	}
}
