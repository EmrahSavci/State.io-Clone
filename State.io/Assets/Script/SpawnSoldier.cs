using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
public class SpawnSoldier : MonoBehaviour
{

    public GameObject Soldier;
    public TextMeshProUGUI SoldierCountTMP;
    public int PlayerIndex = 0;
    public int SoldierCount = 0, MaxSoldierCount;
    public int FirstCount = 0;
    public int NumberEnemyAttacking = 0;
    public float SpawnTime = 1f;
    public bool IsSpawn = false;
    [Header("Slider Bar")]
    [SerializeField] Image SliderBar;
    [SerializeField] Image MaxSoldierImg;
    [Header("Colors")]
    public MapColor Map;

    [SerializeField] float ColorChangeTime;

    [Header("Target Enemy")]
    public Transform TargetEnemy;


    [SerializeField] List<Transform> SpawnPoints = new List<Transform>();
    public Sprite MySprite;
    public Sprite SoldierSprite;
    [Header("Corotunes")]
    public IEnumerator enumerator, AttackEnumator;
    public bool Hover;
    public bool IsEnemy = false;
    Shake shake;
    public bool IsGameFinish = false;
    public bool FirstEnemy = false;
    private void OnMouseOver()
    {
        Hover = true;
    }
    private void OnMouseExit()
    {
        Hover = false;
    }
    private void OnEnable()
    {
        // GetComponent<SpriteRenderer>().sprite = MySprite;
    }
    int levelIndex = 0, AllLevelIndex = 0;
    void Start()
    {
        //UIManager.Instante.SliderBarValueIncrease(PlayerIndex);
        //UIManager.Instante.PlayersoldierCount.Add(SoldierCount);
        SoldierCountTMP = GetComponentInChildren<TextMeshProUGUI>();
        SoldierCountTMP.text = SoldierCount.ToString();
        shake = GetComponent<Shake>();

        SoldierSprite = GetComponent<SpriteRenderer>().sprite;
        MaxSoldierImg = GetComponentInChildren<Image>();
        Map.transform.parent = transform.parent;
        levelIndex = PlayerPrefs.GetInt(GameData.Instante.LevelIndex, 0);
        AllLevelIndex = PlayerPrefs.GetInt(GameData.Instante.AllLevelsIndex, 1);
        IsSpawn = true;
        GetPlayerSoldierCountValue();

        StartCoroutine(VisualUpdate());

    }
    public float attacktime = 0;
    private void Update()
    {
        if (!IsEnemy)
        {
            if (AttackSecond)
                attacktime += Time.deltaTime * SpawnTime;
            if (attacktime >= 5)
            {
                attacktime = 0;
                AttackSecond = false;
            }
        }
        if (MaxSoldierCount > SoldierCount)
            MaxSoldierImg.enabled = false;
        else
            MaxSoldierImg.enabled = true;


        if (Input.GetKeyDown(KeyCode.L))
        {
            if (gameObject.layer == 6 && !IsGameFinish && GetComponent<Enemy>() != null)
                GetComponent<Enemy>().TargetEnemyFind();
        }

    }
    void GetPlayerSoldierCountValue()
    {
        if (!IsEnemy)
        {
            if (AllLevelIndex <= 100)
            {
                SoldierSprite = ColorManager.Instante.SoldierIconSprite[PlayerPrefs.GetInt(GameData.Instante.SoldierIcon)];
                MySprite = ColorManager.Instante.PlayerIconSprite[PlayerPrefs.GetInt(GameData.Instante.PlayerIcon)];
                GetComponent<SpriteRenderer>().sprite = MySprite;
                MaxSoldierCount = SoldierCountData.Instante.soldierCounts[levelIndex].MyMaxSoldierCount;
            }
            else
            {
                SoldierSprite = ColorManager.Instante.SoldierIconSprite[PlayerPrefs.GetInt(GameData.Instante.SoldierIcon)];
                MySprite = ColorManager.Instante.PlayerIconSprite[PlayerPrefs.GetInt(GameData.Instante.PlayerIcon)];
                GetComponent<SpriteRenderer>().sprite = MySprite;
                MaxSoldierCount = SoldierCountData.Instante.soldierCounts[27].MyMaxSoldierCount + PlayerPrefs.GetInt(GameData.Instante.EnemySoldierCount);
            }
        }
        else
        {
            if (AllLevelIndex <= 100)
            {
                if (!FirstEnemy)
                {  // SoldierCountData.Instante.soldierCounts[levelIndex].EnemyStartSoldierCount
                    SoldierCount = SoldierCountData.Instante.soldierCounts[levelIndex].EnemyStartSoldierCount + (int)(PlayerPrefs.GetInt(GameData.Instante.StartUnitsCount, 10) * (25 / 100f));
                    SoldierCountTMP.text = SoldierCount.ToString();
                }

                //SoldierCountData.Instante.soldierCounts[levelIndex].EnemySpawnTime
                MaxSoldierCount = SoldierCountData.Instante.soldierCounts[levelIndex].EnemyMaxSoldierCount;
                SpawnTime = SoldierCountData.Instante.soldierCounts[levelIndex].EnemySpawnTime;
                // Map.whichColor = MapColor.WhichColor.Green;
            }
            else
            {
                if (!FirstEnemy)
                {  // SoldierCountData.Instante.soldierCounts[levelIndex].EnemyStartSoldierCount
                    SoldierCount = PlayerPrefs.GetInt(GameData.Instante.StartUnitsCount, 10) + (int)(PlayerPrefs.GetInt(GameData.Instante.StartUnitsCount, 10) * (10 / 100f));
                    SoldierCountTMP.text = SoldierCount.ToString();
                }

                //SoldierCountData.Instante.soldierCounts[levelIndex].EnemySpawnTime
                MaxSoldierCount = SoldierCountData.Instante.soldierCounts[27].EnemyMaxSoldierCount + PlayerPrefs.GetInt(GameData.Instante.EnemySoldierCount);
                SpawnTime = PlayerPrefs.GetFloat(GameData.Instante.PoduceSpeedTime, 1);
                // Map.whichColor = MapColor.WhichColor.Green;
            }
        }
    }
    public void GetMyPlayerSoldierCount()
    {
        IsSpawn = true;
        SoldierCountTMP = GetComponentInChildren<TextMeshProUGUI>();
        SoldierCount = PlayerPrefs.GetInt(GameData.Instante.StartUnitsCount, 10);
        SoldierCountTMP.text = SoldierCount.ToString();

        SpawnTime = PlayerPrefs.GetFloat(GameData.Instante.PoduceSpeedTime, 1.2f);

    }
    public void GetEnemySoldierValue()
    {
        if (!FirstEnemy)
        {  // SoldierCountData.Instante.soldierCounts[levelIndex].EnemyStartSoldierCount
            SoldierCount = SoldierCountData.Instante.soldierCounts[levelIndex].EnemyStartSoldierCount + (int)(PlayerPrefs.GetInt(GameData.Instante.StartUnitsCount, 10) * (25 / 100f));
            SoldierCountTMP = GetComponentInChildren<TextMeshProUGUI>();
            SoldierCountTMP.text = SoldierCount.ToString();
        }

        //SoldierCountData.Instante.soldierCounts[levelIndex].EnemySpawnTime
        MaxSoldierCount = SoldierCountData.Instante.soldierCounts[levelIndex].EnemyMaxSoldierCount;
        SpawnTime = SoldierCountData.Instante.soldierCounts[levelIndex].EnemySpawnTime + PlayerPrefs.GetFloat(GameData.Instante.PoduceSpeedTime, 1) * (25 / 100f);
    }
    public void SetMyAreaMapColor()
    {
        int ColorIndex = PlayerPrefs.GetInt(GameData.Instante.PlayerColor, 0);
        int PlayerIconIndex = PlayerPrefs.GetInt(GameData.Instante.PlayerIcon, 0);
        int SoldierIconIndex = PlayerPrefs.GetInt(GameData.Instante.SoldierIcon, 0);
        //Map = GetComponentInChildren<MapColor>();
        Map.FirstColor = ColorManager.Instante.mapColors[ColorIndex].LightColor;
        Map.DarkColor = ColorManager.Instante.mapColors[ColorIndex].DarkColor;
        Map.IconColor = ColorManager.Instante.mapColors[ColorIndex].IconColor;
        Map.GetComponent<SpriteRenderer>().color = ColorManager.Instante.mapColors[ColorIndex].LightColor;
        GetComponent<SpriteRenderer>().sprite = ColorManager.Instante.PlayerIconSprite[PlayerIconIndex];
        GetComponent<SpriteRenderer>().color = Map.IconColor;
        SoldierSprite = ColorManager.Instante.SoldierIconSprite[SoldierIconIndex];
    }
    public void startSpawnSoldier()
    {
        if (IsEnemy && !IsGameFinish)
        {
            AttackEnumator = AttackTime();
            StartCoroutine(AttackEnumator);
        }

        IsSpawn = true;
        GetVeriable();
    }
    public void GetVeriable()
    {

        SoldierCountTMP = GetComponentInChildren<TextMeshProUGUI>();


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

        //Map =GetComponentInChildren<MapColor>();
        GetComponent<SpriteRenderer>().color = Map.IconColor;
        enumerator = Spawn();
        StartCoroutine(enumerator);
    }
    public void FirstColorChange(Soldier spawnSoldier)
    {
        SoldierCount = 0;
        EnemyPlayer = spawnSoldier;
        Soldier = EnemyPlayer.soldier;
        Map = GetComponentInChildren<MapColor>();
        Map.FirstColor = EnemyPlayer.LightColor;
        Map.IconColor = EnemyPlayer.IconColor;
        Map.GetComponent<SpriteRenderer>().material.color = new Color(1, 1, 1, 1);
        Map.GetComponent<SpriteRenderer>().color = EnemyPlayer.LightColor;
        GetComponent<SpriteRenderer>().color = Map.IconColor;
        MySprite = EnemyPlayer.PlayerIcon;
        GetComponent<SpriteRenderer>().sprite = MySprite;
        Map.DarkColor = EnemyPlayer.DarkColor;
        MaxSoldierCount = EnemyPlayer.MaxSoldierCount;
        PlayerIndex = EnemyPlayer.PlayerIndex;
        SpawnTime = EnemyPlayer.SpawnTime;
        gameObject.layer = EnemyPlayer.soldierLayer;
        gameObject.tag = EnemyPlayer.tag;

        UIManager.Instante.SliderBarValueIncrease(EnemyPlayer.PlayerIndex);
        if (EnemyPlayer.soldierLayer == 8 && GetComponent<SelectArea>() == null)
        {
            //UIManager.Instante.SliderBarValueIncrease(0);
            gameObject.AddComponent<SelectArea>();
            Destroy(GetComponent<Enemy>());
            IsEnemy = false;
        }
        else if (EnemyPlayer.soldierLayer == 6 && GetComponent<Enemy>() == null)
        {
            if (GetComponent<SelectArea>() != null)
                GetComponent<SelectArea>().ResetLine();


            gameObject.AddComponent<Enemy>();
            Destroy(GetComponent<SelectArea>());
            IsEnemy = true;
            StartCoroutine(AttackTime());
        }

        UIManager.Instante.SoldierAreas.Add(GetComponent<SpawnSoldier>());


        GetVeriable();
    }
    public IEnumerator Spawn()
    {

        
        WaitForSeconds delay = new WaitForSeconds(1 / SpawnTime);
        if (IsEnemy)
            UIManager.Instante.IsLastSoldier = false;
       


        //Map.DarkColorChange(ColorChangeTime);
        while (MaxSoldierCount > SoldierCount && !UIManager.Instante.IsGameFinish)
        {
            yield return delay;
            SoldierCount++;
            SoldierCountTMP.text = SoldierCount.ToString();
            UIManager.Instante.BarFillAmount(PlayerIndex, 1);
            //if (!IsEnemy)
            //    SpawnTime = PlayerPrefs.GetFloat(GameData.Instante.PoduceSpeedLevel);
            

        }
        if (MaxSoldierCount <= SoldierCount)
        {
            attacking = true;

        }



    }


   


