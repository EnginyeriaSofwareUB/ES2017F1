using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//TurnController: Created as a controller for logic issues as death rom sloths and abilities.
public class LogicController: ControllerSingleton<MonoBehaviour>{
   
    private LogicModel logicModel;
    private static List<GameObject> teamSloths1 = new List<GameObject>();
    private static List<GameObject> teamSloths2 = new List<GameObject>();


    public LogicController(List<GameObject> teamSloths1Cont, List<GameObject> teamSloths2Cont)
    {

        teamSloths1 = teamSloths1Cont;
        teamSloths2 = teamSloths2Cont;
    }
    private void OnEnable()
    {
        logicModel = new LogicModel();

        logicModel.SetTeamSloths1(teamSloths1);
        logicModel.SetTeamSloths2(teamSloths2);
    }


}
