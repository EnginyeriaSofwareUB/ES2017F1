using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour {

    public Animator anim;
    public Rigidbody rbody;
    public Sloth sloth;

    private float inputH;
    private float inputV;
    private bool move = false;
    private ChangeTurnModel changeTurnModel;
    Vector3 newPosition = new Vector3(0f, 0f, 0f);
    ShotScript ss;
    HealthScript hs;
    // Use this for initialization

    public AnimPlayer(Sloth sloth)
    {
        this.sloth = sloth;
    }

    void Start()
    {
        ss = GetComponentInChildren<ShotScript>();
        hs = GetComponentInChildren<HealthScript>();
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        changeTurnModel = new ChangeTurnModel();
    }

    // Update is called once per frame
    void Update()
    {
        //set the ap so the fucking animator doesnt play the animation ffs
        anim.SetInteger("ap", changeTurnModel.GetApCurrentSloth());
        if (changeTurnModel.GetApCurrentSloth() >= 0 ){
            print(changeTurnModel.GetApCurrentSloth());
            Movement_Interpretation();
            Movement_Correction();
            //Camera.main.transform.position = new Vector3(transform.position.x, 4.0f, -15.0f);
        } else{
            print("HELLO THIS SHOULD NOT MOVE"+changeTurnModel.GetApCurrentSloth());
            inputH = 0;
            anim.SetFloat("inputH", inputH);
            move = false;
            ss.IsNotMoving();
        }
    }
        
    void Movement_Interpretation()
    {
        //Jump();
        Left_Or_Right();
    }

    // Method for checking spacebar press action to activate jump animation
    void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("jump", true);
        }
        else
        {
            anim.SetBool("jump", false);
        }
    }

    // Checking movement of the player using arrow keys or A and D keys
    void Left_Or_Right()
    {
        if (!move)
        {
            inputH = Input.GetAxis("Horizontal");
            anim.SetFloat("inputH", inputH);

            if (inputH > 0.1)
            {
                move = true;
                inputH = 1;
                newPosition = transform.position + new Vector3(1f, 0f, 0f);
                transform.eulerAngles = new Vector3(0, 90, 0);
                ss.IsMoving(0);
                hs.turnRight();
                changeTurnModel.DecrementApCurrentSloth(1);
            }
            if (inputH < -0.1)
            {
                move = true;
                inputH = -1;
                newPosition = transform.position + new Vector3(-1f, 0f, 0f);
                transform.eulerAngles = new Vector3(0, -90, 0);
                ss.IsMoving(1);
                hs.turnLeft();
                changeTurnModel.DecrementApCurrentSloth(1);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, newPosition) > Vector3.kEpsilon)
            {
                float step = 1 * Time.deltaTime;
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

    // Method for limiting available movement space for the player 
    void Movement_Correction()
    {
        if (transform.position.x > 150)
        {
            move = false;
            transform.position = new Vector3(150f, transform.position.y, transform.position.z);
        }
        if (transform.position.x < 0)
        {
            move = false;
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
    }

    public bool GetMove()
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

