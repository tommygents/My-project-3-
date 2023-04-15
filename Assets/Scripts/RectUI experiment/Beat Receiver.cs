using UnityEngine;

public class BeatReceiver : MonoBehaviour
{
    //the colors to signal UI
    [SerializeField] private Color baseColor;
    [SerializeField] private Color hitColor;
    [SerializeField] private float missColor;

    //the spriterenderer for this object, initialized in Start()
    private SpriteRenderer _sp;

    //the edges of the sprite
    public float leftEdge;
    public float rightEdge;

    // Start is called before the first frame update
    void Start()
    {
         _sp= GetComponent<SpriteRenderer>();
        GetEdges();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetColor()
    {
        _sp.color = baseColor;
    }

    private void GetEdges()
    {
        float _x = this.transform.position.x;
        float _w = this.transform.localScale.x;
        leftEdge = _x - (_w / 2);
        rightEdge = _x + (_w / 2);
    }

    



}
