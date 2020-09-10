using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR : MonoBehaviour
{
    private static MGR p_instance = null;
    public static MGR Instance { get { return p_instance; } }

    public Material mat_Obstacle;

    public GameObject GO_Plane;
    public GameObject GO_Case;

    public GameObject[,] arrGO_Cases;

    int[,] layout;

    public Vector2 vec2_Depart;
    public Vector2 vec2_Arrive;




    //public int length;
    //public int width;
    void Awake()
    {
        if (p_instance == null)
            //if not, set instance to this
            p_instance = this;
        //If instance already exists and it's not this:
        else if (p_instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        arrGO_Cases = new GameObject[10, 10];
        layout = new int[10, 10]
        {
        {1,0,1,1,1,1,1,1,1,1},
        {1,0,1,1,1,1,1,1,1,1},
        {1,0,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1},
        {1,1,1,1,1,1,1,1,1,1}
        };



        Instantiate(GO_Plane.transform, new Vector3(5, 0, 5), Quaternion.identity);
        for(int i = 0; i< 10;i++)
            for(int j = 0; j <10; j++)
            {
                arrGO_Cases[i,j] = (GameObject)Instantiate(GO_Case, new Vector3(i+0.5f, 0.5f, j+0.5f), Quaternion.identity);
                arrGO_Cases[i, j].GetComponent<CaseController>().setPos(i, j);
                arrGO_Cases[i, j].gameObject.name = "Case_" + i + "_" + j; 
                if(layout[i,j] == 1)
                {
                    arrGO_Cases[i, j].GetComponent<CaseController>().setPoids(1.0f);
                }
                else
                {
                    arrGO_Cases[i, j].GetComponent<CaseController>().setPoids(Mathf.Infinity);
                }
            }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
