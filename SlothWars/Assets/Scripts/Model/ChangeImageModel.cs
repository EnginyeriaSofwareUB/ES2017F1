using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
public class ChangeImageModel
{

    private static ChangeImageModel instance;
    public static ChangeImageModel Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ChangeImageModel();
            }
            return instance;
        }
    }

    private Sprite slothSpriteSelected;
    private JSONNode n;
    private  string s;
    private int lastId;
    private string name;
    private ChangeTurnModel changeTurnModel;

    protected ChangeImageModel() {
        s = ((TextAsset)Resources.Load("slothapedia")).text;
        n = JSON.Parse(s);
        changeTurnModel = ChangeTurnModel.Instance;
    }


    //Getters and Setters
    public Sprite GetSprite() { return slothSpriteSelected; }
    public void SetSprite(Sprite spriteFromPreviousScene) { slothSpriteSelected = spriteFromPreviousScene; }
    public void SendSlothInfo(string type)
    {
        switch(type){
            case "Wizard":
                lastId = 0;
                break;
            case "Tank":
                lastId = 1;
                break;
            case "Archer":
                lastId = 2;
                break;
            case "Healer":
                lastId = 3;
                break;
            case "Utility":
                lastId = 4;
                break;
        }
    }

    public string LoadAbillitySprites(int idAb)
    {
        switch (idAb)
        {
            case 1:
                name = n[lastId.ToString()]["idAb1"];
                name = name.Insert(0, "Spellicons/");
                break;
            case 2:
                
                name = n[lastId.ToString()]["idAb2"];
                name = name.Insert(0, "Spellicons/");
                break;
            case 3:
                name = n[lastId.ToString()]["idAb3"];
                name = name.Insert(0, "Spellicons/");
                break;
        }
        return name;
    }

}
