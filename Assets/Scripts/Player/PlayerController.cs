using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    float reboundTime;
    float reboundTimer;

    Rigidbody2D rb;

    [SerializeField]
    Transform playerRealTr;

    int playerId = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reboundTimer = reboundTime;
    }

    public void Init(int id)
    {
        playerId = id;

        // TODO: cambiar por cambiar el sprite
        if (playerId == 1)
            GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        else if (playerId == 2)
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }
    }

    private void Update()
    {
        if (reboundTimer < reboundTime)
            reboundTimer += Time.deltaTime;
    }

    public void OnMove(CallbackContext context)
    {
        if (reboundTimer < reboundTime)
            return;

        Vector2 moveValue = context.ReadValue<Vector2>();
        rb.angularVelocity = -moveValue.x * speed * 1000 * Time.deltaTime;
    }

    public void StopForRebound()
    {
        reboundTimer = 0f;
    }

    public void BubbleSizeChanged(float newScale)
    {
        Vector2 newPos = new(playerRealTr.localPosition.x, newScale / 2f);
        playerRealTr.localPosition = newPos;
    }
}
