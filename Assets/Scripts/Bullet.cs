using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 10f;
    public GameObject cloudPrefab;
    public GameObject waterPrefab;
    public string bulletType;
    private float timer;
    private Animator anim;
    private BoxCollider2D bc;
    private bool isDestroyed;

    private void Start()
    {
        bulletType = "None";
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        isDestroyed = false;
        rb.velocity = -transform.right * speed;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.3f && bulletType == "None" && !isDestroyed)
        {
            anim.SetTrigger("DefaultHit");
            Destroy(gameObject, 0.4f);
            isDestroyed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (!collision.CompareTag("EnemySword") && !collision.CompareTag("AgroLine") && !collision.CompareTag("EnemySword") && !collision.CompareTag("Spawn") && !collision.CompareTag("Wall") && !collision.CompareTag("Exit"))
        {
            BulletAnimation();
        }
        if (collision.CompareTag("Wall"))
        {
            string wallType = collision.gameObject.GetComponent<ElementalWall>().wallType;
            if (wallType == "Earth")
            {
                BulletAnimation();
            }
            if (wallType == "Wind")
            {
                switch (bulletType)
                {
                    case "None":
                        anim.SetTrigger("Wind");
                        bulletType = wallType;
                        return;
                    case "Fire":
                        bulletType = "Tornado";
                        anim.SetTrigger("Tornado");
                        return;
                    case "Ice":
                        anim.SetTrigger("Water");
                        rb.gravityScale = 2;
                        bulletType = "Water";
                        return;
                }
            }
            if (wallType == "Fire")
            {
                switch (bulletType)
                {
                    case "None":
                        anim.SetTrigger("Fire");
                        bulletType = wallType;
                        return;
                    case "Ice":
                        bulletType = "Cloud";
                        anim.SetTrigger("Cloud");
                        StartCoroutine(SpawnCloud(cloudPrefab));
                        rb.gravityScale = 2;
                        return;
                    case "Wind":
                        bulletType = "Tornado";
                        anim.SetTrigger("Tornado");
                        return;
                }
            }
            if (wallType == "Ice")
            {
                switch (bulletType)
                {
                    case "None":
                        bulletType = "Ice";
                        anim.SetTrigger("Ice");
                        return;
                    case "Fire":
                        bulletType = "Cloud";
                        anim.SetTrigger("Cloud");
                        rb.gravityScale = 2;
                        StartCoroutine(SpawnCloud(cloudPrefab));
                        return;
                    case "Wind":
                        anim.SetTrigger("Water");
                        rb.gravityScale = 2;
                        bulletType = "Water";
                        return;
                }
            }
        }
        if (collision.CompareTag("Platform") && bulletType == "Water")
        {
            Instantiate(waterPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    public void BulletAnimation()
    {
        
        switch (bulletType)
        {
            case "None":
                rb.velocity = Vector2.zero;
                anim.SetTrigger("DefaultHit");
                Destroy(gameObject, 0.4f);
                return;
            case "Fire":
                rb.velocity = Vector2.zero;
                Destroy(bc);
                anim.SetTrigger("FireHit");
                Destroy(gameObject, 0.4f);
                return;
            case "Wind":
                anim.SetTrigger("WindHit");
                rb.velocity = new Vector2(2f,0f);
                Destroy(bc,0.6f);
                Destroy(gameObject, 0.6f);
                return;
            case "Ice":
                rb.velocity = Vector2.zero;
                anim.SetTrigger("IceHit");
                Destroy(bc);
                Destroy(gameObject, 0.2f);
                
                return;
            case "Tornado":
                rb.velocity = Vector2.zero;
                anim.SetTrigger("TornadoHit");
                Destroy(gameObject, 0.3f);
                return;
        }
        
        
        
    }

    IEnumerator SpawnCloud(GameObject cloudPrefab)
    {
        yield return new WaitForSeconds(0.17f);
        Instantiate(cloudPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(!collision.gameObject.CompareTag("EnemySword"))
            Destroy(gameObject);
    }
}
