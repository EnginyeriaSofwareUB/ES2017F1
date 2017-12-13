using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private bool shot = false;
    float speed = 10.0f;
    float cameraDistance = -12.0f;
    float cameraHeight = 7.0f;
    GameObject[] projectile;
    GameObject[] explosion;
	int Boundary = 50;

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
		turn = TurnController.Instance.GetActiveSloth ();
        if (turn != null)
        {
            transform.position = new Vector3(turn.transform.position.x, turn.transform.position.y + cameraHeight, cameraDistance);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        projectile = GameObject.FindGameObjectsWithTag("projectile");
        explosion = GameObject.FindGameObjectsWithTag("explosion");
        //print("IS MOVING: " + TurnController.Instance.GetActiveSloth().GetComponent<AnimPlayer>().IsMoving().ToString());

        if (TurnController.Instance.GetActiveSloth().GetComponent<AnimPlayer>().IsMoving())
        {
            transform.position = new Vector3(TurnController.Instance.GetActiveSloth().transform.position.x, 
                TurnController.Instance.GetActiveSloth().transform.position.y + cameraHeight, cameraDistance);
        }
        else if (!TurnController.Instance.GetActiveSloth().GetComponent<AnimPlayer>().IsMoving()
            && !TurnController.Instance.GetActiveSloth().GetComponent<ShotScript>().GetShotLoad()
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

		if (turn != TurnController.Instance.GetActiveSloth ()) 
		{
            transform.position = new Vector3(TurnController.Instance.GetActiveSloth().transform.position.x, 
                TurnController.Instance.GetActiveSloth().transform.position.y + cameraHeight, cameraDistance);
			turn = TurnController.Instance.GetActiveSloth ();
		}
        if(explosion != null && projectile != null && explosion.Length == 0 && projectile.Length == 0 && shot)
        {
            shot = false;
            transform.position = new Vector3(TurnController.Instance.GetActiveSloth().transform.position.x,
                TurnController.Instance.GetActiveSloth().transform.position.y + cameraHeight, cameraDistance);
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

        if (Input.mousePosition.y > theScreenHeight - Boundary)
        {
            transform.position += new Vector3(0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0.0f);   
        }

        if (Input.mousePosition.y < 0 + Boundary)
        {
            transform.position += new Vector3(0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed, 0.0f);   
        }

        float fov = Camera.main.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
