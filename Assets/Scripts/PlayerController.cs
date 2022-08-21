using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed = 10f;
    public static bool inAction;
    private ButtonController JumpButton;
    public ButtonController LeftButton;
    public ButtonController RightButton;
    public CameraFollow cameraMain;

    float horizontalMove = 0f;

    [SerializeField]
    private LayerMask platformsLayerMask;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;
    private Transform sc;
    private Transform viewMenu;
    private Transform viewButton;
    private BoxCollider2D scCollider;
    public List<Transform> buttons;
    

    private bool FacingRight = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        inAction = false;

        GameObject canvas = GameObject.FindGameObjectWithTag("MainCanvas");
        JumpButton = canvas.transform.Find("Jump").GetComponent<ButtonController>();
        viewMenu = canvas.transform.Find("LevelView");
        viewButton = viewMenu.Find("Button");
        sc = transform.Find("SpawnController");
        scCollider = sc.gameObject.GetComponent<BoxCollider2D>();
        foreach (Transform child in canvas.transform)
        {
            if (child.gameObject.CompareTag("SkillButton"))
            {
                buttons.Add(child);

            }
                

        }
    }

    private void StopRun()
    {
        anim.SetBool("StateBool", false);
        viewButton.GetComponent<Button>().interactable = true;


    }


    private void Update()
    {
        if (!GameManagement.instance.isEnded)
        {
            horizontalMove = 0;
            if (RightButton.IsPressed || Input.GetKey(KeyCode.RightArrow))
            {

                anim.SetBool("StateBool", true);
                if (!FacingRight)
                    Flip();
                horizontalMove = 0.2f;
                viewButton.GetComponent<Button>().interactable = false;
            }else if (LeftButton.IsPressed || Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("StateBool", true);
             
                if (FacingRight)
                    Flip();
                horizontalMove = -0.2f;
                viewButton.GetComponent<Button>().interactable = false;
            }
            else
            {
                StopRun();
            }
            



            rb.velocity = new Vector3(horizontalMove * PlayerSpeed, rb.velocity.y);
            
        }

        if (Input.GetKeyDown(KeyCode.Space)||JumpButton.IsPressed)
        {
            if (isGrounded())
            {
                float jumpVelocity = 13f;
                rb.velocity = Vector2.up * jumpVelocity;

            }
        }

        if (isPlatform() && !isInPlatform())
        {
            foreach (Transform button in buttons)
            {
                Transform image = button.Find("Image");
                if (!image.GetComponent<SkillCooldowns>().startCD)
                {
                    image.GetComponent<Image>().color = Color.white;
                    button.GetComponent<Button>().interactable = true;
                } 
            }
                   
        }
        else if (isInPlatform())
        {
            foreach (Transform button in buttons)
            {
                Transform image = button.Find("Image");
                image.GetComponent<Image>().color = Color.gray;
                button.GetComponent<Button>().interactable = false;
            }
        }else
        {
            foreach (Transform button in buttons)
            {
                Transform image = button.Find("Image");
                image.GetComponent<Image>().color = Color.gray;
                button.GetComponent<Button>().interactable = false;
            }
        }

        

        
    }
   

    IEnumerator Endgame()
    {
        yield return new WaitForSeconds(0.6f);
        GameManagement.instance.EndGame();
    }
   
    private bool isGrounded()
    {
        try
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, Vector2.down, 1.3f, platformsLayerMask);
            return raycastHit2D.collider != null;
        }
        catch(MissingReferenceException)
        {
            return false;
        }
       
    }
    private bool isPlatform()
    {
        RaycastHit2D hit = Physics2D.Raycast(sc.position, Vector3.down, 1.5f, platformsLayerMask);
        return hit.collider != null;

    }
    private bool isInPlatform()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(scCollider.bounds.center, scCollider.bounds.size, 0f, Vector2.down, 0.01f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemySword") || collision.gameObject.CompareTag("Enemy") && !GameManagement.instance.isEnded)
        {
            GameManagement.instance.isEnded = true;
            StartCoroutine(cameraMain.Shake(.2f, .4f));
            Destroy(rb);
            Destroy(bc);
            bc.isTrigger = true;
            

            anim.SetInteger("State", 5);
            foreach (Transform child in collision.transform)
            {
                if (child.CompareTag("AgroLine"))
                {
                    child.gameObject.GetComponent<EnemyFollow>().canMove = false;
                }
            }

            StartCoroutine(Endgame());
        }
        if (collision.gameObject.name == "MovingPlatform")
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
        if (collision.gameObject.CompareTag("EnemyArrow") || (collision.gameObject.CompareTag("EnemyStar")))
        {
            GameManagement.instance.isEnded = true;
            StartCoroutine(cameraMain.Shake(.2f, .4f));
            Destroy(rb);
            Destroy(bc);
           
            anim.SetInteger("State", 5);
            foreach (Transform child in collision.transform)
            {
                if (child.CompareTag("AgroLine"))
                {
                    child.gameObject.GetComponent<EnemyFollow>().canMove = false;
                }
            }

            StartCoroutine(Endgame());


        }


    }
    

    private void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
