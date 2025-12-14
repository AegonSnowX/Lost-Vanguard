using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 inputDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 rawInput = new Vector2(x, y);

        // No input, no movement
        if (rawInput == Vector2.zero)
        {
            inputDirection = Vector2.zero;
            return;
        }

        // Snap input to 8 directions
        inputDirection = rawInput.normalized;

        // Optional: hard snap to exact directions (no floating error)
        inputDirection = new Vector2(
            Mathf.Round(inputDirection.x),
            Mathf.Round(inputDirection.y)
        ).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = inputDirection * moveSpeed;
    }
}
