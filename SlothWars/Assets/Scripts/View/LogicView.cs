using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicView : MonoBehaviour {

    public static LogicView lv;

    private LogicModel logicModel;
    private AbilityModel abilityModel;
    private List<GameObject> teamSloths1, teamSloths2;
    private ChangeTurnModel changeTurnModel;
    
    // Use this for initialization
	private void Start () {
        logicModel = LogicModel.Instance;
        abilityModel = AbilityModel.Instance;
        changeTurnModel = ChangeTurnModel.Instance;

        teamSloths1 = changeTurnModel.GetTeam1();
        teamSloths2 = changeTurnModel.GetTeam2();

        lv = this;
	}
	
	// Update is called once per frame
	private void Update () {
        if (teamSloths1 != null && teamSloths2 != null)
        {
            CheckGameOver();
            DeactivateButtonsIfNecessary();
            teamSloths1 = changeTurnModel.GetTeam1();
            teamSloths2 = changeTurnModel.GetTeam2();
        }
	}

    public void DestroySlothSafely(GameObject g)
    {
        bool b1 = teamSloths1.Remove(g);
        bool b2 = teamSloths2.Remove(g);
        Debug.Log(b1 + "check " + b2);
        g.GetComponent<AnimPlayer>().Die();
    }

    public void CheckSlothAlive(GameObject g)
    {
        if (g.GetComponent<HealthScript>().getHealth() <= 0)
        {
            DestroySlothSafely(g);   
        }
        double health;
        AnimPlayer pla;
        int i = 0;
        foreach (GameObject sloth in teamSloths1)
        {
            //health = StorePersistentVariables.Instance.slothTeam1[i].GetHp();
            health = 1;
            
            if (health <= 0)
            {
                pla = sloth.GetComponent<AnimPlayer>();
                pla.Die();
                teamSloths1.Remove(sloth);
               
                //REMOVE LISTGUN
            }
            i++;
        }
        i = 0;
        foreach (GameObject sloth in teamSloths2)
        {
            //health = StorePersistentVariables.Instance.slothTeam1[i].GetHp();
            health = 1;

            if (health <= 0)
            {
                pla = sloth.GetComponent<AnimPlayer>();
                pla.Die();
                teamSloths2.Remove(sloth);
                //REMOVE LISTGUN
            }
            i++;
        }
        changeTurnModel.SetTeams(teamSloths1,teamSloths2);
    }

    private void CheckGameOver(){
        if(teamSloths1.Count == 0){
            //StorePersistentVariables.Instance.winner = "Blue";
            SceneManager.LoadScene("GameOverScene");
        } else if(teamSloths2.Count == 0){
            //StorePersistentVariables.Instance.winner = "Red";
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void DeactivateButtonsIfNecessary(){
        int ab1ap = changeTurnModel.GetCurrentSloth().GetAbility1().GetAp();
        int ab2ap = changeTurnModel.GetCurrentSloth().GetAbility2().GetAp();
        int ab3ap = changeTurnModel.GetCurrentSloth().GetAbility3().GetAp();
        
        abilityModel.DeactivateButtonsIfNecessary(ab1ap, ab2ap, ab3ap, changeTurnModel.GetApCurrentSloth());
    }
}
