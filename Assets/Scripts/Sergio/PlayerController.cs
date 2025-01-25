using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    Rigidbody2D movementParent;

    [SerializeField]
    float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveAction = GetComponent<PlayerInput>().actions.FindAction("Move");
        movementParent = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();

        movementParent.angularVelocity = -moveValue.x * speed * 1000 * Time.deltaTime;
    }
}
