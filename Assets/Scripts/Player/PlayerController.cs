using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.InputSystem.InputAction;

public enum PlayerIDs { PlayerA = 1, PlayerB, Null = -1 }

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

    PlayerIDs playerId = PlayerIDs.Null;

    bool onRebound = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reboundTimer = reboundTime;
    }

    public void Init(int id)
    {
        playerId = (PlayerIDs)id;

        // TODO: cambiar por cambiar el sprite
        if (playerId == PlayerIDs.PlayerA)
            GetComponentInChildren<SpriteRenderer>().color = new Color(255, 0, 206);
        else if (playerId == PlayerIDs.PlayerB)
        {
            GetComponentInChildren<SpriteRenderer>().color = new Color(0, 255, 0);
            transform.localEulerAngles = new Vector3(0, 0, 180);
        }

        GetComponentInChildren<ShieldBehaviour>(true).Init(playerId);
    }

    private void Update()
    {
        if (onRebound)
        {
            reboundTimer += Time.deltaTime;
            if (reboundTimer > reboundTime)
            {
                onRebound = false;
                rb.angularDamping = 0f;
            }
        }
    }

    public void OnMove(CallbackContext context)
    {
        if (onRebound)
            return;

        Vector2 moveValue = context.ReadValue<Vector2>();
        rb.angularVelocity = -moveValue.x * speed * 1000 * Time.deltaTime;
    }

    public void StopForRebound()
    {
        onRebound = true;
        reboundTimer = 0f;
        rb.angularDamping = 1f;
    }

    public void BubbleSizeChanged(float newScale)
    {
        Vector2 newPos = new(playerRealTr.localPosition.x, newScale / 2f);
        playerRealTr.localPosition = newPos;
    }
}
