using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NUnit.Framework;
using System.Security.Cryptography.X509Certificates;

// Class created in order to control the executional flow. It stores variables obtained in the scene and call the rest
// of the controllers.
// TODO: Redo this singleton in order to not have static variables.
public class GameController: MonoBehaviour{

    //SINGLETON
    private static GameController instance;
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameController();
            }
            return instance;
        }
    }
    protected  GameController() { } // guarantee this will be always a singleton only - can't use the constructor!

    ///////*****///////

    //GameObjects (sloths) in the scene
    public static List<GameObject> teamSloths1;
    public static List<GameObject> teamSloths2;

    //Player Lists to attach to the gameObjects in the scene.
    public List<AnimPlayer> playerTeam1, playerTeam2;

    //PLACEPLAYERS VARIABLES: Variables created in order to place the sloths in the scene. 
    private int i = 0;
    private int checkAwake = 0;
    private static int checkPlayer = 0;
    private static int checkTurn = 0;

    //TURNCONTROLLER VARIABLES
    private TurnController turnController;
    public static List<Sprite> teamSprite1, teamSprite2;
    public Button endTurnButton;
    private Text turnLabel;

    //LOGICCONTROLLER VARIABLES
    private LogicController logicController;
    private GameObject prove;

    //UICONTROLLER VARIABLES
    private UIController uiController;
    public Image panelMain;
    public Image panelOpts;

    //ABILITY VARIABLES
    private Transform createGun;
    private AbilityController abilityController;
    private Button buttonAbility1, buttonAbility2, buttonAbility3;
    public static List<Transform> listGunTeam1 = new List<Transform>();
    public static List<Transform> listGunTeam2 = new List<Transform>();


    ///////*****///////

    private void Awake()
    {
        //HOTFIX: Awake tries to execute twice. Have to check why.
        if (checkAwake == 0)
        {
            checkAwake = 1;

            // As long as method Awake is called only here (it must be the only one who calls Awake method)
            // we can control the flow calling the methods in this order in particular.

            //Create the object Animator in the scenes and initialize player and Sprite lists.
            //TODO: Check if we can avoid this method in order to do this.
            InitializePlayer();

            //Create the terrain
            InitializeTerrain();

            //Place the players in the scene.
            //TODO: Put this method in another class.
            PlacePlayers();

            //Initialize all the variables for the rest of controllers (Getting from the scene)
            //Initialize controllers.
            InitializeTurnVariables();
            InitializeAbilityVariables();
            InitializeLogicVariables();
            InitializeUIVariables();
        }

    }

    private void InitializePlayer()
    {

        playerTeam1 = new List<AnimPlayer>();
        playerTeam2 = new List<AnimPlayer>();
        teamSprite1 = new List<Sprite>();
        teamSprite2 = new List<Sprite>();
        CreateTeamsSetSprites();
        //Call the AnimPlayer.cs in order to execute Start method in that cs.
        //TODO: Coroutines.
        GameObject.Find("GameController").GetComponent<AnimPlayer>().enabled = false;
    }

    private void InitializeTerrain()
    {
        //Call the TerrainCreator.cs in order to execute Start method in that cs.
        //TODO: Coroutines.
        GameObject.Find("EmptyTerrain").GetComponent<TerrainCreator>().enabled = true;
    }

    private void PlacePlayers()
    {
        GameObject sloth;
        AnimPlayer pla;
        HealthScript health;
        Animator anim;
        ShotScript shot;
        SlothSelected selected;
        
        if (checkPlayer == 0)
        {
            checkPlayer = 1;
            teamSloths1 = new List<GameObject>();
            teamSloths2 = new List<GameObject>();
            foreach (AnimPlayer playerSloth in playerTeam1)
            {
                sloth = (GameObject)Instantiate(Resources.Load("Prefabs/Sloth"), new Vector3(i + 20, 0, 0), Quaternion.identity);
                // setting health
                health = sloth.AddComponent<HealthScript>();
                health.setHealth(playerSloth.GetSloth().GetHp());
                health.enabled = true;
                anim = sloth.GetComponent<Animator>();
                anim.enabled = true;

                pla = sloth.AddComponent<AnimPlayer>();
                pla.SetSloth(playerSloth.GetSloth());
                pla.enabled = true;

                shot = sloth.GetComponent<ShotScript>();
                shot.enabled = true;

                selected = sloth.AddComponent<SlothSelected>();
                selected.enabled = false;


                //createGun = sloth.GetComponentInChildren<Transform>().Find("Gun");
                //createGun.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));
               
                //listGunTeam1.Add(createGun);
                
                teamSloths1.Add(sloth);

                i++;
            }
            i = 0;
            foreach (AnimPlayer playerSloth in playerTeam2)
            {
                sloth = (GameObject)Instantiate(Resources.Load("Prefabs/Sloth"), new Vector3(i + 10, 0, 0), Quaternion.identity);
                // setting health
                health = sloth.AddComponent<HealthScript>();
                health.setHealth(playerSloth.GetSloth().GetHp());
                health.enabled = true;
                //Start the animation
                anim = sloth.GetComponent<Animator>();
                anim.enabled = true;

                pla = sloth.AddComponent<AnimPlayer>();
                pla.SetSloth(playerSloth.GetSloth());
                pla.enabled = true;

                shot = sloth.GetComponent<ShotScript>();
                shot.enabled = true;

                selected = sloth.AddComponent<SlothSelected>();
                selected.enabled = false;


               // createGun = sloth.GetComponentInChildren<Transform>().Find("Gun");
                //createGun.transform.rotation = Quaternion.Euler(new Vector3(0, -90, 0));

                //listGunTeam2.Add(createGun);

                teamSloths2.Add(sloth);
                i++;
            }
        }       
    }

    private void InitializeTurnVariables()
    {
        if (checkTurn == 0)
        {
            checkTurn = 1;
            turnLabel = GameObject.Find("TurnText").GetComponent<Text>();
            endTurnButton = GameObject.Find("EndTurnButton").GetComponent<Button>();

            //TODO: Change the design in order to avoid calling the constructor.
            //It also initializes the start method.
            turnController = new TurnController(teamSloths1, teamSloths2, teamSprite1 , teamSprite2, endTurnButton, turnLabel);

        }

    }

    private void InitializeAbilityVariables()
    {

        buttonAbility1 =  GameObject.Find("buttonAbility1").GetComponent<Button>();
        buttonAbility2 =  GameObject.Find("buttonAbility2").GetComponent<Button>();
        buttonAbility3 =  GameObject.Find("buttonAbility3").GetComponent<Button>();
        abilityController = new AbilityController(buttonAbility1, buttonAbility2, buttonAbility3);
    }

    private void InitializeLogicVariables()
    {


        //TODO: Change the design in order to avoid calling the constructor.
        //It also initializes the start method.
        logicController = new LogicController(teamSloths1, teamSloths2);
        
    }

    private void InitializeUIVariables()
    {

        panelMain = GameObject.Find("MainUIPanel").GetComponent<Image>();
        panelOpts = GameObject.Find("OptionPanel").GetComponent<Image>();

        //TODO: Change the design in order to avoid calling the constructor.
        //It also initializes the start method.
        uiController = new UIController(panelMain, panelOpts);
        

    }
    
    //Method to store in the lists the elements from the previous Scene (Stored in 
    //StorePersistentVariables.cs)
    private void CreateTeamsSetSprites()
    {

        foreach (Sloth sloth in StorePersistentVariables.Instance.slothTeam1)
        {
            playerTeam1.Add(new AnimPlayer(sloth));
            teamSprite1.Add(Resources.Load<Sprite>(sloth.GetSprite()));
        }
        foreach (Sloth sloth in StorePersistentVariables.Instance.slothTeam2)
        {
            playerTeam2.Add(new AnimPlayer(sloth));
            teamSprite2.Add(Resources.Load<Sprite>(sloth.GetSprite()));
        }
        
    }
    
}
