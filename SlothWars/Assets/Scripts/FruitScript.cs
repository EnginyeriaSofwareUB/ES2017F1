using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour {
    private double healFruit;
    private int apFruit;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sloth")
        {
            other.gameObject.GetComponent<Sloth>().Fruit(healFruit,apFruit);
        }
        Destroy(this.gameObject);

    }
    

    public void SetParams(double h,int a)
    {
        healFruit = h;
        apFruit = a;
    }

}
