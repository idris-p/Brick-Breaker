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

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateColour();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            TakeDamage();
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
