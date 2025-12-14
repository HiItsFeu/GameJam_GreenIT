using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2f;  // pour slow fall
    public float lowJumpMultiplier = 2f; // pour jump court si on rel�che Space
    public Animator animator;
    private int isGrounded = 0;
    private int standing = 0;
    private bool firstCollide = true;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // -1,0,1 plus propre que KeyCheck
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        // Animation set
        if (horizontal < 0)
        {
            if (animator.GetBool("is_alive")) transform.rotation = Quaternion.Euler(0, 0, -7f);
            standing = 0;
            animator.SetBool("is_walking", true);
            animator.SetBool("is_standing", false);
            animator.SetBool("is_right", false);
            animator.SetBool("is_left", true);
        }
        else if (horizontal > 0)
        {
            if (animator.GetBool("is_alive")) transform.rotation = Quaternion.Euler(0, 0, 7f);
            standing = 0;
            animator.SetBool("is_walking", true);
            animator.SetBool("is_standing", false);
            animator.SetBool("is_right", true);
            animator.SetBool("is_left", false);
        }
        else
        {
            standing++;
            if (standing > 15)
            {
                animator.SetBool("is_walking", false);
                animator.SetBool("is_standing", true);
            }
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded == 0)
        {
            animator.SetBool("is_flying", true);
            Jump();
        }

        if (rb.linearVelocity.y < 0)
        {
            float mult = fallMultiplier;
            if (Input.GetButton("Jump")) mult = 0.25f;
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (mult - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!firstCollide)
                isGrounded--;
            firstCollide = false;
            animator.SetBool("is_flying", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded++;
    }
}