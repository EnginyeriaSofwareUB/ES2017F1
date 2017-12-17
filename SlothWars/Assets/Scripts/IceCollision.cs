using UnityEngine;

public class IceCollision : MonoBehaviour {

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLIDE 11111111111");
        if ("Destroyable".Equals(collision.gameObject.tag))
        {
            Debug.Log("COLLIDE 222222222222");
            Vector3 nextPos = gameObject.transform.position;
            nextPos = new Vector3(nextPos.x - 1, nextPos.y, nextPos.z);
            gameObject.transform.position = nextPos;
        }
    }
}
