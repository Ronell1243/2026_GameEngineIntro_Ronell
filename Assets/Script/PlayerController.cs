using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject GameOverPanel;

    private Rigidbody2D rb;
    private Animator pAni;
    private bool isGrounded;
    private float moveInput;

    private bool isInvincible = false;
    private float originalSpeed;
    private float originalJump;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pAni = GetComponent<Animator>();

        originalSpeed = moveSpeed;
        originalJump = jumpForce;
    }


    private void Update()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            pAni.SetBool("Walk", true);
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            pAni.SetBool("Walk", true);
        }
        else
        {
            pAni.SetBool("Walk", false);
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    private void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        moveInput = input.x;
    }

    public void OnJump(InputValue Value)
    {
        if (Value.isPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            pAni.SetTrigger("Jump");
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            ShowGameOverPanel();
        }

        if (collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }
        
        if (collision.CompareTag("Enemy"))
        {
            if (isInvincible)
            {
                Debug.Log("무적 상태입니다.");
            }
            else
            {
                ShowGameOverPanel();
            }
        }

        if (collision.CompareTag("Item"))
        {
            isInvincible = true;
            Invoke(nameof(ResetInvincible), 5f);
            Destroy(collision.gameObject);
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        }

        if (collision.CompareTag("Item_Speed"))
        {
            moveSpeed = originalSpeed * 2f;
            Invoke(nameof(ResetSpeed), 3f);
            Destroy(collision.gameObject);
            GetComponent<SpriteRenderer>().color = Color.cyan;
        }

        if (collision.CompareTag("Item_Jump"))
        {
            jumpForce = originalJump * 1.5f;
            Invoke(nameof(ResetJump), 5f);
            Destroy(collision.gameObject);
            GetComponent<SpriteRenderer>().color = Color.green;
        }
    }

    void ResetInvincible()
    {
        isInvincible = false;
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }

    void ResetSpeed()
    {
        moveSpeed = originalSpeed;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void ResetJump()
    {
        jumpForce = originalJump;
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void ShowGameOverPanel()
    {
        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(true);
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0;
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void OnClickRespawnButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
   
