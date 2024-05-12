using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
    public static UIAnimation Instante;

    [SerializeField] GameObject Finger;
    [SerializeField] Transform FingerParent;
    private void Awake()
    {
        Instante = this;
    }
    public void ButtonAnim(GameObject Btn)
    {
       // LeanTween.cancelAll();
        LeanTween.scale(Btn, new Vector2(0.9f, 0.9f), 0.1f).setOnComplete(() =>
           {
               LeanTween.scale(Btn,Vector2.one, 0.1f);
   
           });
        SoundManager.Instante.ButtonSoundPlay();
    }
     Vector3 ScreenPos;
  public  GameObject _finger;
    Vector3 targetpos;
    public void FingerAnim(Transform MyPlayer,Transform TargetPlayer)
    {
         ScreenPos = Camera.main.WorldToScreenPoint(MyPlayer.position);
         targetpos = Camera.main.WorldToScreenPoint(TargetPlayer.position);
         _finger = Instantiate(Finger, ScreenPos, Quaternion.identity, FingerParent);
        FingerMoveToTarget();
    }
    void FingerMoveToTarget()
    { 
        LeanTween.move(_finger, targetpos, 1f).setOnComplete(() =>
        {
            LeanTween.scale(_finger, Vector2.one * 1.2f, 0.3f).setOnComplete(() =>
            {
                _finger.transform.position = ScreenPos;
                LeanTween.scale(_finger, Vector2.one * 1f, 0.3f).setOnComplete(()=>
                {
                    LeanTween.cancel(_finger);
                    FingerMoveToTarget();
                });
            });
        });
    }
    public void DestroyFinger()
    {  if(_finger!=null)
        {
            LeanTween.cancel(_finger);
            Destroy(_finger);
        }
       
    }
}
