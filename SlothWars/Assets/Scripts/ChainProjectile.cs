using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainProjectile : Projectile {
    // Use this for initialization
    public int lnumber = 3;
    private float range = 20f;
    public string lpathR = "Objects/LightningBolt/LightningBoltRed";
	public string lpathG = "Objects/LightningBolt/LightningBoltGreen";
    private Vector3 position;
	GameObject mark = null;
    private bool apply = false;
    bool g;
	public ChainProjectile(Ability a){
		ability = a;
	}

    public override void ApplyLogic()
    {
		GameObject.Destroy (mark);
		RaycastHit hit;
		GameObject lightning;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8)) {
			List<GameObject> sloths = new List<GameObject> (GameObject.FindGameObjectsWithTag ("sloth"));
			sloths.Remove (hit.collider.gameObject);
			if (hit.collider.gameObject.GetComponent<Sloth> ().GetTeam () == Camera.main.GetComponent<GameController>().GetCurrentSloth().GetTeam()) {
				lightning = (GameObject)GameObject.Instantiate (Resources.Load (lpathG), position, Quaternion.identity);
				mark = (GameObject)GameObject.Instantiate (Resources.Load ("Objects/HealG"), hit.transform.position, Quaternion.identity);
			} 
			else {
				lightning = (GameObject)GameObject.Instantiate (Resources.Load (lpathR), position, Quaternion.identity);
				mark = (GameObject)GameObject.Instantiate (Resources.Load ("Objects/HealR"), hit.transform.position, Quaternion.identity);
            }
			lightning.GetComponentInChildren<Transform> ().Find ("LightningStart").position = position;
			lightning.GetComponentInChildren<Transform> ().Find ("LightningEnd").position = hit.transform.position;
			GameObject.Destroy (lightning, 3f);
			GameObject.Destroy (mark, 3f);
			position = hit.transform.position;
			ability.Apply(hit.collider.gameObject);
			for (int i = 0; i < lnumber-1; i++) {
				sloths.Sort (delegate (GameObject c1, GameObject c2) {
					return Vector3.Distance (position, c1.transform.position).CompareTo
						((Vector3.Distance (position, c2.transform.position)));
				});
				if ((position - sloths[0].transform.position).magnitude < range) {
					if (sloths[0].GetComponent<Sloth> ().GetTeam () == Camera.main.GetComponent<GameController>().GetCurrentSloth().GetTeam()) {
						lightning = (GameObject)GameObject.Instantiate (Resources.Load (lpathG), position, Quaternion.identity);
						mark = (GameObject)GameObject.Instantiate (Resources.Load ("Objects/HealG"), sloths [0].transform.position, Quaternion.identity);
                    } 
					else {
						lightning = (GameObject)GameObject.Instantiate (Resources.Load (lpathR), position, Quaternion.identity);
						mark = (GameObject)GameObject.Instantiate (Resources.Load ("Objects/HealR"), sloths [0].transform.position, Quaternion.identity);
                    }
					lightning.GetComponentInChildren<Transform> ().Find ("LightningStart").position = position;
					lightning.GetComponentInChildren<Transform> ().Find ("LightningEnd").position = sloths [0].transform.position;
					GameObject.Destroy (lightning, 3f);
					GameObject.Destroy (mark, 3f);
					position = sloths [0].transform.position;
					ability.Apply(sloths[0]);
					sloths.Remove (sloths [0]);
				} else {
					break;
				}
			}
		}
    }
	
	// Update is called once per frame
	// needed to set initial parameters
	public override void SetAll(Vector3 positon, Vector3 aimVector, Quaternion rotation, float range, float radius,bool  explosive,string source)
	{
		this.position = positon;
	}
	public override void Mark() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8)) {
            apply = true;
            bool cond = hit.collider.gameObject.GetComponent<Sloth>().GetTeam() == Camera.main.GetComponent<GameController>().GetCurrentSloth().GetTeam();
            if (mark != null)
            {
                if (g && !cond || !g && cond)
                {
                    GameObject.Destroy(mark);
                }
                else
                {
                    mark.transform.position = hit.transform.position;
                }
            }
            else if (cond)
            {
                mark = (GameObject)GameObject.Instantiate(Resources.Load("Objects/HealG"), hit.transform.position, Quaternion.identity);
                g = true;
            }
            else
            {
                mark = (GameObject)GameObject.Instantiate(Resources.Load("Objects/HealR"), hit.transform.position, Quaternion.identity);
                g = false;
            }
		} 
		else if (mark != null){
            apply = false;
            GameObject.Destroy (mark);
			mark = null;
		}

	}
	public override bool GetApply() {
		return apply;
	}
}
