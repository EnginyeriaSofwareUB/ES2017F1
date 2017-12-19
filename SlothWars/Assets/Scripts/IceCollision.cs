using UnityEngine;

public class IceCollision : MonoBehaviour {

    private static bool first = true;

    public void OnCollisionEnter(Collision collision)
    {
        if (first || "Destroyable".Equals(collision.gameObject.tag))
        {
            first = false;
            Vector3 nextPos = gameObject.transform.position;
            nextPos = new Vector3(nextPos.x - 1, nextPos.y, nextPos.z);
            gameObject.transform.position = nextPos;
        }
        else
        {
            first = true;
        }
    }
}
