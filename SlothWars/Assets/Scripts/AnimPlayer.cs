﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour {

    public Rigidbody rbody;
    public Sloth sloth;
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

    public AnimPlayer(Sloth sloth)
    {
        this.sloth = sloth;
    }

    void Start()
    {
        falling = false;
        playerSpeed = 2;
        currentAxis = 0;
        ss = GetComponentInChildren<ShotScript>();
        hs = GetComponentInChildren<HealthScript>();
        rbody = GetComponent<Rigidbody>();
        changeTurnModel = new ChangeTurnModel();
        selected = GetComponentInChildren<SlothSelected>();
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
            //anim.SetFloat("inputV", inputV);
            if (inputV > 0.1)
            {
                move = true;
                inputV = 1;
                newPosition = transform.position + new Vector3(0f, 1f, 0f);
                ss.IsMoving(0);
                //changeTurnModel.DecrementApCurrentSloth(1);
            }
            if (inputV < -0.1)
            {
                move = true;
                inputV = -1;
                newPosition = transform.position + new Vector3(0f, -1f, 0f);
                ss.IsMoving(0);
                //changeTurnModel.DecrementApCurrentSloth(1);
            }
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
            if (inputH > 0.1)
            {
                move = true;
                inputH = 1;
                newPosition = transform.position + new Vector3(1f, 0f, 0f);
                ss.IsMoving(0);
                hs.turnRight();
                changeTurnModel.DecrementApCurrentSloth(1);
                selected.GetLeaf().transform.position = newPosition + new Vector3(-1, 2, 0);
                selected.GetLeaf().transform.rotation = Quaternion.Euler(0, 90, 90);
            }
            if (inputH < -0.1)
            {
                move = true;
                inputH = -1;
                newPosition = transform.position + new Vector3(-1f, 0f, 0f);
                ss.IsMoving(1);
                hs.turnLeft();
                changeTurnModel.DecrementApCurrentSloth(1);
                selected.GetLeaf().transform.position = newPosition + new Vector3(+1, 2, 0);
                selected.GetLeaf().transform.rotation = Quaternion.Euler(0, 90, 90);
            }
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
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
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
        newPos.y = Mathf.Floor(newPos.y - 0.5f) + 0.55f;
        gameObject.transform.position = newPos;
    }

    public bool IsMoving()
    {
        return move;
    }

    public Sloth GetSloth()
    {
        return this.sloth;
    }

    public void SetSloth(Sloth sloth)
    {
        this.sloth = sloth;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}

