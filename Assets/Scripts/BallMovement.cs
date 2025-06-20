using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class BallMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        float moveX = Random.Range(0, 2) == 0 ? 1f : -1f;
        float moveY = 1f;

        rb.linearVelocity = new Vector2(moveX, moveY).normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 ballPos = transform.position;
            Vector3 paddlePos = collision.transform.position;

            float paddleWidth = collision.collider.bounds.size.x;
            float hitFactor = (ballPos.x - paddlePos.x) / (paddleWidth / 2f);

            Vector2 newDir = new Vector2(hitFactor, rb.linearVelocity.y > 0 ? -1f : 1f).normalized;
            rb.linearVelocity = newDir * speed;
        }
        else
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
        }

        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
        }
        
    }

}
