using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instante;

    public List<GameObject> Players = new List<GameObject>();
    [SerializeField] Transform TargetEmptyMap;
    [SerializeField] List<int> ColorIndex = new List<int>();
    [SerializeField] List<Color> SelectedColor = new List<Color>();
    [SerializeField] List<Color> MapColor = new List<Color>();
    private void Awake()
    {
        Instante = this;
    }
    void Start()
    {
        for (int i = 3; i < ColorManager.Instante.mapColors.Count-1; i++)
        {
            ColorIndex.Add(i);
        }

        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].GetComponent<Enemy>() != null)
                EnemySelectColor(i);
        }
    }
    
   public void PlayersStartSpawnSoldier()
    {
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].GetComponent<SpawnSoldier>() != null)
            {
              
                UIManager.Instante.PlayerCount.Add(1);
                UIManager.Instante.PlayerDie.Add(1);
                UIManager.Instante.PlayersoldierCount.Add(Players[i].GetComponent<SpawnSoldier>().SoldierCount);
                UIManager.Instante.GetSoldierCount(Players[i].GetComponent<SpawnSoldier>().SoldierCount);
                UIManager.Instante.SoldierAreas.Add(Players[i].GetComponent<SpawnSoldier>());
                UIManager.Instante.PlayerSoldierCountPercent.Add(0);
                if (Players[i].GetComponent<SelectArea>() != null)
                {
                    UIManager.Instante.PlayerColor.Add(ColorManager.Instante.mapColors[PlayerPrefs.GetInt(GameData.Instante.PlayerColor)].BarColor);
                    UIAnimation.Instante.FingerAnim(Players[i].transform, TargetEmptyMap);
                    Players[i].GetComponent<SpawnSoldier>().SetMyAreaMapColor();
                }
                else
                {
                    UIManager.Instante.PlayerColor.Add(Players[i].GetComponent<SpawnSoldier>().Map.BarColor);
                    MapColor.Add(Players[i].GetComponent<SpawnSoldier>().Map.BarColor);
                }
                    


            }
        }
        UIManager.Instante.PlayerBarSpawn();
        for (int i = 0; i < Players.Count; i++)
        {
            if (Players[i].GetComponent<SpawnSoldier>() != null)
                Players[i].GetComponent<SpawnSoldier>().startSpawnSoldier();
        }
    }
  
    public void MyPlayerIconAndColorChange()
    {
        Players[0].GetComponent<SpawnSoldier>().SetMyAreaMapColor();
    }
    public void GetEnemySoldierCount()
    {
        for (int i = 0; i < Players.Count; i++)
        { if(Players[i].GetComponent<Enemy>()!=null)
            Players[i].GetComponent<SpawnSoldier>().GetEnemySoldierValue();
        }
    }
    public List<Color> managerMapColor = new List<Color>();
    void EnemySelectColor(int Index)
    {   
        int randomColor = Random.Range(0,ColorIndex.Count);
        int index = ColorIndex[randomColor];
        Players[Index].GetComponent<SpawnSoldier>().Map.BarColor = ColorManager.Instante.mapColors[index].BarColor;
        Players[Index].GetComponent<SpawnSoldier>().Map.FirstColor = ColorManager.Instante.mapColors[index].LightColor;
        Players[Index].GetComponent<SpawnSoldier>().Map.DarkColor = ColorManager.Instante.mapColors[index].DarkColor;
        Players[Index].GetComponent<SpawnSoldier>().Map.IconColor = ColorManager.Instante.mapColors[index].IconColor;
        Players[Index].GetComponent<SpriteRenderer>().color = ColorManager.Instante.mapColors[index].IconColor;
        Players[Index].GetComponent<SpawnSoldier>().Map.GetComponent<SpriteRenderer>().color = ColorManager.Instante.mapColors[index].LightColor;
        SelectedColor.Add(Players[Index].GetComponent<SpawnSoldier>().Map.BarColor);
        managerMapColor.Add(ColorManager.Instante.mapColors[index].BarColor);
        ColorIndex.Remove(index);



    }


}
