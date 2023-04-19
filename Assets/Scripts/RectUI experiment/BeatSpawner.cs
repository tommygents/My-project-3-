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

    public float beatInterval;
    private float nextBeatTime;
    private float halfWindowSize;

    private Vector3 pool = new Vector3(200,200);

    public List<BeatBar> beatBars;
    public int bbIndex = 1;

    public bool gameRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        SetBPM(BPM);
        SetWindowSize(windowSize);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region Dep beatspawning
    /*
    public void SpawnBeat()
    {
        //BeatBar _bb = Instantiate(beatBar, this.transform.parent, false);
        BeatBar _bb = Instantiate(beatBar, this.transform.position, beatBar.transform.rotation, this.transform.parent);
        _bb.speed = defaultSpeed;
        _bb.pool = pool;
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
    */
    #endregion

    #region Object Pooling
    //accessible method for starting the coroutine
    public void StartSpawner(List<BeatBar> _bbPool)
    {
        StartCoroutine(SpawnerFromPool(_bbPool));
    }
    


    //co-routine that spawns beatbars from the spawner
    IEnumerator SpawnerFromPool(List<BeatBar> _bbPool)
    {
        while (gameRunning)
        {
            SpawnBeatFromPool(_bbPool);
            yield return new WaitForSeconds(beatInterval);
        }
    }


    //method for spawning individual beatbars
    public void SpawnBeatFromPool(List<BeatBar> _bbPool)
    {
        BeatBar _bb = _bbPool[bbIndex];
        _bb.transform.position = this.transform.position;
        _bb.speed = defaultSpeed;
        bbIndex++;
        if (bbIndex >= beatBars.Count)
        {
            bbIndex = 0;
        }
    }


    #endregion 

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
