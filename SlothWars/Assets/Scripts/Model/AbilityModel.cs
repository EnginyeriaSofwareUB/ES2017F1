using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class AbilityModel {

    private static Button firstAbility, secondAbility, thirdAbility;
    private static List<Button> listAbilities;
    private static List<Transform> listGunTeam1, listGunTeam2;
    private static bool beginStopped;
    private static int turnPlayer1, turnPlayer2;

    public AbilityModel() { }

    public void SetGunTeams(List<Transform> gunTeam1Cont, List<Transform> gunTeam2Cont) {listGunTeam1 = gunTeam1Cont; listGunTeam2 = gunTeam2Cont; }
    public List<Transform> GetGunTeam1() { return listGunTeam1; }
    public List<Transform> GetGunTeam2() { return listGunTeam2; }

    public void SetFirstAbility(Button firstAbilityCont) { firstAbility = firstAbilityCont; }
    public Button GetFirstAbility() { return firstAbility; }

    public void SetSecondAbility(Button secondAbilityCont) { secondAbility = secondAbilityCont; }
    public Button GetSecondAbility() { return firstAbility; }

    public void SetThirdAbility(Button thirdAbilityCont) { thirdAbility = thirdAbilityCont; }
    public Button GetThirdAbility() { return thirdAbility; }

    public void SetListAbilities(List <Button> listAbilitiesCont) { listAbilities = listAbilitiesCont; }
    public List<Button> GetListAbilities() { return listAbilities; }

    public void SetBeginStopped(bool beginStoppedFromTurns) { beginStopped = beginStoppedFromTurns; }
    public bool GetBeginStopped() { return beginStopped; }

    public void SetTurnPlayer1(int turnPlayer1FromTurns) { turnPlayer1 = turnPlayer1FromTurns; }
    public int GetTurnPlayer1() { return turnPlayer1; }

    public void SetTurnPlayer2(int turnPlayer2FromTurns) { turnPlayer2 = turnPlayer2FromTurns; }
    public int GetTurnPlayer2() { return turnPlayer2; }

}
