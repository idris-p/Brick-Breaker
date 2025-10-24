using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float boundary = 2f;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public bool canMove = false;
    public bool hasLaser = false;
    public bool canCatch = false;
    public bool isFlipped = false;
    public GameObject laserPrefab;

    // Update is called once per frame
    void Update()
    {
        float move = 0f;
        if (canMove)
        {
            if (Input.GetKey(leftKey) && Input.GetKey(rightKey))
            {
                move = 0f;
            }
            else if (Input.GetKey(leftKey))
            {
                move = -1f;
            }
            else if (Input.GetKey(rightKey))
            {
                move = 1f;
            }

            transform.Translate(Vector3.right * move * speed * Time.deltaTime);

            Vector3 position = transform.position;
            position.x = Mathf.Clamp(position.x, -boundary, boundary);
            transform.position = position;
        }

        if (hasLaser)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(laserPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            }
        }
    }

    public void flipControls()
    {
        KeyCode temp = leftKey;
        leftKey = rightKey;
        rightKey = temp;
    }

    public void ResetPosition()
    {
        canMove = false;
        transform.position = new Vector3(0f, -4f, 0f);
        if (transform.localScale.x > 1.5f)
        {
            transform.localScale = new Vector3(transform.localScale.x / 1.5f, transform.localScale.y, transform.localScale.z);
        }
        boundary = 2f;
        hasLaser = false;
        canCatch = false;
        if (isFlipped)
        {
            isFlipped = false;
            flipControls();
        }
        
    }
}
