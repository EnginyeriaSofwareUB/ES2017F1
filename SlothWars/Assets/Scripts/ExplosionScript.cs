using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    private Vector3 origin = new Vector3();

    private float range = 10;
    public string namePath = "";
    private bool onCollision = true;
    private float radius = 0.5f;
    public bool xy = false; //if trajectory component z is zero
    private bool colided = false;
    public bool stopOnColision = false;
    private Vector3 dir;
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
    private void OnTriggerEnter(Collider other)
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
            if ("sloth".Equals(currentCollider.tag))
            {
				gameObject.GetComponent<AbilityContainer>().GetAbility().Apply(currentCollider.gameObject);
            }
            else if ("Destroyable".Equals(currentCollider.tag))
            {
				if (gameObject.GetComponent<AbilityContainer> ().GetAbility ().GetAlterTerrain ()) {
					Camera.main.gameObject.GetComponent<GameController> ().DestroyTerrain (currentCollider.gameObject);
				}
                //abilityController.ApplyDestroyTerrainAbility(currentCollider.gameObject);
            }
            i++;
        }

        explosion = (GameObject)Instantiate(Resources.Load(namePath), this.transform.position, this.transform.rotation);
        Destroy(explosion, 3);
        if (stopOnColision)
        {
            this.transform.position += dir * 0.3f;
            this.transform.position += new Vector3(0, 0, 0.1f);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<SphereCollider>().enabled = false;
            Destroy(this.gameObject, 20);
            this.tag = "Untagged";
            this.enabled = false;
            foreach(LineRenderer l in GetComponentsInChildren<LineRenderer>())
            {
                l.gameObject.SetActive(false);
            }
            foreach (ParticleSystem s in GetComponentsInChildren<ParticleSystem>())
            {
                s.Stop();
            }
        }
        else
        {
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
    public void SetDirection(Vector3 v)
    {
        dir = v;
    }

}