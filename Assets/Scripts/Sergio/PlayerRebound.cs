using UnityEngine;

public class PlayerRebound : MonoBehaviour
{
    [SerializeField]
    float reboundForce;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerRebound>() != null)
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

            rb.AddTorque(reboundForce * reboundSign);
        }
    }
}
