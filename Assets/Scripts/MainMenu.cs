using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Transform toggleMusic;
    private int prefState;


    private void Awake()
    {
        toggleMusic = transform.Find("Music");

        prefState = PlayerPrefs.GetInt("MusicOn");
        
       
    }

    private void Start()
    {
        MusicClass.instance.startFadingIn = true;
        if (prefState == 1)
        {
            toggleMusic.GetComponent<Toggle>().isOn = true;
            MusicClass.instance.gameObject.GetComponent<AudioSource>().mute = true;


        }
        else
        {
            toggleMusic.GetComponent<Toggle>().isOn = false;
            MusicClass.instance.gameObject.GetComponent<AudioSource>().mute = false;


        }



    }

    private void Update()
    {
        if (toggleMusic.GetComponent<Toggle>().isOn == true)
        {

            PlayerPrefs.SetInt("MusicOn", 1);
            MusicClass.instance.gameObject.GetComponent<AudioSource>().mute = true;

        }
        else
        {

            PlayerPrefs.SetInt("MusicOn", 0);
            MusicClass.instance.gameObject.GetComponent<AudioSource>().mute = false;


        }

       
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
