using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Boid Boid;
    List<Boid> lst_Boids = new List<Boid>();
    public float f_Density;

    [Range(10, 500)]
    public int nbBoids = 100;

    // Start is called before the first frame update
    void Start()
    {

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
