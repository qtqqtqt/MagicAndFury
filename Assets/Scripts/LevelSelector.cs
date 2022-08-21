using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Animator fader;
    public Button[] levelButtons;

    public float transitionTime = 1f;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }

    }


    public void Select(string levelName)
    {
        StartCoroutine(LoadLevel(levelName));
        MusicClass.instance.startFadingOut = true;
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator LoadLevel(string levelName)
    {
        fader.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelName);
    }

}
