using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour{
  
    private static List<Player> playerTeam1;
    private static List<Player> playerTeam2;

    private static GameObject teamSelectionGameObject;

    // Use this for initialization

    private void Awake()
    {
        playerTeam1 = new List<Player>();
        playerTeam2 = new List<Player>();
        teamSelectionGameObject = GameObject.Find("sceneBehaviour");
        CreateTeams();
    }

    public GameObject GetTeamSelectionGameObject()
    {
        return teamSelectionGameObject;
    }

    public List<Player> GetPlayerTeam(int idTeam)
    {
        if (idTeam == 1)
        {
            return playerTeam1;
        }
        return playerTeam2;
    }

    private void CreateTeams()
    {

        foreach (Sloth sloth in teamSelectionGameObject.GetComponent<TeamSelection>().slothTeam1)
        {
            playerTeam1.Add(new Player(sloth));
        }

        foreach (Sloth sloth in teamSelectionGameObject.GetComponent<TeamSelection>().slothTeam2)
        {
            playerTeam2.Add(new Player(sloth));
        }
        
    }

}
