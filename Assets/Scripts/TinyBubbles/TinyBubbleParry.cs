using UnityEngine;

public class TinyBubbleParry : MonoBehaviour
{
    [SerializeField]
    int remainingHits;
    [SerializeField]
    Color[] colors = new Color[2];
    Color spritecolor;
    float[] colorHue = new float[2];
    Material bubbleMaterial;
    bool parried;

    [SerializeField]
    AudioClip _spikeDestroyed;
    [SerializeField]
    AudioClip _bubbleSpawned;
    [SerializeField]
    AudioClip _bubbleHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleMaterial = gameObject.GetComponent<SpriteRenderer>().material;
        bubbleMaterial.SetFloat("_Level", colorHue[0]);
        parried = false;
        spritecolor = colors[0];
        gameObject.GetComponent<SpriteRenderer>().color = spritecolor;

        GameManager.Instance.PlaySound(_bubbleSpawned, 0.04f);
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingHits <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Hit()
    {
        parried = true;
        bubbleMaterial.SetFloat("_Level", colorHue[1]);
        spritecolor = colors[1];
        gameObject.GetComponent<SpriteRenderer>().color = spritecolor;

        GameManager.Instance.PlaySound(_bubbleHit, 0.04f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && parried)
        {
            collision.GetComponent<PinchoParry>().MainBubbleCollided();
            Destroy(gameObject);
            GameManager.Instance.PlaySound(_spikeDestroyed, 0.02f);
        }
    }

}
