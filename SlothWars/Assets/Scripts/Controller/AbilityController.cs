using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class AbilityController{
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

    private  Ability ability;
    private  Button buttonAbility1, buttonAbility2, buttonAbility3;
    private  GameObject lastTarget;
    
    protected AbilityController(){

    }

    //private void Start(){}
    //sums dmg_heal to the hp bar asociated to target

    public void StartAbilities()
    {
        buttonAbility1.onClick.AddListener(delegate { TriggerAbility1(); });
        buttonAbility2.onClick.AddListener(delegate { TriggerAbility2(); });
        buttonAbility3.onClick.AddListener(delegate { TriggerAbility3(); });

    }

    public void UpdateHpBar(double hp, double shield)
    {
        Debug.Log("upt");
        lastTarget.GetComponent<HealthScript>().UpdateHP(Mathf.FloorToInt((float)hp), Mathf.FloorToInt((float)shield));
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
            ability.Apply(ref g.GetComponent<AnimPlayer>().sloth);
            //ability.Apply(g);
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
            GameObject.Destroy(destroyable);
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