using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    float speed = 10.0f;
    float cameraDistance = -8.0f;
    GameObject[] projectile;
    GameObject[] explosion;
	int Boundary = 50;

	int theScreenWidth = Screen.width;
	int theScreenHeight = Screen.height;

	GameObject turn;

	// Use this for initialization
	void Start () 
    {
		turn = TurnController.Instance.GetActiveSloth ();
		if (turn != null)
			transform.position = new Vector3(turn.transform.position.x, 4.0f, cameraDistance);
	}
	
	// Update is called once per frame
	void Update ()
    {
        projectile = GameObject.FindGameObjectsWithTag("projectile");
        explosion = GameObject.FindGameObjectsWithTag("explosion");

        if (!TurnController.Instance.GetActiveSloth().GetComponent<AnimPlayer>().IsMoving()
            && !TurnController.Instance.GetActiveSloth().GetComponent<ShotScript>().GetShotLoad()
            && projectile.Length == 0
            && explosion.Length == 0)
        {
            MoveCameraFreely();
        }
        else if (projectile.Length > 0)
        {
            Debug.Log("Projectiles: " + projectile.Length.ToString());
            transform.position = new Vector3(projectile[0].transform.position.x, 4.0f, cameraDistance);
        }
        else if (explosion.Length > 0)
        {
            Debug.Log("Explosions: " + explosion.Length.ToString());
            transform.position = new Vector3(explosion[0].transform.position.x, 4.0f, cameraDistance);
        }
        else if (TurnController.Instance.GetActiveSloth().GetComponent<AnimPlayer>().IsMoving())
        {
            transform.position = new Vector3(TurnController.Instance.GetActiveSloth().transform.position.x, 4.0f, cameraDistance);
        }

		if (turn != TurnController.Instance.GetActiveSloth ()) 
		{
			transform.position = new Vector3(TurnController.Instance.GetActiveSloth().transform.position.x, 4.0f, cameraDistance);
			turn = TurnController.Instance.GetActiveSloth ();
		}

        projectile = new GameObject[] {};
        explosion = new GameObject[] {};
	}

    public void MoveCameraFreely()
    {
		if (Input.mousePosition.x > theScreenWidth - Boundary)
		{
			transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0.0f, 0.0f);    
		}

		if (Input.mousePosition.x < 0 + Boundary)
		{
			transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0.0f, 0.0f);    
		}
    }
}
