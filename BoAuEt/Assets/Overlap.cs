using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlap : MonoBehaviour
{
    public Collider2D[] contextColliders;
    // Start is called before the first frame update
    void Start()
    {
        contextColliders = Physics2D.OverlapCircleAll(this.transform.position, 50);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
