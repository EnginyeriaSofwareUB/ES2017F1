using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//TurnController: Created as a controller for logic issues as death rom sloths and abilities.
public class LogicController: ControllerSingleton<MonoBehaviour>{

    private GameController gameController;
    private LogicModel logicModel;

    private List<GameObject> teamSloths1, teamSloths2;
    private List<Button> listAbilities;

    private void Start()
    {
        InitializeLogicControllerVariables();
      
        logicModel.SetTeamSloths1(teamSloths1);
        logicModel.SetTeamSloths2(teamSloths2);
    }

    private void InitializeLogicControllerVariables()
    {
        teamSloths1 = gameController.teamSloths1;
        teamSloths2 = gameController.teamSloths2;
        listAbilities = gameController.listAbilities;
    }

}
