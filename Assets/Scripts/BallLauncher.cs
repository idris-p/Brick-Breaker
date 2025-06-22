using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallLauncher : MonoBehaviour
{
    public Transform paddle;
    public LineRenderer aim;
    public PlayerMovement playerMovement;
    public float angularVelocity = 90f;
    public float launchSpeed = 5f;

    private Rigidbody2D rb;
    private float currentAngle = 90f;
    private bool launched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        UpdateLine();

    }

    void Update()
    {
        if (launched)
        {
            return;
        }
        // if (Input.GetKey(playerMovement.leftKey) && Input.GetKey(playerMovement.leftKey))
        // {
        //     ;
        // }
        if (Input.GetKey(playerMovement.leftKey))
        {
            currentAngle += angularVelocity * Time.deltaTime;
        }
        if (Input.GetKey(playerMovement.rightKey))
        {
            currentAngle -= angularVelocity * Time.deltaTime;
        }

        currentAngle = Mathf.Clamp(currentAngle, 15f, 165f);
        UpdateLine();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0f;
            rb.linearVelocity = AngleToVector(currentAngle) * launchSpeed;
            aim.enabled = false;
            launched = true;

            playerMovement.canMove = true;
        }
    }

    void UpdateLine()
    {
        Vector3 start = paddle.position + Vector3.up * 0.5f;
        Vector3 direction = AngleToVector(currentAngle);

        aim.SetPosition(0, start);
        aim.SetPosition(1, start + direction * 3f);
    }

    Vector2 AngleToVector(float angle)
    {
        float rad = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;
    }
}
