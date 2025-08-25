using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 2f;
    public PowerUpType type;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ApplyPowerUp(collision.gameObject);
            Destroy(gameObject);
        }
    }

    void ApplyPowerUp(GameObject player)
    {
        switch (type)
        {
            case PowerUpType.Long:
                player.transform.localScale = new Vector3(player.transform.localScale.x * 1.5f, player.transform.localScale.y, player.transform.localScale.z);
                break;
        }
    }
}
