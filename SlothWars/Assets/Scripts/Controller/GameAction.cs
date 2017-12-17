using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameAction
{
    public ActionType actionType;
    public float x, y;
    public Sloth targetSloth;
    public Vector3 aimVector;
    public float angleAbility;
    public Ability ability;

    public GameAction() { }

    public enum ActionType
    {
        MOVERSE,EJECUTAR_HABILIDAD,ENDTURN
    }




}