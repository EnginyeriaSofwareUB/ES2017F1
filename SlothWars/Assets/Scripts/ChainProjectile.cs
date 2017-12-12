using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainProjectile : Projectile {
    // Use this for initialization
    public int lnumber = 3;
    private float range = 20f;
    public string lpathR = "Objects/LightningBolt/LightningBoltRed";
	public string lpathG = "Objects/LightningBolt/LightningBoltGreen";
    AbilityController abilityController = AbilityController.Instance;
    private Vector3 position;
	GameObject mark = null;
    public void ApplyLogic()
    {
		GameObject.Destroy (mark);
		RaycastHit hit;
		GameObject lightning;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8)) {
			List<GameObject> sloths = new List<GameObject> (GameObject.FindGameObjectsWithTag ("sloth"));
			sloths.Remove (hit.collider.gameObject);
			if (hit.collider.gameObject.GetComponent<AnimPlayer> ().GetSloth ().GetTeam () == TurnController.Instance.GetActualTurnTeam()) {
				lightning = (GameObject)GameObject.Instantiate (Resources.Load (lpathG), position, Quaternion.identity);
				mark = (GameObject)GameObject.Instantiate (Resources.Load ("Objects/HealG"), hit.transform.position, Quaternion.identity);
			} 
			else {
				lightning = (GameObject)GameObject.Instantiate (Resources.Load (lpathR), position, Quaternion.identity);
				mark = (GameObject)GameObject.Instantiate (Resources.Load ("Objects/HealR"), hit.transform.position, Quaternion.identity);
			}
			lightning.GetComponentInChildren<Transform> ().Find ("LightningStart").position = position;
			lightning.GetComponentInChildren<Transform> ().Find ("LightningEnd").position = hit.transform.position + new Vector3 (0, 0.3f, 0);
			GameObject.Destroy (lightning, 3f);
			GameObject.Destroy (mark, 3f);
			position = hit.transform.position;
			abilityController.ApplyLastAbility (hit.collider.gameObject);
			for (int i = 0; i < lnumber-1; i++) {
				sloths.Sort (delegate (GameObject c1, GameObject c2) {
					return Vector3.Distance (position, c1.transform.position).CompareTo
						((Vector3.Distance (position, c2.transform.position)));
				});
				if ((position - sloths[0].transform.position).magnitude < range) {
					// GameControl.control.ApplyLastAbility(sloths[i]);
					abilityController.ApplyLastAbility (sloths [i]);
					//sloths[i].gameObject.SendMessage("SumToHP", -10);
					if (sloths[0].GetComponent<AnimPlayer> ().GetSloth ().GetTeam () == TurnController.Instance.GetActualTurnTeam()) {
						lightning = (GameObject)GameObject.Instantiate (Resources.Load (lpathG), position, Quaternion.identity);
						mark = (GameObject)GameObject.Instantiate (Resources.Load ("Objects/HealG"), sloths [0].transform.position, Quaternion.identity);
					} 
					else {
						lightning = (GameObject)GameObject.Instantiate (Resources.Load (lpathR), position, Quaternion.identity);
						mark = (GameObject)GameObject.Instantiate (Resources.Load ("Objects/HealR"), sloths [0].transform.position, Quaternion.identity);
					}
					lightning.GetComponentInChildren<Transform> ().Find ("LightningStart").position = position;
					lightning.GetComponentInChildren<Transform> ().Find ("LightningEnd").position = sloths [0].transform.position + new Vector3 (0, 0.3f, 0);
					GameObject.Destroy (lightning, 3f);
					GameObject.Destroy (mark, 3f);
					position = sloths [0].transform.position;
					abilityController.ApplyLastAbility (sloths [0]);
					sloths.Remove (sloths [0]);
				} else {
					break;
				}
			}
		}
    }
	
	// Update is called once per frame
	// needed to set initial parameters
	public void SetAll(Vector3 positon, Vector3 aimVector, Quaternion rotation, float range, float radius,bool  explosive,string source)
	{
		this.position = positon;
	}
	public void Mark() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8)) {
			if (hit.collider.gameObject.GetComponent<AnimPlayer> ().GetSloth ().GetTeam () == TurnController.Instance.GetActualTurnTeam () && mark == null) {
				mark = (GameObject)GameObject.Instantiate (Resources.Load ("Objects/HealG"), hit.transform.position, Quaternion.identity);
			} else if (mark == null) {
				mark = (GameObject)GameObject.Instantiate (Resources.Load ("Objects/HealR"), hit.transform.position, Quaternion.identity);
			} else if (mark != null) {
				mark.transform.position = hit.transform.position;
			}
		} 
		else if (mark != null){
			GameObject.Destroy (mark);
			mark = null;
		}

	}
	public bool GetApply() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		return Physics.Raycast (ray.origin, ray.direction, out hit, Mathf.Infinity, 1 << 8);
	}
}
