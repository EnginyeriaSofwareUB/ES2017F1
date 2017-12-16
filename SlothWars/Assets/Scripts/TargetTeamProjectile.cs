using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTeamProjectile : Projectile
{
    AbilityController abilityController = AbilityController.Instance;
    private Vector3 position;
    private GameObject mark = null;
    private string resource;
    GameObject target;
    private bool apply =false;
    public void ApplyLogic()
    {
        GameObject.Destroy(mark);
        abilityController.ApplyLastAbility(target);
    }

    // Update is called once per frame
    // needed to set initial parameters
    public void SetAll(Vector3 positon, Vector3 aimVector, Quaternion rotation, float range, float radius, bool explosive, string source)
    {
        this.position = positon;
        resource = source;
    }
    public void Mark()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.GetComponent<Sloth>().GetTeam() == Camera.main.GetComponent<GameController2>().GetCurrentSloth().GetTeam())
        {
            apply = true;
            target = hit.collider.gameObject;
            if (mark == null)
            {
                mark = (GameObject)GameObject.Instantiate(Resources.Load(resource), hit.transform.position + new Vector3(0, 0, -0.5f), Quaternion.identity);
            }
            else
            {
                mark.transform.position = hit.transform.position + new Vector3(0, 0, -0.5f);
            }

        }
        else
        {
            apply = false;
            GameObject.Destroy(mark);
            mark = null;

        }

    }
    public bool GetApply()
    {
        return apply;
    }
}