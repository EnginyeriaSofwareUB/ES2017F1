using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBoltCaster : MonoBehaviour {

	// Use this for initialization
    public int lnumber= 3;
    private float range = 4.5f;
    public string lpath= "Objects/LightningBolt/SimpleLightningBoltPrefab";
    AbilityController abilityController = AbilityController.Instance;
    void Start () {
        List<GameObject> sloths = new List<GameObject>(GameObject.FindGameObjectsWithTag("sloth"));
        sloths.Sort(delegate (GameObject c1, GameObject c2) {
            return Vector3.Distance(this.transform.position, c1.transform.position).CompareTo
                        ((Vector3.Distance(this.transform.position, c2.transform.position)));
        });
        for(int i = 0; i < lnumber;i++)
        {
            if ((sloths[i].transform.position - this.transform.position).magnitude < range)
            {
                // GameControl.control.ApplyLastAbility(sloths[i]);
                abilityController.ApplyLastAbility(sloths[i]);
                //sloths[i].gameObject.SendMessage("SumToHP", -10);
                GameObject lightning = (GameObject)Instantiate(Resources.Load(lpath), this.transform.position, this.transform.rotation);
                lightning.GetComponentInChildren<Transform>().Find("LightningStart").position = this.transform.position;
                lightning.GetComponentInChildren<Transform>().Find("LightningEnd").position = sloths[i].transform.position+ new Vector3(0,0.3f,0);
                Destroy(lightning, 3f);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
