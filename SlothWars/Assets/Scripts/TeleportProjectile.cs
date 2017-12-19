using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportProjectile : Projectile
{
    private Vector3 position;
    private GameObject mark = null;
    private string resource;
    private bool firstCast = true;
    private bool apply = false;
    GameObject target;
    private RangeMark rangeMaker;

    public TeleportProjectile(Ability a){
		ability = a;
	}

    public override void ApplyLogic()
    {
        target.transform.parent.position = mark.transform.position+ new Vector3(0, 0, 0.5f);
        GameObject explosion = (GameObject)GameObject.Instantiate(Resources.Load("Objects/SmokeExplosion"), target.transform.position + new Vector3(0,0, 0.5f), Quaternion.identity);
        GameObject.Destroy(explosion, 3);
        GameObject.Destroy(mark);
        rangeMaker.DestroyCubeMarks();
    }

    // Update is called once per frame
    // needed to set initial parameters
	public override void SetAll(Vector3 positon, Vector3 aimVector, Quaternion rotation, float range, float radius, bool explosive, string source)
    {
        this.position = positon;
        resource = source;
        rangeMaker = new RangeMark();
        rangeMaker.MakeCubeMarks(ability.GetRange(), position);
    }
    public override void Mark()
    {
        if (firstCast)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.GetComponent<Sloth>().GetTeam() == Camera.main.GetComponent<GameController>().GetCurrentSloth().GetTeam()&& (hit.collider.transform.position - position).magnitude <= ability.GetRange())
            {
                apply = true;
                if (mark == null)
                {
                    mark = (GameObject)GameObject.Instantiate(Resources.Load(resource), hit.transform.position + new Vector3(0, 0, -0.5f), Quaternion.identity);
                }
                else
                {
                    mark.transform.position = hit.transform.position + new Vector3(0, 0, -0.5f);
                }
                target = hit.collider.gameObject;

            }
            else
            {
                apply = false;
                GameObject.Destroy(mark);
                mark = null;
            }
        }
        else
        {
            apply = true;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 9)&& hit.transform.position.z == 1 &&(hit.collider.transform.position + new Vector3(0, 0, -1) - position).magnitude <= ability.GetRange())
            {
                Collider[] colls = Physics.OverlapBox(hit.transform.position + new Vector3(0,0,-1f), new Vector3(1, 1, 1) * (1 - 0.001f) / 2f);
                foreach(Collider c in colls)
                {
                    if(c.gameObject.tag == "sloth" || c.gameObject.tag == "Destroyable")
                    {
                        apply = false;
                        break;
                    }
                }
            }
            else { apply = false; }
            if (apply)
            {
                if (mark == null)
                {
                    mark = (GameObject)GameObject.Instantiate(Resources.Load(resource), hit.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
                }
                else
                {
                    mark.transform.position = hit.transform.position + new Vector3(0, 0, -1);
                }
            }
            else
            {
                GameObject.Destroy(mark);
                mark = null;
            }
        }
    }
    public override bool GetApply()
    {
        if (apply && firstCast)
        {
            firstCast = false;
            GameObject.Destroy(mark);
            mark = null;
            rangeMaker.DestroyCubeMarks();
            rangeMaker.MakeCubeMarks(ability.GetRange(), target.transform.position);
            resource = "Objects/TPMark";
            return false;
        }
        else if(!firstCast && apply)
        {
            rangeMaker.DestroyCubeMarks();
            return true;
        }
        else { return false; }
    }
    public override void CalcelMark()
    {
        apply = false;
        GameObject.Destroy(mark);
        mark = null;
        rangeMaker.DestroyCubeMarks();
    }
}
