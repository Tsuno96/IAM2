using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

public class Flock : MonoBehaviour
{

    public Boid boid;
    List<Boid> lst_Boids = new List<Boid>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int nbBoids = 200;
    
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    public float squareAvoidanceRadius;
    


    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for(int i = 0; i<nbBoids; i++)
        {
            Vector3 pos = Random.insideUnitSphere * nbBoids * AgentDensity;
            Boid b = Instantiate(boid, new Vector3(pos.x,pos.y,0), Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)),transform);
            b.name = "Boid" + i;
            b.Initialize(this);
            lst_Boids.Add(b);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Boid b in lst_Boids)
        {
            List<Transform> context = GetNearbyObjects(b);
            //b.GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.white, Color.blue, context.Count / 6f);
            
            Vector2 move = behavior.CalculateMove(b, context, this);
            move *= driveFactor;
            if(move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            b.Move(move);

        }
    }

    List<Transform> GetNearbyObjects(Boid b)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(b.transform.position, neighborRadius);

        foreach(Collider2D c in contextColliders)
        {
            if(c != b.col_BoidCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

}
