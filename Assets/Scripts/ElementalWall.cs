using UnityEngine;

public class ElementalWall : MonoBehaviour
{
    public float lifetime;
    public string wallType;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (wallType == "Earth")
        {
            if(collision.gameObject.name == "MovingPlatform")
            {
                transform.parent = collision.transform;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }
    }



   


}


