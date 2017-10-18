using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {
    private float mass = 1;
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().mass = this.mass;
        Destroy(this.gameObject,5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "sloth")
        {
            collision.gameObject.SendMessage("TakeDamage", 20);
            Destroy(this.gameObject);
        }
    }
}