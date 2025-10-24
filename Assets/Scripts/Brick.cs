using UnityEngine;

public class Brick : MonoBehaviour
{
    [Range(1, 4)]
    public int durability = 1;
    public PowerUpType powerUpType = PowerUpType.None;
    public GameObject powerUpPrefab;
    public Color[] durabilityColours = new Color[4]
    {
        new Color(124f/255f, 0f, 0f),
        new Color(154f/255f, 0f, 0f),
        new Color(184f/255f, 0f, 0f),
        new Color(214f/255f, 0f, 0f)
    };

    private SpriteRenderer sr;
    public BallMovement ballMovement;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColour();
        ballMovement = FindFirstObjectByType<BallMovement>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (ballMovement.bomb)
            {
                Explode();
            }
            TakeDamage();
        }
    }

    private void Explode()
    {
        durability = 1;
        ballMovement.bomb = false;
        float explosionRadius = 0.5f;
        Collider2D[] hitBricks = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hitBricks)
        {
            if (hit.CompareTag("Brick") && hit.gameObject != gameObject)
            {
                Brick brick = hit.GetComponent<Brick>();
                if (brick != null)
                {
                    brick.TakeDamage();
                }
            }
        }
    }

    public void TakeDamage()
    {
        durability--;
        if (durability == 0)
        {
            Destroy(gameObject);
            if (powerUpType != PowerUpType.None)
            {
                SpawnPowerUp();
            }
        }
        UpdateColour();
    }

    void UpdateColour()
    {
        int index = Mathf.Clamp(durability - 1, 0, durabilityColours.Length - 1);
        sr.color = durabilityColours[index];
    }

    void SpawnPowerUp()
    {
        GameObject powerUp = Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
        powerUp.transform.rotation = Quaternion.Euler(0, 0, 90f);
        powerUp.GetComponent<PowerUp>().type = powerUpType;
    }
}
