using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseController : MonoBehaviour
{
    private Vector2Int pos;
    public List<GameObject> arrGO_neighbour;
    public float poids;

    
    void Awake()
    {
        arrGO_neighbour = new List<GameObject>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        if(poids >1)
        {
            this.gameObject.GetComponent<Renderer>().material = MGR.Instance.mat_Obstacle;
            BoxCollider bc = gameObject.AddComponent<BoxCollider>() as BoxCollider;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2Int GetPos()
    {
        return pos;
    }

    public void setPos(int _i, int _j)
    {
        pos.x = _i;
        pos.y = _j;
    }

    public void setPoids(float f)
    {
        poids = f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!arrGO_neighbour.Contains(other.gameObject) && other.tag == "Case" /*&& other.gameObject.GetComponent<CaseController>().poids <Mathf.Infinity*/)
        {
            arrGO_neighbour.Add(other.gameObject);
        }
    }
}
