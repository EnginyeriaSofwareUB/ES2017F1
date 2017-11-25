using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlothSelected : MonoBehaviour {

	public GameObject leaf;


		
    public void Active(bool b)
    {
        leaf.gameObject.SetActive(b);
    }

    public void SetLeaf(GameObject leafScene){
        leaf = leafScene;
    }

    public GameObject GetLeaf(){
        return leaf;
    }

}

