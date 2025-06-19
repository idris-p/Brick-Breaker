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
        if (collision.gameObject.tag == "Brick")
        {
            Destroy(collision.gameObject);
        }
        rb.linearVelocity = SnapTo45(rb.linearVelocity.normalized) * speed;
    }

    private Vector2 SnapTo45(Vector2 dir)
    {
        float x = dir.x >= 0 ? 1f : -1f;
        float y = dir.y >= 0 ? 1f : -1f;
        return new Vector2(x, y).normalized;
    }

}
