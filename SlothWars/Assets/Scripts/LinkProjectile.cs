using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkProjectile : Projectile
{
    AbilityController abilityController = AbilityController.Instance;
    private Vector3 position;
    private GameObject mark = null;
    private string resource;
    public string link = "Objects/LightningBolt/Link";
    public void ApplyLogic()
    {
        RaycastHit hit;
        GameObject.Destroy(mark);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.GetComponent<AnimPlayer>().GetSloth().GetTeam() == TurnController.Instance.GetActualTurnTeam())
        {
            abilityController.ApplyLastAbility(hit.collider.gameObject);

        }
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
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.GetComponent<AnimPlayer>().GetSloth().GetTeam() == TurnController.Instance.GetActualTurnTeam() && !hit.collider.gameObject.Equals(TurnController.Instance.GetActiveSloth()))
        {
            if (mark == null)
            {
                mark = (GameObject)GameObject.Instantiate(Resources.Load(link), position, Quaternion.identity);
                mark.GetComponentInChildren<Transform>().Find("LightningStart").position = position;
                mark.GetComponentInChildren<Transform>().Find("LightningEnd").position = hit.transform.position + new Vector3(0, 0.3f, 0);
            }
            else
            {
                mark.transform.position = hit.transform.position;
                mark.GetComponentInChildren<Transform>().Find("LightningStart").position = position;
                mark.GetComponentInChildren<Transform>().Find("LightningEnd").position = hit.transform.position + new Vector3(0, 0.3f, 0);
            }

        }
        else
        {
            GameObject.Destroy(mark);
            mark = null;

        }

    }
    public bool GetApply()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.GetComponent<AnimPlayer>().GetSloth().GetTeam() == TurnController.Instance.GetActualTurnTeam() && !hit.collider.gameObject.Equals(TurnController.Instance.GetActiveSloth()));
    }
}