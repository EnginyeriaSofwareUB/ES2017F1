using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "sloth")
        {
            //collision.gameObject.GetComponent<Sloth>().Fruit(params)
        }
        Destroy(this.gameObject);
    }
    //public SetParams() { }

}
