using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController: MonoBehaviour{
  
    private static List<Player> playerTeam1;
    private static List<Player> playerTeam2;

    private static List<Sprite> teamSprite1, teamSprite2;
    private static GameObject teamSelectionGameObject;

    // Use this for initialization

    private void Awake()
    {
        
        playerTeam1 = new List<Player>();
        playerTeam2 = new List<Player>();
        teamSprite1 = new List<Sprite>();
        teamSprite2 = new List<Sprite>();

        teamSelectionGameObject = GameObject.Find("sceneBehaviour");

        CreateTeamsSetSprites();
    }

    public List<Sprite> GetTeamSprite1()
    {
        return teamSprite1;  
    }

    public List<Sprite> GetTeamSprite2()
    {
        return teamSprite2;
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

    private void CreateTeamsSetSprites()
    {

        foreach (Sloth sloth in teamSelectionGameObject.GetComponent<TeamSelection>().slothTeam1)
        {
            playerTeam1.Add(new Player(sloth));
            teamSprite1.Add(Resources.Load<Sprite>(sloth.GetSprite()));
        }

        foreach (Sloth sloth in teamSelectionGameObject.GetComponent<TeamSelection>().slothTeam2)
        {
            playerTeam2.Add(new Player(sloth));
            teamSprite2.Add(Resources.Load<Sprite>(sloth.GetSprite()));
        }
        
    }
}
