using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField]
    private LayerMask platformsLayerMask;
    private GameObject target;
    public Transform startPoint;
    public float speed = 10f;
    private bool left;
    private BoxCollider2D bc;

    public bool canMove;

    Vector2 dir = -Vector2.right;

    private void Awake()
    {
        canMove = false;
        left = false;
        bc = enemy.GetComponent<BoxCollider2D>();

        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (canMove && !GameManagement.instance.isEnded )
        {
            
            Vector3 actualTarget = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, actualTarget, speed * Time.deltaTime);

            if(actualTarget.x > enemy.transform.position.x)
            {
                if (!left)
                {
                    enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);
                    left = !left;
                }


            }
            else
            {
                if (left)
                {
                    enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);
                    left = !left;
                }
            }

        }

        else if(!canMove  || GameManagement.instance.isEnded)
        {
            Patrol();
           
        }


    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size,0f, Vector2.down, 0.2f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }
    private bool isPlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 1.5f, platformsLayerMask);
        return hit.collider != null;

    }
    private bool isInPlatform()
    {
        if (left)
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, Vector2.right, 1.5f, platformsLayerMask);
            return raycastHit2D.collider != null;
        }else
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(bc.bounds.center, bc.bounds.size, 0f, -Vector2.right, 1.5f, platformsLayerMask);
            return raycastHit2D.collider != null;
        }
       
        
    }




    private void Patrol()
    {
        if (left)
        {
            enemy.transform.Translate(-dir * Time.deltaTime * 5f);
        }
        else
        {
            enemy.transform.Translate(dir * Time.deltaTime * 5f);
        }
       
           

        if (!isPlatform() && isGrounded() || isInPlatform())
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);
            left = !left;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = true;
          
        }
        

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canMove = false;

        }
        
    }
}
