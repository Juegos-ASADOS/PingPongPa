using UnityEngine;

public class Spike : MonoBehaviour
{
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(Vector3 vel)
    {
        rb.totalForce = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(vel);

        var angle = Vector2.SignedAngle(-transform.up, vel.normalized);

        var toRotate = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation *= toRotate;
    }
}
