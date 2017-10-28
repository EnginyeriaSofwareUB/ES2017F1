using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageView : MonoBehaviour {

    private ChangeImageModel changeImageModel;

	void Start () {
        changeImageModel = new ChangeImageModel();		
	}
	
	void Update () {
		ShowImage();
	}

    // Method to show the image in the scene. Called by end turn Button in order to show the image corresponding to the sloth's turn.
    public Sprite ShowImage()
    {
        return changeImageModel.GetImage().sprite;
    }
}
