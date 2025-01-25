using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    [SerializeField]
    float forceReturn;
    [SerializeField]
    float timeParry;
    private float timeActive;   //Tiempo falta para desactivar

    private void Update()
    {
        if(timeActive >=0)
            timeActive -= Time.deltaTime;
        else
            transform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 6)      //Colision con la layer de los proyectiles
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.zero;

            Vector2 dir = other.transform.position - transform.position;
            rb.AddForce(dir.normalized * forceReturn);
            timeActive = timeParry;
        }
    }
}
