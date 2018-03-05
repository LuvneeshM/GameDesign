using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour {

	GameObject spawner;

	GameObject bulletPrefab;
	Transform bulletSpawn;

	public int health = 5;

	public string type = "e";

	double shotRate = 3f;
	double shootNow;

	bool oneSpawn = true;

	// Use this for initialization
	void Start () {
		spawner = GameObject.Find ("enemySpawner");
		bulletPrefab = GameObject.Find ("en_ball");

		bulletSpawn = this.transform.GetChild (0).transform;

		if (type == "e") {
			shotRate = 10f;
		} else if (type == "m") {
			shotRate = 9f;
		} else if (type == "h") {
			shotRate = 8f;
		}

		shootNow = shotRate;
	}
	
	// Update is called once per frame
	void Update () {
		shootNow -= 0.1f; 
		if (shootNow <= 0) {
			shootNow = shotRate;
			fire ();
		}
	}

	void fire(){
		var bullet = (GameObject)Instantiate(bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		if (type == "e") {
			bullet.GetComponent<Rigidbody>().velocity = this.transform.right * -2;
		} else if (type == "m") {
			bullet.GetComponent<Rigidbody>().velocity = this.transform.right * -4;
		} else if (type == "h") {
			bullet.GetComponent<Rigidbody>().velocity = this.transform.right * -6;
		}


		// Destroy the bullet after 2 seconds
		Destroy(bullet, 3.0f);
	}

	void OnCollisionEnter(Collision other){
		if (other.collider.name.Contains ("op_1")) {
			health -= 1;
			Destroy (other.gameObject);
		}
		else if (other.collider.name.Contains ("op_2")) {
			health -= 2;
			Destroy (other.gameObject);
		}
		else if (other.collider.name.Contains ("op_3")) {
			health -= 3;
			Destroy (other.gameObject);
		}

		if (other.collider.name.Contains ("op") && health <= 0) {
			if (oneSpawn) {
				spawner.GetComponent<spawnScript> ().summonEnemy ();
				spawner.GetComponent<spawnScript> ().spawnPlayer (true);
				oneSpawn = false;
			}
			Destroy (GameObject.FindGameObjectWithTag ("Player"));
			Destroy (this.gameObject);
		}
	}
}
