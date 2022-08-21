using UnityEngine;

public class CloudPlatform : MonoBehaviour
{

    private Animator anim;

    
    void Start()
    {
        anim = GetComponent<Animator>();
        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Launch");
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * 20f;
        }
    }
    
}