    public IEnumerator AttackTime()
    {

        int randomWaitTime = Random.Range(10, 16);

        WaitForSeconds delay = new WaitForSeconds(randomWaitTime);
        yield return delay;
        if (gameObject.layer == 6 && !IsGameFinish && GetComponent<Enemy>() != null)
            GetComponent<Enemy>().TargetEnemyFind();
    }
    Vector3 TargetDirection;
    void MySoldier(SoldierAttack _soldier)
    {
        Soldiers.Add(_soldier.gameObject);
        UIManager.Instante.iHaveMovingPlayersCounts[PlayerIndex].movingPlayers.Add(_soldier.gameObject);
        _soldier.GetComponent<SpriteRenderer>().color = Map.IconColor;
        _soldier.GetComponent<SpriteRenderer>().sprite = SoldierSprite;
        if (!IsEnemy)
            _soldier.direction = TargetDirection;
        else
            _soldier.direction = TargetEnemy.transform.position - transform.position;

        _soldier.target = TargetEnemy;
        //StartCoroutine(_soldier.GetComponent<SoldierAttack>().AttackTarget());
        _soldier.tag = gameObject.tag;

        _soldier.soldier.soldierLayer = gameObject.layer;
        _soldier.soldier.tag = gameObject.tag;

        _soldier.soldier.MaxSoldierCount = MaxSoldierCount;
        _soldier.soldier.SpawnTime = SpawnTime;
        _soldier.soldier.PlayerIcon = MySprite;
        _soldier.soldier.LightColor = Map.FirstColor;
        _soldier.soldier.DarkColor = Map.DarkColor;
        _soldier.soldier.IconColor = Map.IconColor;

        _soldier.soldier.NumberEnemyAttacking = count;
        _soldier.soldier.PlayerIndex = PlayerIndex;
        _soldier.soldier.soldier = Soldier;
        _soldier.soldier.SoldierIcon = SoldierSprite;

        SoldierCount--;
        if (SoldierCount <= 0)
            SoldierCount = 0;
        SoldierCountTMP.text = SoldierCount.ToString();
        Debug.Log("SALDIRIYOR*********" + gameObject.name);
    }
    public List<GameObject> Soldiers = new List<GameObject>();
    int count;
    public bool AttackSecond = false;
    public void GetTargetDirection(Transform target)
    {
        TargetEnemy = target;


        TargetDirection = TargetEnemy.transform.position - transform.position;
        if (!IsAttacking)
            StartCoroutine(Attack(target));
    }
    bool IsAttacking = false;

