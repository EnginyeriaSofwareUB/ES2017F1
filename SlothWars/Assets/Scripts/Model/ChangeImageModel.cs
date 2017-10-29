using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageModel {

    private static Sprite slothImageSelected;

    public ChangeImageModel() { }

    //Getters and Setters
    public Sprite GetImage() { return slothImageSelected; }
    public void SetSprite(Sprite imageFromPreviousScene) { slothImageSelected = imageFromPreviousScene; }
    
}
