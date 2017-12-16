using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkScript : MonoBehaviour {
    Transform origin =null;
    Transform end = null;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		if(origin !=null && end != null)
        {
            this.gameObject.GetComponentInChildren<Transform>().Find("LightningStart").position = origin.position;
            this.gameObject.GetComponentInChildren<Transform>().Find("LightningEnd").position = end.position;
        }
	}
    public void SetOrigin(Transform T)
    {
        origin = T;
        this.gameObject.GetComponentInChildren<Transform>().Find("LightningStart").position = origin.position;
    }
    public void SetEnd(Transform T)
    {
        end = T;
        this.gameObject.GetComponentInChildren<Transform>().Find("LightningEnd").position = end.position;
    }
}
