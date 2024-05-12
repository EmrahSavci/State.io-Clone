using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public Vector2 ShakePos;
    public Vector3 FirstScale;
    Transform mypos;
    Vector3 initialPos;
    void Start()
    {
        mypos = transform;
        initialPos = mypos.position;
        ShakePos = new Vector2(0.02f, 0.02f);
        FirstScale = transform.localScale;
    }
    public void ShakeFunc()
    {
        LeanTween.cancel(gameObject);
        HorizontalShake();
    }
    void HorizontalShake()
    {
        //float randomX = Random.Range(-ShakePos.x, ShakePos.x);
        //LeanTween.moveX(gameObject,mypos.position.x+randomX, 0.01f).setOnComplete(() =>VerticalShake());
        LeanTween.scale(gameObject, FirstScale * 0.9f, 0.05f).setOnComplete(() => VerticalShake());
    }
    void VerticalShake()
    {
        //float randomy = Random.Range(-ShakePos.y, ShakePos.y);
        //LeanTween.moveY(gameObject,mypos.position.y+ randomy, 0.05f).setOnComplete(() =>DefaultShake());
        LeanTween.scale(gameObject, FirstScale, 0.05f);
    }
    void DefaultShake()
    {
        LeanTween.move(gameObject, initialPos, 0.1f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
