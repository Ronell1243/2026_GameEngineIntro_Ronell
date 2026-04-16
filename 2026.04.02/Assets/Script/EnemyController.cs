using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 1f;

    private Rigidbody2D rb;
    private bool isMovingUp = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (isMovingUp)
            rb.linearVelocity = new Vector2(rb.linearVelocityX, moveSpeed);
        else
            rb.linearVelocity = new Vector2(rb.linearVelocityX, -moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary"))
        {
            isMovingUp = !isMovingUp;

        }
            
    }
}
