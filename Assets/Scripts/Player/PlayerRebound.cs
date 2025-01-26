using UnityEngine;

public class PlayerRebound : MonoBehaviour
{
    [SerializeField]
    float reboundForce, parryPlayerMulti;

    Rigidbody2D rb;
    PlayerController playerController;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerController = GetComponentInParent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerRebound>() != null || collision.GetComponent<ShieldBehaviour>() != null)
        {
            float thisAngle = transform.parent.rotation.eulerAngles.z;
            float otherAngle = collision.transform.parent.rotation.eulerAngles.z;
            int reboundSign;

            if (otherAngle < thisAngle)
                reboundSign = 1;
            else
                reboundSign = -1;

            if (Mathf.Abs(otherAngle - thisAngle) > 180)
                reboundSign *= -1;

            rb.angularVelocity = 0;
            float multipl = 1f;
            if (collision.GetComponent<ShieldBehaviour>() != null)
                multipl *= parryPlayerMulti;
            rb.AddTorque(reboundForce * multipl * reboundSign / transform.localPosition.y);
            playerController.StopForRebound();
        }
    }
}
