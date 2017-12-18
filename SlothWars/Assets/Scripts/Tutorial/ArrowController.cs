using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour {
	private Vector3 target;
	private bool isWorld;
	private Sprite spriteArrow;
	private GameObject arrow;
	private Vector3 offset;
	private bool vertical;

	// Use this for initialization
	void Start () {
		arrow = GameObject.Find("RedArrow");
		spriteArrow = arrow.GetComponent<Image>().sprite;
		isWorld = true;
		target = new Vector3(22f, 12 + 0.05f, 0.5f);
		offset = new Vector3(0f, spriteArrow.bounds.size.y/2f, 0f);
		vertical = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(arrow.active){
			if(isWorld){
				arrow.transform.position = Camera.main.WorldToScreenPoint(target + offset);
			} else {
				arrow.transform.position = target + offset;
			}

			if(vertical){
				arrow.transform.position = arrow.transform.position + new Vector3(0f, 10f*Mathf.Sin(Time.time*3f), 0f);
			} else {
				arrow.transform.position = arrow.transform.position + new Vector3(10f*Mathf.Sin(Time.time*3f), 0f, 0f);
			}
		}
	}

	public void SetVertical(bool b){
		vertical = b;
	}

	public Vector3 GetTarget(){
		return target;
	}

	public void SetRotation(Vector3 r){
		arrow.transform.Rotate(r);
	}
	public void SetOffset(Vector3 o){
		offset = o;
	}

	public void SetTarget(Vector3 p){
		target = p;
	}

	public void SetIsWorld(bool b){
		isWorld = b;
	}

	public void SetArrowActive(bool b){
		arrow.SetActive(b);
	}
}
