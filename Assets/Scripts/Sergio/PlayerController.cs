using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D movementParent;

    [SerializeField]
    float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementParent = GetComponentInParent<Rigidbody2D>();
    }

    public void OnMove(CallbackContext context)
    {
        Vector2 moveValue = context.ReadValue<Vector2>();
        movementParent.angularVelocity = -moveValue.x * speed * 1000 * Time.deltaTime;
    }
}
