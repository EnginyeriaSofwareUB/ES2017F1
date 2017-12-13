using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothSelected : MonoBehaviour {

	public GameObject leaf;
    private Vector3 slothPosition;


    void Update(){
        leaf.transform.position = leaf.transform.position + new Vector3(0f, 0.015f*Mathf.Cos(Time.time), 0f);
        leaf.transform.rotation = Quaternion.Euler(-160, 0, 90);
    }

    public void Active(bool b)
    {
        leaf.gameObject.SetActive(b);
    }

    public void SetLeaf(GameObject leafScene){
        leaf = leafScene;
        leaf.transform.position = leaf.transform.position + new Vector3(0f, 1f, 0f);
    }

    public GameObject GetLeaf(){
        return leaf;
    }

    public Vector3 GetSlothPosition(){
        return this.slothPosition;
    }

    public void SetSlothPosition(Vector3 slothPosition){
        this.slothPosition = slothPosition;
    }

}

