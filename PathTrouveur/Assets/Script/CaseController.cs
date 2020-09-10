using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseController : MonoBehaviour
{
    public Vector2 pos;
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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (!arrGO_neighbour.Contains(other.gameObject))
        {
            arrGO_neighbour.Add(other.gameObject);
        }
    }
}
