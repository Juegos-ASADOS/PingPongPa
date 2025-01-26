using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEngine.InputSystem.InputAction;

public class MainPompaBehavior : MonoBehaviour
{
    AudioSource audio;
    //The ammount of scale that correspond to each level
    public float radiusLevelsInterval = 1.0f;
    //How many seconds it takes for the bubble to grow to the next level.
    public float growSpeedSeconds = 5.0f;

    //It will go x time faster when the bubble isnt the scale that is desired
    public float animationGrothSpeed = 2.0f;

    //max level into wich it stop growing
    public int maxLevel = 5;

    //this is the variable we are gonna use to animate cand calculate the actual growth
    public float scaleObjetive = 1;

    public int initialLevel = 2;

    private int actualLevel = 1;

    //Percentage use to measure actual level radius
    //float levelPercentageRadius = 0.0f;

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

#if DEBUG
    bool debugDeath = true;
#endif 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
        tr = transform;
        spriteRenderer = tr.GetComponentInChildren<SpriteRenderer>();
        scaleObjetive = actualLevel = initialLevel;
        scaleFactor = tr.localScale;
        bubbleMaterial = tr.GetComponentInChildren<SpriteRenderer>().material;
        animator = tr.GetComponentInChildren<Animator>();
        actualLevelColor = (float)LevelColor.LEVEL1;
        bubbleExplosion = false;
        vfxExplosionLifeTime = 10.0f;
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
        if(Input.GetKeyDown(KeyCode.L))
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
            case 1:
                {
                    actualLevelColor = (float)LevelColor.LEVEL1;
                    nextLevelColor = (float)LevelColor.LEVEL2;
                }
                break;
            case 2:
                {
                    actualLevelColor = (float)LevelColor.LEVEL2;
                    nextLevelColor = (float)LevelColor.LEVEL3;

                }
                break;
            case 3:
                {
                    actualLevelColor = (float)LevelColor.LEVEL3;
                    nextLevelColor = (float)LevelColor.LEVEL4;
                }
                break;
            case 4:
                {
                    actualLevelColor = (float)LevelColor.LEVEL4;
                    nextLevelColor = (float)LevelColor.LEVEL5;

                }
                break;

            case 5:
                {
                    actualLevelColor = (float)LevelColor.LEVEL5;
                    nextLevelColor = (float)LevelColor.LEVEL5;

                }

                break;
        }

        //Color Lerp
        bubbleMaterial.SetFloat("_Level", Mathf.Lerp(actualLevelColor, nextLevelColor, scaleObjetive - actualLevel));
    }


    void bubbleGrowth(float deltaTime)
    {
        if (actualLevel < maxLevel)
        {
            //crecimiento por tiempo
            scaleObjetive += radiusLevelsInterval / growSpeedSeconds * deltaTime;

            if (scaleObjetive / radiusLevelsInterval > actualLevel + 1)
            {
                actualLevel++;
            }

            ////calculate what value the bubble needs to growth or decreasse
            //float magnitude = (scaleFactor * scaleObjetive).magnitude - tr.localScale.magnitude;
            //magnitude = magnitude / Mathf.Abs(magnitude);

            //if (magnitude < 0)
            //{
            //    Debug.LogError("me cachis");
            //}

            //actual scale animation (we would use it in case we want the bubble to scale rapidly to a point instead of instantly)


            if (scaleFactor.magnitude * scaleObjetive < tr.localScale.magnitude)
            {
                tr.localScale += Vector3.one * (growSpeedSeconds * deltaTime);
            }
            else
            {
                tr.localScale = scaleFactor * scaleObjetive;
            }

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
        if (actualLevel >= maxLevel)
            return;

        float sum = radiusLevelsInterval * percentageIncreased;

        scaleObjetive += sum;

        actualLevel = (int)(scaleObjetive / radiusLevelsInterval);

        if (actualLevel > maxLevel)
        {
            actualLevel = maxLevel;
            audio.Play();
            scaleObjetive = actualLevel * radiusLevelsInterval;
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
        scaleObjetive = actualLevel * radiusLevelsInterval;

        //change scale instantly
        tr.localScale = scaleFactor * scaleObjetive;

        if (actualLevel >= 1)
            //Animation
            playBounceAnimation();
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

    }

    public void PlayerSpawned(PlayerInput player)
    {
        playerSpawns[indxSpawn].GetComponent<Animator>().SetTrigger("game");
        playerSpawns[indxSpawn].GetComponent<SpawnPlayer>().player = player.gameObject;
        Transform playerTr = player.transform.GetChild(0);

        Vector2 playerPos = new(playerTr.position.x, tr.localScale.y / 2f);
        playerTr.SetLocalPositionAndRotation(playerPos, playerTr.localRotation);

        OnSizeChange.AddListener(player.GetComponent<PlayerController>().BubbleSizeChanged);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)
        {
            decreaseToLowerLevel();
            other.GetComponent<PinchoParry>().MainBubbleCollided();
        }
        else if (other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
            increaseBubbleOnHit(1);
        }
    }
}
