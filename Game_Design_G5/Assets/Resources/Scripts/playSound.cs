using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playSound : MonoBehaviour {

	public AudioClip sound;
	public AudioClip soundLevel;
	public AudioClip take2;
	public AudioClip resarting;
	public AudioClip splat;

	private Button button { get { return GetComponent<Button>(); } }
	private AudioSource source { get { return GetComponent<AudioSource> (); } }

	public void PlaySound(){
		source.PlayOneShot(sound);
	}

	public void PlayLevelSelectSound(){
		source.PlayOneShot (soundLevel);
	}

	public void take2Sound(){
		var prevPitch = source.pitch;
		source.pitch = 1.0f;
		source.PlayOneShot (take2);
		source.pitch = prevPitch;
	}

	public void restartingTheLevel(){
		var prevPitch = source.pitch;
		source.pitch = 1.0f;
		source.PlayOneShot (resarting);
		source.pitch = prevPitch;
	}

	public void splatSound(){
		var prevPitch = source.pitch;
		source.pitch = 1.0f;
		source.PlayOneShot (splat);
		source.pitch = prevPitch;
	}
}
