using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float boundary = 5f;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public bool canMove = false;

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
    }
}
