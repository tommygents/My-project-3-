using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBeatManager : MonoBehaviour
{

    [SerializeField] private BeatReceiver beatReceiver;
    [SerializeField] private GameObject beatBar;

    private float brLeft;
    private float brRight;

    

    // Start is called before the first frame update
    void Start()
    {
        brLeft = beatReceiver.leftEdge;
        brRight = beatReceiver.rightEdge;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool HitChecker(float _left, float _right)
    {
        bool hit = false;
        
        if (_right > beatReceiver.leftEdge || _left < beatReceiver.rightEdge)
        {
            hit = true;
        }

        return hit;

    }

    public bool BeatMatch()
    {
        bool hit = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //so the problem is that HitChecker gets called from a BeatBar, but it changes the color on the BeatReceiver
            //I think the solution is to have a list of beatbars and just check the first and the second one, maybe. Move the others down the list?
        }

        return hit;
    }

    

}
