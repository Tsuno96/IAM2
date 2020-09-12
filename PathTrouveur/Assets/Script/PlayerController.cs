using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2Int pos;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //MGR.Instance.Dijkstra();
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("Fleche gauche");
            transform.Translate(new Vector3(-1 * MGR.Instance.n_PlayerSpeed * Time.deltaTime, 0, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Fleche gauche");
            transform.Translate(new Vector3(1 * MGR.Instance.n_PlayerSpeed * Time.deltaTime, 0, 0));
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //Debug.Log("Fleche gauche");
            transform.Translate(new Vector3(0, 0, 1 * MGR.Instance.n_PlayerSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //Debug.Log("Fleche gauche"); 
            transform.Translate(new Vector3(0, 0, -1 * MGR.Instance.n_PlayerSpeed * Time.deltaTime));
        }
        pos = new Vector2Int((int)(transform.position.x), (int)(transform.position.z ));
        MGR.Instance.vec2_Arrive = pos;

    }

}
