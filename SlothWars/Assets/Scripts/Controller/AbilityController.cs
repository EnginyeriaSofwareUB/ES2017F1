using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class AbilityController : ControllerSingleton<MonoBehaviour> {

    private AbilityModel abilityModel;
    private static List<Transform> listGunTeam1, listGunTeam2;
	public AbilityController(List<Transform> listGunTeam1Cont, List<Transform> listGunTeam2Cont)
    {
        listGunTeam1 = listGunTeam1Cont;
        listGunTeam2 = listGunTeam2Cont;
    }

    //TODO: ABILITIES
    private void OnEnable()
    {
        abilityModel = new AbilityModel();
        abilityModel.SetGunTeams(listGunTeam1, listGunTeam2);
    }

}
