using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameControl : MonoBehaviour {
    
    // GameControl instance that you may call from any other script in the scene using GameControl.control 
    // and then you may access to any of its public variables and/or expressions.
    public static GameControl control;

    // List of 'sloths' of each team. List of strings, which are the type of sloth.
    [HideInInspector]
    public List<string> slothTeam1;
    [HideInInspector]
    public List<string> slothTeam2;

    public readonly int maxTeamSloths = 4;

    private void Awake()
    {
        if (control == null)
        {
            // if there is no GameControl created yet, makes it persistent between scenes and asigns it to itself
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            // if the next scene already has a GameControl object, destroys it and keeps the one from previous scenes
            Destroy(gameObject);
        }
        else
        {
            // else, do nothing. Keeps the GameControl from previous scenes.
        }
    }
    
    //sums dmg_heal to the hp bar asociated to target
 
    public void SumToHpBar(Sloth target, double dmg_Heal)
    {
        // GameObject-Sloth connection needed HealthScript.SumToHp()
    }
    // sums residual during t turns
    public void SumResidual(Sloth target, Double residual, int t)
    {
        // GameObject-Sloth connection needed
    }
    // apply last ability to the target
    public void ApplyLastAbility(GameObject sloth)
    {
        // GameObject-Sloth connection needed
    }
    private void Start()
    {
        slothTeam1 = new List<string>();
        slothTeam2 = new List<string>();
    }
}
