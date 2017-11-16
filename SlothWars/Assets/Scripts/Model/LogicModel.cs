using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicModel{
    private static List<GameObject> teamSloths1, teamSloths2;

    public LogicModel() { }

    public void SetTeamSloths1(List<GameObject> teamSlothsCont1) { teamSloths1 = teamSlothsCont1; }
    public List<GameObject> GetTeamSloths1(){ return teamSloths1; }

    public void SetTeamSloths2(List<GameObject> teamSlothsCont2) { teamSloths2 = teamSlothsCont2; }
    public List<GameObject> GetTeamSloths2() { return teamSloths2; }

    

}
