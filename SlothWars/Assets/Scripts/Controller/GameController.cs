using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour{
  
    private static List<Player> playerTeam1;
    private static List<Player> playerTeam2;

    private static List<Image> teamImage1, teamImage2;
    private static GameObject teamSelectionGameObject;

    // Use this for initialization

    private void Awake()
    {
        playerTeam1 = new List<Player>();
        playerTeam2 = new List<Player>();
        teamImage1 = new List<Image>();
        teamImage2 = new List<Image>();

        teamSelectionGameObject = GameObject.Find("sceneBehaviour");
        

        SetTeamImage1();
        SetTeamImage2();

        CreateTeams();
    }

    public List<Image> GetTeamImage1()
    {
        return teamImage1;  
    }

    public List<Image> GetTeamImage2()
    {
        return teamImage2;
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

    private void SetTeamImage1()
    {
        print("Holaaaa");
        foreach (Image image in teamSelectionGameObject.GetComponent<TeamSelection>().team1SlothImages) { teamImage1.Add(image); }
        
    }

    private void SetTeamImage2()
    {
        foreach (Image image in teamSelectionGameObject.GetComponent<TeamSelection>().team2SlothImages) { teamImage2.Add(image); }
        
    }

}
