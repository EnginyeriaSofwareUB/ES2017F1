using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainController : MonoBehaviour {
	GameObject rain;
	Sprite rainSprite;
	float elapsedTime;
	float goalTime;



	// Use this for initialization
	void Start () {
		rain = GameObject.Find("Rain");
		rainSprite = rain.GetComponent<SpriteRenderer>().sprite;
		elapsedTime = 0f;
		goalTime = 30f;
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= goalTime){
			elapsedTime = 0f;
			goalTime = Random.Range(30f, 200f);
			if (rain.GetComponent<SpriteRenderer>().sprite == null){
				rain.GetComponent<SpriteRenderer>().sprite = rainSprite;
			}else{
				rain.GetComponent<SpriteRenderer>().sprite = null;
			}
		}
	}
}
