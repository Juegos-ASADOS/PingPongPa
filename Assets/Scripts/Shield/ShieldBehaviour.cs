using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    [SerializeField]
    float forceReturn;
    [SerializeField]
    float timeParry;
    private float timeActive;   //Tiempo falta para desactivar

    PlayerIDs playerId = PlayerIDs.Null;

    public void Init(PlayerIDs id)
    {
        playerId = id;
    }

    private void Update()
    {
        if (timeActive >= 0)
            timeActive -= Time.deltaTime;
        else
            transform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (timeActive <= 0 && other.gameObject.layer == 6)      //Colision con la layer de los proyectiles
        {
            Vector2 dir = other.transform.position - transform.position;    //Calculo del vector de exclusion

            other.GetComponent<Spike>().SetVelocity(dir.normalized * forceReturn);

            other.GetComponent<PinchoParry>().hit(playerId);
            //float angle = 0.0f;

            //if(other.transform.position.x >= 0)
            //    angle = Vector2.Angle(dir, Vector2.down);
            //else
            //    angle = -Vector2.Angle(dir, Vector2.down);

            //rb.transform.eulerAngles = new Vector3(0,0,angle);

        }
    }

    private void OnEnable()
    {
        timeActive = timeParry;
    }
}
