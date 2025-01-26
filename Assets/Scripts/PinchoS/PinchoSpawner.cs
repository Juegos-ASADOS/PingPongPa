using UnityEngine;

//Script encargado de la zona de spawn de los pinchos, el tiempo de spawn y la posición
public class PinchoSpawner : MonoBehaviour
{
    //Variables serializadas
    [SerializeField]
    private float timeBeetweenSpikes = 3.0f;    //Tres segundos por defecto
    [SerializeField]
    private float spawningRadious = 5.0f;
    [SerializeField]
    private float spikeSpeed = 50.0f;
    [SerializeField]
    private GameObject spikePrefab;
    [SerializeField]
    private float angleBetweenSpikes = 5.0f;
    [SerializeField]
    private GameObject spawnPrefab;
    GameObject spawnObject = null;
    Vector2 spawnPosition;
    int angle;
    //Variables privadas
    private float timeSincelastSpawn = 0.0f;
    private float lastAngle = 0.0f;
    bool augmentedRate = false;
    [SerializeField]
    float augmentedTime = 2.35f;

    private void Start()
    {
    }

    private void Awake()
    {
    }

    private void Update()
    {
        if (!GameManager.Instance.gameStarted)
            return;

        if (timeSincelastSpawn < timeBeetweenSpikes)
            timeSincelastSpawn += Time.deltaTime;
        else
        {
            //Posicion spawn
            angle = Random.Range(0, 361);
            while (Mathf.Abs(angle - lastAngle) < angleBetweenSpikes) angle = Random.Range(0, 361);  //Para respetar la diferencia entre pinchos

            spawnPosition = new Vector2(spawningRadious * Mathf.Cos(angle), spawningRadious * Mathf.Sin(angle));    //Formula matemática

            spawnObject = Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
            spawnObject.GetComponentInChildren<SpikeAnimation>().Init(gameObject);
            timeSincelastSpawn = 0;
            lastAngle = angle;


            ////Animación de antes de que aparezca el pincho
            //Debug.Log("Playing animation");
            //anim.Play();
            //SpawnSpike();
        }
        if(GameManager.Instance.Score >= 120 && !augmentedRate)
        {
            augmentedRate = true;
            timeBeetweenSpikes = augmentedTime;
        }
    }

    public void SpawnSpike()
    {
        Destroy(spawnObject);
        GameObject newSpike = Instantiate(spikePrefab, spawnPosition, Quaternion.identity);
        //Calculo del vector hacia el centro.

        Vector3 direction = -newSpike.transform.position; //El mismo pero negado porque es PosFinal - PosInicial

        direction = direction.normalized;       //Normalizamos para que no vaya más deprisa cuanto más lejos aparezca
        direction *= spikeSpeed;    //Multiplicamos por la velocidad

        newSpike.GetComponent<Spike>().SetVelocity(direction);

    }

    //Para Debuggear el área por la que aparecen los pinchos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawningRadious);
    }

    public float GetRadious() { return spawningRadious; }

}
