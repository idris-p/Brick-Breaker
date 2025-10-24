using UnityEngine;

public class MultiBall : MonoBehaviour
{
    public float speed = 5f;
    public float respawn = 2f;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public BallLauncher ballLauncher;
    public PlayerMovement playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ballLauncher = FindFirstObjectByType<BallLauncher>();
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f && ballLauncher.launched)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
        }
        if (!ballLauncher.launched)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 ballPos = transform.position;
            Vector3 paddlePos = collision.transform.position;

            float paddleWidth = collision.collider.bounds.size.x;
            float hitFactor = (ballPos.x - paddlePos.x) / (paddleWidth / 2f);

            Vector2 newDir = new Vector2(hitFactor, 1f).normalized;
            rb.linearVelocity = newDir * speed;
        }
        else if (collision.gameObject.CompareTag("Out"))
        {
            Destroy(gameObject);
        }
        else
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }

    }

}
