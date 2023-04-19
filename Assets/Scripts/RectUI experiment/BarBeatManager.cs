using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBeatManager : MonoBehaviour
{

    [SerializeField] private BeatReceiver beatReceiver;
    [SerializeField] private BeatBar beatBar;
    [SerializeField] private BeatSpawner beatSpawner;
    [SerializeField] private float defaultSpeed = 1f;


    //beatreceiver variables
    private float brLeft;
    private float brRight;

    //object pooling variables
    private Vector3 pool = new Vector3(200, 200);
    private Vector3 spawnPoint = new Vector3(.4f, 0);

    public List<BeatBar> beatBars = new List<BeatBar>();
    public int bbIndex = 0;

    public bool gameRunning = true;

    ContactFilter2D filter = new ContactFilter2D().NoFilter();
    List<Collider2D> results = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        CreateObjectPool();
        beatReceiver.GetEdges();
        brLeft = beatReceiver.leftEdge;
        brRight = beatReceiver.rightEdge;
        Debug.Log("Receiver: Left = " + brLeft + ", Right = " + brRight);
        StartCoroutine(SpawnerFromPool());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (BeatMatch())
            {
                beatReceiver.HitColor();
            }
            else
            {
                beatReceiver.MissColor();
            }
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            beatReceiver.ResetColor();
        }
    }

    //method to create the object pool on Start()
    private void CreateObjectPool()
    {
        for (int i = 1; i < 20; i++)
        {
            BeatBar _bb = Instantiate(beatBar, pool, beatBar.transform.rotation, beatSpawner.transform.parent);
            beatBars.Add(_bb);
            _bb.speed = 0;
        }
    }



    public bool HitChecker(BeatBar _bb)
    {
        bool hit = false;

        //if (_bb.rightEdge > beatReceiver.leftEdge || _bb.leftEdge < beatReceiver.rightEdge)
        if(beatReceiver.GetComponent<Collider2D>().OverlapCollider(filter,results)>0)
        {
            hit = true;
        }
        results.Clear();
        return hit;

    }

    public bool BeatMatch()
    {
        bool hit = false;

        foreach (BeatBar _bb in beatBars)
        {
            if (HitChecker(_bb))
            {
                hit = true;
            }
        }
        Debug.Log(hit);
        return hit;
    }


    IEnumerator SpawnerFromPool()
    {
        while (gameRunning)
        {
            SpawnBeatFromPool();
            yield return new WaitForSeconds(beatSpawner.beatInterval);
        }
    }


    //method for spawning individual beatbars
    public void SpawnBeatFromPool()
    {
        BeatBar _bb = beatBars[bbIndex];
        _bb.transform.position = beatSpawner.transform.position;
        _bb.speed = defaultSpeed;
        bbIndex++;
        if (bbIndex >= beatBars.Count)
        {
            bbIndex = 0;
        }
    }
}

   