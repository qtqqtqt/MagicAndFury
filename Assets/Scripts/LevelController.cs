using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;
   
    private void LoadScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(SceneManager.GetActiveScene().buildIndex != 26)
            {
                GameManagement.instance.WinLevel();
                LoadScene();
            } else
            {
                StartCoroutine(LoadLevel(1));
            }

        }
    }

}
