using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicModel{
    //SINGLETON
    private static LogicModel instance;

    public static LogicModel Instance
    {
        get
        {
            if (instance == null)
            {
                //TODO: FIX THIS TO MAKE IT SINGLETON
                instance = new LogicModel();
            }
            return instance;
        }
    }
    ///////*****///////

    private  List<GameObject> teamSloths1, teamSloths2;

    protected LogicModel() { }

    public void SetTeamSloths1(List<GameObject> teamSlothsCont1) { teamSloths1 = teamSlothsCont1; }
    public List<GameObject> GetTeamSloths1(){ return teamSloths1; }

    public void SetTeamSloths2(List<GameObject> teamSlothsCont2) { teamSloths2 = teamSlothsCont2; }
    public List<GameObject> GetTeamSloths2() { return teamSloths2; }

    

}
