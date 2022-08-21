using UnityEngine;



public class MusicClass : MonoBehaviour
{

    public static MusicClass instance = null;

    public bool startFadingOut;

    public bool startFadingIn;

    private bool canStart;

    private bool isSwitched;


    private void Awake()
    {
        if (instance != null )
        {
            Destroy(gameObject);
           
        }else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }

        startFadingOut = false;
        startFadingIn = false;
        canStart = false;
        isSwitched = false;


    }

    private void Update()
    {
        if (startFadingOut && !canStart)
        {
            if(GetComponent<AudioSource>().volume > 0)
            {
                GetComponent<AudioSource>().volume -= 0.35f * Time.deltaTime;
            }else
            {
                GetComponent<AudioSource>().volume = 0;
                canStart = true;
                startFadingOut = false;
            }
           
                
        }

        if (startFadingIn && canStart)
        {
            if (!isSwitched)
            {
                if (GetComponent<AudioSource>().clip.name == "Menu")
                {
                    GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/bayaz");
                }else
                {
                    GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sounds/Menu");
                }
                GetComponent<AudioSource>().Play();
                isSwitched = true;
            }
            

            if(GetComponent<AudioSource>().volume < 0.7f)
            {
                GetComponent<AudioSource>().volume += 0.25f * Time.deltaTime;
            } else
            {
                GetComponent<AudioSource>().volume = 0.7f;
                canStart = false;
                startFadingIn = false;
                isSwitched = false;
            }
           

        }
    }



}
