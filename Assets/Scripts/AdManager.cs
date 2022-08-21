using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    private RewardedAd rewardedAd;
    private InterstitialAd interstitial;

    private bool isRequested;

    private int n;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    

    private void Start()
    {
        n = PlayerPrefs.GetInt("Count");
        isRequested = false;
        
        CreateAndLoadRewardedAd();
    }

    private void Update()
    {
        if (!isRequested && n > 2)
        {
            RequestInterstitial();
            isRequested = true;

        }

    }
    public void AdCount()
    {
        n += 1;
        PlayerPrefs.SetInt("Count", n);
        if (n > 3)
        {
            if (this.interstitial.IsLoaded())
            {
                this.interstitial.Show();
                isRequested = false;
                n = 0;
                PlayerPrefs.SetInt("Count", n);
            }
            else
            {
                n = 3;
                PlayerPrefs.SetInt("Count", n);
            }

        }
    }
    private void RequestInterstitial()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Clean up interstitial ad before creating a new one.
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Create an empty ad request.

        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);


        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
    }

    public void HandleOnAdLoaded(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    private void CreateAndLoadRewardedAd()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
        string adUnitId = "unexpected_platform";
#endif
        // Create new rewarded ad instance.
        this.rewardedAd = new RewardedAd(adUnitId);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

    }



    public void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
    }

    #region RewardedAd callback handlers

    public void HandleRewardedAdLoaded(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
        CreateAndLoadRewardedAd();

    }

    public void HandleRewardedAdOpening(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: " + args.Message);
        CreateAndLoadRewardedAd();
    }

    public void HandleRewardedAdClosed(object sender, System.EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
        // Create an empty ad request.
        CreateAndLoadRewardedAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
        CreateAndLoadRewardedAd();

        GameManagement.instance.Reward();


    }

    #endregion

}
