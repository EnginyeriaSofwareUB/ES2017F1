using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Security.Cryptography.X509Certificates;

// Class created in order to control the executional flow. It stores variables obtained in the scene and call the rest
// of the controllers.
// TODO: Redo this singleton in order to not have static variables.
public class GameController : MonoBehaviour
{

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
    protected GameController() { } // guarantee this will be always a singleton only - can't use the constructor!

    ///////*****///////

    // Random Generator
    System.Random random = new System.Random();

    //GameObjects (sloths) in the scene
    public List<GameObject> teamSloths1, teamSloths2;

    //Player Lists to attach to the gameObjects in the scene.
    public List<AnimPlayer> playerTeam1, playerTeam2;

    //PLACEPLAYERS VARIABLES: Variables created in order to place the sloths in the scene. 
    private int i = 0;
    private int checkAwake = 0;
    private int checkPlayer = 0;
    private int checkTurn = 0;
    private int checkAbility = 0;

    //TURNCONTROLLER VARIABLES
    private TurnController turnControllerInstance = TurnController.Instance;
    public List<Sprite> teamSprite1, teamSprite2;
    public Button endTurnButton;
    public Text turnLabel;

    //LOGICCONTROLLER VARIABLES
    private LogicController logicControllerInstance = LogicController.Instance;

    //UICONTROLLER VARIABLES
    private UIController uiControllerInstance = UIController.Instance;
    public Image panelMain;
    public Image panelOpts;

    //ABILITY VARIABLES
    private AbilityController abilityControllerInstance = AbilityController.Instance;
    public Button buttonAbility1, buttonAbility2, buttonAbility3;
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
        TerrainCreator.LoadMap();
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
                // Instantiate sloths in random valid positions of the field
                int point = random.Next(0, TerrainCreator.GetAvailablePlaces().Count);
                sloth = (GameObject)Instantiate(Resources.Load("3D Models/Prefabs/" + StorePersistentVariables.Instance.slothTeam1[i].GetModel()), 
                    new Vector3(TerrainCreator.GetAvailablePlaces()[point].x_coord, 
                       TerrainCreator.GetAvailablePlaces()[point].y_coord + 0.05f, 0.5f), 
                    Quaternion.Euler (90, 180, 0));
                // Delete valid position of the field to avoid possible repetitions
                TerrainCreator.GetAvailablePlaces().RemoveAt(point);


                // setting health
                health = sloth.AddComponent<HealthScript>();
                health.setHealth(StorePersistentVariables.Instance.slothTeam1[i].GetHp());
                health.enabled = true;
                anim = sloth.GetComponent<Animator>();
                anim.enabled = true;

                pla = sloth.AddComponent<AnimPlayer>();
                pla.enabled = true;

                shot = sloth.GetComponent<ShotScript>();
                shot.enabled = true;

                sloth.GetComponent<Rigidbody>().useGravity = false;
                Debug.Log(sloth.GetComponent<Rigidbody>().useGravity);
                selected = sloth.AddComponent<SlothSelected>();
                selected.SetLeaf(sloth.GetComponentInChildren<Transform>().Find("leaf_teamA").gameObject);
                selected.GetLeaf().transform.position = sloth.transform.position + new Vector3(0f, 7f, 0f);
                selected.SetSlothPosition(sloth.transform.position);
                Destroy(sloth.GetComponentInChildren<Transform>().Find("leaf_teamB").gameObject);
                sloth.GetComponentInChildren<Transform>().Find("leaf_teamA").position = new Vector3(i + 20, 2, 0);
                sloth.GetComponentInChildren<Transform>().Find("leaf_teamA").rotation = Quaternion.Euler(0, 90, 90);
                selected.Active(true);
                selected.enabled = true;


                teamSloths1.Add(sloth);

