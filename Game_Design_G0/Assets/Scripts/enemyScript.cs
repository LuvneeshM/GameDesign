using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	public int speed;
	public int health;
	public List<GameObject> attackObjects;
	public GameObject playerToAttack;

	List<GameObject> coolDown;
	GameObject lastPlayerHit;
	GameObject gamePieces;

	int totalSize;
	int healthInitial;
	Color originalColor;
	bool s= false;
	bool keepFollowing = true;

	// Use this for initialization
	void Start () {
		gamePieces = GameObject.Find ("GamePeices");
		originalColor = this.gameObject.GetComponent<SpriteRenderer> ().color;
		coolDown = new List<GameObject> ();
		playerToAttack = attackObjects [Random.Range (0, attackObjects.Count)];

		lastPlayerHit = null;

		totalSize = attackObjects.Count;
		healthInitial = health;
	}

	IEnumerator startFollowingAgain(){
		yield return new WaitUntil (() => this.GetComponent<Rigidbody2D> ().velocity.magnitude <= 0.15);
		keepFollowing = true;
	}


	void FixedUpdate () {
		if (s) {
			if (keepFollowing) {
				Vector3 localPosition = playerToAttack.transform.position - transform.position;
				localPosition = localPosition.normalized; // The normalized direction in LOCAL space
				//this.GetComponent<Rigidbody2D>().AddForce(new Vector2(localPosition.x, localPosition.y) * Time.deltaTime * speed);
				this.GetComponent<Rigidbody2D> ().velocity = (new Vector2 (localPosition.x * Time.deltaTime * speed, localPosition.y * Time.deltaTime * speed));
			}

		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (this.gameObject.activeInHierarchy) {
			killMe ();
		}
	}

	public void makeMoreEnemies(){
		for (int i = 0; i < 1; i++) {
			GameObject clone = Instantiate (this.gameObject, gamePieces.transform);

			clone.transform.position = new Vector2 (Random.Range (-2.0f, 2.0f), Random.Range (-2.0f, 2.0f));
			clone.GetComponent<enemyScript> ().health = healthInitial;
			clone.GetComponent<SpriteRenderer> ().color = originalColor;
			clone.GetComponent<enemyScript> ().setStart (true);
		}
	}

	void OnCollisionEnter2D(Collision2D collider){
		//bounce
		keepFollowing = false;
		if(GameObject.Find(this.gameObject.name) != null)
			StartCoroutine ("startFollowingAgain");
		if (collider.gameObject.name == playerToAttack.name) {
			//swapPlayerToAttack ();

			//take damage
			health--;
			//get dark
			Color objColor = this.gameObject.GetComponent<SpriteRenderer>().color;
			objColor.g = (float)(health) / (float)(healthInitial);
			this.gameObject.GetComponent<SpriteRenderer> ().color = objColor;
			//i die
			if(health == 0){
				killMe ();
			}
		}
	
	}

	/*
	void swapPlayerToAttackWithDebug(){
		coolDown.Add (playerToAttack);
		attackObjects.Remove (playerToAttack);

		Debug.Log ("***********");
		Debug.Log ("Stats For " + this.name);
		Debug.Log ("CoolDown length " + coolDown.Count);
		Debug.Log ("AttackObject length" + attackObjects.Count);
		Debug.Log ("***********");

		if (coolDown.Count == totalSize) {
			Debug.Log (this.name + " says hi");
			attackObjects = new List<GameObject> (coolDown);
			coolDown = new List<GameObject> ();

			coolDown.Add (lastPlayerHit);
			attackObjects.Remove (lastPlayerHit);

			Debug.Log ("CoolDown length " + coolDown.Count);
			Debug.Log ("AttackObject length" + attackObjects.Count);
		}



		playerToAttack = attackObjects [Random.Range (0, attackObjects.Count)];

	}

	void swapPlayerToAttack(){
		coolDown.Add (playerToAttack);
		attackObjects.Remove (playerToAttack);

		if (coolDown.Count == totalSize) {
			attackObjects = new List<GameObject> (coolDown);
			coolDown = new List<GameObject> ();
		}

		playerToAttack = attackObjects [Random.Range (0, attackObjects.Count)];

	}
	*/

	public void setStart(bool newVal){
		s = newVal;
	}

	public void killMe(){
		this.gameObject.SetActive (false);
		Destroy (this.gameObject);
	}

}
