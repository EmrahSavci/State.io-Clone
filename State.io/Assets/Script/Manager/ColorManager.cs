using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instante;

    public List<MapColors> mapColors = new List<MapColors>();
    public List<Sprite> PlayerIconSprite = new List<Sprite>();
    public List<Sprite> SoldierIconSprite = new List<Sprite>();
    private void Awake()
    {
        Instante = this;
    }
    void Start()
    {
        
    }

    
}
[System.Serializable]
public class MapColors
{
    public Color LightColor;
    public Color DarkColor;
    public Color IconColor;
    public Color BarColor;
}
