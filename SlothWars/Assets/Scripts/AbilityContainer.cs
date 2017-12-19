using System;
using UnityEngine;

public class AbilityContainer: MonoBehaviour{
    Ability ability;

    public void SetAbility(Ability ability){
        this.ability = ability;
    }
    private void Update()
    {
        if (this.transform.position.x > 120 || this.transform.position.y > 40 || this.transform.position.x < -20)
        {
            Destroy(this.gameObject);
        }
    }

    public Ability GetAbility(){
        return ability;
    }

}