using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;

    private Vector2 lastMoveDirection = Vector2.down;

    private void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (rb == null)
            rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 velocity = rb.linearVelocity;

        bool isMoving = velocity.sqrMagnitude > 0.01f;
        animator.SetBool("IsMoving", isMoving);

        if (isMoving)
        {
            Vector2 moveDir = velocity.normalized;

            // Store last direction for idle facing
            lastMoveDirection = moveDir;

            animator.SetFloat("MoveX", Mathf.Round(moveDir.x));
            animator.SetFloat("MoveY", Mathf.Round(moveDir.y));
        }
        else
        {
            // Keep facing last direction while idle
            animator.SetFloat("MoveX", Mathf.Round(lastMoveDirection.x));
            animator.SetFloat("MoveY", Mathf.Round(lastMoveDirection.y));
        }
    }
}
