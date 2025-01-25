using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;

    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    public void OnMove(CallbackContext context)
    {
        Vector2 moveValue = context.ReadValue<Vector2>();
        rb.angularVelocity = -moveValue.x * speed * 1000 * Time.deltaTime;
    }
}
