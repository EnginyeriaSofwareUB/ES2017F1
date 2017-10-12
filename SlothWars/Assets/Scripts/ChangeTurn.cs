using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTurn : MonoBehaviour
{

    // Getting the sloths in the scene.
    public GameObject enemySloth,gunEnemySloth;
    public GameObject friendlySloth,gunFriendlySloth;

    // Initially, the friendly sloth has not ended his turn.
    public bool endTurnOfSloth = false;


    private void Start()
    {
        enemySloth = GameObject.Find("avatarEnemySloth");
        gunEnemySloth = GameObject.Find("gunEnemySloth");
        
       
        friendlySloth = GameObject.Find("avatarFriendlySloth");
        gunFriendlySloth = GameObject.Find("gunFriendlySloth");


    }
    
    
    private void ActivateSloth(GameObject sloth, GameObject gunSloth)
    {
        sloth.GetComponent<Animator>().enabled = false;
        sloth.GetComponent<player>().enabled = false;
        gunSloth.GetComponent<ShotScript>().enabled = false;

    }

    private void DeactivateSloth(GameObject sloth, GameObject gunSloth)
    {
        sloth.GetComponent<Animator>().enabled = true;
        sloth.GetComponent<player>().enabled = true;
        gunSloth.GetComponent<ShotScript>().enabled = true;
    }

    public void FinishTurn()
    {
        // If he has ended, he will press the button, changing the variable to true.
        if (!endTurnOfSloth)
        {
            endTurnOfSloth = true;
            DeactivateSloth(friendlySloth, gunFriendlySloth);
            ActivateSloth(enemySloth, gunEnemySloth);

            /* If the variable is already true, it means that it is the turn for the enemy sloth.
            If we arrive here it will mean that the enemy sloth has finished his turn, and 
            it will change the variable to false.*/
        }
        else
        {
            endTurnOfSloth = false;
            DeactivateSloth(enemySloth, gunEnemySloth);
            ActivateSloth(friendlySloth, gunFriendlySloth);

        }
    }

}
