using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class AbilityController : MonoBehaviour {
    //SINGLETON
    private static AbilityController instance;
    private static Button buttonAbility1, buttonAbility2, buttonAbility3;
    private static ChangeTurnModel changeTurnModel;
    private static GameObject lastTarget;

    public static AbilityController Instance
    {
        get
        {
            if (instance == null)
            {
                //TODO: FIX THIS TO MAKE IT SINGLETON
                instance = new AbilityController();
            }
            return instance;
        }
    }
    ///////*****///////

    private static AbilityModel abilityModel;
    private Ability ability;

	public AbilityController(Button bnAbility1, Button bnAbility2, Button bnAbility3)
    {
        buttonAbility1 = bnAbility1;
        buttonAbility2 = bnAbility2;
        buttonAbility3 = bnAbility3;
        changeTurnModel  = new ChangeTurnModel();
        abilityModel = new AbilityModel();
    }

    //TODO: FIX THIS
    protected AbilityController(){

    }
    
    //sums dmg_heal to the hp bar asociated to target

    public void SumToHpBar(double dmg_Heal)
    {
        lastTarget.GetComponent<HealthScript>().SumToHP(Mathf.FloorToInt((float)dmg_Heal));
        GameObject.Find("GameController").GetComponent<LogicView>().CheckSlothAlive(lastTarget);
    }
    // sums residual during t turns
    public void SumResidual(GameObject target, Double residual, int t)
    {
        // GameObject-Sloth connection needed
    }
    // apply last ability to the target
    public void ApplyLastAbility(GameObject g)
    {
        ability = abilityModel.GetLastAbility();
        if (g.gameObject.tag == "sloth") {
            lastTarget = g;
            ability.Apply(ref g.GetComponent<AnimPlayer>().sloth);
        }
        else{ability.Apply(g); }
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

    public void DeactivateButtonsIfNecessary(int ab1ap, int ab2ap, int ab3ap, int currentAp){
        if(ab1ap > currentAp){
            buttonAbility1.interactable = false;
        }else{
            buttonAbility1.interactable = true;
        }
        if(ab1ap > currentAp){
            buttonAbility2.interactable = false;
        }else{
            buttonAbility2.interactable = true;
        }
        if(ab1ap > currentAp){
            buttonAbility3.interactable = false;
        }else{
            buttonAbility3.interactable = true;
        }
        
    }
}