using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    float speed = 10.0f;
    GameObject[] projectile;
    GameObject[] explosion;

	// Use this for initialization
	void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        projectile = GameObject.FindGameObjectsWithTag("projectile");
        explosion = GameObject.FindGameObjectsWithTag("explosion");

        if (!TurnController.Instance.GetActiveSloth().GetComponent<AnimPlayer>().GetMove()
            && !TurnController.Instance.GetActiveSloth().GetComponent<ShotScript>().GetShotLoad()
            && projectile.Length == 0
            && explosion.Length == 0)
        {
            MoveCameraFreely();
        }
        else if (projectile.Length > 0)
        {
            Debug.Log("Projectiles: " + projectile.Length.ToString());
            transform.position = new Vector3(projectile[0].transform.position.x, 4.0f, -15.0f);
        }
        else if (explosion.Length > 0)
        {
            Debug.Log("Explosions: " + explosion.Length.ToString());
            transform.position = new Vector3(explosion[0].transform.position.x, 4.0f, -15.0f);
        }
        else if (TurnController.Instance.GetActiveSloth().GetComponent<AnimPlayer>().GetMove())
        {
            transform.position = new Vector3(TurnController.Instance.GetActiveSloth().transform.position.x, 4.0f, -15.0f);
        }

        projectile = new GameObject[] {};
        explosion = new GameObject[] {};
	}

    public void MoveCameraFreely()
    {
        if (Input.GetAxis("Mouse X") > 0)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0.0f, 0.0f);    
        }
        else if (Input.GetAxis("Mouse X") < 0)
        {
            transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0.0f, 0.0f);
        }
    }
}