    public IEnumerator VisualUpdate()
    {
        while (!UIManager.Instante.IsGameFinish)
        {
            
            yield return new WaitForSeconds(Time.deltaTime);
            float time = (float)SoldierCount / (float)MaxSoldierCount;
            Map.DarkColorChange(time);
        }
    }

    public IEnumerator Attack(Transform target)
    {
        if (SoldierCount > 0)
        {


            AttackSecond = true;
            //MY Area Attack
            StopCoroutine(enumerator);



            if (!IsGameFinish)
            {

                TargetEnemy = target;

                if (!IsEnemy)
                    TargetDirection = TargetEnemy.transform.position - transform.position;

                IsSpawn = false;
                WaitForSeconds delay = new WaitForSeconds(0.3f);
                count = SoldierCount;
                FirstCount = count;
                NumberEnemyAttacking = SoldierCount;

                // Map.LightColorChange(count * 0.075f);


                //Soldier Spawn
                int lineup = 0;

                lineup = FirstCount / 4;

                for (int i = 0; i < lineup; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        //Debug.Log("ASKER GÖNDERÝYOR: " + gameObject.name);
                        if (!IsGameFinish && SoldierCount > 0)
                        {

                            // UIManager.Instante.BarFillAmount(PlayerIndex, -1);
                            GameObject _soldier = Instantiate(Soldier, SpawnPoints[j].transform.position, Quaternion.identity);
                            MySoldier(_soldier.GetComponent<SoldierAttack>());
                            FirstCount--;
                            if (!IsEnemy)
                                SoundManager.Instante.MoveSoldierPlay();
                            else
                                UIManager.Instante.IsLastSoldier = true;

                            IsAttacking = true;
                        }


                        else
                            lineup = 0;






                    }
                    yield return delay;
                }



                if (SoldierCount > 0 && !IsGameFinish)
                {

                    for (int j = 0; j < 3; j++)
                    {
                        //Debug.Log("ASKER GÖNDERÝYOR222222: " + gameObject.name);
                        if (!IsGameFinish && SoldierCount > 0)
                        {

                            // UIManager.Instante.BarFillAmount(PlayerIndex, -1);

                            GameObject _soldier = Instantiate(Soldier, SpawnPoints[j].transform.position, Quaternion.identity);
                            MySoldier(_soldier.GetComponent<SoldierAttack>());
                            FirstCount--;
                            if (!IsEnemy)
                                SoundManager.Instante.MoveSoldierPlay();
                            else
                                UIManager.Instante.IsLastSoldier = true;

                            IsAttacking = true;
                        }





                    }

                    yield return delay;






                }



                AttackSecond = false;
                yield return delay;
                if (!IsEnemy)
                    GetComponent<SelectArea>().IsClick = false;
                IsSpawn = true;
                IsAttacking = false;
                AgainSoldierSpawn();



            }

        }
    }
    void AgainSoldierSpawn()
    {
        //IsGameFinish = false;
        StopCoroutine(enumerator);
        StopCoroutine(Attack(transform));
        enumerator = Spawn();
        StartCoroutine(enumerator);
        if (IsEnemy)
        {

            AttackEnumator = AttackTime();
            StartCoroutine(AttackEnumator);
        }
        else
            TargetEnemy = null;

    }

    public Soldier EnemyPlayer;
    public bool attacking = false;

    public int enemySoldierCount = 0;
    void ChangeValue()
    {

        UIManager.Instante.BarFillAmount(EnemyPlayer.PlayerIndex, -1);
        //if (enemySoldierCount <= 0)
        //    enemySoldierCount = EnemyPlayer.NumberEnemyAttacking;

        if (SoldierCount > 0 )
        {

            SoldierCount--;
            SoldierCountTMP.text = SoldierCount.ToString();
            UIManager.Instante.BarFillAmount(PlayerIndex, -1);

            //enemySoldierCount--;

        }
        //if (SoldierCount > 0 && attacking)
        //{
        //    if (!IsEnemy)
        //        UIManager.Instante.IsLastSoldier = false;
        //    attacking = false;
        //    StartCoroutine(Spawn());
        //}



        else if (SoldierCount <= 0 && !IsGameFinish)
        {
            IsGameFinish = true;
          

            UIManager.Instante.SliderBarValueDecrease(PlayerIndex);
            // Map.transform.parent = transform;
            if (EnemyPlayer.soldierLayer == 8 && GetComponent<SelectArea>() == null)
            {
                //UIManager.Instante.SliderBarValueIncrease(0);
                gameObject.AddComponent<SelectArea>();
                Destroy(GetComponent<Enemy>());
                IsEnemy = false;
            }
            else if (EnemyPlayer.soldierLayer == 6 && GetComponent<Enemy>() == null)
            {
                if (GetComponent<SelectArea>() != null)
                {

                    GetComponent<SelectArea>().ResetLine();
                }



                gameObject.AddComponent<Enemy>();
                Destroy(GetComponent<SelectArea>());

                IsEnemy = true;
                // StartCoroutine(AttackTime());
            }

            gameObject.layer = EnemyPlayer.soldierLayer;
            gameObject.tag = EnemyPlayer.tag;
            MySprite = EnemyPlayer.PlayerIcon;

            GetComponent<SpriteRenderer>().sprite = MySprite;
            SoldierSprite = EnemyPlayer.SoldierIcon;
            MaxSoldierCount = EnemyPlayer.MaxSoldierCount;
            SpawnTime = EnemyPlayer.SpawnTime;
            PlayerIndex = EnemyPlayer.PlayerIndex;

            Map.GetComponent<SpriteRenderer>().color = EnemyPlayer.LightColor;
            Map.FirstColor = EnemyPlayer.LightColor;
            Map.IconColor = EnemyPlayer.IconColor;
            Map.DarkColor = EnemyPlayer.DarkColor;

            GetComponent<SpriteRenderer>().color = Map.IconColor;


            attacking = false;
            if (!IsEnemy)
                UIManager.Instante.IsLastSoldier = false;
            //else
            //    UIManager.Instante.IsLastSoldier = true;
            UIManager.Instante.SliderBarValueIncrease(PlayerIndex);


            EnemyPlayer = null;


            Invoke("IsFinishChange", 1);
            AgainSoldierSpawn();

        }
        shake.ShakeFunc();

    }
    public void IsFinishChange()
    {
        IsGameFinish = false;
    }
    public Soldier FirstSoldier;

    public void EnemyPlayerFunc(GameObject _enemyplayer)
    {

        if (_enemyplayer.gameObject.tag == gameObject.tag)
        {
            Debug.Log("Ayný asker saldýrdý*********" + gameObject.name);
            SoldierCount++;
            SoldierCountTMP.text = SoldierCount.ToString();
            // UIManager.Instante.BarFillAmount(PlayerIndex, 1);
            //UIManager.Instante.BarFillAmount(EnemyPlayer.PlayerIndex, -1);





            //Map.DarkColorChange(ColorChangeTime);
            shake.ShakeFunc();
            if (SoldierCount >= MaxSoldierCount)
            {
                attacking = true;

            }


            if (!IsEnemy)
            {
                SoundManager.Instante.DamageSoundPlay();
                GetComponent<SelectArea>().Target = null;
            }

        }
        else if (_enemyplayer.gameObject.tag != gameObject.tag)
        {
            EnemyPlayer = _enemyplayer.gameObject.GetComponent<SoldierAttack>().soldier;
          
            //StopCoroutine(Spawn());

            ChangeValue();
            if (_enemyplayer.gameObject.CompareTag("Player"))
            {

                SoundManager.Instante.DamageSoundPlay();
                Vibration.VibratePeek();
            }



        }

    }
    public void ResetValue()
    {
        // IsGameFinish = true;
        StopCoroutine(enumerator);
        StopCoroutine(AttackTime());
        StopCoroutine(Attack(transform));
    }
}
