using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Boid : MonoBehaviour
{
    public float speed = 1;
    public float neighborRadius = 10;
    public List<Transform> nghbs;

    public Vector2 velocity;

    public float coefCohe = 1;
    public float coefAl = 1;

    public float coefEl = 1;

    float maxVelocity = 10;

    // Start is called before the first frame update
    void Start()
    {
        nghbs = new List<Transform>();
        velocity = new Vector2(Random.Range(1,10), Random.Range(1, 10));
    }

    public void Initialise(float speed)
    {

    }

    // Update is called once per frame
    void Update()
    {
        nghbs = GetNeighbors();
        moveCloser(nghbs);
        moveWidth(nghbs);
        moveAway(nghbs, neighborRadius);

        int border = 100;

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
        }




        move();
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
                if (c.gameObject.tag == "Obstacle")
                {
                    Debug.Log(c);
                }
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
                if (b.gameObject.tag == "Boid")
                {
                    avg += (Vector2)(transform.position - b.position);
                }
            }
            avg /= boidsNeighbors.Count;
            avg /= 100;
            velocity -= avg *coefCohe;

        }
    }

    public void moveWidth(List<Transform> boidsNeighbors)
    {

        if (boidsNeighbors.Count > 0)
        {
            Vector2 avg = Vector2.zero;
            foreach (Transform b in boidsNeighbors)
            {
                if (b.gameObject.tag == "Boid")
                {
                    avg += (Vector2)(transform.position - b.position);
                }
            }
            avg /= boidsNeighbors.Count;
            avg /= 40;
            velocity += avg * coefAl;

        }


    }

    public void moveAway(List<Transform> boidsNeighbors, float minDistance)
    {
        if (boidsNeighbors.Count > 0)
        {
            Vector2 distance = Vector2.zero;
            int numClose = 0;
            foreach (Transform b in boidsNeighbors)
            {
                Vector2 diff = (Vector2)(transform.position - b.position);
                if(diff.magnitude <minDistance)
                {
                    numClose++;
                    if(diff.x >= 0) { diff.x = Mathf.Sqrt(minDistance) - diff.x; }
                    else if(diff.x < 0) { diff.x = -Mathf.Sqrt(minDistance) - diff.x; }

                    if (diff.y >= 0) { diff.y = Mathf.Sqrt(minDistance) - diff.y; }
                    else if (diff.y < 0) { diff.y = -Mathf.Sqrt(minDistance) - diff.y; }

                    int scale = 1;
                    if (b.gameObject.tag == "Obstacle")
                    {
                        Debug.Log(b);
                        scale = 10;
                    }
                    distance += diff * scale *coefEl;
                }
            }

            
            distance /= 5;
            velocity -= distance;
            

        }
    }


    public void move()
    {
        if(Mathf.Abs(velocity.x) > maxVelocity || Mathf.Abs(velocity.x) > maxVelocity)
        {
            float scaleFactor = maxVelocity / Mathf.Max(Mathf.Abs(velocity.x), Mathf.Abs(velocity.y));
            velocity *= scaleFactor;
        }
            
        transform.position -= ((Vector3)velocity  *speed* Time.deltaTime);
        transform.up = (Vector3)velocity;
        /*Vector3 newDirection = Vector3.RotateTowards(transform.forward, (Vector3)velocity, 0.0f, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);*/

    }




}
