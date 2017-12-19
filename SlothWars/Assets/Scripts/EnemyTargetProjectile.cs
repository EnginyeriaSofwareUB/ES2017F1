using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetProjectile : Projectile
{
    private Vector3 position;
    private GameObject mark = null;
    private string resource;
    GameObject target;
    private bool apply = false;
    public string link = "Objects/LightningBolt/DecrementApLightning";
    private RangeMark rangeMaker;

    public EnemyTargetProjectile(Ability a)
    {
        ability = a;
    }

    public override void ApplyLogic()
    {
        GameObject.Destroy(mark);
        ability.Apply(target);
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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.GetComponent<Sloth>().GetTeam() != Camera.main.GetComponent<GameController>().GetCurrentSloth().GetTeam()&& (hit.collider.transform.position - position).magnitude <= ability.GetRange())
        {
            apply = true;
            target = hit.collider.gameObject;
            if (mark == null)
            {
                mark = (GameObject)GameObject.Instantiate(Resources.Load(link), position, Quaternion.identity);
                mark.GetComponentInChildren<Transform>().Find("LightningStart").position = position;
                mark.GetComponentInChildren<Transform>().Find("LightningEnd").position = hit.transform.position;
            }
            else
            {
                mark.GetComponentInChildren<Transform>().Find("LightningEnd").position = hit.transform.position;
            }

        }
        else
        {
            apply = false;
            GameObject.Destroy(mark);
            mark = null;

        }

    }
    public override bool GetApply()
    {
        return apply;
    }
    public override void CalcelMark()
    {
        apply = false;
        GameObject.Destroy(mark);
        mark = null;
        rangeMaker.DestroyCubeMarks();
    }
}
