using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Boid : MonoBehaviour
{

    public Flock agentFlock;



    public Collider2D col_BoidCollider;
    // Start is called before the first frame update
    void Start()
    {
        col_BoidCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Flock f)
    {
        agentFlock = f;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
