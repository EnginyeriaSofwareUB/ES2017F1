using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathProjectile : Projectile
{

    // Use this for initialization
    private float scale = 1;
    private float nCubes = 1;
    private Vector3 direction;
    private bool apply = false;
    private GameObject mark;
    private bool created = false;
    private GameObject Floor;

	public PathProjectile(Ability a){
		ability = a;
	}

    public override void Mark()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 9) &&  hit.transform.position.z == 1)
        {
            Vector3 d = hit.point -hit.transform.position;
            float max = Mathf.Max(Mathf.Abs(d.x), Mathf.Abs(d.y));
            if (max == Mathf.Abs(d.y))
            {
                if (d.y > 0) d = Vector3.up;
                else { d = Vector3.down; }
            }
            else
            {
                if (d.x > 0) d = Vector3.right;
                else { d = Vector3.left; }
            }
            Collider[] cubes = Physics.OverlapBox(hit.transform.position + 0 * scale * d + d * scale, new Vector3(1, 1, 1) * (scale - 0.001f) / 2f);
            apply = true;
            foreach (Collider c in cubes)
            {
                if(c.tag == "Destroyable")
                {
                    apply = false;
                    break;
                }
            }
            this.Floor = hit.collider.gameObject;
            direction = d;
            if (!created)
            {
                mark = (GameObject)GameObject.Instantiate(Resources.Load("Objects/Mark"), hit.transform.position + d * scale / 2 + d * nCubes * scale / 2 + new Vector3(0, 0, -0.01f), Quaternion.identity);
                if (d.y < 0)
                {
                    d.y *= -1;
                }
                if (d.x < 0)
                {
                    d.x *= -1;
                }
                mark.gameObject.transform.localScale = new Vector3(1, 1, 1) * scale + (nCubes - 1) * d * scale;
                created = true;
            }
            else
            {
                mark.transform.position = hit.transform.position + d * scale / 2 + d * nCubes * scale / 2 + new Vector3(0,0,-0.01f);
                if (d.y < 0)
                {
                    d.y *= -1;
                }
                if (d.x < 0)
                {
                    d.x *= -1;
                }
                mark.gameObject.transform.localScale = new Vector3(1, 1, 1) * scale + (nCubes - 1) * d * scale;
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
            created = false;
            GameObject.Destroy(mark);
        }
    }
    public override void ApplyLogic()
    {
        GameObject.Destroy(mark);
        ability.Apply(direction);
        ability.Apply(Floor);
        //AbilityController.Instance.ApplyLastAbility(direction);
        //AbilityController.Instance.ApplyLastAbility(Floor);
    }
    public override void SetAll(Vector3 position, Vector3 aimVector, Quaternion rotation, float range, float radius, bool explosive, string source)
    {
        nCubes = radius;
    }
    public override bool GetApply() { return apply; }
}