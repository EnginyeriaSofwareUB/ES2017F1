using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeMark {
    private string path = "Objects/CubeMark";
	public void MakeCubeMarks(float range,Vector3 position)
    {
        position.z = 1;
        Collider[] cubes  = Physics.OverlapSphere(position, range);
        foreach (Collider c in cubes)
        {
            if((position - c.transform.position).magnitude <= range && c.transform.position.z == 1)
            {
                GameObject.Instantiate(Resources.Load(path), c.transform.position + new Vector3(0,0,-0.501f),Quaternion.identity);
            }
        }
    }
    public void DestroyCubeMarks()
    {
        foreach (GameObject cm in GameObject.FindGameObjectsWithTag("cubeMark"))
        {
            GameObject.Destroy(cm);
        }
    }
}
