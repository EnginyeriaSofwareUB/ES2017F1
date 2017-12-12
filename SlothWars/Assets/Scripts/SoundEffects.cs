using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {
	private AudioSource aSource;
	private AudioClip[] launcher;
	private AudioClip[] explosions;
	// Use this for initialization
	void Start () {
		Debug.Log ("hola");
		aSource = GameObject.Find ("sfx_source").GetComponent<AudioSource>();
			
		launcher = Resources.LoadAll<AudioClip>("Sound/Effects/launcher");
		explosions = Resources.LoadAll<AudioClip>("Sound/Effects/explosions");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playLaunchEffect(){
		int rand = Random.Range (0, launcher.Length);
		aSource.volume = 0.5f;
		aSource.PlayOneShot (launcher [rand]);
	}

	public void playExplosionEffect(){
		int rand = Random.Range (0, explosions.Length);
		aSource.volume = 0.5f;
		aSource.PlayOneShot (explosions [rand]);
	}

}
