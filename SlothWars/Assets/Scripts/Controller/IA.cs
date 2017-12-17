using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class IA: IAInterface
{
    private bool checkDistance = false;
    private Vector3 actualPosition = new Vector3(0f,0f,0f);
    private static Vector3 positionNearestEnemySloth = new Vector3(0f, 0f, 0f);
    private Vector3 positionNearestFriendlySloth = new Vector3(0f, 0f, 0f);
    private List<float> rangeListAbilities;
    
    public IA() { }

    public GameAction GetAction(GameController gameController)
    {
        GameAction gameAction = new GameAction();

        actualPosition = GetActualPosition(gameController);
        rangeListAbilities = GetRangeAbilities(gameController);

        positionNearestEnemySloth = PositionNearestEnemySloth(gameController,actualPosition);
        Debug.Log(actualPosition);
        Debug.Log(positionNearestEnemySloth);
        Vector3 dir = GetNextMove(-actualPosition + positionNearestEnemySloth);
        gameAction.x = dir.x;
        gameAction.y = dir.y;

        checkDistance = CheckDistance(actualPosition,positionNearestEnemySloth, rangeListAbilities);
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

    private Vector3 GetActualPosition(GameController gameController) { return gameController.GetCurrentSloth().transform.position; }

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
    private bool CheckDistance(Vector3 iAPosition, Vector3 positionNearestSloth, List<float> rangeListAbilities) {
        rangeListAbilities.Sort();
        if ((iAPosition - positionNearestSloth).magnitude <= rangeListAbilities[rangeListAbilities.Count-1])
        {
            return true;
        }
        return false;
    }

    private Vector3 GetNextMove(Vector3 EnvsIA) {
        if(EnvsIA.x == 0 && EnvsIA.y == 0) { return new Vector3(0f, 0f, 0f); }
        if(EnvsIA.x > 0) { return new Vector3(1f, 0f, 0f); }
        if (EnvsIA.y > 0) { return new Vector3(0f, 1f, 0f); }
        if (EnvsIA.y < 0) { return new Vector3(0f, -1f, 0f); }
        if (EnvsIA.x < 0) { return new Vector3(-1f, 0f, 0f); }
        if(EnvsIA.y > 0) { return new Vector3(0f, 1f, 0f); }
        if(EnvsIA.y < 0) { return new Vector3(0f, -1f, 0f); }
        else { Debug.Log("Hay un problema"); return new Vector3(0f, 0f, 0f); }
    }


    private Vector3 PositionNearestEnemySloth(GameController gameController, Vector3 actualIAPosition) {
        Vector3 nearestSloth = new Vector3(0f, 0f, 0f);
        Vector3 nearestSlothAux = new Vector3(0f, 0f, 0f);
        float norm = 0f;
        float normAux = 0f;
        foreach ( Sloth sloth in gameController.GetTeamBlue())
        {
            nearestSloth = sloth.transform.position;
            //if (CheckEnemyAlive(gameController,nearestSloth))
            //{
                norm = (nearestSloth - actualIAPosition).magnitude;
                if (normAux < norm)
                {
                    normAux = norm;
                    nearestSlothAux = nearestSloth;
                }
            //}
            //else
            //{
            //    nearestSlothAux = nearestSloth;
//            }
        }
        return nearestSlothAux;
        
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