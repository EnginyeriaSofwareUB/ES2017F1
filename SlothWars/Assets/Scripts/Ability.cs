using System;

public interface Ability
{

    void Apply(Sloth target);
    void Apply();
    float GetRange();
    float GetRadius();
}