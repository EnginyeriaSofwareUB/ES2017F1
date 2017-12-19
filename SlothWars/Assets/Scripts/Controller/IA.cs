using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IA: IAInterface
{
    private static bool checkRight = false;
    private static bool checkMovement = false;
    private static bool checkDistance = false;
    private static bool checkFlag = false;
    private static bool flagFalling = false;
    private string tagSloth;
    private Vector3 previousPosition = new Vector3(0f, 0f, 0f);
    private Vector3 actualPosition = new Vector3(0f,0f,0f);
    private Vector3 positionNearestEnemySloth = new Vector3(0f, 0f, 0f);
    private Vector3 positionNearestFriendlySloth = new Vector3(0f, 0f, 0f);
    private List<float> rangeListAbilities;
    private static List<Sloth> listSloths = new List<Sloth>();
    private static List<Vector3> previousPositionList = new List<Vector3>();
    public IA() { }

    public GameAction GetAction(GameController gameController)
    {
        GameAction gameAction = new GameAction();
        if (gameController.GetGravity()) { Debug.Log("Me cai"); flagFalling = true; }

        tagSloth = GetActualTag(gameController);
        actualPosition = GetActualPosition(gameController);
        Debug.Log("actualPosition" + actualPosition);
        rangeListAbilities = GetRangeAbilities(gameController);
        
        positionNearestEnemySloth = PositionNearestEnemySloth(gameController,actualPosition);
        
        Debug.Log("EnemyPosition " + positionNearestEnemySloth);
        Vector3 dir = GetNextMove(positionNearestEnemySloth - actualPosition, flagFalling,gameController);

        flagFalling = false;
        gameAction.x = dir.x;
        gameAction.y = dir.y;

        checkDistance = CheckDistance(actualPosition,positionNearestEnemySloth, rangeListAbilities, tagSloth);
        if (checkDistance) {
            gameAction.targetSloth = TargetSloth(gameController, actualPosition);

            gameAction.angleAbility = GetAngle(positionNearestEnemySloth, actualPosition);
            gameAction.aimVector = GetDirectionNearestSloth(gameAction.angleAbility);
 
            gameAction.angleAbility = GetAngle(positionNearestEnemySloth,actualPosition);
            gameAction.ability = GetAbility(gameController);
            gameAction.actionType = GameAction.ActionType.EJECUTAR_HABILIDAD; 
        } else
        {
            gameAction.actionType = GameAction.ActionType.MOVERSE;
        }
        
        if (gameController.GetCurrentAp() <= 0)
        {
            gameAction.actionType = GameAction.ActionType.ENDTURN;
        }
        return gameAction;
    }

    private Vector3 GetActualPosition(GameController gameController) {
        Vector3 actualPosition = gameController.GetCurrentSloth().transform.position;
        actualPosition.x = Mathf.Round(actualPosition.x);
        actualPosition.y = Mathf.Round(actualPosition.y);
        return actualPosition;
    }

    private Ability GetAbility(GameController gameController)
    {
        List<Ability> listAbilities = new List<Ability>();
        listAbilities.Add(gameController.GetCurrentSloth().GetAbility1());
        listAbilities.Add(gameController.GetCurrentSloth().GetAbility2());
        listAbilities.Add(gameController.GetCurrentSloth().GetAbility3());

        float range = -1f;
        Ability a = null;

        foreach (Ability ability in listAbilities)
        {
            if (range < ability.GetRange())
            {
                range = ability.GetRange();
                a = ability;
            }
        }
        
        return a;
       
    }
    private bool CheckDistance(Vector3 iAPosition, Vector3 positionNearestSloth, List<float> rangeListAbilities, string tagCurrentSloth) {
        rangeListAbilities.Sort();
        if (tagCurrentSloth.Equals("TankP2"))
        {
            if ((iAPosition - positionNearestSloth).magnitude <= rangeListAbilities[rangeListAbilities.Count - 1]) { // rango 2 (Asi le da con las hachas)"
                return true;
            }
        }
        else if ((iAPosition - positionNearestSloth).magnitude <= rangeListAbilities[rangeListAbilities.Count-1])
        {
            return true;
        }
        return false;
    }

    private Vector3 GetNextMove(Vector3 EnvsIA, bool flagFalling, GameController gameController) {
        //checkMovement: Se pone a true cuando esta en la misma x del sloth y ha de ir hacia arriba y en la casilla superior no hay terreno
        //checkFlag: Se pone a true cuando el sloth ha caido
        //checkRight: Se pone a true cuando el sloth enemigo esta mas a la derecha que el current
        Debug.Log("checkMovement " + checkMovement);
        Debug.Log("Flag " + flagFalling);
        Debug.Log("checkFlag" + checkFlag);
        if (!flagFalling)
        {
            if (checkFlag) {
                checkFlag = false;
                Debug.Log("Me he caido");
            } //Mirar porque no siempre se ha de mover en esta direccion


            if (checkMovement)
            {
                if (EnvsIA.y > 0)
                {
                    checkMovement = false;
                    Debug.Log("Deberia moverme a la derecha o a la izquierda");
                    if (checkRight) { checkRight = false; return new Vector3(1f, 0f, 0f); }
                    return new Vector3(-1f, 0f, 0f);
                }
            }
            
            if (EnvsIA.x == 0 && EnvsIA.y == 0) { return new Vector3(0f, 0f, 0f); }
            if (EnvsIA.x > 0) { Debug.Log("Avanzare a la derecha"); checkRight = true;  return new Vector3(1f, 0f, 0f); }
            if (EnvsIA.x < 0) { Debug.Log("Avanzare a la izquierda"); return new Vector3(-1f, 0f, 0f); }
            if (EnvsIA.y > 0) { Debug.Log("Avanzare arriba"); return new Vector3(0f, 1f, 0f); }
            if (EnvsIA.y < 0) { Debug.Log("Avanzare abajo"); return new Vector3(0f, -1f, 0f); }
            else { Debug.Log("Hay un problema"); return new Vector3(0f, 0f, 0f); }
        } else
        {
            gameController.SetCurrentAp(gameController.GetCurrentAp() + 1);
            checkFlag = true;
            if(EnvsIA.x == 0) { checkMovement = true; }
            return new Vector3(0f, 0f, 0f);
            
        }
    }


    private Vector3 PositionNearestEnemySloth(GameController gameController, Vector3 actualIAPosition) {
        Vector3 roundPosition = new Vector3(-10f, -10f, -10f);
        Vector3 nearestSloth = new Vector3(0f, 0f, 0f);
        Vector3 nearestSlothAux = new Vector3(0f, 0f, 0f);
        float norm = 0f;
        float normAux = 100000000f;
        foreach ( Sloth sloth in gameController.GetTeamBlue())
        {
            nearestSloth = sloth.transform.position;
            norm = (nearestSloth - actualIAPosition).sqrMagnitude;
            
            if (normAux > norm)
            {
                normAux = norm;
                nearestSlothAux = nearestSloth;
            }
        }
        roundPosition.x = Mathf.Round(nearestSlothAux.x);
        roundPosition.y = Mathf.Round(nearestSlothAux.y);
        roundPosition.z = nearestSlothAux.z;
        return roundPosition;
        
    }

    private bool CheckEnemyAlive(GameController gameController, Vector3 slothPosition)
    {
        foreach(Sloth sloth in gameController.GetTeamBlue())
        {
            if(sloth.transform.position == slothPosition)
            {
                return false;
            }
        }
        return true;
    }

    private string GetActualTag(GameController gameController) { return gameController.GetCurrentSloth().transform.parent.name; }
    private Vector3 GetDirectionNearestSloth(float angle)
    {
        return new Vector3(Mathf.Cos(angle*(Mathf.PI)/180), Mathf.Sin(angle*(Mathf.PI)/180), 0f);
    }
    
    private float GetAngle(Vector3 positionEnemySloth, Vector3 actualIAPosition)
    {
        float xAng = positionEnemySloth.x - actualIAPosition.x;
        float yAng = positionEnemySloth.y - actualIAPosition.y;
        float angle = Mathf.Atan2(yAng, xAng) * (180f / Mathf.PI);
        
        return angle;
    }

    private List<float> GetRangeAbilities(GameController gameController)
    {
        List<float> rangeListAbilities = new List<float>();
        rangeListAbilities.Add(gameController.GetCurrentSloth().GetAbility1().GetRange());
        rangeListAbilities.Add(gameController.GetCurrentSloth().GetAbility2().GetRange());
        rangeListAbilities.Add(gameController.GetCurrentSloth().GetAbility3().GetRange());

        return rangeListAbilities;

    }
    
    private Sloth TargetSloth(GameController gameController, Vector3 actualIAPosition)
    {
        Sloth nearestSloth = null; 
        Vector3 nearestSlothPos = new Vector3(0f, 0f, 0f);
        Vector3 nearestSlothAux = new Vector3(0f, 0f, 0f);
        float norm = 0f;
        float normAux = 0f;
        foreach (Sloth sloth in gameController.GetTeamRed())
        {
            nearestSlothPos = sloth.transform.position;
            norm = (nearestSlothPos - actualIAPosition).magnitude;
            if (normAux < norm)
            {
                normAux = norm;
                nearestSloth = sloth;
                nearestSlothAux = nearestSlothPos;
            }
        }
        return nearestSloth;

    }
}