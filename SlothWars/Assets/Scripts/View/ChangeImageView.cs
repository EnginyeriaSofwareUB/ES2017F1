using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageView : MonoBehaviour {

    private ChangeImageModel changeImageModel;
    private Image newImage;
    void Awake () {
        changeImageModel = new ChangeImageModel();
        newImage = GameObject.Find("AbilitiesPanel").GetComponentsInChildren<Image>()[1];
    }
	
	void Update () {
        print("Hola, estoy en Update");
        
        if (changeImageModel.GetImage() != null)
        {
            print("Voy a mostrar la imagen!!");
            ShowImage();
        }
        print("Problema: No coge la imagen de la escena anterior");
    }

    // Method to show the image in the scene. Called by end turn Button in order to show the image corresponding to the sloth's turn.
    public void ShowImage()
    {
       newImage.sprite  = changeImageModel.GetImage().sprite;
    }
}
