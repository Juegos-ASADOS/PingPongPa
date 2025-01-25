using UnityEngine;

/* 
 * Script que se encarga del movimiento aleatorio del pincho
 */
public class PinchoMovement : MonoBehaviour
{
    [SerializeField]
    float speed;
    Vector3 position;
    float screenWidth, screenHeight;
    float top, bottom, left, right;
    private void Awake()
    {
        position = transform.position;
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        Debug.Log(transform.position);
        float h = transform.localScale.y/2;
        float w = transform.localScale.x/2;
        top = screenHeight / 2 - h;
        bottom = -1* screenHeight / 2 - h;
        left = -1* screenWidth / 2 - w;
        right = screenWidth / 2 - w;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        if(transform.position.y >= top)
        {
            position.x = top;
        }
    }
}
