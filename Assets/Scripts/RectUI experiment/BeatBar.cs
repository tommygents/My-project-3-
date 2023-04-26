using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBar : MonoBehaviour
{
    public float speed = 1f;
    public float leftEdge;
    public float rightEdge;
    public Vector3 pool;
    public SpriteRenderer spriteRenderer;
    public BarBeatManager bbm;
    void Start()
    {
        
    }

    private void Awake()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //move it left
        MoveLeft();

        //check for outside of parent object
        if (transform.localPosition.x < -.5f)
        {
            bbm.OnMiss();
            ReturnToPool();

        }

    }

    //moves the beatbar and updates the position of the edges
    private void MoveLeft()
    {
        transform.localPosition += -transform.right * speed * Time.deltaTime;
        GetEdges();
    }

    public void GetEdges()
    {
        float _x = spriteRenderer.bounds.center.x;
        float _w = spriteRenderer.bounds.size.x;
        leftEdge = _x - (_w / 2);
        rightEdge = _x + (_w / 2);
    }

    public void ReturnToPool()
    {
        transform.position = pool;
        speed = 0;
        GetEdges();
    }
}
