using UnityEngine;

public class FrozenScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    private Rigidbody2D rb;
    private GameObject player;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            string bulletType = collision.gameObject.GetComponent<Bullet>().bulletType;
            switch (bulletType)
            {
                case "Fire":
                    if(enemyPrefab != null)
                    {
                        Instantiate(enemyPrefab, transform.position, transform.rotation);
                        Destroy(gameObject);
                    }
                    return;
                case "Wind":
                    collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    if (bulletType == "Wind")
                    {
                        collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                        if ((player.transform.position.x - transform.position.x) < 0)
                        {
                            rb.velocity = new Vector2(20, 0);
                        }
                        else
                        {
                            rb.velocity = new Vector2(-20, 0);
                        }

                    }
                    return;
                case "Tornado":
                    if (bulletType == "Tornado")
                    {
                        collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                        if ((player.transform.position.x - transform.position.x) < 0)
                        {
                            rb.velocity = new Vector2(-20, 0);
                        }
                        else
                        {
                            rb.velocity = new Vector2(20, 0);
                        }

                    }
                    return;
            }

            
        }
    }
}
