using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	public GameObject emptyGameObjectPrefab;

	void Start(){
		Instantiate (emptyGameObjectPrefab, transform.position, Quaternion.identity);
	}
}
