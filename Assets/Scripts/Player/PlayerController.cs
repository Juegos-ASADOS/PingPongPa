using UnityEngine;
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
    Animator animator, bubbleAnimator;
    [SerializeField]
    SpriteRenderer spriteRenderer;
    [SerializeField]
    SpriteRenderer spriteRendererFlare;

    [SerializeField]
    RuntimeAnimatorController[] controllers, bubbleControllers;

    [SerializeField]
    Transform playerRealTr;

    PlayerIDs playerId = PlayerIDs.Null;

    bool onRebound = false;

    Color[] colorPlayers = { new Color(0.259f, 0.863f, 0.737f), new Color(1.0f, 0.631f, 0.773f) };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reboundTimer = reboundTime;
    }

    public void Init(int id)
    {
        playerId = (PlayerIDs)id;
        animator.runtimeAnimatorController = controllers[id - 1];
        bubbleAnimator.runtimeAnimatorController = bubbleControllers[id - 1];
        transform.localEulerAngles = new Vector3(0, 0, 180 * (id - 1));
        Material mat = spriteRendererFlare.material;
        if (mat && mat.HasProperty("_Color"))
        {
            mat.SetColor("_Color", colorPlayers[id - 1]);
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
                rb.angularVelocity = 0;
                animator.SetTrigger("NoPegao");
                onRebound = false;
            }
        }

        animator.SetFloat("vel", Mathf.Abs(rb.angularVelocity));
        if (rb.angularVelocity > 0) spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
    }

    public void OnMove(CallbackContext context)
    {
        if (onRebound || !GameManager.Instance.gameStarted)
            return;
        if (context.canceled)
        {
            rb.angularVelocity = 0;
            return;
        }

        Vector2 moveValue = context.ReadValue<Vector2>();
        int moveSign = (int)(moveValue.x / Mathf.Abs(moveValue.x));
        rb.angularVelocity = -moveSign * speed * 1000 * Time.deltaTime / playerRealTr.localPosition.y;
        rb.angularDamping = 0f;
    }

    public void StopForRebound()
    {
        animator.SetTrigger("Pegao");
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
