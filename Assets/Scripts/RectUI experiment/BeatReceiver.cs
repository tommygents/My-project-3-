using UnityEngine;

public class BeatReceiver : MonoBehaviour
{
    //the colors to signal UI
    [SerializeField] private Color baseColor;
    [SerializeField] private Color hitColor;
    [SerializeField] private Color missColor;

    //the other components for this object, initialized in Start()
    private SpriteRenderer spriteRenderer;
    new public Collider2D collider2D;

    //the edges of the sprite
    public float leftEdge;
    public float rightEdge;

    // Start is called before the first frame update
    void Start()
    {
         spriteRenderer= GetComponent<SpriteRenderer>();
        collider2D= GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetColor()
    {
        spriteRenderer.color = baseColor;
    }

    public void GetEdges()
    {
        
        float _x = spriteRenderer.bounds.center.x;
        float _w = spriteRenderer.bounds.size.x;
        leftEdge = _x - (_w / 2);
        rightEdge = _x + (_w / 2);
    }

    public void HitColor()
    {
        spriteRenderer.color = hitColor;
    }
    
    public void MissColor()
    {
        spriteRenderer.color = missColor;
    }

    public void HitColor(float alpha)
    {
        hitColor.a= alpha;
        spriteRenderer.color = hitColor;
    }

    public void MissColor(float alpha)
    {
        missColor.a= alpha;
        spriteRenderer.color = missColor;
    }

}
