using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
/* 
 * Script que se encarga del movimiento aleatorio del pincho
 */
public class PinchoBounds : MonoBehaviour
{
    [SerializeField]
    float sceneryRadius=4.5f;

    void Start()
    {
        //Para que no se salga el sprite de la pantalla
        sceneryRadius = sceneryRadius - (transform.localScale.x / 200);
    }

    void Update()
    {
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
        if (distanceToCenter > sceneryRadius)
        {
            Vector2 normal = position.normalized;
            
            float angle = 180 + Random.Range(30, -30);

            // Ajusta la rotación para que apunte en la nueva dirección reflejada
            transform.rotation *= Quaternion.Euler(0, 0, angle);

            // Ajusta la posición para que quede justo dentro del círculo
            transform.position = normal * sceneryRadius;

            Debug.Log(distanceToCenter);
        }
    }

}

