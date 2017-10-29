using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageModel {

    private static Sprite slothSpriteSelected;

    public ChangeImageModel() { }

    //Getters and Setters
    public Sprite GetSprite() { return slothSpriteSelected; }
    public void SetSprite(Sprite spriteFromPreviousScene) { slothSpriteSelected = spriteFromPreviousScene; }
    
}
