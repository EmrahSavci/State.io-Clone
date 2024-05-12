using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EmptyArea : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    [Header("Text")]
    [SerializeField] TextMeshProUGUI FirstSoldierCountTMP;
    [SerializeField] int FirstSoldierCount;
    [SerializeField] int MaxSoldierCount;
    [SerializeField] float SpawnTime;
    [SerializeField] GameObject Map;
    [Header("Colors")]
    [SerializeField] Color LightColor;
    [SerializeField] Color DarkColor;
    float ColorValue = 0.60f;
    public GameObject RedCircle;
    public Image MaxSoldierImg;
    Soldier EnemyPlayer;
    IEnumerator CountTextChange;
    Shake shake;
    bool IsRedCircleActive = false;
    void Start()
    {
        int levelIndex = PlayerPrefs.GetInt(GameData.Instante.LevelIndex, 0);
        FirstSoldierCountTMP = GetComponentInChildren<TextMeshProUGUI>();


        spriteRenderer = Map.GetComponent<SpriteRenderer>();
        GetComponentInChildren<LineRenderer>().enabled = false;

        //spriteRenderer.color= LightColor;
        shake = GetComponent<Shake>();
        // UIManager.Instante.GetSoldierCount(FirstSoldierCount);

        MaxSoldierImg = GetComponentInChildren<Image>();
        int AlllevelIndex = PlayerPrefs.GetInt(GameData.Instante.AllLevelsIndex, 1);
        if (AlllevelIndex <= 100)
        {
            FirstSoldierCount = SoldierCountData.Instante.soldierCounts[levelIndex].EmptyPlayerSoldierCount;
            MaxSoldierCount = FirstSoldierCount;
            FirstSoldierCountTMP.text = FirstSoldierCount.ToString();
            SpawnTime = 1 / (SoldierCountData.Instante.soldierCounts[levelIndex].EnemySpawnTime);
        }
        else
        {
            FirstSoldierCount = SoldierCountData.Instante.soldierCounts[27].EmptyPlayerSoldierCount + PlayerPrefs.GetInt(GameData.Instante.EnemySoldierCount);
            MaxSoldierCount = FirstSoldierCount;
            FirstSoldierCountTMP.text = FirstSoldierCount.ToString();
            SpawnTime = 1 / (SoldierCountData.Instante.soldierCounts[27].EnemySpawnTime + PlayerPrefs.GetFloat(GameData.Instante.PoduceSpeedTime, 1) * (15 / 100f));
        }
        UIManager.Instante.EmptyAreaSoldierCount += FirstSoldierCount;
    }
    private void OnMouseOver()
    {
        MyArea.Instante.Target = this.transform;
        

    }
    
    private void OnMouseExit()
    {
        
        RedCircle.SetActive(false);
    }
    public bool FirstAttack = false;

    private void Update()
    {
        

        if (Input.GetMouseButtonUp(0))
            RedCircle.SetActive(false);
    }
    void ChangeValue()
    {

        if (FirstSoldierCount > 0)
        {

            FirstSoldierCount--;
            UIManager.Instante.EmptyAreaSoldierCount--;
            ColorValue += (0.34f / MaxSoldierCount);
            spriteRenderer.color = new Color(ColorValue, ColorValue, ColorValue, 1);
            FirstSoldierCountTMP.text = FirstSoldierCount.ToString();

            if (!FirstAttack)
            {
                FirstAttack = true;
                Map.transform.parent = transform.parent;
                CountTextChange = SoldierCountfunc();
                StartCoroutine(SoldierCountfunc());
            }
            UIManager.Instante.IsLastSoldier = false;
        }
        if (FirstSoldierCount <= 0 && GetComponent<SpawnSoldier>() == null)
        {
            Map.transform.parent = transform;
            gameObject.AddComponent<SpawnSoldier>();
            GetComponent<SpawnSoldier>().FirstEnemy = true;
            GetComponent<SpawnSoldier>().FirstColorChange(EnemyPlayer);

            StopCoroutine(CountTextChange);
            RedCircle.SetActive(false);

            Destroy(GetComponent<EmptyArea>());


        }
        shake.ShakeFunc();
    }
    IEnumerator SoldierCountfunc()
    {


        WaitForSeconds delay = new WaitForSeconds(SpawnTime);

        while (MaxSoldierCount > FirstSoldierCount)
        {
            FirstSoldierCount++;
            UIManager.Instante.EmptyAreaSoldierCount++;
            ColorValue -= (0.34f / MaxSoldierCount);
            spriteRenderer.color = new Color(ColorValue, ColorValue, ColorValue, 1);
            FirstSoldierCountTMP.text = FirstSoldierCount.ToString();

            yield return delay;
        }
        FirstAttack = false;


    }
    public void EnemySoldier(GameObject enemy)
    {
        EnemyPlayer = enemy.gameObject.GetComponent<SoldierAttack>().soldier;
        UIManager.Instante.BarFillAmount(EnemyPlayer.PlayerIndex, -1);
        // Destroy(collision.gameObject);
        ChangeValue();
        if (enemy.gameObject.CompareTag("Player"))
        {
            SoundManager.Instante.DamageSoundPlay();
            Vibration.VibratePeek();
        }
        else
            UIManager.Instante.IsLastSoldier = true;
    }

}
