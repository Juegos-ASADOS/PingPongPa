using UnityEngine;

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
        if (!GameManager.Instance.gameStarted)
            return;

        if (timeSincelastSpawn < timeBeetweenTinyBubbles)
            timeSincelastSpawn += Time.deltaTime;
        else
        {
            //Posicion spawn
            int angle = Random.Range(0, 361);
            while (Mathf.Abs(angle - lastAngle) < angleBetweenTinyBubbles) angle = Random.Range(0, 361);  //Para respetar la diferencia entre pinchos

            Vector2 spawnPosition = new Vector2(spawningRadious * Mathf.Cos(angle), spawningRadious * Mathf.Sin(angle));    //Formula matemática

            GameObject newSpike = Instantiate(TinyBubblePrefab, spawnPosition, Quaternion.identity);
            //Calculo del vector hacia el centro.

            Vector3 direction = -newSpike.transform.position; //El mismo pero negado porque es PosFinal - PosInicial

            direction = direction.normalized;       //Normalizamos para que no vaya más deprisa cuanto más lejos aparezca
            direction *= TinyBubbleSpeed;    //Multiplicamos por la velocidad

            newSpike.GetComponent<TinyBubble>().SetVelocity(direction);

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
