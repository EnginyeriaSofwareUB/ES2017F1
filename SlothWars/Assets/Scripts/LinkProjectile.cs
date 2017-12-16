using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkProjectile : Projectile
{
    private Vector3 position;
    private GameObject mark = null;
    private string resource;
    public string link = "Objects/LightningBolt/Link";
    public void ApplyLogic()
    {
		GameController2 gameController = GameObject.Find ("Main Camera").GetComponent<GameController2> ();

        RaycastHit hit;
        GameObject.Destroy(mark);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.GetComponent<Sloth>().GetTeam() == gameController.GetActualTeam())
        {
            //abilityController.ApplyLastAbility(hit.collider.gameObject);

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
		GameController2 gameController = GameObject.Find ("Main Camera").GetComponent<GameController2> ();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.GetComponent<Sloth>().GetTeam() == gameController.GetActualTeam() && !hit.collider.gameObject.Equals(gameController.GetCurrentSloth()))
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
		GameController2 gameController = GameObject.Find ("Main Camera").GetComponent<GameController2> ();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		return (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.GetComponent<Sloth>().GetTeam() == gameController.GetActualTeam() && !hit.collider.gameObject.Equals(gameController.GetCurrentSloth()));
    }
}