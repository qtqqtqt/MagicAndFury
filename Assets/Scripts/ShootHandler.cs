using System.Collections;
using UnityEngine;

public class ShootHandler : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject player;

    private Animator anim;

    private void Start()
    {
        anim = player.GetComponent<Animator>();
    }

    public void Shoot()
    {
        if (!GameManagement.instance.isEnded)
        {
            anim.SetTrigger("Shoot");
            GameManagement.instance.UpdateMana();
            StartCoroutine(WaitToShoot());
        }

    }

    IEnumerator WaitToShoot()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
