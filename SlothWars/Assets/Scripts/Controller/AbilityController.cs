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

    private static Ability ability;
    private static Button buttonAbility1, buttonAbility2, buttonAbility3;
    private static GameObject lastTarget;
    
    protected AbilityController(){

    }

    private void Start(){}
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
        ability = AbilityModel.Instance.GetLastAbility();
        if (g.gameObject.tag == "sloth") {
            lastTarget = g;
            //ability.Apply(ref g.GetComponent<AnimPlayer>().sloth);
            ability.Apply(g);
        }
        else{ability.Apply(g); }
    }
    public void ApplyLastAbility(Vector3 position)
    {
        AbilityModel.Instance.GetLastAbility().Apply(position);
    }
    // apply last ability to terrain blocks in range
    public void ApplyDestroyTerrainAbility(GameObject destroyable) {
        ability = AbilityModel.Instance.GetLastAbility();
        if (ability.GetAlterTerrain())
        {
            Destroy(destroyable);
        }
    }
    public void TriggerAbility1()
    {
        int id = ChangeTurnModel.Instance.GetId();
        int currentTurn = ChangeTurnModel.Instance.GetCurrentTurn();

        GameObject s = TurnController.Instance.GetActiveSloth();
        switch (id)
        {
            case 1:
                s.GetComponent<ShotScript>().Shot(StorePersistentVariables.Instance.slothTeam1[currentTurn].GetAbility1());
                break;
            case 2:
                s.GetComponent<ShotScript>().Shot(StorePersistentVariables.Instance.slothTeam2[currentTurn].GetAbility1());
                break;
        }
            
    }

    public void TriggerAbility2()
    {
        int id = ChangeTurnModel.Instance.GetId();
        int currentTurn = ChangeTurnModel.Instance.GetCurrentTurn();

        GameObject s = TurnController.Instance.GetActiveSloth();
        switch (id)
        {
            case 1:
                s.GetComponent<ShotScript>().Shot(StorePersistentVariables.Instance.slothTeam1[currentTurn].GetAbility2());
                break;
            case 2:
                s.GetComponent<ShotScript>().Shot(StorePersistentVariables.Instance.slothTeam2[currentTurn].GetAbility2());
                break;
        }
    }

    public void TriggerAbility3()
    {
        int id = ChangeTurnModel.Instance.GetId();
        int currentTurn = ChangeTurnModel.Instance.GetCurrentTurn();

        GameObject s = TurnController.Instance.GetActiveSloth();
        switch (id)
        {
            case 1:
                s.GetComponent<ShotScript>().Shot(StorePersistentVariables.Instance.slothTeam1[currentTurn].GetAbility3());
                break;
            case 2:
                s.GetComponent<ShotScript>().Shot(StorePersistentVariables.Instance.slothTeam2[currentTurn].GetAbility3());
                break;
        }
    }

    public void DeactivateButtonsIfNecessary(int ab1ap, int ab2ap, int ab3ap, int currentAp){
        if(ab1ap > currentAp){
            buttonAbility1.interactable = false;
        }else{
            buttonAbility1.interactable = true;
        }
        if (ab2ap > currentAp){
            buttonAbility2.interactable = false;
        }else{
            buttonAbility2.interactable = true;
        }
        if(ab3ap > currentAp){
            buttonAbility3.interactable = false;
        }else{
            buttonAbility3.interactable = true;
        }
        
    }

    public void SetAbility1(Button ability1Cont)
    {
        buttonAbility1 = ability1Cont;
    }
    public void SetAbility2(Button ability2Cont)
    {
        buttonAbility2 = ability2Cont;
    }
    public void SetAbility3(Button ability3Cont)
    {
        buttonAbility3 = ability3Cont;
    }
}