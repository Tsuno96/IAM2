using System.Collections;
using System.Collections.Generic;
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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Fleche gauche");
            transform.Translate(new Vector3(-1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Fleche gauche");
            transform.Translate(new Vector3(1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Fleche gauche");
            transform.Translate(new Vector3(0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Fleche gauche");
            transform.Translate(new Vector3(0, 0, -1));
        }
        pos = new Vector2Int((int)(transform.position.x - 0.5f), (int)(transform.position.z - 0.5f));
        MGR.Instance.vec2_Arrive = pos;

    }

}
