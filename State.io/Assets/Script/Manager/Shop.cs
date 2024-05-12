using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Shop : MonoBehaviour
{
    public static Shop Instante;

    [SerializeField] TextMeshProUGUI CoinAmountTMP;
    [Header("Tap Bar Buttons")]
    [SerializeField] Image[] TapBarBtns;
    [SerializeField] Sprite TapMenuSelectedBtnSprite;
    [SerializeField] Sprite TapMenuUnSelectedBtnSprite;

    [Header("Player Item Panels")]
    [SerializeField] GameObject[] ItemPanels;
    [SerializeField] Sprite SelectedItemSprite;
    [SerializeField] Sprite UnSelectedItemSprite;
    [Header("Circle Items")]
    [SerializeField] Image[] MapInCircle;
    [SerializeField] Image[] IconInCircle;
    [SerializeField] GameObject[] Soldiers;
    [SerializeField] Color[] MapColors;
    Transform target;
    int targetIndex = 1;
    int colorIndex = 3;
    int TargetMapColorIndex = 0;
    bool IsTargetFirstMap = false;

    [Header("Buildin Panel Items")]
    public Sprite[] PlayerIconSprites;
    [SerializeField] Image[] PlayerIconImg;

    [Header("Fighting Panel Items")]
    public Sprite[] SoldierIconSprites;
    [SerializeField] Image[] SoldierIconImg;

    [Header("Color Panel Items")]
    [SerializeField] ShopColors[] PlayerColors;

    [Header("Items data")]
    [SerializeField] List<int> playerIconIndex = new List<int>();
    [SerializeField] List<GameObject> PlayerIconGetBtn = new List<GameObject>();

    [SerializeField] List<int> SoldierIconIndex = new List<int>();
    [SerializeField] List<GameObject> SoldierIconGetBtn = new List<GameObject>();

    [SerializeField] List<int> ColorIconIndex = new List<int>();
    [SerializeField] List<GameObject> ColorGetBtn = new List<GameObject>();
    public int PlayerIcon = 0;
    public int SoldierIcons = 0;
    public int ColorIcon = 0;
    private void Awake()
    {
        Instante = this;
    }
    void Start()
    {
        PlayerIcon = PlayerPrefs.GetInt(GameData.Instante.PlayerIcon);
        SoldierIcons = PlayerPrefs.GetInt(GameData.Instante.SoldierIcon);
        ColorIcon = PlayerPrefs.GetInt(GameData.Instante.PlayerColor, 0);

        CoinAmountTMP.text = PlayerPrefs.GetInt(GameData.Instante.TotalCoin).ToString();
        ButtonColorIndex = PlayerPrefs.GetInt(GameData.Instante.PlayerColor, 0);
        SelectColor(ButtonColorIndex);
        GetShopData();
        StartCoroutine(SoldierAttackAnim());
    }
    void GetShopData()
    {
        SelectPlayerIcon(PlayerPrefs.GetInt(GameData.Instante.PlayerIcon));
        SoldierIconSelect(PlayerPrefs.GetInt(GameData.Instante.SoldierIcon));

        //Get Buttons Data
        playerIconIndex = PlayerPrefsExtra.GetList<int>(GameData.Instante.PlayerIconIndexs, new List<int>());
        SoldierIconIndex = PlayerPrefsExtra.GetList<int>(GameData.Instante.SoldierIconIndexs, new List<int>());
        ColorIconIndex = PlayerPrefsExtra.GetList<int>(GameData.Instante.ColorIconIndexs, new List<int>());

        for (int i = 0; i < playerIconIndex.Count; i++)
        {
            PlayerIconGetBtn[playerIconIndex[i]].SetActive(false);
        }
        for (int i = 0; i < SoldierIconIndex.Count; i++)
        {
            SoldierIconGetBtn[SoldierIconIndex[i]].SetActive(false);
        }
        for (int i = 0; i < ColorIconIndex.Count; i++)
        {
           ColorGetBtn[ColorIconIndex[i]].SetActive(false);
        }
    }
    int ItemIndex = 0;
    public void TapBarBtn(int BtnIndex)
    {
        ItemIndex = BtnIndex;
        for (int i = 0; i < TapBarBtns.Length; i++)
        {
            TapBarBtns[i].sprite = TapMenuUnSelectedBtnSprite;
            ItemPanels[i].SetActive(false);
        }
        TapBarBtns[BtnIndex].sprite = TapMenuSelectedBtnSprite;
        ItemPanels[BtnIndex].SetActive(true);
    }
    public void SelectPlayerIcon(int IconIndex)
    {
        IconInCircle[0].sprite = PlayerIconSprites[IconIndex];
        IconInCircle[1].sprite = PlayerIconSprites[IconIndex];
        BtnSpriteChange(0,IconIndex);
        PlayerPrefs.SetInt(GameData.Instante.PlayerIcon, IconIndex);
    }
    int PlayerIconIndex = 0;
    public void GetPlayerIcon(int IconIndex)
    {
        PlayerIconIndex = IconIndex;
    }
    public void RewardPlayerIcon()
    {
        playerIconIndex.Add(PlayerIconIndex);
        PlayerPrefsExtra.SetList<int>(GameData.Instante.PlayerIconIndexs, playerIconIndex);

        PlayerIconGetBtn[PlayerIconIndex].SetActive(false);
        SelectPlayerIcon(PlayerIconIndex);

       // AdsManager.Instante.AgainLoadRewarAds("Rewarded Ad_ShopPlayerIcon");
    }
    public void SoldierIconSelect(int IconIndex)
    {
        for (int i = 0; i < Soldiers.Length; i++)
        {
            Soldiers[i].GetComponent<Image>().sprite = SoldierIconSprites[IconIndex];
        }
        BtnSpriteChange(1,IconIndex);
        PlayerPrefs.SetInt(GameData.Instante.SoldierIcon, IconIndex);
    }
    int SoldierIcon = 0;
    public void GetSoldierIcon(int IconIndex)
    {
        SoldierIcon = IconIndex;
    }
    public void RewardSoldierIcon()
    {
        SoldierIconIndex.Add(SoldierIcon);
        PlayerPrefsExtra.SetList<int>(GameData.Instante.SoldierIconIndexs, SoldierIconIndex);

        SoldierIconGetBtn[SoldierIcon].SetActive(false);
        SoldierIconSelect(SoldierIcon);
        //AdsManager.Instante.AgainLoadRewarAds("Rewarded Ad_ShopSoldierIcon");
    }
    int ButtonColorIndex = 0;
    public void SelectColor(int ColorIndex)
    {
        ButtonColorIndex = ColorIndex;
        //Player Icon Color change in circle
        IconInCircle[0].color = PlayerColors[ColorIndex].DarkColor;
        IconInCircle[1].color = PlayerColors[ColorIndex].DarkColor;
        MapInCircle[1].color = PlayerColors[ColorIndex].MapColors[colorIndex];
        MapInCircle[0].color = PlayerColors[ColorIndex].MapColors[colorIndex];
        //Soldier Icon Color Change in Circle
        for (int i = 0; i < Soldiers.Length; i++)
        {
            Soldiers[i].GetComponent<Image>().color = PlayerColors[ColorIndex].DarkColor;
        }
       
        //Player ?con color change in buttons
        for (int i = 0; i < PlayerIconSprites.Length; i++)
        {
            PlayerIconImg[i].GetComponent<Image>().color = PlayerColors[ColorIndex].DarkColor;
        }

        //Soldier Icon Color Change in Buttons
        for (int i = 0; i < SoldierIconImg.Length; i++)
        {
            SoldierIconImg[i].GetComponent<Image>().color = PlayerColors[ColorIndex].DarkColor;
        }
        BtnSpriteChange(2,ColorIndex);
        PlayerPrefs.SetInt(GameData.Instante.PlayerColor, ButtonColorIndex);
    }
    int ColorIndex = 0;
    public void GetColor(int IconIndex)
    {
        ColorIndex = IconIndex;
    }
    public void RewardColor()
    {
        ColorIconIndex.Add(ColorIndex);
        PlayerPrefsExtra.SetList<int>(GameData.Instante.ColorIconIndexs, ColorIconIndex);

        ColorGetBtn[ColorIndex].SetActive(false);
        SelectColor(ColorIndex);
        
    }
    void BtnSpriteChange(int ItemIndex,int BtnIndex)
    {
        for (int i = 0; i < ItemPanels[ItemIndex].transform.childCount; i++)
        {
            ItemPanels[ItemIndex].transform.GetChild(i).GetComponent<Image>().sprite = UnSelectedItemSprite;
        }
        ItemPanels[ItemIndex].transform.GetChild(BtnIndex).GetComponent<Image>().sprite = SelectedItemSprite;
    }
    #region MapColor Change And Soldier Attack in Circle
    IEnumerator SoldierAttackAnim()
    {   yeniden:
        if (targetIndex > 1)
            targetIndex = 0;
        target = IconInCircle[targetIndex].transform;
        
        for (int i = 0; i < Soldiers.Length; i++)
        {
            LeanTween.move(Soldiers[i], target, 0.5f).setOnComplete(()=>TargetMapColorChange());
            MapColorChange();
            yield return new WaitForSeconds(0.5f);
            
            
        }
        
        yield return new WaitForSeconds(1f);
        IsTargetFirstMap = !IsTargetFirstMap;
        colorIndex = 3;
        TargetMapColorIndex = 0;
        targetIndex++;
        goto yeniden;
    }
     
    void TargetMapColorChange()
    {
        TargetMapColorIndex++;
        if (TargetMapColorIndex >=PlayerColors[ButtonColorIndex].MapColors.Length)
            TargetMapColorIndex = 3;
        Color color = PlayerColors[ButtonColorIndex].MapColors[TargetMapColorIndex];
        if (!IsTargetFirstMap)
        {
           
            MapInCircle[1].color = new Color(color.r, color.g, color.b);
            
        }
        else if(IsTargetFirstMap)
        {
            
            MapInCircle[0].color = new Color(color.r, color.g, color.b);
        }
    }
    void MapColorChange()
    {
        
        Color color = PlayerColors[ButtonColorIndex].MapColors[colorIndex];
        colorIndex--;
        if (colorIndex < 0)
            colorIndex = 3;
        if (targetIndex==0)
          MapInCircle[1].color = new Color(color.r,color.g,color.b);
        if (targetIndex == 1)
            MapInCircle[0].color = new Color(color.r, color.g, color.b);

    }
    #endregion
}
[System.Serializable]
public class ShopColors
{
    public Color[] MapColors;
    public Color DarkColor;
}