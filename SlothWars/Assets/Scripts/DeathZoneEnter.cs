using UnityEngine;

public class DeathZoneEnter : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        GameObject.Find("Main Camera").GetComponent<GameController>().OnDieSloth(
            other.gameObject.GetComponentInChildren<Sloth>());
    }

}
