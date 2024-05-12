using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GiftBox : MonoBehaviour
{
    public static GiftBox Instante;
    [SerializeField] GameObject GiftBoxAnim;
    [SerializeField] GameObject GiftImgBg;
    [SerializeField] Image GiftItemSprite;
    [SerializeField] Button ClaimBtn;
    [SerializeField] GameObject NoThanksBtn;
    [Header("purchased icon indexes")]
    [SerializeField] List<int> PlayerIconIndex =new List<int>();
    [SerializeField] List<int> SoldierIconIndex = new List<int>();

    [Header("Icons Indexs")]
    [SerializeField] List<int> PlayerIconIndex2 = new List<int>();
    [SerializeField] List<int> SoldierIconIndex2 = new List<int>();
    [Header("Icon Sprite List")]
    [SerializeField] List<Sprite> PlayerIconSprite=new List<Sprite>();
    [SerializeField] List<Sprite> SoldierIconSprite=new List<Sprite>();

    private void Awake()
    {
        Instante = this;
    }
    void Start()
    {
        
        PlayerIconIndex = PlayerPrefsExtra.GetList<int>(GameData.Instante.PlayerIconIndexs, new List<int>());
        SoldierIconIndex = PlayerPrefsExtra.GetList<int>(GameData.Instante.SoldierIconIndexs, new List<int>());
        if(PlayerPrefs.GetInt("FirsClaimPlayerIcon")==1)
        {
            PlayerIconIndex2 = PlayerPrefsExtra.GetList<int>("BuyingPlayerIcon", new List<int>());
           
        }
        if (PlayerPrefs.GetInt("FirsClaimSoldierIcon") == 1)
        {
            
            SoldierIconIndex2 = PlayerPrefsExtra.GetList<int>("BuyingSoldierIcon", new List<int>());
        }
        PlayerIconSprite = ColorManager.Instante.PlayerIconSprite;
        SoldierIconSprite = ColorManager.Instante.SoldierIconSprite;
        LeanTween.rotateAround(GiftBoxAnim, new Vector3(0, 0, 10), -60, 0.5f).setRepeat(4).setLoopPingPong().setOnComplete(()=>
        {
            ClaimBtn.interactable = true;
            SelectRewardIcon();

        });
    }
    private void Update()
    {
        if(IsGiftGet)
        {
            IsGiftGet = false;
            if (randomItem == 0)
            {
                PlayerIconIndex.Add(PlayerIconIndex2[randomPlayerItem]);
               
                PlayerPrefsExtra.SetList<int>(GameData.Instante.PlayerIconIndexs, PlayerIconIndex);
                PlayerPrefs.SetInt(GameData.Instante.PlayerIcon, PlayerIconIndex2[randomPlayerItem]);
                PlayerIconIndex2.Remove(PlayerIconIndex2[randomPlayerItem]);
                PlayerPrefsExtra.SetList<int>("BuyingPlayerIcon", PlayerIconIndex2);
                PlayerPrefs.SetInt("FirsClaimPlayerIcon", 1);
            }
            else
            {
                SoldierIconIndex.Add(SoldierIconIndex2[randomSoldierItem]);
               
                PlayerPrefsExtra.SetList<int>(GameData.Instante.SoldierIconIndexs, SoldierIconIndex);
                PlayerPrefs.SetInt(GameData.Instante.SoldierIcon, SoldierIconIndex2[randomSoldierItem]);
                SoldierIconIndex2.Remove(SoldierIconIndex2[randomSoldierItem]);
                PlayerPrefsExtra.SetList<int>("BuyingSoldierIcon", SoldierIconIndex2);

               
                PlayerPrefs.SetInt("FirsClaimSoldierIcon", 1);
            }

            UIManager.Instante.GiftPanelNoThanksBtn();
        }
    }
    public int randomItem, randomPlayerItem, randomSoldierItem;
    void SelectRewardIcon()
    {
        GiftImgBg.SetActive(true);
        GiftBoxAnim.SetActive(false);
        randomItem = Random.Range(0, 2);
        AgainSelectIcon:
       
        if (randomItem==0 && PlayerIconIndex2.Count>1)
        {
            randomPlayerItem = Random.Range(1, PlayerIconIndex2.Count-1);
            GiftItemSprite.sprite = PlayerIconSprite[PlayerIconIndex2[randomPlayerItem]];
            return;
        }
        else
        {
            randomItem = 1;
        }
         if(randomItem>0 && SoldierIconIndex2.Count>1)
        {
            randomSoldierItem = Random.Range(1, SoldierIconIndex2.Count-1);
            GiftItemSprite.sprite = SoldierIconSprite[SoldierIconIndex2[randomSoldierItem]];
            return;
        }
         else
        {
            if (PlayerIconIndex2.Count >1)
            {
                randomItem = 0;
                goto AgainSelectIcon;
            }
            else
                UIManager.Instante.GiftPanelNoThanksBtn();
        }
        //else
        //{
        //     randomPlayerItem = Random.Range(1, PlayerIconSprite.Count);
            
        //        GiftItemSprite.sprite = PlayerIconSprite[randomPlayerItem];
               

            
        //}
    }
    public bool IsGiftGet = false;
    public void GetRewardIcon()
    {
        IsGiftGet = true;
        
    }
   
   
}
