using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject icePrefab;
    public bool alive;
    private Animator anim;
    private Rigidbody2D rb;
    private GameObject player;
    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        alive = true;
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            if (collision.transform.localScale.x < 0 && transform.localScale.x > 0 || collision.transform.localScale.x > 0 && transform.localScale.x < 0)
            {
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                rb.isKinematic = true;

            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            rb.isKinematic = false;
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
                    anim.SetInteger("State", 1);
                    gameObject.tag = "Untagged";
                    foreach (Transform child in gameObject.transform)
                    {
                        if (child.CompareTag("AgroLine"))
                        {
                            child.GetComponent<EnemyFollow>().enabled = false;
                        }
                    }
                    Destroy(gameObject, 0.6f);
                    alive = false;
                    return;
                case "Ice": 
                    GameObject ice = Instantiate(icePrefab, transform.position, transform.rotation);
                    if(transform.localScale.x < 0)
                    {
                        ice.transform.localScale = new Vector3(ice.transform.localScale.x * -1, ice.transform.localScale.y, ice.transform.localScale.z);
                    }
                    
                    Destroy(gameObject);
                    alive = false;
                    return;
                case "Wind":
                    foreach (Transform child in gameObject.transform)
                    {
                        if (child.CompareTag("AgroLine"))
                        {
                            child.GetComponent<EnemyFollow>().enabled = false;
                        }
                    }
                    collision.gameObject.GetComponent<Rigidbody2D>().mass = 30;
                    collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    StartCoroutine(EnablePatrol());
                    return;
                case "Tornado":
                    foreach (Transform child in gameObject.transform)
                    {
                        if (child.CompareTag("AgroLine"))
                        {
                            child.GetComponent<EnemyFollow>().enabled = false;
                        }
                    }
                    collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                    rb.velocity = Vector3.zero;
                    if ((player.transform.position.x - transform.position.x) < 0)
                    {
                        rb.velocity = new Vector2(-10, 0);
                    }
                    else
                    {
                        rb.velocity = new Vector2(10, 0);
                    }
                    StartCoroutine(EnablePatrol());
                    return;
                
            }
            
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name== "TilemapDeath"|| collision.gameObject.name == "Death_Platform")
        {
            anim.SetInteger("State", 1);
            foreach (Transform child in gameObject.transform)
            {
                if (child.CompareTag("AgroLine"))
                {
                    child.GetComponent<EnemyFollow>().enabled = false;
                }
            }
            Destroy(gameObject, 0.6f);
            alive = false;
        }
    }
    

   
    IEnumerator EnablePatrol()
    {
        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector3.zero;

        foreach (Transform child in gameObject.transform)
        {
            if (child.CompareTag("AgroLine"))
            {
                child.GetComponent<EnemyFollow>().enabled = true;
            }
        }
    }


}
