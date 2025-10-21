using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        if (transform.position.y > 6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Brick"))
        {
            Brick brick = collider.GetComponent<Brick>();
            brick.TakeDamage();
            Destroy(gameObject);
        }
    }
}