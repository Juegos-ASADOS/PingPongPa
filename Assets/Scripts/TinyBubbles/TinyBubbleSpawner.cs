using UnityEngine;
using UnityEngine.Timeline;

//Script encargado de la zona de spawn de los pinchos, el tiempo de spawn y la posición
public class TinyBubbleSpawner : MonoBehaviour
{
    //Variables serializadas
    [SerializeField]
    private float timeBeetweenTinyBubbles = 3.0f;    //Tres segundos por defecto
    [SerializeField]
    private float spawningRadious = 5.0f;
    [SerializeField]
    private float TinyBubbleSpeed = 50.0f;
    [SerializeField]
    private GameObject TinyBubblePrefab;
    [SerializeField]
    private float angleBetweenTinyBubbles = 5.0f;

    //Variables privadas
    private float timeSincelastSpawn = 0.0f;
    private float lastAngle = 0.0f;


    private void Update()
    {
        if (timeSincelastSpawn < timeBeetweenTinyBubbles)
            timeSincelastSpawn += Time.deltaTime;
        else
        {
            //Posicion spawn
            int angle = Random.Range(0, 361);
            while (Mathf.Abs(angle - lastAngle) < angleBetweenTinyBubbles) angle = Random.Range(0, 361);  //Para respetar la diferencia entre pinchos

            Vector2 spawnPosition = new Vector2(spawningRadious * Mathf.Cos(angle), spawningRadious * Mathf.Sin(angle));    //Formula matemática

            GameObject newTinyBubble = Instantiate(TinyBubblePrefab, spawnPosition, Quaternion.identity);
            //Calculo del vector hacia el centro.

            Vector3 direction = -newTinyBubble.transform.position; //El mismo pero negado porque es PosFinal - PosInicial


            float rotationAngle = Mathf.Atan2(spawnPosition.x, spawnPosition.y) * Mathf.Rad2Deg;
            rotationAngle = 360 - rotationAngle;  //Porque gira del revés
            newTinyBubble.transform.Rotate(new Vector3(0, 0, rotationAngle)); //Lo pongo apuntando al centro

            newTinyBubble.GetComponent<TinyBubbleBounds>().setSceneryRadius(spawningRadious);

            direction = direction.normalized;       //Normalizamos para que no vaya más deprisa cuanto más lejos aparezca
            direction *= TinyBubbleSpeed;    //Multiplicamos por la velocidad


            newTinyBubble.GetComponent<Rigidbody2D>().AddForce(direction);

            timeSincelastSpawn = 0;
            lastAngle = angle;
        }
    }

    //Para Debuggear el área por la que aparecen los pinchos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawningRadious);
    }

    public float GetRadious() { return spawningRadious; }

}
