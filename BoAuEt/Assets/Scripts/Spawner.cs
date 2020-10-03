using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Boid Boid;
    public List<Boid> lst_Boids = new List<Boid>();
    public float f_Density;

    [Range(10, 500)]
    public int nbBoids = 100;
    [Range(0, 20)]
    public int cohesion = 1;
    [Range(0, 20)]
    public int alignement = 1;
    [Range(0, 20)]
    public int eloignement = 1;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    public float neighborRadius;
    public float squareMaxSpeed;
    public float squareNeighborRadius;
    public float squareAvoidanceRadius;
    


    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        for (int i = 0; i < nbBoids; i++)
        {
            Vector3 pos = Random.insideUnitSphere * nbBoids * f_Density;
            Boid b = Instantiate(Boid, new Vector3(pos.x, pos.y, 0), Quaternion.Euler(Vector3.forward * Random.Range(0, 360f)), transform);
            b.name = "Boid" + i;
            lst_Boids.Add(b);
        }



    }

    // Update is called once per frame
    void Update()
    {

    }
}
