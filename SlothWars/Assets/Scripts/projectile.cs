using System;
using UnityEngine;
public interface Projectile
{

    //subject to change
    void ApplyLogic();
    void SetAll( Vector3 position,Vector3 aimVector, Quaternion rotation ,float range ,float radius);
}
