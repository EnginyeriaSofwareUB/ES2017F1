using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GameAction
{
    public ActionType actionType;
    public float x, y;
    public Ability ability;

    public GameAction() { }

    public enum ActionType
    {
        MOVERSE,EJECUTAR_HABILIDAD,ENDTURN
    }




}