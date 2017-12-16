using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTeamProjectile : Projectile
{
    private Vector3 position;
    private GameObject mark = null;
    private string resource;
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
		if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.GetComponent<Sloth>().GetTeam() == gameController.GetActualTeam())
        {
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
            GameObject.Destroy(mark);
            mark = null;

        }

    }
    public bool GetApply()
    {
		GameController2 gameController = GameObject.Find ("Main Camera").GetComponent<GameController2> ();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		return (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.GetComponent<Sloth>().GetTeam() == gameController.GetActualTeam());
    }
}