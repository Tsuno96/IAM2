using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Target : MonoBehaviour
{

    public int state = 0;
    public SpriteRenderer sr;
    public List<Sprite> lstSprite;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        //lstSprite = new List<Sprite>();
        sr = GetComponent<SpriteRenderer>();
        switch(state)
        {
            case 0: sr.sprite = lstSprite[0];
                break;
            case 1: sr.sprite = lstSprite[1];
                break;
            case 2: sr.sprite = lstSprite[2];
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += (Vector3)(new Vector2(0, -1) * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += (Vector3)(new Vector2(0, 1) * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += (Vector3)(new Vector2(1, 0) * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += (Vector3)(new Vector2(-1, 0) * speed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            state = 0;
            sr.sprite = lstSprite[0];
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            state = 1;
            sr.sprite = lstSprite[1];
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            state = 2;
            sr.sprite = lstSprite[2];
        }



    }
}
