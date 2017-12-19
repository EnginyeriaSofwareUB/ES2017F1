using System.Collections;
using UnityEngine;

public class SlothGravity : MonoBehaviour
{

    public Rigidbody rbody;
    private bool falling;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponentInParent<Rigidbody>();
        falling = false;
    }

    private void Update()
    {
        if (!gameObject.GetComponent<MovementController>().IsMoving())
        {
            if (!falling)
            {
                if (!IsBlockInFront())
                {
                    InvokeGravity();
                }
            }
        }
    }

    // Checks if there is a block in front of the current sloth
    public bool IsBlockInFront()
    {
        Vector3 fwd = transform.TransformDirection(transform.forward);
        Vector3 origin = transform.position;
        Vector3 rect = new Vector3(origin.x, origin.y, origin.z - 0.3f);
        Debug.DrawRay(rect, fwd, Color.green);
        return Physics.Raycast(rect, fwd, 2);
    }

    public bool IsBlockInDirection(int direction)
    {
        Vector3 origin = transform.position;
        Vector3 dtn = Vector3.zero;
        switch (direction)
        {
            case DirectionValues.RIGHT:
                dtn = new Vector3(1,0,0);
                break;
            case DirectionValues.LEFT:
                dtn = new Vector3(-1, 0, 0);
                break;
            case DirectionValues.UP:
                dtn = new Vector3(0, 1, 0);
                break;
            case DirectionValues.DOWN:
                dtn = new Vector3(0, -1, 0);
                break;
        }
        Debug.DrawRay(origin, dtn, Color.green);
        return Physics.Raycast(origin, dtn, 1f);
    }

    public void InvokeGravity()
    {
        Camera.main.gameObject.GetComponent<GameController>().NotifyFalling();
        falling = true;
        StartCoroutine(ApplyGravity());
    }

    // Makes the sloth fall off the tree. It grabs to another block if it founds any.
    private IEnumerator ApplyGravity()
    {
        rbody.useGravity = true;
        rbody.isKinematic = false;
        yield return new WaitForSeconds(0.01f);
        while (!IsBlockInFront())
        {
            yield return new WaitForSeconds(0.01f);
        }
        rbody.isKinematic = true;
        rbody.useGravity = false;
        rbody.velocity = Vector3.zero;
        GrabPositionCorrection();
        falling = false;
        Camera.main.gameObject.GetComponent<GameController>().NotifyFallingEnded();
    }

    private void GrabPositionCorrection()
    {
        Vector3 newPos = gameObject.transform.parent.position;
        newPos.y = newPos.y - 0.38f;
        gameObject.transform.parent.position = newPos;
    }
}