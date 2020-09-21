using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Behavior/Alignement")]
public class AlignementBehavior : FlockBehavior
{
    public override Vector2 CalculateMove(Boid b, List<Transform> context, Flock flock)
    {
        if (context.Count == 0)
            return b.transform.up;

        Vector2 alignementMove = Vector2.zero;
        foreach (Transform item in context)
        {
            alignementMove += (Vector2)item.transform.up;
        }
        alignementMove /= context.Count;

        return alignementMove;

    }
}
