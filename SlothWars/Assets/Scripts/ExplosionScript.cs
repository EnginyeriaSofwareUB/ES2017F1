using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {
    private Vector3 origin = new Vector3();
    private float range = 10;
    public string namePath = "";
    private bool onCollision = true;
    public float DieIn= 2;
    private float radius = 0.5f;
    public int d_h = 20; // used while Sloth-gameobject conection doesnt exit
    public bool xy =false; //if trajectory component z is zero
    private bool colided = false;
    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {
       if (xy && (this.GetComponent<Transform>().position - origin).magnitude >= range)
        {
            GameObject explosion = (GameObject)Instantiate(Resources.Load(namePath), this.transform.position, this.transform.rotation);
            List<GameObject> sloths = new List<GameObject>(GameObject.FindGameObjectsWithTag("sloth"));
            foreach(GameObject s in sloths) {
                if ((s.transform.position - this.transform.position).magnitude <= radius)
                {
                    //GameControl.control.ApplyLastAbility(s);
                    s.gameObject.SendMessage("SumToHP", d_h);
                    Debug.Log("message send");
                }
            }
            Destroy(explosion, 3);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (onCollision){
            Debug.Log("radius "+radius);
            GameObject explosion = (GameObject)Instantiate(Resources.Load(namePath), this.transform.position, this.transform.rotation);
            List<GameObject> sloths = new List<GameObject>(GameObject.FindGameObjectsWithTag("sloth"));
            foreach (GameObject s in sloths)
            {
                if ((s.transform.position - this.transform.position).magnitude <= radius)
                {
                    //GameControl.control.ApplyLastAbility(s);
                    if (!colided)
                    {
                        s.gameObject.SendMessage("SumToHP", d_h);
                        Debug.Log("message send col");
                    }
                }
            }
            colided = true;
            Destroy(explosion, 3);
            Destroy(this.gameObject);
        }
    }
    public void SetOrigin(Vector3 p)
    {
        origin = p;
    }
    public void SetRange(float r)
    {
        range = r;
    }
    public void SetRadius(float r)
    {
        radius = r;
    }
}
