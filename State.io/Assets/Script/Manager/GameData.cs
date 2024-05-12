using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instante;

    [Header("Level Index Data")]
    public string LevelIndex = "LevelIndex";
    public string MapIndex = "MapIndex";
    public string AllLevelsIndex = "AllLevels";
    public string TotalCoin = "TotalCoin";
    [Header("Start Units Data")]
    public string StartUnitsLevel = "SULevel";
    public string StartUnitsCoin = "SUCoin";
    public string StartUnitsCount = "SUCount";

    [Header("Poduce Speed Data")]
    public string PoduceSpeedLevel = "PSLevel";
    public string PoduceSpeedCoin = "PSCoin";
    public string PoduceSpeedTime = "PSTime";

    [Header("Offline Earnings Data")]
    public string OfflineLevel = "OELevell";
    public string OfflineCoin = "OECoinn";
    public string OfflineAmount = "OEAmountt";

    [Header("Level Win Panel")]
    public string LevelEarningCoin = "LevelEarningCoin";

    [Header("Time Data")]
    public string OfflineTime = "OfflineTime";
    public string Isoffline = "IsOffline";
    [Header("Player Icons")]
    public string PlayerIcon = "PlayerIcon";
    public string SoldierIcon = "SoldierIcon";
    public string PlayerColor = "PlayerColor";

    [Header("SoundManager Data")]
    public string Music = "Music";
    public string Sound = "Sound";
    public string Vibrate = "Vibrate";

    [Header("Shop Item Data")]
    public string PlayerIconIndexs = "PlayerIconIndexs";
    public string SoldierIconIndexs = "SoldierIconIndexs";
    public string ColorIconIndexs = "ColorIconIndexs";

    [Header("Enemy Soldier Count")]
    public string EnemySoldierCount = "EnemySoldier";
    public string EnemySpawnTime = "EnemySpawn";
    [Header("Reward Ads")]
    public string RewardAds = "RewardAds";
    public string iosRequest = "iosrequest";
    private void Awake()
    {
        Instante = this;
    }
    void Start()
    {
        
    }

   
}
