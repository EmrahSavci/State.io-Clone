//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using GoogleMobileAds.Placement;

//using UnityEngine.SceneManagement;
//public class AdsManager : MonoBehaviour
//{
//    public static AdsManager Instante;

//    InterstitialAdGameObject interstitialAdGameObject;
//    RewardedAdGameObject RewardedAdGameObject;
//    RewardedAdGameObject RewardedAdGameObjectUnits;
//    RewardedAdGameObject RewardedAdGameObjectPoduce;
//    RewardedAdGameObject RewardedAdGameObjectOffline;
//    private void Awake()
//    {
//        Instante = this;
//    }
//    void Start()
//    {
       
//        interstitialAdGameObject = MobileAds.Instance
//    .GetAd<InterstitialAdGameObject>("Interstitial Ad");
//        interstitialAdGameObject.LoadAd();
//        // Access InterstitialAd object

        
//        //UNÝTS ADS
//        RewardedAdGameObjectUnits = MobileAds.Instance
//     .GetAd<RewardedAdGameObject>("Rewarded Ad_StartUnitsBtn");


//        //PODUCE SPEED
//        RewardedAdGameObjectPoduce = MobileAds.Instance
//     .GetAd<RewardedAdGameObject>("Rewarded Ad_PoduceSpeed");


//        //OFFLÝNE EARNING ADS
//        RewardedAdGameObjectOffline = MobileAds.Instance
//     .GetAd<RewardedAdGameObject>("Rewarded Ad_OfflineEarn");


//        MobileAds.Initialize((initStatus) =>
//        {
//            interstitialAdGameObject.LoadAd();
//            RewardedAdGameObjectUnits.LoadAd();
//            RewardedAdGameObjectPoduce.LoadAd();
//            RewardedAdGameObjectOffline.LoadAd();
//        });
//    }
//   public void AgainLoadRewarAds(string name)
//    {
//        RewardedAdGameObject = MobileAds.Instance
//     .GetAd<RewardedAdGameObject>(name);
//        RewardedAdGameObject.LoadAd();
//    }
//    // Update is called once per frame
//    public void OnBannerAdFailedToLoad(string reason)
//    {
//        Debug.Log("Banner ad failed to load: " + reason);
//    }
//    public void OnClickShowGameSceneButton()
//    {
//        // Display an interstitial ad
//        interstitialAdGameObject.ShowIfLoaded();

//        // Load a scene named "GameScene"
//        //SceneManager.LoadScene("GameScene");
//    }
//    public void OnUserEarnedReward(Reward reward)
//    {
       
//        OfflineEarning.Instante.PassiveOfflinePanel();
        
        

//    }
//    public void RewardLevelEarningCoinClaim()
//    {
//        PlayerPrefs.SetInt(GameData.Instante.TotalCoin,(UIManager.Instante.TotalCoinAmount+(UIManager.Instante.LevelEarnCoinAmount*3)));
//        PlayerPrefs.SetInt(GameData.Instante.LevelEarningCoin, (UIManager.Instante.LevelEarnCoinAmount+160));
//        PlayerPrefs.SetInt("IsGameStart", 0);
//        PlayerPrefs.SetInt(GameData.Instante.AllLevelsIndex, (UIManager.Instante.LevelIndex + 1));
//        PlayerPrefs.SetInt(GameData.Instante.MapIndex, (GameManager.Instante.MapIndex + 1));
       
//        if (PlayerPrefs.GetInt(GameData.Instante.MapIndex) >= GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers.Count)
//        {
//            UIManager.Instante.GiftPanel.SetActive(true);
//            UIManager.Instante.YouWon.SetActive(false);

//        }
//        else
//        {
            
//            LeanTween.cancelAll();
//            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
//        }
        
//    }
//}
