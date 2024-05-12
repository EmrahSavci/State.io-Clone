
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UnityAds : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static UnityAds Instante;

    //REWARD ADS
    [SerializeField] string _androidAdUnitIdReward = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitIdReward = "Rewarded_iOS";
    string _adUnitIdReward = null; // This will remain null for unsupported platforms

    // INSTERTITIAL
    [SerializeField] string _androidAdUnitInstertitial = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitIdInstertitial = "Interstitial_iOS";
    string _adUnitIdInstertitial;

    [SerializeField] Image[] AdsButtons;
    [SerializeField] Sprite YellowSprite;
    [SerializeField] Sprite GreySprite;

    void Awake()
    {
        Instante = this;
        // Get the Ad Unit ID for the current platform:


        RewardAdsAdUnit();
        InstertitialAdsAdUnit();


        // BannerAdsAdUnit();
    }
    bool IsWatchCoinAds = false;
    void Update()
    {

        if (IsCompleteRewardAds)
        {
            IsCompleteRewardAds = false;
            if (RewardAdsBtnIndex == 0)
            {
                OfflineEarning.Instante.PassiveOfflinePanel();
            }
            else if (RewardAdsBtnIndex == 1)
            {
                UIManager.Instante.ShowStartUnitsAds();
            }
            else if (RewardAdsBtnIndex == 2)
            {
                UIManager.Instante.ShowPoduceSpeedAds();
            }
            else if (RewardAdsBtnIndex == 3)
            {
                UIManager.Instante.ShowOfflineAds();
            }
            else if (RewardAdsBtnIndex == 4)
            {
                IsWatchCoinAds = true;
                //UIManager.Instante.RewardEarnCoin();
            }
            else if (RewardAdsBtnIndex == 5)
            {
                GiftBox.Instante.IsGiftGet = true;

            }
            else if (RewardAdsBtnIndex == 6)
            {
                GameFinishAnim.Instante.AttackToRivalPlayers();
            }
            else if (RewardAdsBtnIndex == 7)
            {
                Shop.Instante.RewardPlayerIcon();
            }
            else if (RewardAdsBtnIndex == 8)
            {
                Shop.Instante.RewardSoldierIcon();
            }
            else if (RewardAdsBtnIndex == 9)
            {
                Shop.Instante.RewardColor();
            }
        }
        if (IsWatchCoinAds)
        {
            IsWatchCoinAds = false;
            PlayerPrefs.SetInt(GameData.Instante.TotalCoin, (UIManager.Instante.TotalCoinAmount + (UIManager.Instante.LevelEarnCoinAmount * 3)));
            PlayerPrefs.SetInt(GameData.Instante.LevelEarningCoin, (UIManager.Instante.LevelEarnCoinAmount + 160));
            PlayerPrefs.SetInt("IsGameStart", 0);
            PlayerPrefs.SetInt(GameData.Instante.AllLevelsIndex, (UIManager.Instante.LevelIndex + 1));
            PlayerPrefs.SetInt(GameData.Instante.MapIndex, (GameManager.Instante.MapIndex + 1));

            if (PlayerPrefs.GetInt(GameData.Instante.MapIndex) >= GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers.Count)
            {
                UIManager.Instante.GiftPanel.SetActive(true);
                UIManager.Instante.YouWon.SetActive(false);

            }
            else
            {

                LeanTween.cancelAll();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    #region REWARD ADS
    void RewardAdsAdUnit()
    {
#if UNITY_IOS
        _adUnitIdReward = _iOSAdUnitIdReward;
#elif UNITY_ANDROID
        _adUnitIdReward = _androidAdUnitIdReward;
#endif
    }
    // Load content to the Ad Unit:
    public void LoadAdReward()
    {

        Debug.Log("Loading Ad: " + _adUnitIdReward);
        Advertisement.Load(_adUnitIdReward, this);

    }
    //public void OnUnityAdsReady(string placementId,UnityAdsLoadError unityAdsLoadError,string message)
    //{
    //    // If the ready Placement is rewarded, activate the button: 
    //    if (placementId.Equals(_adUnitIdReward))
    //    {
    //        for (int i = 0; i < AdsButtons.Length; i++)
    //        {
    //            AdsButtons[i].sprite = YellowSprite;
    //            AdsButtons[i].GetComponent<Button>().enabled = true;
    //        }

    //    }
    //}
    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitIdReward))
        {
            for (int i = 0; i < AdsButtons.Length; i++)
            {
                AdsButtons[i].sprite = YellowSprite;
                AdsButtons[i].GetComponent<Button>().enabled = true;
            }

        }
    }

    // Implement a method to execute when the user clicks the button:
    public int RewardAdsBtnIndex = 0;
    public void ShowAdReward(int btnIndex)
    {
        // Disable the button:
        RewardAdsBtnIndex = btnIndex;
        IsInstantitialAds = false;
        // GameFinishAnim.Instante.AttackToRivalPlayers();
        //GiftBox.Instante.IsGiftGet = true;
        // Then show the ad:
        Advertisement.Show(_adUnitIdReward, this);
    }
    bool IsCompleteRewardAds = false;
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED) && !IsInstantitialAds)
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            IsCompleteRewardAds = true;



        }
        if (IsGameFail && IsInstantitialAds)
        {
            IsInstantitialAds = false;
            IsGameFail = false;

            UIManager.Instante.GameFinishAds();
        }
        Advertisement.Load(_adUnitIdInstertitial, this);
        Advertisement.Load(_adUnitIdReward, this);
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");

        if (adUnitId.Equals(_adUnitIdReward))
        {
            Debug.Log("reklam hazýr ddeðil");
            for (int i = 0; i < AdsButtons.Length; i++)
            {
                AdsButtons[i].sprite = GreySprite;
                AdsButtons[i].GetComponent<Button>().enabled = false;
            }
        }

    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        //if (IsGameFail)
        //{

        //    UIManager.Instante.GameFinishAds();
        //}

    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:

    }
    #endregion
    #region INSTERTITIAL ADS
    void InstertitialAdsAdUnit()
    {
#if UNITY_IOS
        _adUnitIdInstertitial = _iOsAdUnitIdInstertitial;
#elif UNITY_ANDROID
        _adUnitIdInstertitial = _androidAdUnitInstertitial;
#endif
    }
    public void LoadAdInstertitial()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitIdInstertitial);
        Advertisement.Load(_adUnitIdInstertitial, this);
        //UIManager.Instante.IsReadyAds = false;
    }



    // Show the loaded content in the Ad Unit:
    bool IsGameFail = false;
    bool IsInstantitialAds = false;
    public void ShowAdInstertitial(bool IsFalse)
    {
        IsInstantitialAds = true;
        IsGameFail = IsFalse;

        if (PlayerPrefs.GetInt(GameData.Instante.RewardAds) == 0)
        {
            Debug.Log("Showing Ad: " + _adUnitIdInstertitial);


            Advertisement.Show(_adUnitIdInstertitial, this);
        }
        else if (PlayerPrefs.GetInt(GameData.Instante.RewardAds) == 1 && IsFalse)
            UIManager.Instante.GameFinishAds();
    }

    #endregion

    #region BANNER ADS

    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    [SerializeField] string _androidAdUnitIdBanner = "Banner_Android";
    [SerializeField] string _iOSAdUnitIdBanner = "Banner_iOS";
    string _adUnitIdBanner = null; // This will remain null for unsupported platforms.

    void BannerAdsAdUnit()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitIdBanner = _iOSAdUnitIdBanner;
#elif UNITY_ANDROID
        _adUnitIdBanner = _androidAdUnitIdBanner;
#endif



        // Set the banner position:
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);


    }

    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        // Advertisement.Banner.SetPosition(BannerPosition.CENTER);
        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitIdBanner, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
        // Configure the Show Banner button to call the ShowBannerAd() method when clicked:

    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitIdBanner, options);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }


    #endregion

    public void OnPurchaseComplete(Product product)
    {
        PlayerPrefs.SetInt(GameData.Instante.RewardAds, 1);
    }
}
