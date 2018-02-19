using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodSpawner : MonoBehaviour {
	// Food Prefab
	public GameObject foodPrefab;
	public GameObject SnakeManHead;
	// Borders
	public Transform borderTop;
	public Transform borderBottom;
	public Transform borderLeft;
	public Transform borderRight;

	int maxFood = 25;
	public int currFoodCount = 0;
	// Use this for initialization
	void Start () {
		// Spawn food every 4 seconds, starting in 3
		InvokeRepeating("Spawn", 3, 4);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Spawn one piece of food
	void Spawn() {
		if (currFoodCount < maxFood && SnakeManHead.GetComponent<SnakeMan>().gameOn) {
			// x position between left & right border
			int x = (int)Random.Range (borderLeft.position.x,
				       borderRight.position.x);

			// y position between top & bottom border
			int y = (int)Random.Range (borderBottom.position.y,
				       borderTop.position.y);

			// Instantiate the food at (x, y)
			Instantiate (foodPrefab,	new Vector2 (x, y),	Quaternion.identity, this.transform);
		}
	}

	public void restart(){
		SceneManager.LoadScene (0);
	}
}
