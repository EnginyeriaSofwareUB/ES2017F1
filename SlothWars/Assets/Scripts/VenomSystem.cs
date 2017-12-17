using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenomSystem {
    private List<Venom> venoms;
    public VenomSystem()
    {
        venoms = new List<Venom>();
    }
    public void AddVenom(Venom v)
    {
        venoms.Add(v);
    }
    public void ApplyVenoms()
    {
        for (int i=0;i< venoms.Count; i++)
        {
            venoms[i].Apply();
            if (venoms[i].Finished())
            {
                venoms.Remove(venoms[i]);
                i--;
            }
        }
    }
}
