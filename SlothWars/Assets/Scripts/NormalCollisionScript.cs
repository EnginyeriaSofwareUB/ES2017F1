using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCollisionScript : MonoBehaviour
{
    private Vector3 torque;
    private Rigidbody rb;
    private Vector3 dir;
    private float rot = 90;
    public bool rotative = false;
    private bool colided = false;
    public bool capsuleCol = false;
    AbilityController abilityController = AbilityController.Instance;
    bool target = false;
    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotative)
        {
            this.transform.Rotate(torque * Time.deltaTime);
        }
        if(this.transform.position.z >= 1.5f)
        {
            Destroy(this.gameObject);
        }
    }
    void FixedUpdate()
    {
        /*if (rotative) {
            rb.AddTorque (torque);
        } */
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sloth")
        {
            if (!target)
            {
                AbilityController.Instance.ApplyLastAbility(other.gameObject);
                target = true;
            }
        }
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "sloth")
        {
            if (!target)
            {
                AbilityController.Instance.ApplyLastAbility(collision.gameObject);
                target = true;
            }
        }
        if (!colided)
        {
            colided = true;
            //abilityController.ApplyLastAbility(collision.gameObject);
            this.transform.position += dir*0.3f;
            if (rotative)
            {
                Vector3 v = transform.TransformPoint(this.GetComponent<CapsuleCollider>().center);
                //this.transform.Rotate(0, 90, 0);
                //this.transform.rotation = Quaternion.Euler(0, rot, 0);
                this.transform.rotation = Quaternion.Euler(0, rot + Random.Range(-20, 20), 0);
                v -= transform.TransformPoint(this.GetComponent<CapsuleCollider>().center);
                this.transform.position += v;
            }
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            if (capsuleCol)
            {
                GetComponent<CapsuleCollider>().enabled = false;
            }
            else
            {
                GetComponent<SphereCollider>().enabled = false;
            }
            rotative = false;
            rb.useGravity = false;
            Destroy(this.gameObject, 20);
            this.tag = "Untagged";
        }
    }

    public void SetDirection(Vector3 v)
    {
        dir = v;
        if (rotative)
        {
            if (v.x >= 0)
            {
                this.transform.Rotate(0, 180, 0);
                torque = new Vector3(-3 * 360, 0, 0);
                rot = 180;
            }
            else
            {
                torque = new Vector3(-3 * 360, 0, 0);
                rot = 180;
            }
        }
    }
}