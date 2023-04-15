using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSpawner : MonoBehaviour
{

    [SerializeField] private float defaultSpeed = 1f;
    [SerializeField] private BeatBar beatBar;
    [SerializeField] private float BPM = 120.0f;
    [SerializeField] private float windowSize = 0.1f;

    private float beatInterval;
    private float nextBeatTime;
    private float halfWindowSize;

    private Vector3 pool = new Vector3(200,200);

    public List<BeatBar> beatBars;

    // Start is called before the first frame update
    void Start()
    {
        SetBPM(BPM);
        SetWindowSize(windowSize);
        //Reset();
        CreateObjectPool();
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
    public void SpawnBeat()
    {
        //BeatBar _bb = Instantiate(beatBar, this.transform.parent, false);
        BeatBar _bb = Instantiate(beatBar, this.transform.position, beatBar.transform.rotation, this.transform.parent);
        _bb.speed = defaultSpeed;
        _bb.pool= pool;
        beatBars.Add(_bb);

    }


    IEnumerator Spawn()
    {
        while (true)
        {
            SpawnBeat();
            yield return new WaitForSeconds(beatInterval);
        }
    }


    private void SetBPM(float bpm)
    {
        if (bpm <= 0)
            throw new ArgumentException("BPM must be greater than 0");

        BPM = bpm;
        beatInterval = 60.0f / bpm;
    }

    private void SetWindowSize(float windowSize)
    {
        if (windowSize <= 0)
            throw new ArgumentException("WindowSize must be greater than 0");

        this.windowSize = windowSize;
        halfWindowSize = windowSize / 2.0f;
    }

    private void Reset()
    {
        nextBeatTime = beatInterval;
    }

    //method to create the object pool on Start()
    private void CreateObjectPool()
    {
        for (int i=1;i < 20;i++)
        {
            BeatBar _bb = Instantiate(beatBar, pool, beatBar.transform.rotation, this.transform.parent);
            beatBars.Add( _bb);
            _bb.speed = 0;
        }
    }

    private void ObjectSpawn(BeatBar _bb)
    {
        _bb.transform.position = this.transform.position;
        _bb.speed = defaultSpeed;
    }

    private void ObjectReturn(BeatBar _bb)
    {
        _bb.transform.position = pool;
        _bb.speed = 0;
    }
}
