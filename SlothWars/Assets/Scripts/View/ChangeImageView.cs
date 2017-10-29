using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageView : MonoBehaviour {

    private ChangeImageModel changeImageModel;
    private Sprite newSprite;
    void Awake () {
        changeImageModel = new ChangeImageModel();
        newSprite = GameObject.Find("AbilitiesPanel").GetComponentsInChildren<Sprite>()[1];
    }
	
	void Update () {

        ShowImage();

    }

    // Method to show the image in the scene. Called by end turn Button in order to show the image corresponding to the sloth's turn.
    public void ShowImage()
    {
       newSprite = changeImageModel.GetImage();
    }
}
