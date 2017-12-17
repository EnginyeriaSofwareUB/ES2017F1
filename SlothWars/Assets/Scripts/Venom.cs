using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venom  {
    private Sloth sloth;
    private Ability ability;
    private int turns;
    public Venom(Sloth s, Ability a, int t)
    {
        sloth = s;
        ability = a;
        turns = t;
    }
    public void Apply()
    {
        turns -= 1;
        ability.Apply(ref sloth);
    }
    public bool Finished()
    {
        return turns <= 0;
    }
}
