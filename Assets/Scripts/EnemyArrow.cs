using UnityEngine;

public class EnemyArrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D bc;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            if (collision.gameObject.GetComponent<ElementalWall>().wallType == "Earth")
            {
                anim.SetTrigger("Destroy");
                Destroy(bc);
                rb.velocity = Vector2.zero;
                Destroy(gameObject, 0.2f);

            }


        }
        else if (collision.CompareTag("Platform") || collision.CompareTag("Movable") || collision.gameObject.name == "TilemapDeath")
        {
            anim.SetTrigger("Destroy");
            Destroy(bc);
            rb.velocity = Vector2.zero;
            Destroy(gameObject, 0.2f);
        }
        else if (collision.CompareTag("Bullet"))
        {
            if (collision.gameObject.GetComponent<Bullet>().bulletType == "Fire")
            {
                anim.SetTrigger("Destroy");
                Destroy(bc);
                rb.velocity = Vector2.zero;
                Destroy(gameObject, 0.2f);

            }

        }

    }
}
