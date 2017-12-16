using System;
using UnityEngine;

public class AbilityContainer: MonoBehaviour{
    Ability ability;

    public void SetAbility(Ability ability){
        this.ability = ability;
    }

    public Ability GetAbility(){
        return ability;
    }

}