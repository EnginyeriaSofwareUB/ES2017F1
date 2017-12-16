using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillArrow : MonoBehaviour
{
    private Rigidbody rb;
    bool firstTarget =false;
    Vector3 dir;
    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        Debug.Log("vel"+ dir);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("vel" + rb.velocity);
        if (this.transform.position.z >= 1.5f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "sloth")
        {
			gameObject.GetComponent<AbilityContainer>().GetAbility().Apply(other.gameObject);
            if (!firstTarget)
            {
                firstTarget = true;
                RemoveWindEffect();

            }
            else { Destroy(this.gameObject); }
        }
        else
        {
            if (!firstTarget)
            {
                firstTarget = true;
                RemoveWindEffect();
                if (other.gameObject.tag == "Destroyable")
                {
                    Destroy(other.gameObject);
                }
            }
            else
            {
                this.transform.position += dir * 0.3f;
                this.transform.position += new Vector3(0, 0, 0.1f);
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                GetComponent<SphereCollider>().enabled = false;
                Destroy(this.gameObject, 20);
                this.tag = "Untagged";
                this.enabled = false;
            }
        }
    }
    private void RemoveWindEffect()
    {
        foreach (ParticleSystem s in GetComponentsInChildren<ParticleSystem>())
        {
            s.Stop();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "sloth")
        {
			
			gameObject.GetComponent<AbilityContainer>().GetAbility().Apply(collision.gameObject);
            if (!firstTarget)
            {
                firstTarget = true;
                RemoveWindEffect();
            }
            else { Destroy(this.gameObject); }
        }
        else
        {
            if (!firstTarget)
            {
                firstTarget = true;
                RemoveWindEffect();
                if(collision.gameObject.tag == "Destroyable")
                {
                    Destroy(collision.gameObject);
                }
            }
            else
            {
                this.transform.position += dir*0.3f;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                GetComponent<SphereCollider>().enabled = false;
                Destroy(this.gameObject, 20);
                this.tag = "Untagged";
                this.enabled = false;
            }
        }
    }

    public void SetDirection(Vector3 v)
    {
        dir = v;
    }
}
