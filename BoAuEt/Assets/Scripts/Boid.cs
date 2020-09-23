using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Boid : MonoBehaviour
{
    public float speed = 1;
    public float neighborRadius = 1;
    public List<Transform> nghbs;

    // Start is called before the first frame update
    void Start()
    {
        nghbs = new List<Transform>();
       
    }

    public void Initialise(float speed)
    {

    }

    // Update is called once per frame
    void Update()
    {
        nghbs = GetNeighbors();
        moveCloser(nghbs);
    }


    List<Transform> GetNeighbors()
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(this.transform.position, neighborRadius);
        
        foreach (Collider2D c in contextColliders)
        {
            if (c != GetComponent<Collider2D>())
            {
                context.Add(c.transform);
            }
        }
        return context;

    }

    public void moveCloser(List<Transform> boidsNeighbors)
    {
        if(boidsNeighbors.Count > 0)
        {
            Vector2 avg = Vector2.zero;
            
            foreach(Transform b in boidsNeighbors)
            {
                avg += (Vector2)(transform.position - b.position);
            }
            avg /= boidsNeighbors.Count;
            transform.position -= ((Vector3)avg *10* Time.deltaTime);
        }
    }



}
