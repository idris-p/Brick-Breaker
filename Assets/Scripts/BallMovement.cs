using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallMovement : MonoBehaviour
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
    }

    void Update()
    {
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f && ballLauncher.launched)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, speed);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerMovement.canCatch == true)
        {
            playerMovement.canMove = false;
            CatchBall();
        }
        else if (collision.gameObject.CompareTag("Player"))
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
            rb.linearVelocity = Vector2.zero;
            sr.enabled = false;
            StartCoroutine(ResetBall());
        }
        else
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }

    }

    public void CatchBall()
    {
        rb.linearVelocity = Vector2.zero;
        ballLauncher.launched = false;
        ballLauncher.aim.enabled = true;
        ballLauncher.currentAngle = 90f;
        ballLauncher.UpdateLine();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private IEnumerator ResetBall()
    {
        yield return new WaitForSeconds(respawn);
        playerMovement.ResetPosition();
        transform.position = new Vector3(0f, -3.6f, 0f);
        sr.enabled = true;
        CatchBall();
    }

}
