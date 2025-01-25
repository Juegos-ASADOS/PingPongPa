using UnityEngine;
/* 
 * Script que se encarga del movimiento aleatorio del pincho
 */
public class PinchoMovement : MonoBehaviour
{
    [SerializeField]
    float speed;
    float screenWidth, screenHeight;
    float top, bottom, left, right;
    Vector2 direction, position;
    private void Awake()
    {
        // Detecta los bordes de la pantalla
        position = Camera.main.WorldToViewportPoint(transform.position);
        // Dirección inicial aleatoria
        //direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        //transform.rotation = Quaternion.Euler(direction);
        // transform.up = direction;
        //transform.rotation = direction;
        
        Debug.Log("Up: " + transform.up);
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        float h = transform.localScale.y / 2;
        float w = transform.localScale.x / 2;
        top = (screenHeight / 2 - h) / 100;
        bottom = (-1 * screenHeight / 2) / 100;
        left = (-1 * screenWidth / 2 + w) / 100;
        right = (screenWidth / 2 - w) / 100;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().AddForce(direction);
        // Mueve el objeto
        transform.Translate(-transform.up * speed * Time.deltaTime, Space.World);

        //if (transform.position.y >= top)
        //{
        //    direction.y = -direction.y;
        //    // transform.up = direction;
        //}
        //else if (transform.position.y <= bottom)
        //{
        //    direction.y = -direction.y;
        //    // transform.up = direction;
        //}
        //else if (transform.position.x <= left)
        //{
        //    direction.x = -direction.x;
        //    // transform.up = direction;
        //}
        //else if (transform.position.x >= right)
        //{
        //    direction.x = -direction.x;
        //    // transform.up = direction;
        //}
        //transform.up = direction;
    }
}
//using UnityEngine;
///* 
// * Script que se encarga del movimiento aleatorio del pincho
// */
//public class PinchoMovement : MonoBehaviour
//{
//    [SerializeField]
//    private float speed = 5f; // Velocidad del pincho

//    private Vector2 direction; // Dirección del movimiento
//    private float top, bottom, left, right; // Límites de la pantalla

//    private void Awake()
//    {
//        // Dirección inicial aleatoria y normalizada
//        direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

//        // Calcular los límites del mundo (posición en coordenadas del mundo)
//        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
//        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

//        // Ajustar límites según el tamaño del objeto
//        float halfWidth = transform.localScale.x / 2;
//        float halfHeight = transform.localScale.y / 2;

//        left = screenBottomLeft.x + halfWidth;
//        right = screenTopRight.x - halfWidth;
//        bottom = screenBottomLeft.y + halfHeight;
//        top = screenTopRight.y - halfHeight;
//    }

//    private void Update()
//    {
//        // Mueve el objeto en la dirección actual
//        transform.Translate(direction * speed * Time.deltaTime);

//        // Verifica los límites de la pantalla para cambiar la dirección
//        if (transform.position.y >= top || transform.position.y <= bottom)
//        {
//            direction.y = -direction.y; // Invierte la dirección en Y
//        }

//        if (transform.position.x >= right || transform.position.x <= left)
//        {
//            direction.x = -direction.x; // Invierte la dirección en X
//        }
//    }
//}

