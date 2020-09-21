using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
   // public Vector3 vec3_Pos;
   // public Vector3 vec3_Velocity;
    public Collider2D col_BoidCollider;
    // Start is called before the first frame update
    void Start()
    {
        col_BoidCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
