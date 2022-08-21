using UnityEngine;

public class Xplatform : MonoBehaviour
{
    public float moveSpeed;

    public Vector2 velocity;

    public float range;

    public bool isXplatform;

    private bool moveRight = false;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (isXplatform)
        {
            if (transform.position.x - startPos.x > range)
                moveRight = false;
            if (transform.position.x - startPos.x < -range)
                moveRight = true;

            if (moveRight)
               transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
            else
                 transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);

        }
        else
        {
            if (transform.position.y - startPos.y > range)
                moveRight = false;
            if (transform.position.y - startPos.y < -range)
                moveRight = true;

            if (moveRight)
                transform.position = new Vector2(transform.position.x , transform.position.y + moveSpeed * Time.deltaTime);
            else
                transform.position = new Vector2(transform.position.x , transform.position.y - moveSpeed * Time.deltaTime);

        }

       

    }
}
