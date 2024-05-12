using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameFinishAnim : MonoBehaviour
{
    public static GameFinishAnim Instante;

    [SerializeField] GameObject[] Ballon;
    [SerializeField] float[] MoveYPos;
    [SerializeField] Image TimerBar;
    [SerializeField] GameObject NoThanksBtn;
    [SerializeField] GameObject Circle;
    [SerializeField] Transform CircleParent;

    [SerializeField] Sprite sprite;

    [Header("AirPort")]
    [SerializeField] GameObject[] AirPort;
    [SerializeField] Transform[] AirPortMovePos;
    [SerializeField] Vector3[] AirPortFirstPos;
    public bool IsYouLosePanelActive = false;
    bool IsAdsShow = true;
    int levelIndex = 0;
    private void Awake()
    {
        Instante = this;
    }

    private void Update()
    {

    }
    void Start()
    {   
        //AdsManager.Instante.AgainLoadRewarAds("Rewarded Ad_MoreUnits");
        levelIndex = PlayerPrefs.GetInt(GameData.Instante.LevelIndex, 0);
        
        for (int i = 0; i < AirPort.Length; i++)
        {
            AirPortFirstPos[i] = AirPort[i].transform.position;
        }
        for (int i = 0; i < Ballon.Length; i++)
        {
            Vector2 firstPos = Ballon[i].transform.localPosition;
            LeanTween.rotateZ(Ballon[i], 40,1.5f).setEaseLinear().setLoopPingPong();
            LeanTween.moveLocalY(Ballon[i],MoveYPos[i], 6);
        }
        LeanTween.value(1, 0, 8).setOnUpdate((float value) => TimerBar.fillAmount = value).setOnComplete(() =>
        {
            UIManager.Instante.levelFailNoThanks(IsAdsShow);

        });
        LeanTween.scale(NoThanksBtn, Vector2.one, 1);
        Invoke("YouLosePanelActive", 9);
    }
    void YouLosePanelActive()
    { if(!IsYouLosePanelActive)
        UIManager.Instante.levelFailNoThanks(IsAdsShow);
    }
    bool IsFirstEnd = false;
    public void ResetFinishPanel()
    {
        for (int i = 0; i < AirPort.Length; i++)
        {
            AirPort[i].transform.position=AirPortFirstPos[i];
        }
        for (int i = 0; i < transform.childCount - 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int i = 0; i < CircleParent.transform.childCount; i++)
        {
            CircleParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        TimerBar.fillAmount = 1;
        LeanTween.value(1, 0, 8).setOnUpdate((float value) => TimerBar.fillAmount = value);
        LeanTween.scale(NoThanksBtn, Vector2.one, 1);
    }
    public List<GameObject> RivalPlayers = new List<GameObject>();
    public void MarkRivalPlayers(int RivalCharacterCount,List<GameObject> PlayersPos)
    {
        StopAllCoroutines();
        int count = 0;
        if (IsFirstEnd)
            ResetFinishPanel();
        //for (int i = 0; i < RivalCharacterCount; i++)
        //{
        //    if (PlayersPos[GameManager.Instante.MapIndex].transform.GetChild(i).gameObject.layer == 6)
        //        count++;
        //}
        RivalPlayers.Clear();
        print("GameFinishAnim info1 :" + GameManager.Instante.MapIndex);
        print("GameFinishAnim info2 :" + PlayersPos[GameManager.Instante.MapIndex].transform.childCount);
        print("GameFinishAnim info3 :" + RivalCharacterCount);
        print("GameFinishAnim info4 :" + count);
        for (int i = 0; i < RivalCharacterCount-1; i++)
        {  if(PlayersPos[GameManager.Instante.MapIndex].transform.GetChild(i).gameObject.layer==6)
            {
                GameObject players = PlayersPos[GameManager.Instante.MapIndex].transform.GetChild(i).gameObject;
                
                
                RivalPlayers.Add(players);
                Vector3 CirclePos = Camera.main.WorldToScreenPoint(players.transform.position);
                GameObject _circle = Instantiate(Circle, CirclePos, Quaternion.identity, CircleParent);
                LeanTween.rotateAroundLocal(_circle.GetComponent<RectTransform>(), new Vector3(0, 0, 10), 360, 2).setLoopPingPong();
            }
            
        }
        UIManager.Instante.SoldierStop = true;
        IsFirstEnd = true;
    }
    [SerializeField] GameObject NewSoldier;
    public void AttackToRivalPlayers()
    {
        IsAdsShow = false;
        UIManager.Instante.IsGameFinish = false;
        //if(TimerBar.IsActive())
        LeanTween.cancel(TimerBar.gameObject);
        
        for (int i = 0; i < RivalPlayers.Count; i++)
        {    
            GameObject rivalPlayers = RivalPlayers[i];
            Destroy(rivalPlayers.GetComponent<Enemy>());
            rivalPlayers.AddComponent<SelectArea>();
            rivalPlayers.GetComponent<SpriteRenderer>().sprite = ColorManager.Instante.PlayerIconSprite[PlayerPrefs.GetInt(GameData.Instante.PlayerIcon)];
            rivalPlayers.gameObject.tag = "Player";
            rivalPlayers.layer = 8;
            SpawnSoldier spawnSoldier = rivalPlayers.GetComponent<SpawnSoldier>();
           
            //spawnSoldier.Map.transform.parent = rivalPlayers.transform;
            spawnSoldier.MaxSoldierCount = SoldierCountData.Instante.soldierCounts[levelIndex].MyMaxSoldierCount;
            spawnSoldier.SoldierCount = 0;
            spawnSoldier.SoldierCountTMP.text = spawnSoldier.SoldierCount.ToString();
            spawnSoldier.PlayerIndex = 0;
            spawnSoldier.MySprite = ColorManager.Instante.PlayerIconSprite[PlayerPrefs.GetInt(GameData.Instante.PlayerIcon)];
            spawnSoldier.SpawnTime =PlayerPrefs.GetFloat(GameData.Instante.PoduceSpeedTime,1);
            spawnSoldier.IsEnemy = false;
            spawnSoldier.SetMyAreaMapColor();
            //spawnSoldier.Map.whichColor = MapColor.WhichColor.Blue;
            //spawnSoldier.Map.GetColor();
            spawnSoldier.IsGameFinish = false;
            
            rivalPlayers.GetComponent<SpriteRenderer>().color = spawnSoldier.Map.IconColor;
            
        }
        
        for (int i = 0; i < transform.childCount-4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        UIManager.Instante.PlayersBarRemove();
        CallAirForce();
        StartCoroutine(SupportForceSpawn());
        
    }
    void CallAirForce()
    {
        for (int i = 0; i < AirPort.Length; i++)
        {
            LeanTween.move(AirPort[i], AirPortMovePos[i].position, 2);
        }
    }
   
    IEnumerator SupportForceSpawn()
    {
        UIManager.Instante.SoldierStop = false;
        UIManager.Instante.fail = false;
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        for (int i = 0; i < RivalPlayers.Count; i++)
        {
            for (int j = 0; j < 35; j++)
            {
                GameObject _soldier = Instantiate(NewSoldier, RivalPlayers[i].transform.position + Random.insideUnitSphere*1.5f, Quaternion.identity);
                _soldier.GetComponent<SpriteRenderer>().color = ColorManager.Instante.mapColors[PlayerPrefs.GetInt(GameData.Instante.PlayerColor, 0)].IconColor;
                
                SoldierAttack soldierAttack = _soldier.GetComponent<SoldierAttack>();
                
                soldierAttack.soldier.soldier = NewSoldier;
                soldierAttack.soldier.soldierLayer = 8;
                soldierAttack.soldier.MaxSoldierCount = SoldierCountData.Instante.soldierCounts[levelIndex].MyMaxSoldierCount;
                soldierAttack.soldier.SpawnTime = PlayerPrefs.GetFloat(GameData.Instante.PoduceSpeedTime, 1);
                soldierAttack.tag = "Player";
                soldierAttack.soldier.tag = "Player";
                soldierAttack.soldier.PlayerIndex = 0;
                soldierAttack.soldier.PlayerIcon = sprite;

                soldierAttack.soldier.LightColor = ColorManager.Instante.mapColors[PlayerPrefs.GetInt(GameData.Instante.PlayerColor, 0)].LightColor;
                soldierAttack.soldier.DarkColor = ColorManager.Instante.mapColors[PlayerPrefs.GetInt(GameData.Instante.PlayerColor, 0)].DarkColor;
                soldierAttack.soldier.IconColor = ColorManager.Instante.mapColors[PlayerPrefs.GetInt(GameData.Instante.PlayerColor, 0)].IconColor;
                soldierAttack.direction = RivalPlayers[i].transform.position - _soldier.transform.position;
                soldierAttack.target = RivalPlayers[i].transform;
                
              
                
            }
            UIManager.Instante.BarFillAmount(0, 35);
            yield return delay;
            
        }

        UIManager.Instante.EnemyAgainAttack();
        this.gameObject.SetActive(false);
        RivalPlayers.Clear();
        
    }
    
}
