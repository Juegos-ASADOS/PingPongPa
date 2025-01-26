using UnityEngine;

public class TinyBubbleParry : MonoBehaviour
{
    [SerializeField]
    int remainingHits;
    [SerializeField]
    float[] colorHue = new float[2];
    Material bubbleMaterial;
    bool parried;

    //Bubble Explosion
    public GameObject bubbleExplosionVFX;
    float vfxExplosionLifeTime;
    bool bubbleExplosion;

    [SerializeField]
    AudioClip _spikeDestroyed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bubbleMaterial = gameObject.GetComponent<SpriteRenderer>().material;
        bubbleMaterial.SetFloat("_Level", colorHue[0]);
        parried = false;
        //Bubble Explosion Params
        bubbleExplosion = false;
        vfxExplosionLifeTime = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        checkBubbleExplosion(Time.deltaTime);
    }

    public void Hit()
    {
        parried = true;
        bubbleMaterial.SetFloat("_Level", colorHue[1]);
    }
    void checkBubbleExplosion(float deltaTime)
    {
        //Countdown for the mainObject to destroy
        if (bubbleExplosion)
        {
            vfxExplosionLifeTime -= deltaTime;

            if (vfxExplosionLifeTime <= 0)
                Destroy(this.gameObject);
        }
    }

    void playBubbleExplosion()
    {
        Destroy(transform.gameObject.GetComponent<SpriteRenderer>());
        

        GameObject vfxExplosion = Instantiate(bubbleExplosionVFX, transform);
        vfxExplosion.transform.SetParent(transform);
        vfxExplosion.transform.GetChild(0).localScale = transform.localScale;

        //Tiempo de vida de las particulas
        vfxExplosionLifeTime = vfxExplosion.transform.GetChild(0).GetComponent<ParticleSystem>().main.duration;
        bubbleExplosion = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6 && parried)
        {
            collision.GetComponent<PinchoParry>().MainBubbleCollided();
            playBubbleExplosion();
            var audio = GameManager.Instance.GetComponent<AudioSource>();
            audio.pitch = UnityEngine.Random.Range(0.98f, 1.02f);
            audio.PlayOneShot(_spikeDestroyed);
        }
    }

}
