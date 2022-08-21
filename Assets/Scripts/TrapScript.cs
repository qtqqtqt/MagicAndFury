using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public GameObject arrowPrefab;

    private Transform firePoint;
    private Transform arrowSpawn;
    private Transform trapButton;
    private Animator anim;
    

    void Start()
    {

        arrowSpawn = transform.Find("ArrowSpawner");
        trapButton = transform.Find("TrapButton");
        firePoint = arrowSpawn.Find("FirePoint");
        anim = GetComponent<Animator>();



    }


    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("Pressed", true);
            trapButton.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Level/Traps/TrapButton");
            GameObject arrow = Instantiate(arrowPrefab, firePoint.position, transform.rotation);
            if (gameObject.name == "ArrowTrapLeft")
            {
                arrow.GetComponent<Rigidbody2D>().velocity = transform.right * 10f;
            }else if (gameObject.name == "ArrowTrapRight")
            {
                arrow.GetComponent<Rigidbody2D>().velocity = -transform.right * 10f;
                arrow.transform.Rotate(0, 180, 0);
            }else if(gameObject.name == "ArrowTrapUp")
            {
                arrow.GetComponent<Rigidbody2D>().velocity = Vector2.down * 10f;
                arrow.transform.Rotate(0, 0, 270);
            }
           
        }
        if (collision.CompareTag("WaterMan"))
        {
            trapButton.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Level/Traps/TrapButton");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("Pressed", false);

        }
    }
}
