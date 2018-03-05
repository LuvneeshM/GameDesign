using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	GameObject spawner;
	public GameObject[] bulletPrefab;
	public Transform bulletSpawn;

	bool shoot = true;
	bool oneSpawn = true;

	public double shotRate = 2f;
	double timeShot = 0;

	public int health = 3;
	public int pos = 1;

	// Use this for initialization
	void Start () {
		spawner = GameObject.Find ("enemySpawner");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			this.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(10, 0f, 0f));
		}
		if (Input.GetKey (KeyCode.LeftArrow)) {
			this.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(-10f, 0f, 0f));
		}
		if (Input.GetKey (KeyCode.UpArrow)) {
			this.gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(0, 10f, 0f));
		}

		if (Input.GetKey (KeyCode.Space)) {
			if (shoot == true) {
				timeShot = shotRate;
				shoot = false;
				fire ();
			}
		}
		if (timeShot > 0) {
			timeShot -= 0.05f;
			if (timeShot <= 0) {
				shoot = true;
			}
		}
	}

	void fire(){
		
		var bullet = (GameObject)Instantiate(bulletPrefab[pos].gameObject,
			bulletSpawn.position,
			bulletSpawn.rotation);

		bullet.AddComponent<bulletScript> ();

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = this.transform.right * 6;

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);
	}

	void OnCollisionEnter(Collision other){
		if (other.collider.name.Contains ("en_ball")) {
			var r = Random.Range (1, 3);
			health -= r;
			Destroy (other.gameObject);
			if (health <= 0) {
				if (oneSpawn) {
					spawner.GetComponent<spawnScript> ().spawnPlayer (false);
					oneSpawn = false;
				}
				Destroy (this.gameObject);
			}
		}



	}
}
