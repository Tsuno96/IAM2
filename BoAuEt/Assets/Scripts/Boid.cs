using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Boid : MonoBehaviour
{
    public float speed = 1;
   
    public List<Transform> nghbs;

    public Vector2 velocity;
    
    Spawner spawner;



    float maxVelocity = 10;

    // Start is called before the first frame update
    void Start()
    {
        spawner = GetComponentInParent<Spawner>();
        //Debug.Log(spawner);
        nghbs = new List<Transform>();
        velocity = new Vector2(Random.Range(1,10)/10, Random.Range(1, 10)/10);
    }

  

    // Update is called once per frame
    void FixedUpdate()
    {
        nghbs = GetNeighbors();
        
        velocity = moveComposite(moveCloserSmooth(nghbs),moveWidth(nghbs),moveAway(nghbs));
        velocity *= spawner.driveFactor;

        if (velocity.sqrMagnitude > spawner.squareMaxSpeed)
        {
            velocity = velocity.normalized * spawner.maxSpeed;
        }
        /*int border = 100;

        if (transform.position.x > border && velocity.x < 0)
        {
            velocity.x = -velocity.x;
        }
        if (transform.position.x < -border && velocity.x > 0)
        {
            velocity.x = -velocity.x ;
        }

        if (transform.position.y > border && velocity.y < 0)
        {
            velocity.y = -velocity.y ;
        }
        if (transform.position.y < -border && velocity.y > 0)
        {
            velocity.y = -velocity.y ;
        }*/

        move();
    }


    List<Transform> GetNeighbors()
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(this.transform.position, spawner.neighborRadius);
        
        foreach (Collider2D c in contextColliders)
        {
            if (c != GetComponent<Collider2D>())
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

    public Vector2 moveCloser(List<Transform> boidsNeighbors)
    {
        if(boidsNeighbors.Count > 0)
        {
            Vector2 cohesionMove = Vector2.zero;
            
            foreach(Transform b in boidsNeighbors)
            {
                if (b.gameObject.tag == "Boid")
                {
                    cohesionMove += (Vector2)(b.position);
                }
            }
            cohesionMove /= boidsNeighbors.Count;
            
            cohesionMove -= (Vector2)this.transform.position;
            return cohesionMove;
        }
        return Vector2.zero;
    }

    public Vector2 moveCloserSmooth(List<Transform> boidsNeighbors)
    {
        Vector2 currentVelocity;
        float agentSmoothTime = 0.5f;

        if (boidsNeighbors.Count > 0)
        {
            Vector2 cohesionMove = Vector2.zero;

            foreach (Transform b in boidsNeighbors)
            {
                if (b.gameObject.tag == "Boid")
                {
                    cohesionMove += (Vector2)(b.position);
                }
            }
            cohesionMove /= boidsNeighbors.Count;

            cohesionMove -= (Vector2)this.transform.position;
            cohesionMove = Vector2.SmoothDamp(transform.up, cohesionMove, ref velocity, agentSmoothTime);
            return cohesionMove;
        }
        return Vector2.zero;
    }

    public Vector2 moveWidth(List<Transform> boidsNeighbors)
    {
        if (boidsNeighbors.Count > 0)
        {
            Vector2 alignementMove = Vector2.zero;
            foreach (Transform b in boidsNeighbors)
            {
                if (b.gameObject.tag == "Boid")
                {
                    alignementMove += (Vector2)(b.transform.up);
                }
            }
            alignementMove /= boidsNeighbors.Count;          
            return alignementMove;
        }
        return transform.up;
    }

    public Vector2 moveAway(List<Transform> boidsNeighbors)
    {
        Vector2 avoidanceMove = Vector2.zero;
        if (boidsNeighbors.Count > 0)
        {
            
            int numClose = 0;
            foreach (Transform b in boidsNeighbors)
            {
                if (Vector2.SqrMagnitude(b.position - this.transform.position)< spawner.squareAvoidanceRadius)
                {
                    numClose++;
                    avoidanceMove +=(Vector2)(this.transform.position - b.position);
                }
                
            }

            if(numClose>0)
            {
                avoidanceMove /= numClose;
            }
            
        }
        return avoidanceMove;
    }

    public Vector2 moveComposite(Vector2 cohesion, Vector2 alignement, Vector2 eloignement)
    {
        Vector2 move = Vector2.zero;
        cohesion *= spawner.cohesion;
        if (cohesion.sqrMagnitude > (spawner.cohesion * spawner.cohesion))
        {
            cohesion.Normalize();
            cohesion *= spawner.cohesion;
        }
        move += cohesion;
        alignement *= spawner.alignement;
        if (alignement.sqrMagnitude > (spawner.alignement * spawner.alignement))
        {
            alignement.Normalize();
            alignement *= spawner.alignement;
        }
        move += alignement;
        eloignement *= spawner.eloignement;
        if (eloignement.sqrMagnitude > (spawner.eloignement * spawner.eloignement))
        {
            eloignement.Normalize();
            eloignement *= spawner.eloignement;
        }
        move += eloignement;

        return move;
    }

    public void move()
    {
            
        transform.up = (Vector3)velocity;
        transform.position += ((Vector3)velocity* Time.deltaTime);
        /*Vector3 newDirection = Vector3.RotateTowards(transform.forward, (Vector3)velocity, 0.0f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);*/

    }




}
