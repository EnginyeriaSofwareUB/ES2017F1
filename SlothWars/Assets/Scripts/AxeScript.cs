using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour
{
    private Vector3 torque = new Vector3(-3 * 360, 0, 0);
    private Rigidbody rb;
    private Vector3 dir;
    private float rot = -60;
    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(torque * Time.deltaTime);
        if(this.transform.position.z >= 1.5f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sloth")
        {
			gameObject.GetComponent<AbilityContainer>().GetAbility().Apply(other.gameObject);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "sloth")
        {
			gameObject.GetComponent<AbilityContainer>().GetAbility().Apply(collision.gameObject);
            Destroy(this.gameObject);
        }
        else
        { 
            if (collision.transform.position.z == 1)
            {
                Vector3 v = transform.TransformPoint(this.GetComponent<CapsuleCollider>().center);
                this.transform.rotation = Quaternion.Euler(0, 180 + rot, 0);
                v -= transform.TransformPoint(this.GetComponent<CapsuleCollider>().center);
                this.transform.position += v;
                this.transform.position += new Vector3(0,0,0.05f);
            }
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            GetComponent<CapsuleCollider>().enabled = false;
            rb.useGravity = false;
            Destroy(this.gameObject, 20);
            this.tag = "Untagged";
            this.enabled = false;
        }
    }

    public void SetDirection(Vector3 v)
    {
        dir = v;
        if (v.x >= 0)
        {
            rot = 60;
            this.transform.Rotate(0, 180, 0);
        }
    }
}