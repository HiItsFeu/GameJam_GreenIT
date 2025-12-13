using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public float fallMultiplier = 2f;  // pour slow fall
    public float lowJumpMultiplier = 2f; // pour jump court si on rel�che Space
    private int isGrounded = 0;
    private bool firstCollide = true;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // D�placement horizontal
        float horizontal = Input.GetAxisRaw("Horizontal"); // -1,0,1 plus propre que KeyCheck
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded == 0)
        {
            Jump();
        }

        // Better jump (slow fall & jump cut)
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
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
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded++;
    }
}