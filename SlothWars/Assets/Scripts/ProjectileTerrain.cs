using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTerrain : Projectile {

    // Use this for initialization
    private float scale = 1;
    private float nCubes = 1;
    private Vector3 direction;
    private Vector3 position = new Vector3();
    private Vector3 AimVector;
    private bool apply = false;
    private GameObject mark;
    private bool created = false;

	public ProjectileTerrain(Ability a){
		ability = a;
	}

    public override void Mark()
    {
        Plane playerPlane = new Plane(Vector3.forward, new Vector3(0,0,0));
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            ray.GetPoint(hitdist);
            position = ray.GetPoint(hitdist);
            for (int i = 0; i < nCubes; i++)
            {
                Collider[] sloths = Physics.OverlapBox(position +  i * scale * new Vector3(0, 1, 0), new Vector3(1, 1, 1) * (scale-0.001f)/ 2f);
                apply = true;
                foreach (Collider c in sloths)
                {
                    if(c.tag == "sloth" || c.tag == "Destroyable")
                    {
                        apply = false;
                        break;
                    }
                }
                if (!apply) { break; }
            }
            if (!created)
            {
                mark = (GameObject)GameObject.Instantiate(Resources.Load("Objects/Mark"), position + new Vector3(0, 1, 0) *nCubes* scale / 2, Quaternion.identity);
                mark.gameObject.transform.localScale = new Vector3(1, 1, 1) * scale + (nCubes - 1) * new Vector3(0, 1, 0)*scale;
                created = true;
            }
            else
            {
                mark.transform.position = position + new Vector3(0, 1, 0) * (nCubes-1) * scale / 2;
                if (apply)
                {
                    mark.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/green_mark");
                }
                else
                {
                    mark.GetComponent<Renderer>().material = (Material)Resources.Load("Materials/red_mark");
                }
            }
        }
        else if (created)
        {
            //Debug.Log(hit.transform.position.y + hit.transform.localScale.y + " " + hit.point.y);
            created = false;
            GameObject.Destroy(mark);
        }
    }
    public override void ApplyLogic()
    {
        GameObject.Destroy(mark);
		ability.Apply (position);
	}
	
	public override void SetAll(Vector3 position, Vector3 aimVector, Quaternion rotation, float range, float radius,bool explosive,string source)
    {
        nCubes = radius;
    }
    public override bool GetApply(){return apply;}
    public override void CalcelMark()
    {
        apply = false;
        GameObject.Destroy(mark);
        mark = null;
    }
}