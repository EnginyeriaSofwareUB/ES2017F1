using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicModel{
    private List<GameObject> teamSloths1, teamSloths2;

    public LogicModel() { }

    public void SetTeamSloths1(List<GameObject> teamSloths1) { this.teamSloths1 = teamSloths1; }
    public List<GameObject> GetTeamSloths1(){ return teamSloths1; }

    public void SetTeamSloths2(List<GameObject> teamSloths2) { this.teamSloths2 = teamSloths2; }
    public List<GameObject> GetTeamSloths2() { return teamSloths2; }

    

}
