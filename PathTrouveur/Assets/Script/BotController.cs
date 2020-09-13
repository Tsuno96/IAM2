using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public bool letsgo;
    Vector2Int pos;
    
    // Start is called before the first frame update
    void Start()
    {
        letsgo = false;
        pos = new Vector2Int((int)(transform.position.x), (int)(transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {
        float step = MGR.Instance.n_BotSpeed * Time.deltaTime; // calculate distance to move
        if (letsgo)
        {

            transform.position = Vector3.MoveTowards(transform.position, MGR.Instance.vec3_BotDst, step);
        }
        if (pos != new Vector2Int((int)(transform.position.x), (int)(transform.position.z)))
        {
            pos = new Vector2Int((int)(transform.position.x), (int)(transform.position.z));
            MGR.Instance.vec2_Depart = pos;

            if (MGR.Instance.DijkstraOffAstarOn)
            {
                MGR.Instance.Astar();
            }
            else
            {
                MGR.Instance.Dijkstra();
            }
         
        }
            
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            MGR.Instance.EndGame(0);
        }
    }





}
