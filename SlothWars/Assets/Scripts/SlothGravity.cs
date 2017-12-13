using System.Collections;
using UnityEngine;

public class SlothGravity : MonoBehaviour
{

    public Rigidbody rbody;
    private bool falling;

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        falling = false;
    }

    private void Update()
    {
        if (!gameObject.GetComponent<AnimPlayer>().isActiveAndEnabled)
        {
            if (!falling)
            {
                if (!IsBlockInFront())
                {
                    invokeGravity();
                }
            }
        }
    }

    // Checks if there is a block in front of the current sloth
    public bool IsBlockInFront()
    {
        Vector3 fwd = transform.TransformDirection(transform.forward);
        Debug.DrawRay(transform.position, fwd, Color.green);
        if (Physics.Raycast(transform.position, fwd, 3))
        {
            return true;
        }
        return false;
    }

    public void invokeGravity()
    {
        falling = true;
        StartCoroutine(ApplyGravity());
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
        if (gameObject.GetComponent<AnimPlayer>().isActiveAndEnabled)
        {
            ScreenMessage.sm.ForceShowMessage("Whew!", 1.0f);
        }
        falling = false;
    }

    private void GrabPositionCorrection()
    {
        Vector3 newPos = gameObject.transform.position;
        //newPos.y = Mathf.Floor(newPos.y - 0.5f) + 0.55f;
        newPos.y = newPos.y - 0.3f;
        gameObject.transform.position = newPos;
    }
}