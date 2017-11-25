using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour {

    public Rigidbody rbody;
    public Sloth sloth;

    private float inputH;
    private float inputV;
    private int currentAxis;  // 0 = horizontal 1 = vertical
    private int playerSpeed;
    private bool move = false;
    private ChangeTurnModel changeTurnModel;
    Vector3 newPosition = new Vector3(0f, 0f, 0f);
    ShotScript ss;
    HealthScript hs;

    public AnimPlayer(Sloth sloth)
    {
        this.sloth = sloth;
    }

    void Start()
    {
        playerSpeed = 2;
        currentAxis = 0;
        ss = GetComponentInChildren<ShotScript>();
        hs = GetComponentInChildren<HealthScript>();
        rbody = GetComponent<Rigidbody>();
        changeTurnModel = new ChangeTurnModel();
    }

    // Update is called once per frame
    void Update()
    {
        if (changeTurnModel.GetApCurrentSloth() >= 0 ){
            //Debug.Log("Current sloth: " + changeTurnModel.GetApCurrentSloth());
            if (!move)
            {
                Movement_Interpretation();
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
        } else{
            //Debug.Log("HELLO THIS SHOULD NOT MOVE" +changeTurnModel.GetApCurrentSloth());
            inputH = 0;
            move = false;
            ss.IsNotMoving();
        }
    }
        
    void Movement_Interpretation()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
        // Debug.Log("Horizontal axis: " + inputH + " | Vertical axis: " + inputV);
        if (inputH != 0 && inputV != 0)
        {
            Debug.Log("Only one direction at a time plz");
        }
        else if (inputH != 0)
        {
            IsBlockInFront();
            currentAxis = 0;
            Left_Or_Right();
        }
        else if (inputV != 0)
        {
            IsBlockInFront();
            currentAxis = 1;
            Up_Or_Down();
        }
        
    }

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
        if (!move)
        {
            //anim.SetFloat("inputH", inputH);
            if (inputH > 0.1)
            {
                move = true;
                inputH = 1;
                newPosition = transform.position + new Vector3(1f, 0f, 0f);
                ss.IsMoving(0);
                //changeTurnModel.DecrementApCurrentSloth(1);
            }
            if (inputH < -0.1)
            {
                move = true;
                inputH = -1;
                newPosition = transform.position + new Vector3(-1f, 0f, 0f);
                ss.IsMoving(1);
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
                inputH = 0;
                move = false;
                ss.IsNotMoving();
            }
        }
    }

    public bool IsBlockInFront()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, 3))
        {
            Debug.Log("There's SOMETHING in front of the object!");
            return true;
        }
        Debug.Log("There NOTHING in front of the object!");
        return false;
    }

    public bool IsMoving()
    {
        return this.move;
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

