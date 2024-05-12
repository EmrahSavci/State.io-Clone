using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instante;

    [Header("SettingsPanel")]
    [SerializeField] GameObject SettingPanel;
    [SerializeField] GameObject[] Cark;

    [SerializeField] Sprite OnButtonSprite;
    [SerializeField] Sprite OffButtonSprite;

    [Header("AudioSource")]
    [SerializeField] AudioSource Music;
    [SerializeField] AudioSource ButtonSound;
    [SerializeField] AudioSource DamageSound;
    [SerializeField] AudioSource FindSoldier;
    [SerializeField] AudioSource WarStart;
    [SerializeField] AudioSource Go;



    [Header("Buttons")]
    [SerializeField] Image MusicBtnImg;
    [SerializeField] Image SoundBtnImg;
    [SerializeField] Image VibrateBtnImg;
    private void Awake()
    {
        Instante = this;
    }
    void Start()
    {
        for (int i = 0; i < Cark.Length; i++)
        {
            LeanTween.rotateAround(Cark[i], new Vector3(0, 0, 120), 360, 5).setRepeat(-1);
        }
        FirstValues();
    }
    void FirstValues()
    {

        //MUSÝC VALUE
        if (PlayerPrefs.GetInt(GameData.Instante.Music) == 1)
        {
            MusicBtnImg.sprite = OffButtonSprite;
            Music.mute = true;

        }
        else if (PlayerPrefs.GetInt(GameData.Instante.Music) == 0)
        {
            MusicBtnImg.sprite = OnButtonSprite;
            Music.mute = false;

        }

        //SOUND VALUE
        if (PlayerPrefs.GetInt(GameData.Instante.Sound) == 1)
        {
            SoundBtnImg.sprite = OffButtonSprite;
            SoundMute(true);

        }
        else if (PlayerPrefs.GetInt(GameData.Instante.Sound) == 0)
        {
            SoundBtnImg.sprite = OnButtonSprite;
            SoundMute(false);
            PlayerPrefs.SetInt(GameData.Instante.Sound, 0);
        }

        //VIBRATE VALUE
        if (PlayerPrefs.GetInt(GameData.Instante.Vibrate) == 1)
        {
            VibrateBtnImg.sprite = OffButtonSprite;
            Vibration.IsOff = true;

        }
        else if (PlayerPrefs.GetInt(GameData.Instante.Vibrate) == 0)
        {
            VibrateBtnImg.sprite = OnButtonSprite;
            Vibration.IsOff = false;

        }
    }
    #region SOUND
    void SoundMute(bool _mute)
    {
        ButtonSound.mute = _mute;
        DamageSound.mute = _mute;
        FindSoldier.mute = _mute;
        Go.mute = _mute;
        WarStart.mute = _mute;
    }
    public void ButtonSoundPlay()
    {
        ButtonSound.Play();
    }
    public void GoSoundPlay()
    {
        Go.Play();
    }
    public void StartWarPlay()
    {
        WarStart.Play();
    }
    public void MoveSoldierPlay()
    {
        if (!UIManager.Instante.IsGameFinish)
            FindSoldier.Play();
    }
    public void DamageSoundPlay()
    {
        if (!UIManager.Instante.IsGameFinish)
            DamageSound.Play();
    }
    #endregion
    public void SettingsPanelOpen(bool _enable)
    {
        SettingPanel.SetActive(_enable);
    }
    #region BUTTON ON OFF
    public void MusicOff()
    {
        if (PlayerPrefs.GetInt(GameData.Instante.Music) == 0)
        {
            MusicBtnImg.sprite = OffButtonSprite;
            Music.mute = true;
            PlayerPrefs.SetInt(GameData.Instante.Music, 1);
        }
        else if (PlayerPrefs.GetInt(GameData.Instante.Music) == 1)
        {
            MusicBtnImg.sprite = OnButtonSprite;
            Music.mute = false;
            PlayerPrefs.SetInt(GameData.Instante.Music, 0);
        }
    }
    public void SoundOff()
    {
        if (PlayerPrefs.GetInt(GameData.Instante.Sound) == 0)
        {
            SoundBtnImg.sprite = OffButtonSprite;
            SoundMute(true);
            PlayerPrefs.SetInt(GameData.Instante.Sound, 1);
        }
        else if (PlayerPrefs.GetInt(GameData.Instante.Sound) == 1)
        {
            SoundBtnImg.sprite = OnButtonSprite;
            SoundMute(false);
            PlayerPrefs.SetInt(GameData.Instante.Sound, 0);
        }
    }
    public void VibrateOff()
    {
        if (PlayerPrefs.GetInt(GameData.Instante.Vibrate) == 0)
        {
            VibrateBtnImg.sprite = OffButtonSprite;
            Vibration.IsOff = true;
            PlayerPrefs.SetInt(GameData.Instante.Vibrate, 1);
        }
        else if (PlayerPrefs.GetInt(GameData.Instante.Vibrate) == 1)
        {
            VibrateBtnImg.sprite = OnButtonSprite;
            Vibration.IsOff = false;
            PlayerPrefs.SetInt(GameData.Instante.Vibrate, 0);
        }
    }
    #endregion
}
