using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 2f;
    public PowerUpType type;
    public PlayerMovement playerMovement;

    void Start()
    {
        // Find the PlayerMovement script in the scene
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            ApplyPowerUp(collider.gameObject);
            Destroy(gameObject);
        }
    }

    void ApplyPowerUp(GameObject player)
    {
        switch (type)
        {
            case PowerUpType.Long:
                if (player.transform.localScale.x <= 1.5f)
                {
                    player.transform.localScale = new Vector3(player.transform.localScale.x * 1.5f, player.transform.localScale.y, player.transform.localScale.z);
                }
                playerMovement.boundary = 1.625f;
                break;
            
            case PowerUpType.Laser:
                playerMovement.hasLaser = true;
                break;
            case PowerUpType.Catch:
                playerMovement.canCatch = true;
                break;
        }
    }
}
