using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageModel {

    private Image slothImageSelected;

    public ChangeImageModel() { }

    //Getters and Setters
    public Image GetImage() { return slothImageSelected; }
    public void SetImage(Image imageFromPreviousScene) { slothImageSelected = imageFromPreviousScene; }
    
}
