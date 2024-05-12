using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierCountData : MonoBehaviour
{
    public static SoldierCountData Instante;

    public List<SoldierCount> soldierCounts = new List<SoldierCount>();
    private void Awake()
    {
        Instante = this;
    }
   
    
}
[System.Serializable]
public class SoldierCount
{
    public int EmptyPlayerSoldierCount = 10;

    public int EnemyStartSoldierCount = 10;
    public int EnemyMaxSoldierCount = 50;
    public float EnemySpawnTime = 1;
    public int MyMaxSoldierCount = 50;
}