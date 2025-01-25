using Unity.VisualScripting;
using UnityEngine;

public class MainPompaBehavior : MonoBehaviour
{

    //The ammount of scale taht correspond to each level
    public float scaleLevelsInterval = 1.0f;
    //How many seconds it takes for the bubble to grow to the next level.
    public float GrowSpeedSeconds = 5.0f;

    //It will go 2 time faster when the bubble isnt the scale that is desired
    public float animationGrothSpeed = 2.0f;


    //max level into wich it stop growing
    public int maxLevel = 5;

    //this is the variable we are gonna use to animate cand calculate the actual growth
    public float scaleObjetive = 1;

    public int initialLevel = 2;

    int actualLevel = 1;

    //this one is in case the object has an initial scale diferent to (1,1,1)
    private Vector3 scaleFactor;

    public SpriteRenderer spriteRenderer;
    public Transform trans;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        trans = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        scaleObjetive = actualLevel = initialLevel;
        scaleFactor = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

        float delta = Time.deltaTime;
        continousGrowing(delta);

        ScalarLerp a = new ScalarLerp();

        //calculate what value the bubble needs to growth or decreasse
        float magnitude =  (scaleFactor * scaleObjetive).x - trans.localScale.x;
        magnitude = magnitude / Mathf.Abs(magnitude);

        //actual scale aniamtion
        trans.localScale += Vector3.one * (animationGrothSpeed * delta) * magnitude;

    }

    void inmediateGrowing()
    {
        if (actualLevel < maxLevel)
        {
            actualLevel++;
            scaleObjetive = scaleLevelsInterval * actualLevel;
        }
    }

    void continousGrowing(float deltaTime)
    {
        if (actualLevel < maxLevel)
        {
            //growth by time
            scaleObjetive += scaleLevelsInterval / GrowSpeedSeconds * deltaTime;

            if (scaleObjetive / scaleLevelsInterval > actualLevel + 1)
            {
                actualLevel++;
            }
        }
    }

}
