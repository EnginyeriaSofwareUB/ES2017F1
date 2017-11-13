using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LogicController: GameController{

    private static Button firstAbility;
    private static Button secondAbility;
    private static Button thirdAbility;


    public LogicController(List<Button> listAbilities)
    {
        firstAbility = listAbilities[0];
        secondAbility = listAbilities[1];
        thirdAbility = listAbilities[2];
    } 
		
}
