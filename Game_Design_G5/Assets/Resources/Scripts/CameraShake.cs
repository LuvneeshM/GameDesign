using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	// Transform of the camera to shake. Grabs the gameObject's transform
	// if null.
	public Transform[] items;
	
	// How long the object should shake for.
	public float shakeDuration = 0f;

	
	// Amplitude of the shake. A larger value shakes the camera harder.
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;
	public bool shackShake = false;

	float originalShake;

	Vector3[] originalPos = new Vector3[4];

	void Awake()
	{
		originalShake = shakeDuration;
		for (int i = 0; i < items.Length; i++) {
			if (items[i] == null)
			{
				items[i] = GetComponent(typeof(Transform)) as Transform;
			}
		}
		/*foreach (Transform camTransform in items){
			
		}*/
	}
	public void softReset(){
		shackShake = false;
		shakeDuration = originalShake;
	}

	void OnEnable()
	{
		for (int i = 0; i < items.Length; i++) {
			originalPos[i] = items[i].localPosition;
		}
	}

	void Update()
	{
		if (shackShake) {
			for (int i = 0; i < items.Length; i++) {
				if (shakeDuration > 0) {
					items [i].localPosition = originalPos [i] + Random.insideUnitSphere * shakeAmount;
			
					shakeDuration -= Time.deltaTime * decreaseFactor;
				} else {
					shakeDuration = 0f;
					items [i].localPosition = originalPos [i];
				}
			}
		}
	}
}
