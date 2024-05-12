using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instante;

    [SerializeField] GameObject NoInternetText;
    [SerializeField] GameObject Play_Text;
    [Header("Screen Panels")]
    [SerializeField] GameObject MainMenu;
    public GameObject YouWon;
    [SerializeField] GameObject YouLose;
    [SerializeField] GameObject FailPanel;
    [SerializeField] GameObject InGamePanel;
    [SerializeField] GameObject GoPanel;
    [SerializeField] GameObject GoText;
    [SerializeField] GameObject OfflinePanel;
    [SerializeField] GameObject WinPanelLight;
    [SerializeField] GameObject ExitGamePanel;
    [SerializeField] GameObject LeaveGamePanel;
    [SerializeField] GameObject ShopPanel;
    [SerializeField] GameObject GameFinishPanel;
    [SerializeField] GameObject RestoreBtn;
    public GameObject GiftPanel;
    [Header("Players Bar")]
    [SerializeField] GameObject PlayerBarBorder;
    [SerializeField] GameObject TotalSoldierBar;
    [SerializeField] GameObject BarImg;
    public List<Image> BarSlider = new List<Image>();
    public List<Color> PlayerColor = new List<Color>();


    [Header("Level Soldier Count")]
    public List<int> PlayerCount = new List<int>();
    public List<int> PlayersoldierCount = new List<int>();
    public List<float> PlayerSoldierCountPercent = new List<float>();
    public List<int> PlayerDie = new List<int>();
    public int totalSoldierCount = 0;

    public int EmptyAreaSoldierCount = 0;
    public int PlayersCount = 0;

    [Header("Level Won And Lose Panel Value")]
    [SerializeField] TextMeshProUGUI LevelEarnCoinAmountTMP;
    [HideInInspector] public int LevelEarnCoinAmount = 200;
    [SerializeField] TextMeshProUGUI LevelBonusAdsCoinAmountTMP;
    [SerializeField] GameObject NoThanks_Btn;

    [Header("Level Index")]
    [SerializeField] TextMeshProUGUI LevelIndexTMP;
    public int LevelIndex = 1;
    #region Update Buttons Value Text
    [Header("Total Coin Values")]
    [HideInInspector] public TextMeshProUGUI TotalCoinTMP;
    public int TotalCoinAmount = 0;

    [Header("Start Units Data")]
    [SerializeField] TextMeshProUGUI StartUnitsLevelTMP;
    [SerializeField] int StartUnitsLevel = 0;
    [SerializeField] TextMeshProUGUI StartUnitsCoinTMP;
    [SerializeField] int StartUnitsCoin = 0;
    [SerializeField] TextMeshProUGUI StartUnitsCountTMP;
    [SerializeField] int StartUnitsCount = 0;

    [Header("Poduce Speed Data")]
    [SerializeField] TextMeshProUGUI PoduceSpeedLevelTMP;
    [SerializeField] int PoduceSpeedLevel = 0;
    [SerializeField] TextMeshProUGUI PoduceSpeedCoinTMP;
    [SerializeField] int PoduceSpeedCoin = 0;
    [SerializeField] TextMeshProUGUI PoduceSpeedTimeTMP;
    [SerializeField] float PoduceSpeedTime = 0;

    [Header("Offline Earnings Data")]
    [SerializeField] TextMeshProUGUI OfflineLevelTMP;
    [SerializeField] int OfflineLevel = 0;
    [SerializeField] TextMeshProUGUI OfflineCoinTMP;
    [SerializeField] int OfflineCoin = 0;
    [SerializeField] TextMeshProUGUI OfflineAmountTMP;
    [SerializeField] int OfflineAmount = 0;
    #endregion

    [Header("Gift Bar")]
    [SerializeField] GameObject GiftBarParent;
    [SerializeField] Image GiftBarBg;
    [SerializeField] Sprite MapCompletedImg;
    [SerializeField] GameObject MapCompletedImgParent;
    [SerializeField] Button PlayBtn;
    [Header("Update Button Sprite")]
    [SerializeField] Image[] UpdateButtonsImg;
    [SerializeField] Sprite OrangeButtonSprite;
    [SerializeField] Sprite GreyButtonSprite;
    [SerializeField] Color OrangeColor;
    [SerializeField] Color GrayColor;

    [Header("Update Buttons Ads Icon")]
    [SerializeField] GameObject StartUnitsAds;
    [SerializeField] GameObject StartUnityGold;
    [SerializeField] bool IsWatchAdsStartUnits = false;


    [SerializeField] GameObject ProduceSpeedsAds;
    [SerializeField] GameObject ProduceSpeedsAdsGold;
    [SerializeField] bool IsWatchAdsProduceSpeedsAds = false;


    [SerializeField] GameObject OfflineAds;
    [SerializeField] GameObject OfflineGold;
    [SerializeField] bool IsWatchAdsOffline = false;

    public List<SpawnSoldier> SoldierAreas = new List<SpawnSoldier>();
    private void Awake()
    {

        Instante = this;
        // DontDestroyOnLoad(this.gameObject);

    }
    //void OnApplicationPause(bool pause)
    //{
    //    if (pause)
    //    {
    //        if (PlayerPrefs.GetInt("IsGameStart")==0)
    //        {
    //            DateTime dateTime = DateTime.Now;
    //            Debug.Log("T?ME:" + dateTime.ToString());
    //            PlayerPrefs.SetString(GameData.Instante.OfflineTime, dateTime.ToString());
    //        }


    //    }


    //}
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            DateTime dateTime = DateTime.Now;
            Debug.Log("T?ME:" + dateTime.ToString());
            PlayerPrefs.SetString(GameData.Instante.OfflineTime, dateTime.ToString());
            PlayerPrefs.SetInt("IsGameStart", 1);
        }
    }

    public List<int> ihaveplayercounts = new List<int>();
    public List<MovingPlayersHolder> iHaveMovingPlayersCounts = new List<MovingPlayersHolder>();
    public List<int> SoldierTotalSoldierCount = new List<int>();

    [Serializable]
    public class MovingPlayersHolder
    {
        public List<GameObject> movingPlayers = new List<GameObject>();
    }

    void Start()
    {
        // LeanTween.cancelAll();
        for (int i = 0; i < 7; i++)
        {
            iHaveMovingPlayersCounts.Add(new MovingPlayersHolder());

        }

        string PreviousDateTime = PlayerPrefs.GetString(GameData.Instante.OfflineTime, " ");
        if (!PreviousDateTime.Equals(" ") && PlayerPrefs.GetInt("IsGameStart") == 1)
        {
            OfflinePanel.SetActive(true);
        }
        PlayerPrefs.SetInt(GameData.Instante.Isoffline, 1);

        GetFirstValue();
#if PLATFORM_ANDROID
        RestoreBtn.SetActive(false);
#endif
    }
    void GetFirstValue()
    {
        PlayersCount = GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers[GameManager.Instante.MapIndex].Players.Count;
        LevelIndex = PlayerPrefs.GetInt(GameData.Instante.AllLevelsIndex, 1);
        LevelIndexTMP.text = "LEVEL " + LevelIndex.ToString();
        PlayerPrefs.SetInt("IsGameStart", 0);

        TotalCoinAmount = PlayerPrefs.GetInt(GameData.Instante.TotalCoin, 0);
        TotalCoinTMP.text = TotalCoinAmount.ToString();

        GetUpdateButtonsValue();
    }
    float timer = 0f;
    public float Winwaittime = 0, Losewaittime = 0;
    [SerializeField] int enemyTotalSoldierCount = 0;
    private void Update()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            NoInternetText.SetActive(true);
            Play_Text.SetActive(false);
        }
        else
        {
            NoInternetText.SetActive(false);
            Play_Text.SetActive(true);
        }


        WinPanelLight.transform.Rotate(0, 0, 20 * Time.deltaTime);
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {


                IsLeaveGame(true);
            }

        }

        if (IsGameStart)
        {
            
            int mysoldiercount = SoldierTotalSoldierCount[0];

            int tempEnemyTotalSoldierCount = 0;

            

            


            for (int i = 0; i < PlayerCount.Count; i++)
            {
                PlayerSoldierCountPercent[i] = ((float)SoldierTotalSoldierCount[i] / (float)(EmptyAreaSoldierCount + mysoldiercount + enemyTotalSoldierCount)) * 100f;
                float SizeDeltaX = BarSlider[i].GetComponent<RectTransform>().sizeDelta.x;
                BarSlider[i].GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Clamp(SizeDeltaX, 0, 700), 50);
                BarSlider[i].GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(SizeDeltaX, PlayerSoldierCountPercent[i] * 7, 5*Time.deltaTime), 50);
            }



            List<int> tempPlayerCount = new List<int>();
            tempPlayerCount.Clear();

            for (int i = 0; i < PlayerCount.Count; i++)
            {
                tempPlayerCount.Add(0);

            }

            for (int i = 0; i < SoldierAreas.Count; i++)
            {

                tempPlayerCount[SoldierAreas[i].PlayerIndex]++;

            }

            for (int i = 0; i < PlayerCount.Count; i++)
            {
                PlayerCount[i] = tempPlayerCount[i];
            }


            for (int i = 1; i < PlayerCount.Count; i++)
            {
               
                if (tempPlayerCount[i] <= 0)
                {
                      EnemySoldierCount2++;
                    
                }
                else
                {
                   EnemySoldierCount2 = 0;
                    
                }

               
            }
            Winwaittime += Time.deltaTime;
            if(Winwaittime>=1)
            {
                Winwaittime = 0;
                if (enemyTotalSoldierCount <= 0 && !IsGameFinish && IsGameStart && EnemySoldierCount2 >= PlayerCount.Count - 1)
                {
                    print("EnemySoldierCount: " + enemyTotalSoldierCount);
                    print("Enemy area count: " + EnemySoldierCount2);
                    IsGameFinish = true;
                    BarSlider[0].GetComponent<RectTransform>().sizeDelta = new Vector2(700, 50);
                    YouWonFunc();
                }
            }
                
            



            if (PlayersoldierCount.Count > 0)
            {

                if ((PlayerCount[0] <= 0 && SoldierTotalSoldierCount[0] <= 0 && iHaveMovingPlayersCounts[0].movingPlayers.Count <= 0) && !fail && !IsGameFinish)
                {

                    Debug.Log("oyun bitti");
                    fail = true;
                    IsGameFinish = true;
                    BarSlider[0].GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50);
                    YouLoseFunc();
                }

            }
        }











    }
    private void FixedUpdate()
    { if(IsGameStart)
        {
            ihaveplayercounts.Clear();
            SoldierTotalSoldierCount.Clear();


            for (int i = 0; i < PlayerCount.Count; i++)
            {
                ihaveplayercounts.Add(0);
                SoldierTotalSoldierCount.Add(0);
            }

            if (PlayerCount.Count > 0 && SoldierAreas.Count > 0)
            {

                for (int a = 0; a < PlayerCount.Count; a++)
                {

                    for (int i = 0; i < SoldierAreas.Count; i++)
                    {
                        if (SoldierAreas[i].PlayerIndex == a)
                        {
                            ihaveplayercounts[a] += SoldierAreas[i].SoldierCount;
                            SoldierTotalSoldierCount[a] += iHaveMovingPlayersCounts[a].movingPlayers.Count + SoldierAreas[i].SoldierCount;
                        }
                    }
                }
            }
            enemyTotalSoldierCount = 0;
            for (int i = 1; i < SoldierTotalSoldierCount.Count; i++)
            {
                enemyTotalSoldierCount += SoldierTotalSoldierCount[i];
            }
            
        }
        
    }

    



    #region EXIT GAME


    public void IsLeaveGame(bool IsLeave)
    {

        LeaveGamePanel.SetActive(IsLeave);
    }
    public void LeaveGame()
    {

        DateTime dateTime = DateTime.Now;
        Debug.Log("T?ME:" + dateTime.ToString());
        PlayerPrefs.SetString(GameData.Instante.OfflineTime, dateTime.ToString());
        PlayerPrefs.SetInt("IsGameStart", 1);
        Application.Quit();

    }

    public void ExitGame(bool IsExit)
    {
        if (IsExit)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        ExitGamePanel.SetActive(IsExit);

    }
    public void BackToMenuFromGame()
    {
        Time.timeScale = 1;
        ExitGamePanel.SetActive(false);
        InGamePanel.SetActive(false);
        IsbackToMenu = true;
        GameManager.Instante.CamGotoFirstPos();

        //GameManager.Instante.GetLevelIndex();
        ////GameManager.Instante.PreviousMapClose();
        //DeleteListValue();
        //PlayersCount = 0;
    }
    #endregion
    #region  UPDATE BUTTON VALUES
    public void TotalCoinTextAnim(int coinAmount)
    {
        LeanTween.cancel(TotalCoinTMP.gameObject);
        float coin = TotalCoinAmount;
        float coin2 = TotalCoinAmount;

        LeanTween.scale(TotalCoinTMP.gameObject, Vector2.one * 1.3f, 0.2f).setOnComplete(() =>
        {
            LeanTween.value(coin, coin2 - coinAmount, 1).setOnUpdate((float value) =>
              {
                  //coin = value;
                  TotalCoinTMP.text = value.ToString("0");
              })
            .setOnComplete(() => LeanTween.scale(TotalCoinTMP.gameObject, Vector2.one, 0.2f));
            //TotalCoinTMP.text = TotalCoinAmount.ToString();
            //LeanTween.scale(TotalCoinTMP.gameObject, Vector2.one, 0.2f);
        });
        TotalCoinAmount -= coinAmount;
        PlayerPrefs.SetInt(GameData.Instante.TotalCoin, TotalCoinAmount);
    }
    public void GetUpdateButtonsValue()
    { //YETERL? PARA OLMADI?INDA BUTONLAR GR? OLACAK
        TotalCoinAmount = PlayerPrefs.GetInt(GameData.Instante.TotalCoin, 0);

        //START UNITS BUTTON VALUES
        StartUnitsLevel = PlayerPrefs.GetInt(GameData.Instante.StartUnitsLevel, 1);
        StartUnitsLevelTMP.text = "Lvl " + StartUnitsLevel.ToString();

        StartUnitsCoin = PlayerPrefs.GetInt(GameData.Instante.StartUnitsCoin, 50);
        StartUnitsCoinTMP.text = StartUnitsCoin.ToString();

        StartUnitsCount = PlayerPrefs.GetInt(GameData.Instante.StartUnitsCount, 10);
        StartUnitsCountTMP.text = StartUnitsCount.ToString();


        //PRODUCE SPEED BUTTON VALUES
        PoduceSpeedLevel = PlayerPrefs.GetInt(GameData.Instante.PoduceSpeedLevel, 1);
        PoduceSpeedLevelTMP.text = "Lvl " + PoduceSpeedLevel.ToString();

        PoduceSpeedCoin = PlayerPrefs.GetInt(GameData.Instante.PoduceSpeedCoin, 50);
        PoduceSpeedCoinTMP.text = PoduceSpeedCoin.ToString();

        PoduceSpeedTime = PlayerPrefs.GetFloat(GameData.Instante.PoduceSpeedTime, 1);
        PoduceSpeedTimeTMP.text = PoduceSpeedTime.ToString("0.00");


        //OFFL?NE EARN?NG BUTTON VALUES
        OfflineLevel = PlayerPrefs.GetInt(GameData.Instante.OfflineLevel, 1);
        OfflineLevelTMP.text = "Lvl " + OfflineLevel.ToString();

        OfflineCoin = PlayerPrefs.GetInt(GameData.Instante.OfflineCoin, 50);
        OfflineCoinTMP.text = OfflineCoin.ToString();

        OfflineAmount = PlayerPrefs.GetInt(GameData.Instante.OfflineAmount, 50);
        OfflineAmountTMP.text = OfflineAmount.ToString();

        if (TotalCoinAmount < StartUnitsCoin)
        {
            if (!IsWatchAdsStartUnits)
            {
                StartUnitsAds.SetActive(true);
                StartUnityGold.SetActive(false);
            }
            else
            {
                StartUnitsAds.SetActive(false);
                StartUnityGold.SetActive(true);
                UpdateButtonsImg[0].sprite = GreyButtonSprite;
                UpdateButtonsImg[0].transform.GetChild(1).GetComponent<Image>().color = GrayColor;
            }
        }
        else if (TotalCoinAmount >= StartUnitsCoin)
        {
            StartUnitsAds.SetActive(false);
            StartUnityGold.SetActive(true);
            UpdateButtonsImg[0].sprite = OrangeButtonSprite;
            UpdateButtonsImg[0].transform.GetChild(1).GetComponent<Image>().color = OrangeColor;
        }
        if (TotalCoinAmount < PoduceSpeedCoin)
        {
            if (!IsWatchAdsProduceSpeedsAds)
            {
                ProduceSpeedsAds.SetActive(true);
                ProduceSpeedsAdsGold.SetActive(false);
            }
            else
            {
                ProduceSpeedsAds.SetActive(false);
                ProduceSpeedsAdsGold.SetActive(true);
                UpdateButtonsImg[1].sprite = GreyButtonSprite;
                UpdateButtonsImg[1].transform.GetChild(1).GetComponent<Image>().color = GrayColor;
            }

        }
        else if (TotalCoinAmount >= PoduceSpeedCoin)
        {
            ProduceSpeedsAds.SetActive(false);
            ProduceSpeedsAdsGold.SetActive(true);
            UpdateButtonsImg[1].sprite = OrangeButtonSprite;
            UpdateButtonsImg[1].transform.GetChild(1).GetComponent<Image>().color = OrangeColor;
        }
        if (TotalCoinAmount < OfflineCoin)
        {
            if (!IsWatchAdsOffline)
            {
                OfflineAds.SetActive(true);
                OfflineGold.SetActive(false);
            }
            else
            {
                OfflineAds.SetActive(false);
                OfflineGold.SetActive(true);
                UpdateButtonsImg[2].sprite = GreyButtonSprite;
                UpdateButtonsImg[2].transform.GetChild(1).GetComponent<Image>().color = GrayColor;
            }
        }
        else if (TotalCoinAmount >= OfflineCoin)
        {
            OfflineAds.SetActive(false);
            OfflineGold.SetActive(true);
            UpdateButtonsImg[2].sprite = OrangeButtonSprite;
            UpdateButtonsImg[2].transform.GetChild(1).GetComponent<Image>().color = OrangeColor;
        }

        LevelManager.Instante.GetEnemySoldierCount();
    }

    public void StartUnitsBtn(int btnindex)
    {
        if (TotalCoinAmount >= StartUnitsCoin)
        {
            TotalCoinTextAnim(StartUnitsCoin);
            //TotalCoinAmount -= StartUnitsCoin;
            //PlayerPrefs.SetInt(GameData.Instante.TotalCoin, TotalCoinAmount);

            StartUnitsCoin += 95;
            PlayerPrefs.SetInt(GameData.Instante.StartUnitsCoin, StartUnitsCoin);

            StartUnitsLevel++;
            PlayerPrefs.SetInt(GameData.Instante.StartUnitsLevel, StartUnitsLevel);

            StartUnitsCount++;
            PlayerPrefs.SetInt(GameData.Instante.StartUnitsCount, StartUnitsCount);

            GetUpdateButtonsValue();

            SelectArea.Instante.SetSoldierCount();
        }
        else if (!IsWatchAdsStartUnits)
        {
            UnityAds.Instante.ShowAdReward(btnindex);
        }
    }
    public void ShowStartUnitsAds()
    {
        IsWatchAdsStartUnits = true;
        StartUnitsLevel++;
        PlayerPrefs.SetInt(GameData.Instante.StartUnitsLevel, StartUnitsLevel);

        StartUnitsCount++;
        PlayerPrefs.SetInt(GameData.Instante.StartUnitsCount, StartUnitsCount);

        GetUpdateButtonsValue();

        SelectArea.Instante.SetSoldierCount();

    }
    public void PoduceSpeedBtn(int btnindex)
    {
        if (TotalCoinAmount >= PoduceSpeedCoin)
        {
            TotalCoinTextAnim(PoduceSpeedCoin);
            //TotalCoinAmount -= PoduceSpeedCoin;
            //PlayerPrefs.SetInt(GameData.Instante.TotalCoin, TotalCoinAmount);

            PoduceSpeedCoin += 95;
            PlayerPrefs.SetInt(GameData.Instante.PoduceSpeedCoin, PoduceSpeedCoin);

            PoduceSpeedLevel++;
            PlayerPrefs.SetInt(GameData.Instante.PoduceSpeedLevel, PoduceSpeedLevel);

            PoduceSpeedTime += 0.07f;
            PlayerPrefs.SetFloat(GameData.Instante.PoduceSpeedTime, PoduceSpeedTime);

            GetUpdateButtonsValue();
            SelectArea.Instante.SetSoldierCount();
        }
        else if (!IsWatchAdsProduceSpeedsAds)
        {
            UnityAds.Instante.ShowAdReward(btnindex);
        }
    }
    public void ShowPoduceSpeedAds()
    {
        IsWatchAdsProduceSpeedsAds = true;

        PoduceSpeedLevel++;
        PlayerPrefs.SetInt(GameData.Instante.PoduceSpeedLevel, PoduceSpeedLevel);

        PoduceSpeedTime += 0.07f;
        PlayerPrefs.SetFloat(GameData.Instante.PoduceSpeedTime, PoduceSpeedTime);

        GetUpdateButtonsValue();
        SelectArea.Instante.SetSoldierCount();
    }
    public void OfflineEarnBtn(int btnindex)
    {
        if (TotalCoinAmount >= OfflineCoin)
        {
            TotalCoinTextAnim(OfflineCoin);
            //TotalCoinAmount -= OfflineCoin;
            //PlayerPrefs.SetInt(GameData.Instante.TotalCoin, TotalCoinAmount);

            OfflineCoin += 95;
            PlayerPrefs.SetInt(GameData.Instante.OfflineCoin, OfflineCoin);

            OfflineLevel++;
            PlayerPrefs.SetInt(GameData.Instante.OfflineLevel, OfflineLevel);

            OfflineAmount += 30;
            PlayerPrefs.SetInt(GameData.Instante.OfflineAmount, OfflineAmount);

            GetUpdateButtonsValue();
            SelectArea.Instante.SetSoldierCount();
        }
        else if (!IsWatchAdsOffline)
        {
            //AdsManagerAdmob.Instante.UserChoseToWatchAd(btnindex);
            UnityAds.Instante.ShowAdReward(btnindex);
        }
    }
    public void ShowOfflineAds()
    {
        IsWatchAdsOffline = true;
        OfflineLevel++;
        PlayerPrefs.SetInt(GameData.Instante.OfflineLevel, OfflineLevel);

        OfflineAmount += 30;
        PlayerPrefs.SetInt(GameData.Instante.OfflineAmount, OfflineAmount);

        GetUpdateButtonsValue();
        SelectArea.Instante.SetSoldierCount();
    }
    #endregion
    public bool IsGameStart = false;
    public void StartGame()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            LeanTween.cancelAll();
            MainMenu.SetActive(false);
            GoPanel.SetActive(true);
            GoText.SetActive(true);

            LeanTween.scale(GoText, Vector2.one, 0.5f).setOnComplete(() =>
            {
                GoPanel.SetActive(false);
                GoText.GetComponent<RectTransform>().localScale = new Vector3(0.1f, 0.1f, 0.1f);
                GoText.SetActive(false);
                InGamePanel.SetActive(true);
                SoundManager.Instante.GoSoundPlay();
                GameManager.Instante.StartFight();
                //StartCoroutine(BarPercentCalculate());
            });
            StartCoroutine(ControlGameEnd());
        }

    }
    public void NextLevel()
    {
        PlayerPrefs.SetInt("IsGameStart", 0);

        TotalCoinAmount += LevelEarnCoinAmount;
        PlayerPrefs.SetInt(GameData.Instante.TotalCoin, TotalCoinAmount);
        LevelEarnCoinAmount += 160;
        PlayerPrefs.SetInt(GameData.Instante.LevelEarningCoin, LevelEarnCoinAmount);

        PlayerPrefs.SetInt(GameData.Instante.AllLevelsIndex, (LevelIndex + 1));
        PlayerPrefs.SetInt(GameData.Instante.MapIndex, (GameManager.Instante.MapIndex + 1));
        InGamePanel.SetActive(false);
        LeanTween.cancelAll();
        if (PlayerPrefs.GetInt(GameData.Instante.MapIndex) >= GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers.Count && (playerIconIndex.Count < 8 && playerSoldierIndex.Count < 8))
        {
            GiftPanel.SetActive(true);
            YouWon.SetActive(false);

        }
        else
        {
            if (PlayerPrefs.GetInt(GameData.Instante.MapIndex) >= GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers.Count && playerIconIndex.Count >= 8 && playerSoldierIndex.Count >= 8)
            {
                PlayerPrefs.SetInt(GameData.Instante.LevelIndex, (PlayerPrefs.GetInt(GameData.Instante.LevelIndex) + 1));
                PlayerPrefs.SetInt(GameData.Instante.MapIndex, 0);
            }
            if (PlayerPrefs.GetInt(GameData.Instante.AllLevelsIndex) >= 100)
            {
                GameFinishPanel.SetActive(true);
                YouWon.SetActive(false);

            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }


    }
    public void NextlevelTest()
    {
        PlayerPrefs.SetInt(GameData.Instante.AllLevelsIndex, (LevelIndex + 1));
        PlayerPrefs.SetInt(GameData.Instante.MapIndex, (GameManager.Instante.MapIndex + 1));
        if (PlayerPrefs.GetInt(GameData.Instante.MapIndex) >= GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers.Count)
        {
            PlayerPrefs.SetInt(GameData.Instante.LevelIndex, (PlayerPrefs.GetInt(GameData.Instante.LevelIndex) + 1));
            PlayerPrefs.SetInt(GameData.Instante.MapIndex, 0);
        }
        PlayerPrefs.SetInt(GameData.Instante.TotalCoin, (PlayerPrefs.GetInt(GameData.Instante.TotalCoin) + 20000));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void previosLevel()
    {
        PlayerPrefs.SetInt(GameData.Instante.AllLevelsIndex, (LevelIndex - 1));
        PlayerPrefs.SetInt(GameData.Instante.MapIndex, (GameManager.Instante.MapIndex - 1));
        if (PlayerPrefs.GetInt(GameData.Instante.MapIndex) < 0)
        {
            PlayerPrefs.SetInt(GameData.Instante.LevelIndex, (PlayerPrefs.GetInt(GameData.Instante.LevelIndex) - 1));
            PlayerPrefs.SetInt(GameData.Instante.MapIndex, GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers.Count - 1);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void GiftPanelNoThanksBtn()
    {
        PlayerPrefs.SetInt("IsGameStart", 0);
        PlayerPrefs.SetInt(GameData.Instante.AllLevelsIndex, (LevelIndex + 1));
        PlayerPrefs.SetInt(GameData.Instante.MapIndex, 0);
        PlayerPrefs.SetInt(GameData.Instante.LevelIndex, (PlayerPrefs.GetInt(GameData.Instante.LevelIndex) + 1));
        if (PlayerPrefs.GetInt(GameData.Instante.LevelIndex) >= GameManager.Instante.Levels.Count)
        {
            PlayerPrefs.SetInt(GameData.Instante.LevelIndex, 0);
        }
        if (PlayerPrefs.GetInt(GameData.Instante.AllLevelsIndex) >= 100)
        {
            GameFinishPanel.SetActive(true);
            GiftPanel.SetActive(false);

        }
        else
        {
            GiftPanel.SetActive(false);
            MainMenu.SetActive(true);
            InGamePanel.SetActive(false);
            LeanTween.cancelAll();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    public void RewardEarnCoin()
    {


        PlayerPrefs.SetInt(GameData.Instante.TotalCoin, (UIManager.Instante.TotalCoinAmount + (UIManager.Instante.LevelEarnCoinAmount * 3)));
        PlayerPrefs.SetInt(GameData.Instante.LevelEarningCoin, (UIManager.Instante.LevelEarnCoinAmount + 160));
        PlayerPrefs.SetInt("IsGameStart", 0);
        PlayerPrefs.SetInt(GameData.Instante.AllLevelsIndex, (UIManager.Instante.LevelIndex + 1));
        PlayerPrefs.SetInt(GameData.Instante.MapIndex, (GameManager.Instante.MapIndex + 1));

        if (PlayerPrefs.GetInt(GameData.Instante.MapIndex) >= GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers.Count)
        {
            GiftPanel.SetActive(true);
            YouWon.SetActive(false);

        }
        else
        {

            LeanTween.cancelAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }
    public void levelFailNoThanks(bool enable)
    {
        if (enable)
        {
            FailPanel.SetActive(false);
            YouLose.SetActive(true);
            GameFinishAnim.Instante.IsYouLosePanelActive = true;
        }


    }

    public void PlayerBarSpawn()
    {
        for (int i = 0; i < PlayerCount.Count; i++)
        {
            GameObject Img = Instantiate(BarImg, TotalSoldierBar.transform.position, Quaternion.identity, TotalSoldierBar.transform);
            Img.GetComponent<Image>().color = PlayerColor[i];
            BarSlider.Add(Img.GetComponent<Image>());

        }
    }
    #region PANELS
    bool IsbackToMenu = false;
    public void MainMenuAcive()
    {
        LeanTween.cancelAll();
        // MainMenu.SetActive(true);
        YouLose.SetActive(false);
        PlayerPrefs.SetInt("IsGameStart", 0);
        if (!IsbackToMenu)
            PlayerPrefs.SetInt(GameData.Instante.TotalCoin, (TotalCoinAmount + 100));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //GameManager.Instante.GetLevelIndex();
        //GameManager.Instante.PreviousMapClose();
        //DeleteListValue();

    }
    public bool IsGameFinish = false;
    public void YouWonFunc()
    {

        LevelEarnCoinAmount = PlayerPrefs.GetInt(GameData.Instante.LevelEarningCoin, 200);
        LevelEarnCoinAmountTMP.text = "+" + LevelEarnCoinAmount.ToString();
        LevelBonusAdsCoinAmountTMP.text = (LevelEarnCoinAmount * 3).ToString();
        InGamePanel.SetActive(false);

        GameManager.Instante.YouWonTheGame();

    }
    public void YouWonPanelActive()
    {

        Invoke("InstertitialAdsShow", 1);
        YouWon.SetActive(true);
        Invoke("Btn_Active", 2);
    }
    void InstertitialAdsShow()
    {
        UnityAds.Instante.ShowAdInstertitial(false);
    }
    void Btn_Active()
    {
        NoThanks_Btn.SetActive(true);
    }
    public void YouLoseFunc()
    {

        //LeanTween.cancelAll();
        Invoke("ShowInsterstial", 1);

        //StartCoroutine(WaitShowAds());
        //DeleteListValue();
    }
    public bool IsReadyAds = true;
    IEnumerator WaitShowAds()
    {
        yield return new WaitForSeconds(2);
        if (IsReadyAds)
        {
            IsReadyAds = false;
            GameFinishAds();
        }
    }
    void ShowInsterstial()
    {

        UnityAds.Instante.ShowAdInstertitial(true);
    }
    public void GameFinishAds()
    {
        IsReadyAds = false;
        FailPanel.SetActive(true);
        GameManager.Instante.GameFail();


    }
    public void ShopPanelEnable(bool _enable)
    {
        ShopPanel.SetActive(_enable);
        LevelManager.Instante.MyPlayerIconAndColorChange();
        GameManager.Instante.SelectMap();
    }
    #endregion
    List<int> playerIconIndex = new List<int>();
    List<int> playerSoldierIndex = new List<int>();
    public void GiftBar(int levelMapCount, int MapIndex)
    {
        if (levelMapCount == 3)
        {
            GiftBarBg.GetComponent<RectTransform>().sizeDelta = new Vector2(370, 77);
            PlayBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-12.5f, -170.38f);
        }
        else if (levelMapCount == 4)
        {
            GiftBarBg.GetComponent<RectTransform>().sizeDelta = new Vector2(470, 77);
            PlayBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(30f, -170.38f);
        }
        for (int i = 0; i < levelMapCount; i++)
        {
            MapCompletedImgParent.transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int i = 0; i < MapIndex; i++)
        {
            MapCompletedImgParent.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = MapCompletedImg;
        }
        playerIconIndex = PlayerPrefsExtra.GetList<int>(GameData.Instante.PlayerIconIndexs, new List<int>());
        playerSoldierIndex = PlayerPrefsExtra.GetList<int>(GameData.Instante.SoldierIconIndexs, new List<int>());
        if (playerIconIndex.Count >= 8 && playerSoldierIndex.Count >= 8)
        {
            PlayBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(-12.5f, -170.38f);
            GiftBarParent.SetActive(false);
        }

    }
    #region SOLD?ER AND PLAYERS COUNT
    public void GetSoldierCount(int count)
    {
        totalSoldierCount += count;


    }
    public bool IsLastSoldier = false;
    public void SliderBarValueIncrease(int SliderIndex)
    {

        //PlayerCount[SliderIndex]++;




    }
    IEnumerator ControlGameEnd()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        while (!IsGameFinish)
        {

            for (int i = 1; i < PlayerDie.Count; i++)
            {
                yield return delay;
                if (PlayerCount[i] <= 0)
                {
                    //  EnemySoldierCount2++;
                    //if (EnemySoldierCount2 >= PlayerCount.Count - 1 && !IsGameFinish)
                    //    YouWonFunc();
                }
                else
                {
                    //    EnemySoldierCount2 = 0;
                    break;
                }

                //yield return new WaitForSeconds(0.5f); 
            }
            yield return null;
        }
    }


    public int EnemySoldierCount2 = 0;

    public bool fail = false, SoldierStop = false;
    public int DieEnemyCount = 0;
    public void SliderBarValueDecrease(int SliderIndex)
    {

        //PlayerCount[SliderIndex]--;

    }


    public void BarFillAmount(int SliderIndex, int value)
    {
        PlayersoldierCount[SliderIndex] += value;

        totalSoldierCount += value;


        BarSlider[SliderIndex].gameObject.SetActive(true);


        // BarSlider[SliderIndex].GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Lerp(BarSlider[SliderIndex].GetComponent<RectTransform>().sizeDelta.x,PlayerSoldierCountPercent[SliderIndex] * 7,Time.deltaTime*5), 100);
    }
    public void PlayersBarRemove()
    {
        StartCoroutine(ControlGameEnd());

        totalSoldierCount = 0;
        DieEnemyCount = 0;
        for (int i = 0; i < PlayerCount.Count; i++)
        {
            PlayerCount[i] = 0;

            PlayersoldierCount[i] = 0;
            SoldierTotalSoldierCount[i] = 0;
            PlayerSoldierCountPercent[i] = 0;
            iHaveMovingPlayersCounts[i].movingPlayers.RemoveRange(0, iHaveMovingPlayersCounts[i].movingPlayers.Count);
            BarSlider[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < PlayersCount; i++)
        {
            GameObject player = GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers[GameManager.Instante.MapIndex].Players[i];
            if (player.GetComponent<SpawnSoldier>() != null)
            {
                totalSoldierCount += player.GetComponent<SpawnSoldier>().SoldierCount;
                PlayersoldierCount[player.GetComponent<SpawnSoldier>().PlayerIndex] += player.GetComponent<SpawnSoldier>().SoldierCount;
                PlayerCount[player.GetComponent<SpawnSoldier>().PlayerIndex]++;


                player.GetComponent<SpawnSoldier>().GetVeriable();

            }

        }
        for (int i = 0; i < PlayersCount; i++)
        {
            GameObject player = GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers[GameManager.Instante.MapIndex].Players[i];
            if (player.GetComponent<SpawnSoldier>())
            {
                BarSlider[player.GetComponent<SpawnSoldier>().PlayerIndex].GetComponent<Image>().color = PlayerColor[player.GetComponent<SpawnSoldier>().PlayerIndex];
                BarFillAmount(player.GetComponent<SpawnSoldier>().PlayerIndex, 0);
                BarSlider[player.GetComponent<SpawnSoldier>().PlayerIndex].gameObject.SetActive(true);

            }
        }







    }
    public void EnemyAgainAttack()
    {
        for (int i = 0; i < PlayersCount; i++)
        {
            GameObject player = GameManager.Instante.maps[GameManager.Instante.LevelIndex].levelManagers[GameManager.Instante.MapIndex].Players[i];
            if (player.GetComponent<Enemy>())
            {
                StopCoroutine(player.GetComponent<SpawnSoldier>().AttackTime());
                StartCoroutine(player.GetComponent<SpawnSoldier>().AttackTime());

            }
            if (player.GetComponent<SpawnSoldier>())
                StartCoroutine(player.GetComponent<SpawnSoldier>().VisualUpdate());
        }


        // Invoke("failValueChange", 2);



    }

    #endregion

    public void GameFinish()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
