using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class IA: IAInterface
{
    private bool checkDistance = false;
    private static Vector3 actualPosition = new Vector3(0f,0f,0f);
    private static Vector3 positionNearestEnemySloth = new Vector3(0f, 0f, 0f);
    private static Vector3 positionNearestFriendlySloth = new Vector3(0f, 0f, 0f);
    private List<float> rangeListAbilities;
    private Sloth nearestSloth;
    public IA() { }

    public GameAction GetAction(GameController gameController)
    {
        actualPosition = GetActualPosition(gameController);
        rangeListAbilities = GetRangeAbilities(gameController);

        positionNearestEnemySloth = PositionNearestEnemySloth(gameController,actualPosition);
        
        GameAction gameAction = new GameAction();
        checkDistance = CheckDistance(actualPosition,positionNearestEnemySloth, rangeListAbilities);
        if (checkDistance) {
            gameAction.ability = GetAbility(gameController);
            gameAction.actionType = GameAction.ActionType.EJECUTAR_HABILIDAD; 
        } else
        {
            gameAction.actionType = GameAction.ActionType.MOVERSE;
            gameAction.x = GetDirectionNearestSloth(actualPosition, positionNearestEnemySloth).x;
            gameAction.y = GetDirectionNearestSloth(actualPosition, positionNearestEnemySloth).y;
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

    private Vector3 GetDirectionNearestSloth(Vector3 actualIAPosition, Vector3 positionNearestSloth) {
        if (positionNearestSloth.x == actualIAPosition.x && positionNearestSloth.y == actualIAPosition.y) { return new Vector3(0f, 0f, 0f); }

        if (positionNearestSloth.x > actualIAPosition.x && positionNearestSloth.y == actualIAPosition.y) { return new Vector3(1f, 0f, 0f); }
        if (positionNearestSloth.x < actualIAPosition.x && positionNearestSloth.y == actualIAPosition.y) { return new Vector3(-1f, 0f, 0f); }
        if (positionNearestSloth.x == actualIAPosition.x) { if (positionNearestSloth.y > actualIAPosition.y) { return new Vector3(0f, 1f, 0f); } else { return new Vector3(0f, -1f, 0f); } }

        if (positionNearestSloth.y > actualIAPosition.y && positionNearestSloth.x == actualIAPosition.x) { return new Vector3(0f, 1f, 0f); }
        if (positionNearestSloth.y < actualIAPosition.y && positionNearestSloth.x == actualIAPosition.x) { return new Vector3(0f, -1f, 0f); }
        if (positionNearestSloth.y == actualIAPosition.y) { if (positionNearestSloth.x > actualIAPosition.x) { return new Vector3(1f, 0f, 0f); } else { return new Vector3(-1f, 0f, 0f); } }

        if (positionNearestSloth.x < actualIAPosition.x) { return new Vector3(-1f, 0f, 0f); }
        if (positionNearestSloth.y < actualIAPosition.y) { return new Vector3(0f, -1f, 0f); }
        if (positionNearestSloth.y > actualIAPosition.y) { return new Vector3(0f, 1f, 0f); }
        else { return new Vector3(0f, 0f, 0f); }
    }


    private Vector3 PositionNearestEnemySloth(GameController gameController, Vector3 actualIAPosition) {
        Vector3 nearestSloth = new Vector3(0f, 0f, 0f);
        Vector3 nearestSlothAux = new Vector3(0f, 0f, 0f);
        float norm = 0f;
        float normAux = 0f;
        foreach ( Sloth sloth in gameController.GetTeamBlue())
        {
            nearestSloth = sloth.transform.position;
            norm = (nearestSloth - actualIAPosition).magnitude;
            if (normAux < norm)
            {
                normAux = norm;
                nearestSlothAux = nearestSloth;
            }
        }
        return nearestSlothAux;
        
    }

    private Vector3 PositionNearestFriendlySloth(GameController gameController, Vector3 actualIAPosition)
    {
        Vector3 nearestSloth = new Vector3(0f, 0f, 0f);
        Vector3 nearestSlothAux = new Vector3(0f, 0f, 0f);
        float norm = 0f;
        float normAux = 0f;
        foreach (Sloth sloth in gameController.GetTeamRed())
        {
            nearestSloth = sloth.transform.position;
            norm = (nearestSloth - actualIAPosition).magnitude;
            if (normAux < norm)
            {
                normAux = norm;
                nearestSlothAux = nearestSloth;
            }
        }
        return nearestSlothAux;

    }

    private List<float> GetRangeAbilities(GameController gameController)
    {
        List<float> rangeListAbilities = new List<float>();
        rangeListAbilities.Add(gameController.GetCurrentSloth().GetAbility1().GetRange());
        rangeListAbilities.Add(gameController.GetCurrentSloth().GetAbility2().GetRange());
        rangeListAbilities.Add(gameController.GetCurrentSloth().GetAbility3().GetRange());

        return rangeListAbilities;

    }
    
    
}