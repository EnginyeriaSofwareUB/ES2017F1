using System;
using UnityEngine;
public interface Ability
{

    void Apply(ref Sloth target);
    void Apply(GameObject g);
    float GetRange();
    float GetRadius();
    bool GetBuildTerrain();
    int GetTerrainSize();
	int GetAp();
}