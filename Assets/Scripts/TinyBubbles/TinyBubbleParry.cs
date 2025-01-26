using UnityEngine;

public class TinyBubbleParry : MonoBehaviour
{
    [SerializeField]
    int remainingHits;
    [SerializeField]
    float[] colorHue = new float[2];
    Material bubbleMaterial;
    bool parried;

    [SerializeField]
    AudioClip _spikeDestroyed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleMaterial = gameObject.GetComponent<SpriteRenderer>().material;
        bubbleMaterial.SetFloat("_Level", colorHue[0]);
        parried = false;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && parried)
        {
            collision.GetComponent<PinchoParry>().MainBubbleCollided();
            Destroy(gameObject);
            var audio = GameManager.Instance.GetComponent<AudioSource>();
            audio.pitch = UnityEngine.Random.Range(0.98f, 1.02f);
            audio.PlayOneShot(_spikeDestroyed);
        }
    }

}
