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

	public AbilityController()
    {
    }

    private void Awake()
    {
        abilityModel = new AbilityModel();
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
    public void TriggerAbility1()
    {
        GameObject s = TurnController.Instance.GetActiveSloth();
        s.GetComponent<ShotScript>().Shot(s.GetComponent<AnimPlayer>().sloth.GetAbility1());
    }

    public void TriggerAbility2()
    {
        GameObject s = TurnController.Instance.GetActiveSloth();
        s.GetComponent<ShotScript>().Shot(s.GetComponent<AnimPlayer>().sloth.GetAbility2());
    }

    public void TriggerAbility3()
    {
        GameObject s = TurnController.Instance.GetActiveSloth();
        s.GetComponent<ShotScript>().Shot(s.GetComponent<AnimPlayer>().sloth.GetAbility3());
    }
}