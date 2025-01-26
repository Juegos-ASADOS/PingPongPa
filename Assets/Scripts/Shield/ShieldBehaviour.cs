using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    [SerializeField]
    float forceReturn;
    [SerializeField]
    float timeParry;
    private float timeActive;   //Tiempo falta para desactivar

    PlayerIDs playerId = PlayerIDs.Null;

    [SerializeField]
    AudioClip parrySound;

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
        if (other.gameObject.layer == 6)      //Colision con la layer de los proyectiles
        {
            Vector2 dir = other.transform.position - transform.position;    //Calculo del vector de exclusion

            other.GetComponent<Spike>().SetVelocity(dir.normalized * forceReturn);

            other.GetComponent<PinchoParry>().hit(playerId);

        }
        else if (other.gameObject.layer == 8)
        {
            Vector2 dir = other.transform.position - transform.position;    //Calculo del vector de exclusion
            other.GetComponent<TinyBubble>().SetVelocity(dir.normalized * forceReturn);

            other.GetComponent<TinyBubbleParry>().Hit();
        }
    }

    private void OnEnable()
    {
        timeActive = timeParry;
        GameManager.Instance.PlaySound(parrySound, 0.05f);
    }
}
