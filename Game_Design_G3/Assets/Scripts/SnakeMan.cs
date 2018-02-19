using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class SnakeMan : MonoBehaviour {
	
	public GameObject tailPrefab;
	public GameObject foodSpawner;
	public GameObject wallPrefab;
	public GameObject wallHolder;
	public GameObject gg;
	public bool gameOn = true;


	Vector2 dir = Vector2.right;

	List<Transform> tail = new List<Transform>();

	bool ate = false;
	float invokeSpeed = 0.10f;

	//up = 0
	//down = 1
	//left = 2
	//right = 3
	int curDir = 3;

	// Use this for initialization
	void Start () {
		InvokeRepeating("Move", 0.3f, invokeSpeed); 	
	}
	// Update is called once per Frame
	void Update() {
		if (gameOn) {
			// Move in a new Direction?
			if (Input.GetKey (KeyCode.RightArrow) && curDir != 2) {
				dir = Vector2.right;
				curDir = 3;
			} else if (Input.GetKey (KeyCode.DownArrow) && curDir != 0) {
				dir = -Vector2.up;
				curDir = 1;
			} else if (Input.GetKey (KeyCode.LeftArrow) && curDir != 3) {
				dir = -Vector2.right; 
				curDir = 2;
			} else if (Input.GetKey (KeyCode.UpArrow) && curDir != 1) {
				dir = Vector2.up;
				curDir = 0;
			}
		} else {
			dir = Vector2.zero;
		}
	}

	void Move() {
		if (gameOn) {
			// Save current position (gap will be here)
			Vector2 v = transform.position;

			transform.Translate (dir);

			// Ate something? Then insert new Element into gap
			if (ate) {
				GameObject g = (GameObject)Instantiate (tailPrefab, v, Quaternion.identity);
				tail.Insert (0, g.transform);
				foodSpawner.GetComponent<FoodSpawner> ().currFoodCount--;
				// i am full now
				ate = false;
			} else if (tail.Count > 0) {
				tail.Last ().position = v; //moving last tail up

				tail.Insert (0, tail.Last ());
				tail.RemoveAt (tail.Count - 1);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		// did i hit food
		if (coll.name.StartsWith ("foodPrefab")) {
			ate = true;

			Destroy (coll.gameObject);
		} else {
			gameOn = false;
			Debug.Log (gameOn + " trigger");
		}
	}

	void OnCollisionEnter2D(Collision2D coll){
		gameSetMatch ();
	}

	void gameSetMatch(){
		gameOn = false;
		gg.SetActive (true);
	}

	public void loseATail(){
		if (tail.Count > 0) {
			GameObject rock = (GameObject)Instantiate (wallPrefab, tail.Last ().position, Quaternion.identity, wallHolder.transform);

			Destroy (tail [tail.Count - 1].gameObject);
			tail.RemoveAt (tail.Count - 1);

		} else {
			gameSetMatch ();
		}
	}

	public int tailLength(){
		return tail.Count;
	}

}
