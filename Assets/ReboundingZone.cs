using UnityEngine;

public class ReboundingZone : MonoBehaviour
{
    const int SPIKELAYER_OSCAR = 6;

    [SerializeField]
    private float reboundingSpeed = 50.0f;

    [SerializeField]
    private float _randomAngleMax = 30f;

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.layer == SPIKELAYER_OSCAR)
        {
            float angle = (Random.Range(_randomAngleMax, -_randomAngleMax)) * Mathf.Deg2Rad;          //Angulo random para que no sea la normal

            Vector2 dir = -collision.transform.position;

            dir = dir.normalized;
            dir = new Vector2(
                dir.x * Mathf.Cos(angle) - dir.y * Mathf.Sin(angle),
                dir.x * Mathf.Sin(angle) + dir.y * Mathf.Cos(angle)
            );

            dir = dir.normalized;
            dir *= reboundingSpeed;

            collision.GetComponent<Spike>().SetVelocity(dir);


            //float rotationAngle = Mathf.Atan2(spikeTransform.position.x, spikeTransform.position.y) * Mathf.Rad2Deg;
            ////rotationAngle = 360 - rotationAngle;
            //spikeTransform.Rotate(new Vector3(0,0, rotationAngle));

        }
    }
}
