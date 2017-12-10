using System;
using UnityEngine;
public interface Ability
{

    void Apply(ref Sloth target);
    void Apply(GameObject g);
    void Apply(Vector3 position);
    float GetRange();
    float GetRadius();
    bool GetBuildTerrain();
    int GetTerrainSize();
	int GetAp();
    bool GetAlterTerrain();
	string GetProjectile ();
	bool GetExplosive ();
	string GetSource ();
}