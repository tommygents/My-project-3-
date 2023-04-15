using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBar : MonoBehaviour
{
    public float speed = 1f;
    private float leftEdge;
    private float rightEdge;
    public Vector3 pool;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move it left
        MoveLeft();

        //check for outside of parent object
        if (transform.localPosition.x < -.5f)
        {
            ReturnToPool();
        }

    }

    private void MoveLeft()
    {
        transform.localPosition += -transform.right * speed * Time.deltaTime;
    }

    private void GetEdges()
    {
        float _x = this.transform.position.x;
        float _w = this.transform.localScale.x;
        leftEdge = _x - (_w / 2);
        rightEdge = _x + (_w / 2);
    }

    private void ReturnToPool()
    {
        transform.position = pool;
        speed = 0;
    }
}
