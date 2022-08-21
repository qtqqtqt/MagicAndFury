using UnityEngine;

public class WaterObject : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;
    private BoxCollider2D bc;
    [SerializeField]
    private LayerMask platformsLayerMask;
    private bool left;
    private bool isDestroyed;

    public float speed = 3f;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if(player.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            rb.velocity = -transform.right * speed;
            left = true;
        }
        else
        {
            rb.velocity = transform.right * speed;
            left = false;
        }
        isDestroyed = false;

    }

    private string isInPlatform()
    {
        if (left)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, -transform.right, 0.5f, platformsLayerMask);
            if (raycastHit2D.collider != null)
            {
                return raycastHit2D.collider.tag;
            }
                
            else
                return null;
        }
        else
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, transform.right, 0.5f, platformsLayerMask);
            if (raycastHit2D.collider != null)
            {
                return raycastHit2D.collider.tag;
            }
               
            else
                return null;
        }


    }

    private void Update()
    {
        if (isInPlatform() != null && !isDestroyed)
        {
            anim.SetTrigger("Destroy");
            Destroy(gameObject, 0.4f);
            isDestroyed = true;
        }
        if (isGrounded())
        {
            if (left)
            {
                rb.velocity = -transform.right * speed;
            }
            else
            { 
                rb.velocity = transform.right * speed;
            }
        }
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.down, 0.5f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("EnemySword") && !collision.gameObject.CompareTag("AgroLine") && !collision.gameObject.CompareTag("EnemySword") && !collision.gameObject.CompareTag("Spawn") && !collision.gameObject.CompareTag("Platform"))
        {
            if (!isDestroyed)
            {
                anim.SetTrigger("Destroy");
                Destroy(gameObject, 0.4f);
                isDestroyed = true;
            }
        }
            
    }
}
