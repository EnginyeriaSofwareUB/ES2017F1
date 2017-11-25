using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTerrain : Projectile {

    // Use this for initialization
    private float scale = 1;
    private float nCubes = 1;
    private Vector3 direction;
    private Vector3 position;
    private Vector3 AimVector;
    private bool apply = false;
    private GameObject mark;
    private bool created = false;
    private GameObject Floor;
    public void Mark()
    {
        RaycastHit hit;
        if (Physics.Raycast(position, AimVector, out hit,Mathf.Infinity,1<<8) && hit.transform.position.y + hit.transform.lossyScale.y != hit.point.y && hit.transform.position.z == 0)
        {
            this.Floor = hit.collider.gameObject;
            for (int i = 0; i < nCubes; i++)
            {
                Collider[] sloths = Physics.OverlapBox(hit.transform.position +  i * scale * new Vector3(0, 1, 0) +new Vector3(0, 1, 0) * scale, new Vector3(1, 1, 1) * (scale-0.001f)/ 2f);
                apply = true;
                foreach (Collider c in sloths)
                {
                    if(c.tag == "sloth" || c.tag == "WallCube")
                    {
                        apply = false;
                        break;
                    }
                }
                if (!apply) { break; }
            }
            if (!created)
            {
                Debug.Log(hit.point + new Vector3(0, 1, 0) *nCubes* scale / 2f+ " "+ new Vector3(0, 1, 0) * nCubes * scale / 2);
                mark = (GameObject)GameObject.Instantiate(Resources.Load("Objects/Mark"), hit.transform.position+ new Vector3(0,1,0)*scale/2 + new Vector3(0, 1, 0) *nCubes* scale / 2, Quaternion.identity);
                mark.gameObject.transform.localScale = new Vector3(1, 1, 1) * scale + (nCubes - 1) * new Vector3(0, 1, 0)*scale;
                created = true;
            }
            else
            {
                mark.transform.position = hit.transform.position + new Vector3(0, 1, 0) * scale / 2 + new Vector3(0, 1, 0) * nCubes * scale / 2;
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
    public void ApplyLogic()
    {
        GameObject.Destroy(mark);
        AbilityController.Instance.ApplyLastAbility(Floor);
    }
    public void SetAll(Vector3 position, Vector3 aimVector, Quaternion rotation, float range, float radius)
    {
        this.position = position;
        AimVector = aimVector;
        this.nCubes = radius;
    }
    public bool GetApply(){return apply;}
}