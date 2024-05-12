using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Networking;

public class OfflineEarning : MonoBehaviour
{
    public static OfflineEarning Instante;
    [SerializeField] GameObject Arrow;
    [SerializeField] float rotate;

    [SerializeField] GameObject LightImg;
    [SerializeField] GameObject NoThankBtn;
    [SerializeField] int OfflineEarningCoinAmount = 500;
    [SerializeField] TextMeshProUGUI AdsCoinAmount_TMP;
    [SerializeField] TextMeshProUGUI Time_TMP;
    [SerializeField] TextMeshProUGUI OfflineEarningCoinAmount_TMP;
    [HideInInspector] public int AdsCoinAmount = 0;

    private void Awake()
    {
        Instante = this;
    }

    void Start()
    {
        LeanTween.rotateAround(Arrow, new Vector3(0, 0, 40), -80, 0.6f).setLoopPingPong().setOnUpdate((float value)=>rotate=value);
        
        
        string PreviousDateTime= PlayerPrefs.GetString(GameData.Instante.OfflineTime, " ");
        if (!PreviousDateTime.Equals(" "))
        {
            DateTime previousDate = DateTime.Parse(PreviousDateTime);
            DateTime CurrentTime = DateTime.Now;
            TimeSpan timeSpan = CurrentTime - previousDate;
            if (timeSpan.TotalHours <=12)
            {
                
                Time_TMP.text = timeSpan.Hours + "H:" + timeSpan.Minutes + "M:" + timeSpan.Seconds + "S";
                Debug.Log(timeSpan.TotalSeconds);
                OfflineEarningCoinAmount = (int)(timeSpan.TotalSeconds) * PlayerPrefs.GetInt(GameData.Instante.OfflineAmount, 50);
                OfflineEarningCoinAmount /= 3600;
                if (OfflineEarningCoinAmount <= 1)
                    OfflineEarningCoinAmount = 1;
                OfflineEarningCoinAmount_TMP.text = OfflineEarningCoinAmount.ToString("0");
            }
            else
            {
                Time_TMP.text = 12 + "H:" + 0 + "M:" + 0 + "S";
               
                OfflineEarningCoinAmount = (int)(12) * PlayerPrefs.GetInt(GameData.Instante.OfflineAmount, 50);
                //OfflineEarningCoinAmount /= 3600f;
                OfflineEarningCoinAmount_TMP.text = OfflineEarningCoinAmount.ToString("0");
            }
            PlayerPrefs.SetString(GameData.Instante.OfflineTime, " ");
        }
       
       
        Invoke("NoThanksBtnActive", 3); 
    }
   void NoThanksBtnActive()
    {
        NoThankBtn.SetActive(true);
        LeanTween.scale(NoThankBtn, Vector2.one, 0.5f);
    }
    public void PassiveOfflinePanel()
    {
        gameObject.SetActive(false);
        UIManager.Instante.TotalCoinAmount += (int)AdsCoinAmount;
        PlayerPrefs.SetInt(GameData.Instante.TotalCoin, UIManager.Instante.TotalCoinAmount);
        UIManager.Instante.TotalCoinTextAnim(1);
    }
    // Update is called once per frame
    void Update()
    {
        LightImg.transform.Rotate(0, 0, 20 * Time.deltaTime);
       if((rotate<=0 && rotate>=-10) || (rotate <=-70 && rotate >=-80))
        {
            AdsCoinAmount = OfflineEarningCoinAmount * 2;
            AdsCoinAmount_TMP.text = AdsCoinAmount.ToString("0");
        }
        else if ((rotate <-10 && rotate >= -30) || (rotate < -50 && rotate > -70))
        {
            AdsCoinAmount = OfflineEarningCoinAmount * 3;
            AdsCoinAmount_TMP.text = AdsCoinAmount.ToString("0");
        }
        else if ((rotate <= -30 && rotate >= -50))
        {
            AdsCoinAmount = OfflineEarningCoinAmount * 4;
            AdsCoinAmount_TMP.text = AdsCoinAmount.ToString("0");
        }
    }
    
}
