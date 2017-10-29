using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageModel {

    private static Image slothImageSelected;

    public ChangeImageModel() { }

    //Getters and Setters
    public Image GetImage() { Debug.Log("Hola, estoy en model"); return slothImageSelected; }
    public void SetImage(Image imageFromPreviousScene) { slothImageSelected = imageFromPreviousScene; }
    
}
