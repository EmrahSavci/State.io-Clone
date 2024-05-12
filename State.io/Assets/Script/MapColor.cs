using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapColor : MonoBehaviour
{
    public Color FirstColor;
    public Color DarkColor;
    public Color IconColor;
    public Color BarColor;
    public SpriteRenderer spriteRenderer;
    public enum WhichColor
    {
        Blue,
        Red,
        Green,
        Orange,
        Pink,
        DarkBlue,
        Purple,
        Turquoise,
        Grey
    }
    public WhichColor whichColor;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        if (transform.parent.gameObject.tag == "Empty")
            whichColor = WhichColor.Grey;
        
       // GetColor();
        
    }
    public void GetColor()
    {
        LeanTween.cancel(this.gameObject);
        switch (whichColor)
        {
            case WhichColor.Blue:
                FirstColor = ColorManager.Instante.mapColors[0].LightColor;
                DarkColor = ColorManager.Instante.mapColors[0].DarkColor;
                IconColor = ColorManager.Instante.mapColors[0].IconColor;
                BarColor=ColorManager.Instante.mapColors[0].BarColor;
                break;
            case WhichColor.Red:
                FirstColor = ColorManager.Instante.mapColors[6].LightColor;
                DarkColor = ColorManager.Instante.mapColors[6].DarkColor;
                IconColor = ColorManager.Instante.mapColors[6].IconColor;
                BarColor = ColorManager.Instante.mapColors[6].BarColor;
                break;
            case WhichColor.Green:
                FirstColor = ColorManager.Instante.mapColors[3].LightColor;
                DarkColor = ColorManager.Instante.mapColors[3].DarkColor;
                IconColor = ColorManager.Instante.mapColors[3].IconColor;
                BarColor = ColorManager.Instante.mapColors[3].BarColor;
                break;
            case WhichColor.Orange:
                FirstColor = ColorManager.Instante.mapColors[4].LightColor;
                DarkColor = ColorManager.Instante.mapColors[4].DarkColor;
                IconColor = ColorManager.Instante.mapColors[4].IconColor;
                BarColor = ColorManager.Instante.mapColors[4].BarColor;
                break;
            case WhichColor.Grey:
                FirstColor = ColorManager.Instante.mapColors[9].LightColor;
                DarkColor = ColorManager.Instante.mapColors[9].DarkColor;
                IconColor = ColorManager.Instante.mapColors[9].IconColor;
                BarColor = ColorManager.Instante.mapColors[9].BarColor;
                break;
            default:
                break;

        }
        spriteRenderer.color = FirstColor;
    }
    public void DarkColorChange(float time)
    {

        //LeanTween.cancel(gameObject);
       
        //LeanTween.color(this.gameObject, DarkColor, time);
        spriteRenderer.color=Color.Lerp(FirstColor, DarkColor, time);

       
    }
    public void LightColorChange(float time)
    {

        //LeanTween.cancel(gameObject);

        //LeanTween.color(this.gameObject, FirstColor, time);
        spriteRenderer.color = Color.Lerp(DarkColor, FirstColor, time);
    }
    private void LateUpdate()
    {
        spriteRenderer.material.color = new Color(1, 1, 1, 1);
       
    }
}
