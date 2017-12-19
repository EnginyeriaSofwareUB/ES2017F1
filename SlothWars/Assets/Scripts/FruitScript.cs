using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour {
    private double healFruit;
    private int apFruit;
    private Transform cube;
	// Use this for initialization
	void Start () {
        //cube = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if(cube == null)
        {
            Rigidbody rb = this.GetComponent<Rigidbody>();
            rb.useGravity = true;
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sloth")
        {
            other.gameObject.GetComponent<Sloth>().Fruit(healFruit, apFruit);
        }
        else if (other.gameObject.tag == "DeathZone") { Destroy(this.gameObject); }
        else
        {
            Rigidbody rb = this.GetComponent<Rigidbody>();
            rb.useGravity = true;
        }
    }
    

    public void SetParams(double h,int a)
    {
        healFruit = h;
        apFruit = a;
    }
    public void SetCube(Transform t)
    {
        cube = t;
    }

}
