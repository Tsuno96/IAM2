using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behavior/Alignement")]
public class AlignementBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(Boid b, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
            return b.transform.up;

        Vector2 alignementMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(b, context);
        foreach (Transform item in filteredContext)
        {
            alignementMove += (Vector2)item.transform.up;
        }
        alignementMove /= context.Count;

        return alignementMove;

    }
}
