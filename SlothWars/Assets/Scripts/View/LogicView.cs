using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicView : MonoBehaviour {

    private LogicModel logicModel;
    private List<GameObject> teamSloths1, teamSloths2;
    
    // Use this for initialization
	private void Start () {
        logicModel = new LogicModel();

        teamSloths1 = logicModel.GetTeamSloths1();
        teamSloths2 = logicModel.GetTeamSloths2();
	}
	
	// Update is called once per frame
	private void Update () {
        CheckSlothsAlive();
	}

    private void CheckSlothsAlive()
    {
        HealthScript health;
        AnimPlayer pla;
        int i = 1;
        foreach (GameObject sloth in teamSloths1)
        {
            health = sloth.GetComponent<HealthScript>();
            if (health.getHealth() <= 0)
            {
                pla = sloth.GetComponent<AnimPlayer>();
                pla.Die();
                teamSloths1.Remove(sloth);
            }
            i++;
        }
        i = 0;
        foreach (GameObject sloth in teamSloths2)
        {
            health = sloth.GetComponent<HealthScript>();
            if (health.getHealth() <= 0)
            {
                pla = sloth.GetComponent<AnimPlayer>();
                pla.Die();
                teamSloths2.Remove(sloth);
            }
            i++;
        }
    }
}
