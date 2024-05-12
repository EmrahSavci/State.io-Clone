
//using UnityEngine;
//using GoogleMobileAds.Api;
//using GoogleMobileAds.Common;
//using System;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine.Purchasing;
//public class AdsManagerAdmob : MonoBehaviour
//{
//    public static AdsManagerAdmob Instante;
//    private BannerView bannerView;
//    private InterstitialAd interstitial;
//    private RewardedAd rewardedAd;
//    private void Awake()
//    {
//        Instante = this;
//    }
//    public void Start()
//    {
//        // Initialize the Google Mobile Ads SDK.
//        MobileAds.Initialize(initStatus => { });
//        if (PlayerPrefs.GetInt(GameData.Instante.RewardAds) == 0)
//        {
//           // this.RequestBanner();
//            RequestInterstitial();
//        }


//        RequestRewardAds();

//    }
//    public void OnPurchaseComplete(Product product)
//    {
//        PlayerPrefs.SetInt(GameData.Instante.RewardAds, 1);
//    }
//    bool IsRewardWatch = false;
//    private void Update()
//    {
//        if (IsRewardWatch)
//        {
//            IsRewardWatch = false;
//            PlayerPrefs.SetInt(GameData.Instante.TotalCoin, (UIManager.Instante.TotalCoinAmount + (UIManager.Instante.LevelEarnCoinAmount * 3)));
//            PlayerPrefs.SetInt(GameData.Instante.LevelEarningCoin, (UIManager.Instante.LevelEarnCoinAmount + 160));
//            PlayerPrefs.SetInt("IsGameStart", 0);
//            PlayerPrefs.SetInt(GameData.Instante.AllLevelsIndex, (UIManager.Instante.LevelIndex + 1));
//            PlayerPrefs.SetInt(GameData.Instante.MapIndex, (GameManager.Instante.MapIndex + 1));

//            if (PlayerPrefs.GetInt(GameData.Instante.MapIndex) >= GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers.Count)
//            {
//                UIManager.Instante.GiftPanel.SetActive(true);
//                UIManager.Instante.YouWon.SetActive(false);

//            }
//            else
//            {

//                LeanTween.cancelAll();
//                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//            }
//        }
//    }
//    #region REWARD_ADS
//    void RequestRewardAds()
//    {
//        string adUnitId;
//#if UNITY_ANDROID
//        adUnitId = "ca-app-pub-3940256099942544/5224354917";
//#elif UNITY_IPHONE
//            adUnitId = "ca-app-pub-3940256099942544/6978759866";
//#else
//            adUnitId = "unexpected_platform";
//#endif

//        this.rewardedAd = new RewardedAd(adUnitId);

//        // Called when an ad request has successfully loaded.
//        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
//        // Called when an ad request failed to load.
//        // this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
//        // Called when an ad is shown.
//        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
//        // Called when an ad request failed to show.
//        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
//        // Called when the user should be rewarded for interacting with the ad.
//        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
//        // Called when the ad is closed.
//        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

//        // Create an empty ad request.
//        AdRequest request = new AdRequest.Builder().Build();
//        // Load the rewarded ad with the request.
//        this.rewardedAd.LoadAd(request);
//    }
//    public int RewardAdsBtnIndex = 0;
//    public void UserChoseToWatchAd(int BtnIndex)
//    {
//        RewardAdsBtnIndex = BtnIndex;
//        if (this.rewardedAd.IsLoaded())
//        {
//            this.rewardedAd.Show();
//        }
//        else
//            RequestRewardAds();
//    }
//    public void HandleRewardedAdLoaded(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleRewardedAdLoaded event received");
//    }

//    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        MonoBehaviour.print(
//            "HandleRewardedAdFailedToLoad event received with message: "
//                             + args.Message);
//    }

//    public void HandleRewardedAdOpening(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleRewardedAdOpening event received");
//    }

//    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
//    {
//        MonoBehaviour.print(
//            "HandleRewardedAdFailedToShow event received with message: "
//                             + args.Message);
//    }

//    public void HandleRewardedAdClosed(object sender, EventArgs args)
//    {
//        RequestRewardAds();
//    }

//    public void HandleUserEarnedReward(object sender, Reward args)
//    {
//        //string type = args.Type;
//        //double amount = args.Amount;
//        //MonoBehaviour.print(
//        //    "HandleRewardedAdRewarded event received for "
//        //                + amount.ToString() + " " + type);
//        if (RewardAdsBtnIndex == 0)
//        {
//            OfflineEarning.Instante.PassiveOfflinePanel();
//        }
//        else if (RewardAdsBtnIndex == 1)
//        {
//            UIManager.Instante.ShowStartUnitsAds();
//        }
//        else if (RewardAdsBtnIndex == 2)
//        {
//            UIManager.Instante.ShowPoduceSpeedAds();
//        }
//        else if (RewardAdsBtnIndex == 3)
//        {
//            UIManager.Instante.ShowOfflineAds();
//        }
//        else if (RewardAdsBtnIndex == 4)
//        {
//            RewardLevelEarningCoinClaim();
//        }
//        else if (RewardAdsBtnIndex == 5)
//        {
//            GiftBox.Instante.IsGiftGet = true;

//        }
//        else if (RewardAdsBtnIndex == 6)
//        {
//            GameFinishAnim.Instante.AttackToRivalPlayers();
//        }
//        else if (RewardAdsBtnIndex == 7)
//        {
//            Shop.Instante.RewardPlayerIcon();
//        }
//        else if (RewardAdsBtnIndex == 8)
//        {
//            Shop.Instante.RewardSoldierIcon();
//        }
//        else if (RewardAdsBtnIndex == 9)
//        {
//            Shop.Instante.RewardColor();
//        }
//    }
//public void RewardLevelEarningCoinClaim()
//{
//    IsRewardWatch = true;

//}
//    #endregion
//    #region Insterstitial
//    private void RequestInterstitial()
//    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        // Initialize an InterstitialAd.
//        this.interstitial = new InterstitialAd(adUnitId);
//        AdRequest request = new AdRequest.Builder().Build();
//        // Load the interstitial with the request.
//        this.interstitial.LoadAd(request);
//    }
//    public void ShowInstantialAds()
//    {
//        if (PlayerPrefs.GetInt(GameData.Instante.RewardAds) == 0)
//        {

//            if (this.interstitial.IsLoaded())
//            {
//                this.interstitial.Show();
//            }
//            else
//                RequestInterstitial();

//        }
//    }
//    #endregion
//    #region BANNER
//    private void RequestBanner()
//    {
//#if UNITY_ANDROID
//        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
//#elif UNITY_IPHONE
//            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
//#else
//            string adUnitId = "unexpected_platform";
//#endif

//        // Create a 320x50 banner at the top of the screen.

//        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
//        AdRequest request = new AdRequest.Builder().Build();

//        // Load the banner with the request.
//        this.bannerView.LoadAd(request);
//    }
//    #endregion
//}
