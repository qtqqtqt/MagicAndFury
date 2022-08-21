using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private bool show;
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    public float dampTime = 0.15f;
    public float viewRange;
    private float startSize;

    private void Start()
    {
        show = false;
        offset = transform.position - target.transform.position;
        startSize = Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        Vector3 newPos = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z + offset.z);
        transform.parent.position = Vector3.SmoothDamp(transform.parent.position, newPos, ref velocity, dampTime);
    }

    public void LevelView(Transform canvas)
    {
        if (!show)
        {
            show = true;
            //GameManagement.instance.isPaused = true;
            foreach (Transform child in canvas)
            {
                if (!child.CompareTag("Menu"))
                {
                    child.gameObject.SetActive(false);
                }
            }
            Camera.main.orthographicSize = viewRange;
            //StartCoroutine(TimeFreezeDelay());
            
        }
        else
        {
            show = false;
            //GameManagement.instance.isPaused = false;
            foreach (Transform child in canvas)
            {
                if (!child.CompareTag("Menu"))
                {
                    child.gameObject.SetActive(true);

                }
            }
            Camera.main.orthographicSize = startSize;
            //Time.timeScale = 1f;
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-0.5f, 0.5f) * magnitude;
            float y = Random.Range(-0.5f, 0.5f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }

    IEnumerator TimeFreezeDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0f;
    }

}
