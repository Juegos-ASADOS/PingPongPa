using UnityEngine;
using UnityEngine.UIElements;

public class ReboundingZone : MonoBehaviour
{
    const int SPIKELAYER_OSCAR = 6;

    [SerializeField]
    private float reboundingSpeed = 50.0f;


    private void OnTriggerExit2D(Collider2D collision)
    {   
        float angle = 180 + Random.Range(30, -30);          //Angulo random para que no sea la normal

        // Ajusta la rotación para que apunte en la nueva dirección reflejada
        collision.transform.rotation *= Quaternion.Euler(0, 0, angle);

        Vector2 dir = -collision.transform.position;
        dir = dir.normalized;  
        dir*=reboundingSpeed;

        if (collision.gameObject.layer == SPIKELAYER_OSCAR)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Transform spikeTransform = rb.transform;

            rb.linearVelocity = Vector2.zero;
            rb.AddForce(dir);

            //float rotationAngle = Mathf.Atan2(spikeTransform.position.x, spikeTransform.position.y) * Mathf.Rad2Deg;
            ////rotationAngle = 360 - rotationAngle;
            //spikeTransform.Rotate(new Vector3(0,0, rotationAngle));

        }
    }
}