                i++;
            }
            turnControllerInstance.SetTeamSloths1(teamSloths1);
           
            i = 0;
            foreach (AnimPlayer playerSloth in playerTeam2)
            {
                // Instantiate sloths in random valid positions of the field
                int point = random.Next(0, TerrainCreator.GetAvailablePlaces().Count);
                sloth = (GameObject)Instantiate(Resources.Load("3D Models/Prefabs/" + StorePersistentVariables.Instance.slothTeam2[i].GetModel()), 
                    new Vector3(TerrainCreator.GetAvailablePlaces()[point].x_coord, 
                        TerrainCreator.GetAvailablePlaces()[point].y_coord+0.05f, 0.5f), 
                    Quaternion.Euler (90, 180, 0));
                // Delete valid position of the field to avoid possible repetitions
               TerrainCreator.GetAvailablePlaces().RemoveAt(point);

                
                // setting health
                health = sloth.AddComponent<HealthScript>();
                health.setHealth(StorePersistentVariables.Instance.slothTeam2[i].GetHp());
                health.enabled = true;
                //Start the animation
                anim = sloth.GetComponent<Animator>();
                anim.enabled = true;

                pla = sloth.AddComponent<AnimPlayer>();
                pla.enabled = true;

                shot = sloth.GetComponent<ShotScript>();
                shot.enabled = true;

                sloth.GetComponent<Rigidbody>().useGravity = false;
                Debug.Log(sloth.GetComponent<Rigidbody>().useGravity);
                selected = sloth.AddComponent<SlothSelected>();
                selected.SetLeaf(sloth.GetComponentInChildren<Transform>().Find("leaf_teamB").gameObject);
                selected.GetLeaf().transform.position = sloth.transform.position + new Vector3(0f, 7f, 0f);
                selected.SetSlothPosition(sloth.transform.position);
                Destroy(sloth.GetComponentInChildren<Transform>().Find("leaf_teamA").gameObject);
                sloth.GetComponentInChildren<Transform>().Find("leaf_teamB").position = new Vector3(i + 10, 2, 0);
                sloth.GetComponentInChildren<Transform>().Find("leaf_teamB").rotation = Quaternion.Euler(0, 90, 90);
                selected.Active(true);
                selected.enabled = true;
       

                teamSloths2.Add(sloth);
                i++;
            }
            turnControllerInstance.SetTeamSloths2(teamSloths2);
        }
    }

    private void InitializeTurnVariables()
    {
        if (checkTurn == 0)
        {
            checkTurn = 1;
            turnLabel = GameObject.Find("TurnText").GetComponent<Text>();
            endTurnButton = GameObject.Find("EndTurnButton").GetComponent<Button>();

            turnControllerInstance.SetEndTurnButton(endTurnButton);
            turnControllerInstance.SetTurnLabel(turnLabel);
            turnControllerInstance.StartTurns();
        }

    }

    private void InitializeAbilityVariables()
    {
        
        buttonAbility1 = GameObject.Find("buttonAbility1").GetComponent<Button>();
        buttonAbility2 = GameObject.Find("buttonAbility2").GetComponent<Button>();
        buttonAbility3 = GameObject.Find("buttonAbility3").GetComponent<Button>();

        abilityControllerInstance.SetAbility1(buttonAbility1);
        abilityControllerInstance.SetAbility2(buttonAbility2);
        abilityControllerInstance.SetAbility3(buttonAbility3);

        abilityControllerInstance.StartAbilities();

    }

    private void InitializeLogicVariables()
    {
        logicControllerInstance.StartLogic();
    }

    private void InitializeUIVariables()
    {

        panelMain = GameObject.Find("MainUIPanel").GetComponent<Image>();
        panelOpts = GameObject.Find("OptionPanel").GetComponent<Image>();

        //TODO: Change the design in order to avoid calling the constructor.
        //It also initializes the start method.

        uiControllerInstance.SetPanelMain(panelMain);
        uiControllerInstance.SetPanelOpts(panelOpts);

        //GameObject.Find("GameController").GetComponent<UIController>().enabled = true;


    }

    //Method to store in the lists the elements from the previous Scene (Stored in 
    //StorePersistentVariables.cs)
    private void CreateTeamsSetSprites()
    {

        foreach (Sloth sloth in StorePersistentVariables.Instance.slothTeam1)
        {
            playerTeam1.Add(new AnimPlayer());
            teamSprite1.Add(Resources.Load<Sprite>(sloth.GetSprite()));
        }
        foreach (Sloth sloth in StorePersistentVariables.Instance.slothTeam2)
        {
            playerTeam2.Add(new AnimPlayer());
            teamSprite2.Add(Resources.Load<Sprite>(sloth.GetSprite()));
        }

        TurnController.Instance.SetTeamSprite1(teamSprite1);
        TurnController.Instance.SetTeamSprite2(teamSprite2);

    }

}