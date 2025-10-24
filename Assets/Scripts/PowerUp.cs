using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public float speed = 2f;
    public PowerUpType type;
    public PlayerMovement playerMovement;
    public GameObject multiBallPrefab;
    public BallMovement BallMovement;

    void Start()
    {
        // Find the PlayerMovement script in the scene
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        BallMovement = FindFirstObjectByType<BallMovement>();
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
            case PowerUpType.Flip:
                playerMovement.isFlipped = true;
                playerMovement.flipControls();
                break;
            case PowerUpType.Multi:
                SpawnMultiBall();
                break;
            case PowerUpType.Bomb:
                BallMovement.SetBomb();
                break;

        }
    }

    void SpawnMultiBall()
    {
        GameObject ball1 = Instantiate(multiBallPrefab, BallMovement.transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity);
        ball1.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-1f, 1f).normalized * BallMovement.speed * 0.8f;
        GameObject ball2 = Instantiate(multiBallPrefab, BallMovement.transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity);
        ball2.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(1f, 1f).normalized * BallMovement.speed * 0.8f;
        GameObject ball3 = Instantiate(multiBallPrefab, BallMovement.transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity);
        ball3.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0f, -1f).normalized * BallMovement.speed * 0.8f;
    }
}
