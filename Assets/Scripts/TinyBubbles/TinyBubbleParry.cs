using UnityEngine;

public class TinyBubbleParry : MonoBehaviour
{
    [SerializeField]
    int remainingHits;
    [SerializeField]
    Color[] colors = new Color[2];
    Color spritecolor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spritecolor = colors[0];
        gameObject.GetComponent<SpriteRenderer>().color = spritecolor;
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingHits <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void hit()
    {
        spritecolor = colors[1];
        gameObject.GetComponent<SpriteRenderer>().color = spritecolor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6 && spritecolor == colors[1])
        {
            collision.GetComponent<PinchoParry>().MainBubbleCollided();
            Destroy(gameObject);
        }
    }

}
