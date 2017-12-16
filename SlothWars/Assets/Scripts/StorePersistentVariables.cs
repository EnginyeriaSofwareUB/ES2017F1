using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePersistentVariables{

    // Here we will have the variables that we want to persist between scenes. In our case,
    // it will be the sloth teams created in TeamSelection scene.

    /* 
     To refer to this variables in any cs file, you have to put the code below:

        if you want to set it:
            StorePersistentVariables.Instance.NAMEPERSISTENTVALUE = value;
        if you want to get it:
            value = StorePersistentVariables.Instance.NAMEPERSISTENTVALUE;
    
    */

    // PERSISTENT (PUBLIC) VARIABLES HERE:
	public List<string> slothTeam1 = new List<string>();
	public List<string> slothTeam2 = new List<string>();
    private static StorePersistentVariables instance;
    public bool iaPlaying = false;
    public int winner = 0;
    
    //

    public static StorePersistentVariables Instance
    {
        get
        {
            if(instance == null){
                instance = new StorePersistentVariables();
            }
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    protected StorePersistentVariables() { }
}
