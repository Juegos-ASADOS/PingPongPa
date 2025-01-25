using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
/* 
 * Script que se encarga del movimiento aleatorio del pincho
 */
public class PinchoMovement : MonoBehaviour
{
    [SerializeField]
    float speed;
    float sceneryRadius=4.5f;
    private void Awake()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sceneryRadius = sceneryRadius - (transform.localScale.x / 200);
    }


    // Update is called once per frame
    void Update()
    {
        // Mueve el objeto
        transform.Translate(-transform.up * speed * Time.deltaTime, Space.World);
        CheckBounds();
    }
    public void setSceneryRadius(float radius)
    {
        sceneryRadius = radius-(transform.localScale.x / 200);
    }
    private void CheckBounds()
    {
        // Calcula la distancia del objeto al centro del círculo
        Vector2 position = transform.position;
        float distanceToCenter = position.magnitude;

               // Si el objeto está fuera del radio del escenario, ajusta la dirección
        if (distanceToCenter >= sceneryRadius)
        {
            //// Obtén la dirección hacia el centro del círculo
            //Vector2 toCenter = -position.normalized;

            //// Calcula el ángulo necesario para reorientar el objeto
            //float angle = Mathf.Atan2(toCenter.y, toCenter.x) * Mathf.Rad2Deg;
            //angle = 360 - angle;
            //// Ajusta la rotación para que apunte hacia el interior del círculo
            //transform.rotation = Quaternion.Euler(0, 0, angle + 180);
            // Calcula la normal del punto de colisión (dirección desde el centro)
            Vector2 normal = position.normalized;
            
            // Refleja el vector 'transform.up' respecto a la normal
            Vector2 reflectedDirection = Vector2.Reflect(transform.up, normal);

            float angle=180 + Random.Range(30, -30);
            // Calcula el ángulo de la nueva dirección
            // float angle = Mathf.Atan2(-reflectedDirection.y, -reflectedDirection.x) * Mathf.Rad2Deg;
           // angle = 360 - angle;
            // Ajusta la rotación para que apunte en la nueva dirección reflejada
            transform.rotation *= Quaternion.Euler(0, 0, angle);
            Debug.DrawRay(position, -reflectedDirection,Color.red,3);

            // Ajusta la posición para que quede justo dentro del círculo
            transform.position = normal * sceneryRadius;
            
        }
    }
}

