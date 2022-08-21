using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public GameObject wallPrefab;
    public List<Transform> walls;
    public GameObject player;
    public GameObject spawnButton;


    private bool canSpawn;
    private bool triggered = false;
    private GameObject wall_1;
    private Animator anim;
    private Vector3 spawnPos;

    Collider2D collision;

    private void Start()
    {
        canSpawn = true;
        anim = player.GetComponent<Animator>();
    }

    private void Update()
    {
        if(triggered && !collision)
        {
            canSpawn = true;
        }
        
    }
    public void SpawnWall(GameObject prefab)
    {
        if (canSpawn && !GameManagement.instance.isEnded && !PlayerController.inAction)
        {
            anim.SetTrigger("Cast");
            spawnPos = transform.position;
            wall_1 = Instantiate(prefab, spawnPos, Quaternion.identity);
            StartCoroutine(WaitForCast(prefab));
            PlayerController.inAction = true;
            
        }
        else
        {
            Debug.Log("Can't place here");
        }
        
    }

    IEnumerator WaitForCast(GameObject prefab)
    {
        yield return new WaitForSeconds(0.3f);
        wall_1.GetComponent<Animator>().SetTrigger("Spawned");
        walls.Add(wall_1.transform);
        StartCoroutine(DestroyWall(wall_1));
        if (walls.Count > 2)
        {
            if (walls[0] != null)
            {
                walls[0].GetComponent<Animator>().SetTrigger("Destroy");
                Destroy(walls[0].gameObject,0.35f);
                walls.Remove(walls[0]);
            }
                
        }
        if (walls.Count > 1)
        {
            for (int i = 1; i < walls.Count; i++)
            {
                if (walls[i] != null && walls[i - 1] != null)
                {
                    if (Mathf.Abs((walls[i].transform.position - walls[i - 1].transform.position).x) < 0.4f)
                    {
                        walls[i-1].GetComponent<Animator>().SetTrigger("Destroy");
                        Destroy(walls[i - 1].gameObject, 0.35f);
                        walls.Remove(walls[i - 1]);
                    }
                }

            }
        }
        
        PlayerController.inAction = false;

    }

    IEnumerator DestroyWall(GameObject wall)
    {
        yield return new WaitForSeconds(2.65f);
        try
        {    
            walls.Remove(wall.transform);
            wall.GetComponent<Animator>().SetTrigger("Destroy");
            Destroy(wall, 0.35f);
        }
        catch (MissingReferenceException)
        {
            //nothing
        }
        
   
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            triggered = true;
            this.collision = collision;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            canSpawn = true;
        }
    }


}
