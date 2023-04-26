using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
    public List<bool> chosenRhythm = new List<bool>();
    public int rhythmIndex = 0;

    public bool gameRunning = true;
    public float alpha = 1f;
    public float change = .125f;
    public SpriteRenderer[] childSPs;
    public SpriteRenderer doomInd;
    public SpriteRenderer damageIndicator;
    public bool damageTaken = false;

    ContactFilter2D filter = new ContactFilter2D().NoFilter();
    List<Collider2D> results = new List<Collider2D>();


    [SerializeField] float flashTime = 6, flashTimeCurrent;

    public float doomVar = 0f;

   



    // Start is called before the first frame update
    void Start()
    {
        CreateObjectPool();
        beatReceiver.GetEdges();
        brLeft = beatReceiver.leftEdge;
        brRight = beatReceiver.rightEdge;
        
        childSPs = GetComponentsInChildren<SpriteRenderer>();
        UpdateDoomInd();
        InitializeTransparencies();
        InitRhythms();
        chosenRhythm = rhythms[Random.Range(0, rhythms.Count)];
        Debug.Log(chosenRhythm.ToString());
        StartCoroutine(SpawnerFromPool());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (BeatMatch())
            {
                OnHit();
            }

            else
            {
                OnMiss();
            }
        }


        if (Input.GetKeyUp(KeyCode.Space))
        {
            beatReceiver.ResetColor();
        }
    }

    private void FixedUpdate()
    {
        if (damageTaken)
            flashTimeCurrent = flashTime;

        flashTimeCurrent--;

        // if flash time is more than 0 then flash
    }

    //spawner and objectpool functions
    #region spawners

    //method to create the object pool on Start()
    private void CreateObjectPool()
    {
        for (int i = 1; i < 20; i++)
        {
            BeatBar _bb = Instantiate(beatBar, pool, beatBar.transform.rotation, beatSpawner.transform.parent);
            beatBars.Add(_bb);
            _bb.speed = 0;
            _bb.bbm = this;
        }
    }

    IEnumerator SpawnerFromPool()
    {
        while (gameRunning)
        {
            if (chosenRhythm[rhythmIndex])
            {
                SpawnBeatFromPool();
            }
            rhythmIndex++;
            if (rhythmIndex >= chosenRhythm.Count)
            {
                rhythmIndex = 0;
            }
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

    #endregion

   // functions that manage the rhythm aspect
    #region rhythm
    public bool HitChecker(BeatBar _bb)
    {
        bool hit = false;

        
       if(beatReceiver.GetComponent<Collider2D>().OverlapCollider(filter,results)>0)
        {
            hit = true;
            foreach(Collider2D col in results) 
            {
                col.gameObject.GetComponent<BeatBar>().ReturnToPool();                
            }
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
        
        return hit;
    }

    public void OnHit()
    {
        FadeOut();
        beatReceiver.HitColor(alpha);
    }

    public void OnMiss()
    {
        FadeIn();
        beatReceiver.MissColor(alpha);
        MoreDoom();

    }

    #endregion

    //functions that fade-in and fade-out the rhythm indicator
    #region transparencies
    public void InitializeTransparencies()
    {
        foreach(SpriteRenderer sp in childSPs)
        {
            alpha = 1f;
            Color _c = sp.color;
            _c.a = alpha;
            sp.color = _c;
        }

        Color _i = damageIndicator.color;
        _i.a = 0f;
        damageIndicator.color = _i;
    
    }
    public void FadeOut()
    {
       
        if (alpha >= change)
        {
            alpha = alpha - change;
            
        }
        else alpha = 0;
        

        foreach (SpriteRenderer sp in childSPs)
        {
            Color _c = sp.color;
            _c.a = alpha;
            sp.color = _c;
            
        }

     }

    public void FadeIn()
    {
        alpha = alpha + change;
        if (alpha > 1) alpha = 1; 

        foreach (SpriteRenderer sp in childSPs)
        {
            Color _c = sp.color;
            _c.a = alpha;
            sp.color = _c;

        }
    }
    #endregion

    //functions that manage the doom variable
    #region doom variable stuff

    public void GetFish(float doom)
    {
        float _t = Random.Range(0.0f, 1.0f);
        if (_t < doom)
        {
            Debug.Log("Bad Fish");
        }
        else
        {
            Debug.Log("Good Fish");
        }
    }

    public void MoreDoom()
    {
        doomVar = doomVar + Random.Range(.01f, .05f);
        UpdateDoomInd();

    }

    public void UpdateDoomInd()
    {
        Color _d = doomInd.color;
        _d.a = doomVar;
        doomInd.color = _d;
    }
    #endregion

    //region declaring some rhythm options
    #region beats

    public List<List<bool>> rhythms = new();
    public List<bool> basic = new List<bool>();
    public List<bool> conga = new();
    public List<bool> samba = new();
    public List<bool> drumkick = new();
    public void InitRhythms()
    {
     
        basic.Add(true);
        rhythms.Add(basic);
        for (int i = 0; i < 7; i++)
        {
            conga.Add(true);
        }
        conga.Add(false);
        rhythms.Add(conga);
        for (int i = 0; i < 3; i++)
        {
            samba.Add(true);
        }
        samba.Add(false);
        rhythms.Add(samba);
        for (int i = 0; i < 2; i++)
        {
            drumkick.Add(true);
            drumkick.Add(false);
        }
        drumkick.Add(true);
        drumkick.Add(true);
    }



    #endregion


}

