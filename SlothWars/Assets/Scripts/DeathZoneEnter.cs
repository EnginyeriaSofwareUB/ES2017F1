using UnityEngine;

public class DeathZoneEnter : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "sloth")
        {
            GameObject.Find("Main Camera").GetComponent<GameController>().OnDieSloth(other.gameObject.GetComponentInChildren<Sloth>());
            GameObject.Find("Main Camera").GetComponent<GameController>().NotifyFallingEnded();
        }
        else
        {
            Destroy(other.gameObject);
        }
    }

}
