using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private bool shot = false;
    float cameraDistance = -12.0f;

    private GameController2 gameController;
   
    GameObject[] projectile;
    GameObject[] explosion;
	int Boundary = 50;
	float centerCorrection;
    // Screen Size
	int theScreenWidth = Screen.width;
	int theScreenHeight = Screen.height;

    // Mouse Zoom variables
    float minFov = 15.0f;
    float maxFov = 90.0f;
    float sensitivity = 10.0f;

	GameObject turn;

	// Use this for initialization
	void Start () 
    {
        gameController = GameObject.Find("Main Camera").GetComponent<GameController2>();
		turn = null;
        if (turn != null)
        {
            transform.position = new Vector3(turn.transform.position.x, turn.transform.position.y, cameraDistance);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        projectile = GameObject.FindGameObjectsWithTag("projectile");
        explosion = GameObject.FindGameObjectsWithTag("explosion");
        //print("IS MOVING: " + TurnController.Instance.GetActiveSloth().GetComponent<AnimPlayer>().IsMoving().ToString());
        if(turn == null){
            if( gameController.GetCurrentSloth() != null){
                turn = gameController.GetCurrentSloth().gameObject;
                transform.position = new Vector3(turn.transform.position.x, turn.transform.position.y, cameraDistance);
            }
        } else if (turn.GetComponent<MovementController>().IsMoving())
        {
			
            transform.position = new Vector3(gameController.GetCurrentSloth().gameObject.transform.position.x, 
                
				gameController.GetCurrentSloth().gameObject.transform.position.y , 

				cameraDistance);
        }
        else if (!turn.GetComponent<MovementController>().IsMoving()

            && projectile.Length == 0
            && explosion.Length == 0)
        {
            MoveCameraFreely();
        }
        else if (projectile != null)
        {
            if (projectile.Length > 0)
            {
                shot = true;
                transform.position = new Vector3(projectile[0].transform.position.x, projectile[0].transform.position.y, cameraDistance);
            }
        }
        else if (explosion != null)
        {
            if (explosion.Length > 0)
            {
                shot = true;
                transform.position = new Vector3(explosion[0].transform.position.x, projectile[0].transform.position.y, cameraDistance);
            }
        }

		if (gameController.GetCurrentSloth() != null && turn != gameController.GetCurrentSloth().gameObject) 
		{
            transform.position = new Vector3(gameController.GetCurrentSloth().gameObject.transform.position.x, 
                gameController.GetCurrentSloth().gameObject.transform.position.y , cameraDistance);
			turn = gameController.GetCurrentSloth().gameObject;
		}
        if(explosion != null && projectile != null && explosion.Length == 0 && projectile.Length == 0 && shot)
        {
            shot = false;
            transform.position = new Vector3(gameController.GetCurrentSloth().gameObject.transform.position.x,
                gameController.GetCurrentSloth().gameObject.transform.position.y, cameraDistance);
        } 
        projectile = new GameObject[] {};
        explosion = new GameObject[] {};
	}

    public void MoveCameraFreely()
    {
		Vector3 mousePos;
		float scrollSpeed = 0.1f;
		mousePos = Input.mousePosition; //We need to get the new position every frame

		//if mouse is 50 pixels and less from the left side of the
		//screen, we move the camera in that direction at scrollSpeed
		if (mousePos.x < 50) {
			if (!(gameObject.transform.position.x < 13.5f)) {
				gameObject.transform.Translate (-scrollSpeed, 0, 0);
			}
		}
		//if 50px or less from the right side, move right at scrollSpeeed
		if (mousePos.x > Screen.width - 50) {
			if (!(gameObject.transform.position.x > 90.7f)) {
				gameObject.transform.Translate (scrollSpeed, 0, 0);
			}
		}
		//move up
		if (mousePos.y < 10) {
			if ((gameObject.transform.position.y > 0.8f)) {
				gameObject.transform.Translate (0, -scrollSpeed, 0);
			}
		}
			//move down
		if (mousePos.y > Screen.height - 50){
			if ((gameObject.transform.position.y < 15.3f)) {
				gameObject.transform.Translate (0, scrollSpeed, 0);
			}
		}


        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
