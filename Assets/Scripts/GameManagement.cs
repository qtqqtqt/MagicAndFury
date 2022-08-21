using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameManagement : MonoBehaviour
{
    public static GameManagement instance;
    public Text manaTextUI;
    public Button shootButton;
    public Button adButton;
    public bool isPaused;
    public int currentMana;
    public int maxMana;

    [SerializeField]
    private GameObject gameOverUI;
    public bool isEnded;

    public int levelToUnclock;
    private int prevLevel;

    public GameObject toggleMusic;
    private int prefState;

    private static bool giveExtra;
    private int n;
    private RewardedAd rewardedAd;
    private InterstitialAd interstitial;

   


    private void Awake()
    {
        if (instance == null)
        {
            instance = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManagement>();
        }

        
        MusicClass.instance.startFadingIn = true;       
       

        prefState = PlayerPrefs.GetInt("MusicOn");
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

    private void Start()
    {
        currentMana = maxMana;
        manaTextUI.text = "x " + currentMana.ToString();

        isEnded = false;
        levelToUnclock = SceneManager.GetActiveScene().buildIndex;
        prevLevel = PlayerPrefs.GetInt("levelReached");
        giveExtra = false;
        /*
        CreateAndLoadRewardedAd();
        n = PlayerPrefs.GetInt("Count");
        Debug.Log(n);
        if(n > 2)
            RequestInterstitial();
            */
    }

    

    private void Update()
    {
        if(currentMana == 0)
        {
            //currentMana = 0;
            shootButton.interactable = false;
            if(giveExtra == false)
                adButton.gameObject.SetActive(true);
            Image image = shootButton.transform.Find("Image").GetComponent<Image>();
            image.color = Color.grey;
        }

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
    

   
    public void Reward()
    {
        giveExtra = true;
        currentMana = maxMana;
        manaTextUI.text = "x " + currentMana.ToString();
        adButton.gameObject.SetActive(false);
        shootButton.interactable = true;
        Image image = shootButton.transform.Find("Image").GetComponent<Image>();
        image.color = Color.white;
    }


    
    public void UpdateMana()
    {
        currentMana -= 1;
        manaTextUI.text = "x " + currentMana.ToString();
    }

    public void RefillMana()
    {
        AdManager.instance.ShowRewardedAd();
    }

    public void EndGame()
    {
        AdManager.instance.AdCount();
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        if(levelToUnclock > prevLevel)
            PlayerPrefs.SetInt("levelReached", levelToUnclock);
    }
}
