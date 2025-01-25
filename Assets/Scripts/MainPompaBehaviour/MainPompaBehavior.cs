using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MainPompaBehavior : MonoBehaviour
{


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
    public Transform trans;

    //Invulnerability time
    float invulnerableTime = 0.5f;
    float invulnerableCountDown = 0.0f;
    bool bubbleIsInvulnerable = false;



    //array de materiales
    public Material[] materiales;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trans = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        scaleObjetive = actualLevel = initialLevel;
        scaleFactor = trans.localScale;
        materiales = new Material[maxLevel];
    }

    // Update is called once per frame
    void Update()
    {

#if DEBUG
        // Debug Input Input
        if (Input.GetKeyDown(KeyCode.E))
        {
            increaseBubbleOnHit(1.0f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            decreaseToLowerLevel();
        }
#endif
        float delta = Time.deltaTime;

        checkInvulnerability(delta);

        bubbleGrowth(delta);

        setMaterialByLevel();

    }


    void setMaterialByLevel()
    {
        spriteRenderer.material = materiales[actualLevel-1];
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

            //calculate what value the bubble needs to growth or decreasse
            float magnitude = (scaleFactor * scaleObjetive).x - trans.localScale.x;
            magnitude = magnitude / Mathf.Abs(magnitude);

            //actual scale aniamtion (we would use it in case we want the bubble to scale rapidly to a point instead of instantly)
            trans.localScale += Vector3.one * (animationGrothSpeed * deltaTime) * magnitude;
        }
    }


    //increae the bubbble level by a percentage given by a proyectile
    void increaseBubbleOnHit(float percentageIncreased)
    {

        float sum = radiusLevelsInterval * percentageIncreased;

        scaleObjetive += sum;

        actualLevel = (int)(scaleObjetive / radiusLevelsInterval);

        if (actualLevel >= maxLevel)
        {
            actualLevel = maxLevel;
            scaleObjetive = actualLevel * radiusLevelsInterval;
        }

        //increase instantly
        trans.localScale = scaleFactor * scaleObjetive;



        //actualize the bubble desired scale


        ////Uncomment this code to make the bubble change size dinamically
        ////Increase bubble radius to reach next level and further increase the next level what remains of the percentage increase
        //levelPercentageRadius += percentageIncreased;
        //if (levelPercentageRadius >= 100.0f)
        //{
        //    levelPercentageRadius -= 100.0f;
        //    actualLevel++;
        //}
    }

    void decreaseToLowerLevel()
    {
        actualLevel--;
        scaleObjetive = actualLevel * radiusLevelsInterval;

        //change scale instantly
        trans.localScale = scaleFactor * scaleObjetive;
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
            bubbleIsInvulnerable=false;
            
    }

}
