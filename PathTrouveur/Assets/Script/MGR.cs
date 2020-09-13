using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MGR : MonoBehaviour
{
    private static MGR p_instance = null;
    public static MGR Instance { get { return p_instance; } }

    public Material mat;
    public Material mat_Obstacle;

    public GameObject GO_Plane;
    public GameObject GO_Case;

    public GameObject GO_Player;
    public GameObject GO_Bot;
    Transform Bot;

    public GameObject GO_Coin;
    public List<GameObject> lstGO_Coins;

    public GameObject[,] arrGO_Cases;

    int[,] layout;
    float[,] matInf;
    public Vector2Int vec2_Depart;
    public Vector2Int vec2_Arrive;

    public int n_PlayerSpeed;
    public int n_BotSpeed;

    public Vector3 vec3_BotDst;

    int Counter;

    public bool DijkstraOffAstarOn;



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
        {0  ,0  ,0  ,0  ,0  ,0 ,0 ,0 ,0 ,0 },
        {0  ,2  ,1  ,1  ,1  ,1 ,1 ,2 ,1 ,0 },
        {0  ,0  ,1  ,1  ,1  ,1 ,1 ,0 ,1 ,0 },
        {0  ,1  ,0  ,1  ,2  ,1 ,1 ,1 ,1 ,0 },
        {0  ,1  ,1  ,1  ,0  ,0 ,1 ,2 ,1 ,0 },
        {0  ,1  ,1  ,1  ,0  ,0 ,1 ,1 ,1 ,0 },
        {0  ,1  ,2  ,1  ,1  ,1 ,1 ,1 ,1 ,0 },
        {0  ,1  ,1  ,1  ,1  ,0 ,1 ,0 ,1 ,0 },
        {0  ,1  ,1  ,1  ,1  ,0 ,2 ,0 ,1 ,0 },
        {0  ,0  ,0  ,0  ,0  ,0 ,0 ,0 ,0 ,0 }
        };

        lstGO_Coins = new List<GameObject>();

        Instantiate(GO_Plane.transform, new Vector3(5, 0, 5), Quaternion.identity);
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 10; j++)
            {
                arrGO_Cases[i, j] = (GameObject)Instantiate(GO_Case, new Vector3(i + 0.5f, 0.5f, j + 0.5f), Quaternion.identity);
                arrGO_Cases[i, j].GetComponent<CaseController>().setPos(i, j);
                arrGO_Cases[i, j].gameObject.name = "Case_" + i + "_" + j;
                if (layout[i, j] == 1)
                {
                    arrGO_Cases[i, j].GetComponent<CaseController>().setPoids(1.0f);
                }
                else if(layout[i,j] == 2)
                {
                    arrGO_Cases[i, j].GetComponent<CaseController>().setPoids(1.0f);
                    lstGO_Coins.Add(Instantiate(GO_Coin, new Vector3(i + 0.5f, 0.5f, j + 0.5f), Quaternion.identity));
                }
                else
                {
                    arrGO_Cases[i, j].GetComponent<CaseController>().setPoids(Mathf.Infinity);
                }
            }

        Instantiate(GO_Player.transform, new Vector3(vec2_Arrive.x+.5f, 0.5f, vec2_Arrive.y+.5f), Quaternion.identity);

        Bot = Instantiate(GO_Bot.transform, new Vector3(vec2_Depart.x + .5f, 0.5f, vec2_Depart.y + .5f), Quaternion.identity);

        Counter = lstGO_Coins.Count;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (e.button == 0 && e.isMouse)
        {
            if (arrGO_Cases[vec2_Arrive.x, vec2_Arrive.y].GetComponent<CaseController>().poids < Mathf.Infinity)
            {
                if(DijkstraOffAstarOn)
                {
                    Astar();
                }
                else
                {
                    Dijkstra();
                }
                
            }
        }
        if (e.isKey)
        {
            if (DijkstraOffAstarOn)
            {
                Astar();
            }
            else
            {
                Dijkstra();
            }
        }

    }

    public void Dijkstra()
    {

        List<Vector2Int> P = new List<Vector2Int>();
        /*Initialisation*/
        float[,] dst = new float[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                dst[i, j] = Mathf.Infinity;
            }
        }


        dst[vec2_Depart.x, vec2_Depart.y] = 0;


        List<GameObject> Q = new List<GameObject>();
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                if (arrGO_Cases[i, j].GetComponent<CaseController>().poids < Mathf.Infinity)
                {
                    arrGO_Cases[i, j].GetComponent<Renderer>().material = mat;
                }
            }
        }

        Vector2Int[,] predecesseurs = new Vector2Int[10, 10];
        while (P.Count != 10 * 10) {
            float mindst = Mathf.Infinity;
            Vector2Int minCase = new Vector2Int(vec2_Depart.x, vec2_Depart.y);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (dst[i, j] != Mathf.Infinity)
                    {
                        //Debug.Log("dst[" + i + "," + j + "] : " + dst[i, j]);
                    }
                    if (!P.Contains(new Vector2Int(i, j)) && mindst > dst[i, j])
                    {
                        mindst = dst[i, j];
                        minCase = new Vector2Int(i, j);
                    }
                }
            }
            //Debug.Log("MinCase : " + minCase + " dst : " + mindst);
            P.Add(arrGO_Cases[minCase.x, minCase.y].GetComponent<CaseController>().GetPos());


            List<GameObject> CSnghb = arrGO_Cases[minCase.x, minCase.y].GetComponent<CaseController>().arrGO_neighbour;
            foreach (GameObject GO in CSnghb)
            {
                Vector2Int posGO = GO.GetComponent<CaseController>().GetPos();

                if (dst[posGO.x, posGO.y] > (dst[minCase.x, minCase.y] + arrGO_Cases[posGO.x, posGO.y].GetComponent<CaseController>().poids))
                {
                    dst[posGO.x, posGO.y] = dst[minCase.x, minCase.y] + arrGO_Cases[posGO.x, posGO.y].GetComponent<CaseController>().poids;
                    predecesseurs[posGO.x, posGO.y] = new Vector2Int(minCase.x, minCase.y);
                }
            }


        }

        List<Vector2Int> chemin = new List<Vector2Int>();
        Vector2Int s = vec2_Arrive;
        while (s != vec2_Depart)
        {
            chemin.Add(s);
            arrGO_Cases[s.x, s.y].GetComponent<Renderer>().material = mat_Obstacle;
            arrGO_Cases[s.x, s.y].GetComponent<Renderer>().material.color = Color.blue;
            s = predecesseurs[s.x, s.y];
        }
        arrGO_Cases[vec2_Arrive.x, vec2_Arrive.y].GetComponent<Renderer>().material = mat_Obstacle;
        arrGO_Cases[vec2_Arrive.x, vec2_Arrive.y].GetComponent<Renderer>().material.color = Color.cyan;
        arrGO_Cases[vec2_Depart.x, vec2_Depart.y].GetComponent<Renderer>().material = mat_Obstacle;
        arrGO_Cases[vec2_Depart.x, vec2_Depart.y].GetComponent<Renderer>().material.color = Color.green;

        if (chemin.Count > 0)
        {
            vec3_BotDst = new Vector3(chemin[chemin.Count - 1].x + .5f, 0.5f, chemin[chemin.Count - 1].y + 0.5f);
            Bot.gameObject.GetComponent<BotController>().letsgo = true;
        }


    }

    public void Astar()
    {
        List<Vector2Int> ouverte = new List<Vector2Int>();
        List<Vector2Int> ferme = new List<Vector2Int>();
        ouverte.Add(vec2_Depart);

        float[,] cout = new float[10, 10];
        Vector2Int[,] predecesseurs = new Vector2Int[10, 10];

        cout[vec2_Depart.x, vec2_Depart.y] = 0;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                cout[i, j] = Mathf.Infinity;

                if (arrGO_Cases[i, j].GetComponent<CaseController>().poids < Mathf.Infinity)
                {
                    arrGO_Cases[i, j].GetComponent<Renderer>().material = mat;
                }
            }
        }
        

        bool stop = false;
        int h = 10000;
        while (ouverte.Count > 0 && stop == false && h>0)
        {
            //Debug.Log("A*");
            h--;
            Vector2Int X = vec2_Depart;
            float min = Mathf.Infinity;
            for (int i =0; i<10;i++)
            {
                for(int j =0; j<10;j++)
                {
                    if (ouverte.Contains(new Vector2Int(i,j)) && min > cout[i, j])
                    {
                        X = new Vector2Int(i, j);
                        min = cout[i, j];
                    }
                }

            }
            
            if (X == vec2_Arrive)
            {
                //Debug.Log("Arrivé");
                stop = true;

            }
            else
            {
                ouverte.Remove(X);
                ferme.Add(X);
                List<GameObject> CSnghb = arrGO_Cases[X.x, X.y].GetComponent<CaseController>().arrGO_neighbour;
                foreach (GameObject GO in CSnghb)
                {
                    Vector2Int posGO = GO.GetComponent<CaseController>().GetPos();

                    if(!ouverte.Contains(posGO) && !ferme.Contains(posGO))
                    {
                        ouverte.Add(posGO);
                        float dstGOX = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(posGO.x - X.x),2) + Mathf.Pow(Mathf.Abs(posGO.y - X.y), 2));
                        float dstGOA = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(posGO.x - vec2_Arrive.x),2) + Mathf.Pow(Mathf.Abs(posGO.y - vec2_Arrive.y), 2));
                        cout[posGO.x, posGO.y] =  dstGOX+dstGOA;
                        predecesseurs[posGO.x, posGO.y] = new Vector2Int(X.x, X.y);
                    }
                }


            }

        }
        /*Debug.Log(predecesseurs[vec2_Arrive.x, vec2_Arrive.y]);
        Debug.Log(predecesseurs[9, 8]);
        Debug.Log(predecesseurs[8, 8]);*/

        if (stop) { 
            List<Vector2Int> chemin = new List<Vector2Int>();
            Vector2Int s = vec2_Arrive;
            while (s != vec2_Depart)
            {
                chemin.Add(s);
                arrGO_Cases[s.x, s.y].GetComponent<Renderer>().material = mat_Obstacle;
                arrGO_Cases[s.x, s.y].GetComponent<Renderer>().material.color = Color.blue;
                s = predecesseurs[s.x, s.y];
            }
            arrGO_Cases[vec2_Arrive.x, vec2_Arrive.y].GetComponent<Renderer>().material = mat_Obstacle;
            arrGO_Cases[vec2_Arrive.x, vec2_Arrive.y].GetComponent<Renderer>().material.color = Color.cyan;
            arrGO_Cases[vec2_Depart.x, vec2_Depart.y].GetComponent<Renderer>().material = mat_Obstacle;
            arrGO_Cases[vec2_Depart.x, vec2_Depart.y].GetComponent<Renderer>().material.color = Color.green;

            if (chemin.Count > 0)
            {
                vec3_BotDst = new Vector3(chemin[chemin.Count - 1].x + .5f, 0.5f, chemin[chemin.Count - 1].y + 0.5f);
                Bot.gameObject.GetComponent<BotController>().letsgo = true;
            }
        }

    }

    public void GettedCoin()
    {
        Counter--;
        if(Counter <1)
        {
            EndGame(1);
        }


    }

    public void EndGame(int i)
    {
        if(i == 0)
        {
            Debug.Log("Perdu");
            SceneManager.LoadScene("LoseScene");
        }
        else
        {
            Debug.Log("Gagné");
            SceneManager.LoadScene("Win");
        }

        //Application.Quit();
    }



}
