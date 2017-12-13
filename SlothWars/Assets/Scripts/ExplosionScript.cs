using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private Vector3 origin = new Vector3();

    private float range = 10;
    public string namePath = "";
    private bool onCollision = true;
    AbilityController abilityController = AbilityController.Instance;
    public float DieIn = 2;
    private float radius = 0.5f;
    public int d_h = 20; // used while Sloth-gameobject conection doesnt exit
    public bool xy = false; //if trajectory component z is zero
    private bool colided = false;

    GameObject explosion;
    int secondLayerZ = 1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (xy && (this.GetComponent<Transform>().position - origin).magnitude >= range)
        {
            logicAndDestroy();
        }
        if (this.GetComponent<Transform>().position.z > secondLayerZ)
        {
            logicAndDestroy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (onCollision && !colided)
        {
            colided = true;
            Debug.Log("radius " + radius);
            logicAndDestroy();
            GameObject.FindGameObjectWithTag("soundManager").GetComponent<SoundEffects>().playExplosionEffect();
        }
    }

    void logicAndDestroy()
    {
        int effect_radius = 1;
        Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, effect_radius);
        Collider currentCollider;
        int i = 0;
        Debug.Log("Hits: " + hitColliders.Length);

        while (i < hitColliders.Length)
        {
            currentCollider = hitColliders[i];
            Debug.Log("[TRACE] collider tag: " + currentCollider.tag);
            if ("sloth".Equals(currentCollider.tag))
            {
                abilityController.ApplyLastAbility(currentCollider.gameObject);
            }
            else if ("Destroyable".Equals(currentCollider.tag))
            {
                Destroy(currentCollider.gameObject);
                abilityController.ApplyDestroyTerrainAbility(currentCollider.gameObject);
            }
            i++;
        }

        explosion = (GameObject)Instantiate(Resources.Load(namePath), this.transform.position, this.transform.rotation);
        Destroy(explosion, 3);
        Destroy(this.gameObject);
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