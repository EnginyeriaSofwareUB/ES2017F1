using System;

public interface Ability
{

    void Apply(ref Sloth target);
    void Apply();
    float GetRange();
    float GetRadius();
}