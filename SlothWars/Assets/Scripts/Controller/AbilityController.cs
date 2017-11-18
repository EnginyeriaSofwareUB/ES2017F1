using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class AbilityController : MonoBehaviour {
    //SINGLETON
    private static AbilityController instance;

    public static AbilityController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AbilityController();
            }
            return instance;
        }
    }
    ///////*****///////

    private static AbilityModel abilityModel;
    private Ability ability;
    private static List<Transform> listGunTeam1, listGunTeam2;

	public AbilityController(List<Transform> listGunTeam1Cont, List<Transform> listGunTeam2Cont)
    {
        listGunTeam1 = listGunTeam1Cont;
        listGunTeam2 = listGunTeam2Cont;
    }

    protected AbilityController(){}

    private void Awake()
    {
        abilityModel = new AbilityModel();
    }
    
    private void OnEnable()
    {
        abilityModel.SetGunTeams(listGunTeam1, listGunTeam2);
    }
  
    //sums dmg_heal to the hp bar asociated to target

    public void SumToHpBar(GameObject target, double dmg_Heal)
    {
        target.GetComponent<AnimPlayer>().GetSloth().SumToHp(dmg_Heal);
        target.GetComponent<HealthScript>().SumToHP(Mathf.FloorToInt((float)dmg_Heal));    
    }
    // sums residual during t turns
    public void SumResidual(GameObject target, Double residual, int t)
    {
        // GameObject-Sloth connection needed
    }
    // apply last ability to the target
    public void ApplyLastAbility(GameObject sloth)
    {
        ability = abilityModel.GetLastAbility();
        ability.Apply(ref sloth.GetComponent<AnimPlayer>().sloth);
    }
}
