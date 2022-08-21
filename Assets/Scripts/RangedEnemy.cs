using System.Collections;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject arrowPrefab;
    private Transform target;

    public float secondsBetweenAttacks;

    public Transform firePoint;

    private Animator anim;
    private bool left;

    private void Start()
    {
        left = false;


        InvokeRepeating("Shoot", 0f, secondsBetweenAttacks);
        anim = gameObject.GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if(gameObject.GetComponent<EnemyScript>().alive == false)
        {
            CancelInvoke("Shoot");
        }

        if(transform.position.x < target.position.x)
        {
             if (left)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                    left = !left;
                }
        }else
        {
            if (!left)
            {
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                left = !left;
            }

        }
    }

    private void Shoot()
    {
        anim.SetTrigger("New Trigger");
        StartCoroutine(SpawnBullet());
       
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        if (left)
        {
            arrow.GetComponent<Rigidbody2D>().velocity = -transform.right * 10f;
            arrow.transform.localScale = new Vector3(arrow.transform.localScale.x * -1, arrow.transform.localScale.y, arrow.transform.localScale.z);
        }
        else
        {
            arrow.GetComponent<Rigidbody2D>().velocity = transform.right * 10f;
        }
        
    }
}
