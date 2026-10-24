using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public Rigidbody2D rb;
    bool isFacingRight = true;
    public Animator animator;
    private SpriteRenderer sr;

    [Header("Health")]
    public int health = 100;
    
    [Header("Move")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public Transform groundCheckPosition;
    public Vector2 groundCheckRadius = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    public UIDocument uiDoc;
    private Button restartButton;
    private Label endText;

    float HorizontalMovement;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        restartButton = uiDoc.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += Restart;

        endText = uiDoc.rootVisualElement.Q<Label>("EndText");
        endText.style.display = DisplayStyle.None;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(HorizontalMovement * moveSpeed, rb.linearVelocity.y);

        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetFloat("magnitude", rb.linearVelocity.magnitude);

        Flip();
    }

    public void Move(InputAction.CallbackContext context)
    {
        HorizontalMovement = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded())
        {
            if(context.performed)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                animator.SetTrigger("jump");
            } else if(context.canceled)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                animator.SetTrigger("jump");
            }
        }
    }

    private bool isGrounded()
    {
        if(Physics2D.OverlapBox(groundCheckPosition.position, groundCheckRadius, 0, groundLayer))
        {
            return true;
        }
        return false;
    }

    private void Flip()
    {
        if(isFacingRight && HorizontalMovement < 0 || !isFacingRight && HorizontalMovement > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPosition.position, groundCheckRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Damage")
        {
            health -= 10;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            StartCoroutine(Flash());

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator Flash()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    private void Die()
    {
        Destroy(gameObject);
        restartButton.style.display = DisplayStyle.Flex;
        endText.style.display = DisplayStyle.Flex;
        endText.text = "Game over!";
    }

    private void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("kai tech demo");
    }
}
