using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(Boid b, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        foreach(Transform item in original)
        {
            Boid itemb = item.GetComponent<Boid>();
            if(itemb !=null && itemb.agentFlock == b.agentFlock)
            {
                filtered.Add(item);
            }

        }
        return filtered;
    }
}
