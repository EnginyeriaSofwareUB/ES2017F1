using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//TurnController: Created as a controller for logic issues as death rom sloths and abilities.
public class LogicController{
    //SINGLETON
    private static LogicController instance;
    public static LogicController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LogicController();
            }
            return instance;
        }
    }
    protected  LogicController() { }
    ///////*****///////
    private  List<GameObject> teamSloths1 = new List<GameObject>();
    private  List<GameObject> teamSloths2 = new List<GameObject>();


    public LogicController(List<GameObject> teamSloths1Cont, List<GameObject> teamSloths2Cont)
    {

        teamSloths1 = teamSloths1Cont;
        teamSloths2 = teamSloths2Cont;
    }
    public void StartLogic()
    {
        
        LogicModel.Instance.SetTeamSloths1(teamSloths1);
        LogicModel.Instance.SetTeamSloths2(teamSloths2);
    }


}
