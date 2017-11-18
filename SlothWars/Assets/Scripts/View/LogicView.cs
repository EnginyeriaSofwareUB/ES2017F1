using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicView : MonoBehaviour {

    private LogicModel logicModel;
    private AbilityModel abilityModel;
    private List<GameObject> teamSloths1, teamSloths2;
    private List<Transform> listGunTeam1, listGunTeam2;
    
    // Use this for initialization
	private void Start () {
        logicModel = new LogicModel();
        abilityModel = new AbilityModel();

        teamSloths1 = logicModel.GetTeamSloths1();
        teamSloths2 = logicModel.GetTeamSloths2();
        listGunTeam1 = abilityModel.GetGunTeam1();
        listGunTeam2 = abilityModel.GetGunTeam2();
	}
	
	// Update is called once per frame
	private void Update () {
        if (teamSloths1 != null && teamSloths2 != null)
        {
            CheckSlothsAlive();
        }
	}

    private void CheckSlothsAlive()
    {
        HealthScript health;
        AnimPlayer pla;
        int i = 0;
        foreach (GameObject sloth in teamSloths1)
        {
            health = sloth.GetComponent<HealthScript>();
            
            if (health.getHealth() <= 0)
            {
                pla = sloth.GetComponent<AnimPlayer>();
                pla.Die();
                teamSloths1.Remove(sloth);
                listGunTeam1.Remove(listGunTeam1[i]);
                //REMOVE LISTGUN
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
                listGunTeam2.Remove(listGunTeam2[i]);
            }
            i++;
        }
    }
}
