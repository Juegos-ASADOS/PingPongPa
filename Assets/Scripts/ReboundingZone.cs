using UnityEngine;

public class ReboundingZone : MonoBehaviour
{
    const int SPIKELAYER = 6;

    [SerializeField]
    private float reboundingSpeed = 50.0f;

    [SerializeField]
    private float _randomAngleMax = 30f;

    AudioSource audioSrc;
    [SerializeField]
    AudioClip _bounceClip;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //El pincho desaparece al chocarse con la pompa, y se llama a este método, que me jode los sonidos, esta comprobacion nos aseguramos que solo se lame solo si sale realmente
        if (collision.gameObject.transform.position.magnitude < 2.5f)
        {
            return;
        }

        if (collision.gameObject.layer == SPIKELAYER)
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

            audioSrc.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
            audioSrc.PlayOneShot(_bounceClip);

            //float rotationAngle = Mathf.Atan2(spikeTransform.position.x, spikeTransform.position.y) * Mathf.Rad2Deg;
            ////rotationAngle = 360 - rotationAngle;
            //spikeTransform.Rotate(new Vector3(0,0, rotationAngle));

        }
    }
}
