using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instante;
    public List<GameObject> Levels = new List<GameObject>();
    public Camera camera;
    public Vector2 CamFirstPos;
    public Vector2 CamMapMovePos;
    public float CamFieldView;
    public float firstFieldView;
    public List<Map> maps = new List<Map>();

    [Header("Colors")]
    public Color EmptyCityGreyColor;
    [SerializeField] Sprite Mysprite;
    [SerializeField] Color MyTransparentColor;

    [Header("Level Index")]
    public int LevelIndex = 0;
    public int MapIndex = 0;
    private void Awake()
    {
        Instante = this;
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 45;
        camera = Camera.main;
        CamFirstPos = camera.transform.position;
        firstFieldView = camera.orthographicSize;
    }
    void Start()
    {
        StopAllCoroutines();
        LeanTween.cancelAll();
        GetLevelIndex();
    }
    public void GetLevelIndex()
    { 
        LevelIndex = PlayerPrefs.GetInt(GameData.Instante.LevelIndex,0);
        
        MapIndex = PlayerPrefs.GetInt(GameData.Instante.MapIndex,0);
        if (MapIndex >= 4)
            MapIndex = 0;
        for (int i = 0; i < Levels.Count; i++)
        {
            Levels[i].SetActive(false);
        }
        Levels[LevelIndex].SetActive(true);
        SelectMap();
    }
    public void SelectMap()
    {
        for (int j = 0; j < MapIndex; j++)
        {
            for (int i = 0; i < maps[LevelIndex].GrayMap[j].transform.childCount; i++)
            {
                maps[LevelIndex].GrayMap[j].transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1,1);
                maps[LevelIndex].GrayMap[j].transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = ColorManager.Instante.PlayerIconSprite[PlayerPrefs.GetInt(GameData.Instante.PlayerIcon)];
                maps[LevelIndex].GrayMap[j].transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().color = ColorManager.Instante.mapColors[PlayerPrefs.GetInt(GameData.Instante.PlayerColor)].LightColor;
                maps[LevelIndex].GrayMap[j].transform.GetChild(i).GetComponent<SpriteRenderer>().color= ColorManager.Instante.mapColors[PlayerPrefs.GetInt(GameData.Instante.PlayerColor)].IconColor;
            }
            Debug.Log("harita renk deðiþti");
        }
        Debug.Log("haritalar aktif oldu");
        maps[LevelIndex].ActiveMap[MapIndex].SetActive(true);
        maps[LevelIndex].GrayMap[MapIndex].SetActive(false);
        Debug.Log("haritalar aktif oldu2222222");

        UIManager.Instante.GiftBar(maps[LevelIndex].levelManagers.Count,MapIndex);
        Invoke("SelectRedCircle", 1);
    }
    void SelectRedCircle()
    {
        MyArea.Instante.GetEmptyArea(maps[LevelIndex].levelManagers[MapIndex]);
    }
    public void PreviousMapClose()
    {
        maps[LevelIndex].ActiveMap[MapIndex-1].SetActive(false);
        maps[LevelIndex].GrayMap[MapIndex-1].SetActive(true);
    }
    public void StartFight()
    {
       
        CamMapMovePos = maps[LevelIndex].CamPos[MapIndex];
        CamFieldView = maps[LevelIndex].CamSize[MapIndex];
        LeanTween.move(camera.gameObject, CamMapMovePos, 1f);
        LeanTween.value(camera.gameObject, firstFieldView, CamFieldView, 1f).setOnUpdate((float value)=>camera.orthographicSize = value).setOnComplete(()=>
        {   
            maps[LevelIndex].levelManagers[MapIndex].PlayersStartSpawnSoldier();
            OtherMapGrayColor();
            SoundManager.Instante.StartWarPlay();
          UIManager.Instante.IsGameStart = true;
        });
    }
    void OtherMapGrayColor()
    {
        for (int j = 0; j < maps[LevelIndex].GrayMap.Count; j++)
        {
            for (int i = 0; i < maps[LevelIndex].GrayMap[j].transform.childCount; i++)
            {
                Color MapColor = maps[LevelIndex].GrayMap[j].transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().color;
                Color IconColor = maps[LevelIndex].GrayMap[j].transform.GetChild(i).GetComponent<SpriteRenderer>().color;
                maps[LevelIndex].GrayMap[j].transform.GetChild(i).GetComponent<SpriteRenderer>().color = new Color(IconColor.r, IconColor.g, IconColor.b, 0.4f);
                maps[LevelIndex].GrayMap[j].transform.GetChild(i).GetChild(0).GetComponent<SpriteRenderer>().color = new Color(MapColor.r,MapColor.g,MapColor.b,0.4f);
                
            }
        }
        
    }
    public void CamGotoFirstPos()
    {
        LeanTween.move(camera.gameObject, CamFirstPos,0.7f);
        LeanTween.value(camera.gameObject, CamFieldView, firstFieldView, 0.7f).setOnUpdate((float value) => camera.orthographicSize = value).setOnComplete(() =>
        {
            UIManager.Instante.MainMenuAcive();
        });
    }
    
    public void YouWonTheGame()
    {
        LeanTween.move(camera.gameObject, CamFirstPos, 0.7f);
        LeanTween.value(camera.gameObject, CamFieldView, firstFieldView, 0.7f).setOnUpdate((float value) => camera.orthographicSize = value).setOnComplete(() =>
        {
            UIManager.Instante.YouWonPanelActive();
        });
    }
    public void GameFail()
    {
        int players = maps[LevelIndex].levelManagers[MapIndex].Players.Count;
        int EnemyPlayers = 0;
        for (int i = 0; i < players; i++)
        {
            if (maps[LevelIndex].levelManagers[MapIndex].Players[i].GetComponent<SpawnSoldier>() != null)
            {

                maps[LevelIndex].levelManagers[MapIndex].Players[i].GetComponent<SpawnSoldier>().ResetValue();
                EnemyPlayers++;
               
            }

        }
        GameFinishAnim.Instante.MarkRivalPlayers(EnemyPlayers, maps[LevelIndex].ActiveMap);
    }
}
[System.Serializable]
public class Map
{
    public List<GameObject> ActiveMap = new List<GameObject>();
    public List<GameObject> GrayMap = new List<GameObject>();
    public List<LevelManager> levelManagers = new List<LevelManager>();
    public Vector2[] CamPos;
    public float[] CamSize;
}

