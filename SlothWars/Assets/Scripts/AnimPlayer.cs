using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour {

    public Rigidbody rbody;
    SlothSelected selected;
    private float inputH;
    private float inputV;
    private int currentAxis;  // 0 = horizontal 1 = vertical
    private int playerSpeed;
    private bool move = false;
    private ChangeTurnModel changeTurnModel;
    Vector3 newPosition = new Vector3(0f, 0f, 0f);
    ShotScript ss;
    HealthScript hs;
    private bool falling;
	Vector3 aPos;

    public AnimPlayer(){}

    void Start()
    {
        falling = false;
        playerSpeed = 2;
        currentAxis = 0;
        ss = GetComponentInChildren<ShotScript>();
        hs = GetComponentInChildren<HealthScript>();
        rbody = GetComponent<Rigidbody>();
        changeTurnModel = ChangeTurnModel.Instance;
        selected = GetComponentInChildren<SlothSelected>();
		aPos = gameObject.transform.position;
		//gameObject.transform.position = new Vector3 (aPos.x,aPos.y+1,aPos.z-1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!falling)
        {
            if (changeTurnModel.GetApCurrentSloth() >= 0)
            {
                //Debug.Log("Current sloth: " + changeTurnModel.GetApCurrentSloth());
                if (!move)
                {
                    if (IsBlockInFront())
                    {
                        Movement_Interpretation();
                    }
                    else
                    {
                        falling = true;
                        ScreenMessage.sm.ShowMessage("Whoops...", 0.7f);
                        StartCoroutine(ApplyGravity());
                    }
                }
                else
                {
                    if (currentAxis == 0)
                    {
                        Left_Or_Right();
                    }
                    if (currentAxis == 1)
                    {
                        Up_Or_Down();
                    }
                }
            }
            else
            {
                ScreenMessage.sm.ForceShowMessage("You don't have any action points left",0.1f);
                inputH = 0;
                inputV = 0;
                move = false;
                ss.IsNotMoving();
            }
        }
    }
        
    void Movement_Interpretation()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        // Debug.Log("Horizontal axis: " + inputH + " | Vertical axis: " + inputV);
        if (inputH != 0 && inputV != 0)
        {
            ScreenMessage.sm.ShowMessage("Sloths can only move one direction at a time", 3.0f);
        }
        else if (inputH != 0)
        {
            currentAxis = 0;
            Left_Or_Right();
        }
        else if (inputV != 0)
        {
            currentAxis = 1;
            Up_Or_Down();
        }
        
    }

    // Checking movement of the player using arrow keys or W and S keys
    void Up_Or_Down() {
        if (!move) {

            selected.enabled = false;
            selected.Active(false);
            //anim.SetFloat("inputV", inputV);
            if (inputV > 0.1)
            {
                move = true;
                inputV = 1;
                newPosition = transform.position + new Vector3(0f, 1f, 0f);
                ss.IsMoving(0);
                changeTurnModel.DecrementApCurrentSloth(1);
                selected.GetLeaf().transform.position = newPosition + new Vector3(0f, 1f, 0f);
                selected.GetLeaf().transform.rotation = Quaternion.Euler(0, 90, 90);
                selected.SetSlothPosition(newPosition);
            }
            if (inputV < -0.1)
            {
                move = true;
                inputV = -1;
                newPosition = transform.position + new Vector3(0f, -1f, 0f);
                ss.IsMoving(0);
                changeTurnModel.DecrementApCurrentSloth(1);
                selected.GetLeaf().transform.position = newPosition + new Vector3(0f, 3f, 0f);
                selected.GetLeaf().transform.rotation = Quaternion.Euler(0, 90, 90);
                selected.SetSlothPosition(newPosition);
            }

            selected.enabled = true;
            selected.Active(true);
        }
        else
        {
            if (Vector3.Distance(transform.position, newPosition) > Vector3.kEpsilon)
            {
                float step = playerSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
            }
            else
            {
                inputV = 0;
                move = false;
                ss.IsNotMoving();
            }
        }
    }

    // Checking movement of the player using arrow keys or A and D keys
    void Left_Or_Right()
    {
        if (!move && !ss.GetShotLoad())
        {

            selected.enabled = false;
            selected.Active(false);
            if (inputH > 0.1)
            {
                move = true;
                inputH = 1;
                newPosition = transform.position + new Vector3(1f, 0f, 0f);
                ss.IsMoving(0);
                hs.turnRight();
                changeTurnModel.DecrementApCurrentSloth(1);
                selected.GetLeaf().transform.position = newPosition + new Vector3(-1, 0, 0);
                selected.GetLeaf().transform.rotation = Quaternion.Euler(0, 90, 90);
                selected.SetSlothPosition(newPosition);
            }
            if (inputH < -0.1)
            {
                move = true;
                inputH = -1;
                newPosition = transform.position + new Vector3(-1f, 0f, 0f);
                ss.IsMoving(1);
                hs.turnLeft();
                changeTurnModel.DecrementApCurrentSloth(1);
                selected.GetLeaf().transform.position = newPosition + new Vector3(+1, 0, 0);
                selected.GetLeaf().transform.rotation = Quaternion.Euler(0, 90, 90);
                selected.SetSlothPosition(newPosition);
            }
            selected.enabled = true;
            selected.Active(true);
        }
        else
        {
            if (Vector3.Distance(transform.position, newPosition) > Vector3.kEpsilon)
            {
                float step = playerSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
            }
            else
            {
                inputH = 0;
                move = false;
                ss.IsNotMoving();
            }
        }
    }

    // Checks if there is a block in front of the current sloth
    public bool IsBlockInFront()
    {
		Vector3 fwd = transform.TransformDirection(transform.forward);
		//Debug.DrawRay (transform.position, fwd, Color.green);
        if (Physics.Raycast(transform.position, fwd, 3))
        {
            return true;
        }
        return false;
    }

    // Makes the sloth fall off the tree. It grabs to another block if it founds any.
    private IEnumerator ApplyGravity()
    {
        rbody.useGravity = true;
        yield return new WaitForSeconds(0.01f);
        while (!IsBlockInFront())
        {
            yield return new WaitForSeconds(0.01f);
        }
        rbody.useGravity = false;
        rbody.velocity = Vector3.zero;
        GrabPositionCorrection();
        ScreenMessage.sm.ForceShowMessage("Whew!", 1.0f);
        falling = false;
    }
   
    private void GrabPositionCorrection()
    {
        Vector3 newPos = gameObject.transform.position;
        //newPos.y = Mathf.Floor(newPos.y - 0.5f) + 0.55f;
		newPos.y = newPos.y-0.3f;
        gameObject.transform.position = newPos;
    }

    public bool IsMoving()
    {
        return move;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

