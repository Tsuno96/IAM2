using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(Boid b, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
            return Vector3.zero;

        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(b, context);
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - b.transform.position)< flock.squareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(b.transform.position - item.position);
            }
            
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;

    }
}
