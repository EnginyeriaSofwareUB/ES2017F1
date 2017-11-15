using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//TurnController: Created as a controller for logic issues as death rom sloths and abilities.
public class LogicController: GameController{
   
    private LogicModel logicModel;
    
    private void Start()
    {
        logicModel.SetTeamSloths1(teamSloths1);
        logicModel.SetTeamSloths2(teamSloths2);
    }


}
