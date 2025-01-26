using UnityEngine;

public class PinchoParry : MonoBehaviour
{
    //GameObject Transform
    Transform tr;


    [SerializeField]
    GameObject destructionParticleSystem;       //Para crearlo cuando se destruya el pincho

    PlayerIDs spikeType;
    [SerializeField]
    int remainingHits;
    [SerializeField]
    Color[] colors = new Color[2];
    //Color spritecolor;
    ColorChangePincho colorCntrl;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    //Sprites
    SpriteRenderer _spriteRenderer;
    public Sprite[] spikeFrames;

    //TrailAnchors
    Transform trailTransform;
    Transform[] trailsAnchors;
    [SerializeField]
    private Vector3 anchorOffset;

    //Sounds
    AudioSource _audioSource;
    [SerializeField]
    AudioClip _hitClip;
    [SerializeField]
    AudioClip _collidedCLip;
    [SerializeField]
    AudioClip _spawnClip;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        tr = transform;
        for (int i = 0; i < remainingHits; i++)
        {
            Vector3 anchorPosition = anchorOffset * i;
            GameObject go = new GameObject();
            GameObject anchor = Instantiate(go, tr);
            anchor.transform.localPosition = anchorPosition;
            anchor.name = "AnchorTrail" + i;
            Destroy(go);
        }
    }

    void Start()
    {
        //Transform de la estela
        trailTransform = tr.Find("Trail").transform;
        //SpriteRenderer
        _spriteRenderer = tr.GetComponent<SpriteRenderer>();

        if (remainingHits > 0)
            _spriteRenderer.sprite = spikeFrames[remainingHits - 1];
        else
            _spriteRenderer.sprite = spikeFrames[0];

        colorCntrl = GetComponent<ColorChangePincho>();
        switch (Random.Range(0, 2))
        {
            case 0:
                spikeType = PlayerIDs.PlayerA;
                break;
            case 1:
                spikeType = PlayerIDs.PlayerB;
                break;
        }
        colorCntrl.ChangeColorPincho((int)spikeType - 1);

        _audioSource.pitch = UnityEngine.Random.Range(0.98f, 1.02f);
        _audioSource.PlayOneShot(_spawnClip);
    }

    // Update is called once per frame
    void Update()
    {
        updateTrailAnchor();

        if (remainingHits <= 0)
        {
            Instantiate(destructionParticleSystem, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void updateTrailAnchor()
    {
        string anchorId;
        if (remainingHits > 0)
            anchorId = "AnchorTrail" + (remainingHits - 1);
        else
            anchorId = "AnchorTrail0";

        trailTransform.localPosition = tr.Find(anchorId).localPosition;
    }

    public void hit(PlayerIDs p)
    {
        if (p == spikeType)
        {
            remainingHits--;

            if (remainingHits > 0)  //Sigue vivo 
                _spriteRenderer.sprite = spikeFrames[remainingHits - 1];
            else   //Se termina de romper
                _spriteRenderer.sprite = spikeFrames[0];


            switch (p)
            {
                case PlayerIDs.PlayerA:
                    spikeType = PlayerIDs.PlayerB;
                    break;
                case PlayerIDs.PlayerB:
                    spikeType = PlayerIDs.PlayerA;
                    break;
            }
        }
        colorCntrl.ChangeColorPincho((int)spikeType - 1);

        _audioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        _audioSource.PlayOneShot(_hitClip);
    }

    public void MainBubbleCollided()
    {
        //Playear la animaci�n de muerte explotar
        _audioSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        _audioSource.PlayOneShot(_collidedCLip);
        Destroy(gameObject);
    }
}
