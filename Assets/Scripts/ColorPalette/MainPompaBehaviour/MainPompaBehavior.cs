using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MainPompaBehavior : MonoBehaviour
{
    AudioSource audio;
    //The ammount of scale that correspond to each level
    public float radiusLevelsInterval = 1.0f;
    //How many seconds it takes for the bubble to grow to the next level.
    public float growSpeedSeconds = 5.0f;


    //max level into wich it stop growing
    public int maxLevel = 5;

    //this is the variable we are gonna use to animate cand calculate the actual growth
    float scaleObjetive = 1;

    [SerializeField]
    float initScale = 2;

    public int initialLevel = 1;

    private int actualLevel;

    //this one is in case the object has an initial scale diferent to (1,1,1)
    private Vector3 scaleFactor;

    public SpriteRenderer spriteRenderer;
    public Transform tr;

    //Invulnerability time
    float invulnerableTime = 0.5f;
    float invulnerableCountDown = 0.0f;
    bool bubbleIsInvulnerable = false;

    //Animator Properties
    Animator animator;
    //array de materiales
    private Material bubbleMaterial;

    public UnityEvent<float> OnSizeChange;
    public UnityEvent OnBubbleDestroy;

    //Color de la burbuja
    float actualLevelColor;
    float nextLevelColor;

    enum LevelColor { LEVEL1 = 135, LEVEL2 = 165, LEVEL3 = 230, LEVEL4 = 340, LEVEL5 = 370 }

    //Bubble Explosion
    public GameObject bubbleExplosionVFX;
    float vfxExplosionLifeTime;
    bool bubbleExplosion;

    [SerializeField]
    GameObject[] playerSpawns;
    private int indxSpawn = 0;
    [SerializeField]
    AudioClip maxHealth;
    [SerializeField]
    AudioClip lowHealth;
    [SerializeField]
    AudioClip lostHealth;
    [SerializeField]
    AudioClip gainHealth;

#if DEBUG
    bool debugDeath = true;
#endif 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        tr = transform;
        spriteRenderer = tr.GetComponentInChildren<SpriteRenderer>();
        actualLevel = initialLevel;
        scaleObjetive = initScale + actualLevel * radiusLevelsInterval;
        scaleFactor = tr.localScale;
        bubbleMaterial = tr.GetComponentInChildren<SpriteRenderer>().material;
        animator = tr.GetComponentInChildren<Animator>();
        actualLevelColor = (float)LevelColor.LEVEL1; //TODO
        nextLevelColor = (float)LevelColor.LEVEL2; //TODO
        bubbleExplosion = false;
        vfxExplosionLifeTime = 10.0f;
        bubbleMaterial.SetFloat("_Level", actualLevelColor);
        tr.localScale = Vector3.one * scaleObjetive;
    }


    private void Update()
    {

#if DEBUG

        // Debug Input Input
        if (Input.GetKeyDown(KeyCode.O))
        {
            increaseBubbleOnHit(1.0f);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            decreaseToLowerLevel();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            playBounceAnimation();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            playBubbleExplosion();
        }
#endif
        checkBubbleExplosion(Time.deltaTime);
        changeColorMaterial();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (!GameManager.Instance.gameStarted)
            return;

        float delta = Time.fixedDeltaTime;

        checkInvulnerability(delta);

        bubbleGrowth(delta);

    }


    void changeColorMaterial()
    {
        //calculate from 0 to 126 (red color in Hue) from the value diference,
        //being the scale of the bubble in the maximun level 5 the hue value 0, and 1 being 126

        switch (actualLevel)
        {
            case 0:
                {
                    actualLevelColor = (float)LevelColor.LEVEL1;
                    nextLevelColor = (float)LevelColor.LEVEL2;
                }
                break;
            case 1:
                {
                    actualLevelColor = (float)LevelColor.LEVEL2;
                    nextLevelColor = (float)LevelColor.LEVEL3;
                }
                break;
            case 2:
                {
                    actualLevelColor = (float)LevelColor.LEVEL3;
                    nextLevelColor = (float)LevelColor.LEVEL4;
                }
                break;
            case 3:
                {
                    actualLevelColor = (float)LevelColor.LEVEL4;
                    nextLevelColor = (float)LevelColor.LEVEL5;
                }
                break;
            case 4:
                {
                    actualLevelColor = (float)LevelColor.LEVEL5;
                    nextLevelColor = (float)LevelColor.LEVEL5;
                }
                break;
        }

        //Color Lerp
        bubbleMaterial.SetFloat("_Level", Mathf.Lerp(actualLevelColor, nextLevelColor, (scaleObjetive - (initScale + radiusLevelsInterval * actualLevel)) / radiusLevelsInterval));
    }


    void bubbleGrowth(float deltaTime)
    {
        if (actualLevel < maxLevel - 1)
        {
            //crecimiento por tiempo
            scaleObjetive += radiusLevelsInterval / growSpeedSeconds * deltaTime;

            if (scaleObjetive > (actualLevel + 1) * radiusLevelsInterval + initScale)
            {
                actualLevel++;
            }
            //actual scale animation (we would use it in case we want the bubble to scale rapidly to a point instead of instantly)
            tr.localScale = scaleFactor * scaleObjetive;

            OnSizeChange.Invoke(tr.localScale.y);
        }
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


    //increae the bubbble level by a percentage given by a proyectile
    void increaseBubbleOnHit(float percentageIncreased)
    {
        if (actualLevel >= maxLevel - 1)
            return;

        float sum = radiusLevelsInterval * percentageIncreased;

        scaleObjetive += sum;

        actualLevel = (int)((scaleObjetive - initScale) / radiusLevelsInterval);        

        if (actualLevel > maxLevel -1)
        {
            actualLevel = maxLevel;
            audio.pitch = UnityEngine.Random.Range(0.99f, 1.01f);
            audio.PlayOneShot(maxHealth);
            scaleObjetive = actualLevel * radiusLevelsInterval + initScale;
        }
        else
            //Animation
            playBounceAnimation();

        //increase instantly
        tr.localScale = scaleFactor * scaleObjetive;

        //actualize the bubble desired scale
    }

    void decreaseToLowerLevel()
    {
        if (actualLevel <= 0)
            return;

        actualLevel--;
        scaleObjetive = actualLevel * radiusLevelsInterval + initScale;

        //change scale instantly
        tr.localScale = scaleFactor * scaleObjetive;

        if (actualLevel >= 1)
            //Animation
            playBounceAnimation();
        if (actualLevel == 1)
        {
            audio.pitch = 1f;
            audio.PlayOneShot(lowHealth);
        }
        else if (actualLevel <= 0)
        {
            playBubbleExplosion();
        }
    }

    void activateInvulnerability()
    {
        invulnerableCountDown = invulnerableTime;
        bubbleIsInvulnerable = true;
    }

    void checkInvulnerability(float deltaTime)
    {
        invulnerableCountDown -= deltaTime;

        if (invulnerableCountDown <= 0.0f)
            bubbleIsInvulnerable = false;

    }

    void playBounceAnimation()
    {
        animator.SetTrigger("BubbleBounce");
    }

    void playBubbleExplosion()
    {
        Transform child = tr.GetChild(0);
        if (child.GetComponent<SpriteRenderer>())
            Destroy(child.gameObject);

        GameObject vfxExplosion = Instantiate(bubbleExplosionVFX, tr);
        vfxExplosion.transform.SetParent(tr);
        vfxExplosion.transform.GetChild(0).localScale = tr.localScale;

        //Tiempo de vida de las particulas
        vfxExplosionLifeTime = vfxExplosion.transform.GetChild(0).GetComponent<ParticleSystem>().main.duration;
        bubbleExplosion = true;
        OnBubbleDestroy.Invoke();
    }

    public void PlayerSpawned(PlayerInput player)
    {
        playerSpawns[indxSpawn].GetComponent<Animator>().SetTrigger("game");
        playerSpawns[indxSpawn].GetComponent<SpawnPlayer>().player = player.gameObject;
        indxSpawn++;

        Transform playerTr = player.transform.GetChild(0);

        Vector2 playerPos = new(playerTr.position.x, tr.localScale.y / 2f);
        playerTr.SetLocalPositionAndRotation(playerPos, playerTr.localRotation);

        PlayerController pc = player.GetComponent<PlayerController>();
        OnSizeChange.AddListener(pc.BubbleSizeChanged);
        OnBubbleDestroy.AddListener(pc.BubbleDestroyed);
        GameManager.Instance.PlayerSpawned(player);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            decreaseToLowerLevel();
            other.GetComponent<PinchoParry>().MainBubbleCollided();

            //Playear la animacion de muerte explotar
            audio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            audio.PlayOneShot(lostHealth);
        }
        else if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
            increaseBubbleOnHit(1);
            //Playear la animacion de muerte explotar
            audio.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            audio.PlayOneShot(gainHealth);
        }
    }
}
