using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ChangePlayer : MonoBehaviour
{
    public GameObject Soldier;
    public TextMeshProUGUI SoldierCountTMP;
    public int SoldierCount = 0, MaxSoldierCount;
    public int NumberEnemyAttacking = 0;
    public float SpawnTime = 1f;
    public bool IsSpawn = false,active;
    public Sprite MySprite;
    [Header("Colors")]
    public MapColor Map;

    [SerializeField] float ColorChangeTime;
    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();

    SpawnSoldier spawnSoldier;
    void Start()
    {
        spawnSoldier = GetComponent<SpawnSoldier>();
        GetVeriable();
    }
    public void GetVeriable()
    {
        active = true;
        Soldier = GameObject.FindGameObjectWithTag("Soldier");
        SoldierCountTMP = GetComponentInChildren<TextMeshProUGUI>();

        SoldierCount = 10;
        MaxSoldierCount = 50;
        SpawnTime = 0.5f;
        Transform Points = null;
        foreach (Transform child in transform)
        {
            if (child.tag == "Points")
            {
                Points = child;
            }
        }
        for (int i = 0; i < Points.transform.childCount; i++)
        {
            SpawnPoints.Add(Points.transform.GetChild(i));
        }

        MySprite = GetComponent<SpriteRenderer>().sprite;
        spawnSoldier.SoldierCount = SoldierCount;
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
