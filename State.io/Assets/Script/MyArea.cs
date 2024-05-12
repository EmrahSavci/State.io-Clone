using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyArea : MonoBehaviour
{

    public static MyArea Instante;
    public Vector3 MousePos;
    public LayerMask MyLayer, EnemyLayer;
    Camera camera;
    public List<SpawnSoldier> spawnSoldiers = new List<SpawnSoldier>();
    public Transform Target, target2;
    public GameObject MyMap;
    RaycastHit2D MyPlayer, TargetPlayer, SameTarget;
    public List<GameObject> emptyAreas = new List<GameObject>();
    private void Awake()
    {
        Instante = this;
    }
    void Start()
    {
        camera = Camera.main;
        
    }
    public void GetEmptyArea(LevelManager levelManager)
    {
        for (int i = 0; i < levelManager.transform.childCount; i++)
        {
            if(levelManager.transform.GetChild(i).GetComponent<EmptyArea>())
            {
                emptyAreas.Add(levelManager.transform.GetChild(i).GetComponent<EmptyArea>().RedCircle);
            }
            else if(levelManager.transform.GetChild(i).GetComponent<Enemy>())
                emptyAreas.Add(levelManager.transform.GetChild(i).GetComponent<Enemy>().RedCircle);
        }
    }
    void RedCircleDeActive()
    {
        for (int i = 0; i < emptyAreas.Count; i++)
        {
            emptyAreas[i].SetActive(false);
        }
    }
    public List<SelectArea> selectArea = new List<SelectArea>();

    void Update()
    {
        if (!UIManager.Instante.IsGameFinish)
        {


            MousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButton(0))
            {   //RAY

                MyPlayer = Physics2D.Raycast(MousePos, Vector2.zero, Mathf.Infinity, MyLayer);
                RedCircleDeActive();
                // RaycastMyPlayer2D MyPlayer = Physics2D.Raycast(transform.position, -Vector2.up*100,Mathf.Infinity,MyLayer);
                if (MyPlayer.collider != null && !MyPlayer.collider.gameObject.GetComponent<SelectArea>().IsClick)
                {
                    selectArea.Add(MyPlayer.collider.gameObject.GetComponent<SelectArea>());
                    MyPlayer.collider.gameObject.GetComponent<SelectArea>().IsClick = true;
                    Vibration.VibratePeek();
                    spawnSoldiers.Add(MyPlayer.collider.gameObject.GetComponent<SpawnSoldier>());
                }
                if (MyPlayer.collider != null)
                {
                    for (int i = 0; i < selectArea.Count; i++)
                    {  
                        if(selectArea[i].Target!=null)
                        {
                            print("Null check 1 --- i =" + i + "  -  " + selectArea[i]);
                            print("Null check 2 --- i =" + i + "  -  " + selectArea[i].Target);
                            print("Null check 3 --- i =" + i + "  -  " + selectArea[i].gameObject);
                            print("Null check 4 --- i =" + i + "  -  " + MyPlayer);
                            print("Null check 5 --- i =" + i + "  -  " + MyPlayer.collider);
                            print("Null check 6 --- i =" + i + "  -  " + MyPlayer.collider.transform);
                            if (selectArea[i].Target != selectArea[i].gameObject)
                                selectArea[i].Target = MyPlayer.collider.transform;
                        }
                        
                    }
                }
                TargetPlayer = Physics2D.Raycast(MousePos, Vector2.zero, Mathf.Infinity, EnemyLayer);
                if (TargetPlayer.collider && selectArea.Count>=1)
                {

                    for (int i = 0; i < selectArea.Count; i++)
                    {
                        selectArea[i].Target = TargetPlayer.collider.transform;
                    }
                    
                    //Target = TargetPlayer.collider.gameObject.transform;
                    if (Target.gameObject.layer == 9 && TargetPlayer==Target)
                    {
                        Debug.Log("SEÇÝLEN HEDEF:" + Target.gameObject.name);
                        Target.GetComponent<EmptyArea>().RedCircle.SetActive(true);
                    }
                    else if (Target.gameObject.layer == 6)
                    {
                        Target.GetComponent<Enemy>().RedCircle.SetActive(true);
                    }


                }






            }

            if (Input.GetMouseButtonUp(0))
            {

                TargetPlayer = Physics2D.Raycast(MousePos, Vector2.zero, Mathf.Infinity, EnemyLayer);
                SameTarget = Physics2D.Raycast(MousePos, Vector2.zero, Mathf.Infinity, MyLayer);
                if (Target != null && TargetPlayer.collider)
                {
                    Debug.Log("Düþman seçildi");

                    //Target = TargetPlayer.collider.gameObject.transform;
                    if (Target.gameObject.layer == 9)
                    {
                        Target.GetComponent<EmptyArea>().RedCircle.SetActive(false);
                    }
                    else if (Target.gameObject.layer == 6)
                    {
                        Target.GetComponent<Enemy>().RedCircle.SetActive(false);
                    }
                    SpawnSoldier();

                }
                else if (SameTarget.collider != null && SameTarget.collider.gameObject.GetComponent<SelectArea>().IsClick)
                {


                    spawnSoldiers.Remove(SameTarget.collider.gameObject.GetComponent<SpawnSoldier>());
                    selectArea.Remove(SameTarget.collider.gameObject.GetComponent<SelectArea>());
                    //  selectArea.Target = hit.collider.gameObject.transform;
                    Target = SameTarget.collider.gameObject.transform;

                    SpawnSoldier();


                }
                else
                {
                    Debug.Log("Boþ yere týklandý");
                    ClearList();
                }
            }
        }
    }
    void SpawnSoldier()
    {
        for (int i = 0; i < spawnSoldiers.Count; i++)
        {
            if (!spawnSoldiers[i].IsEnemy)
            {
                StopCoroutine(spawnSoldiers[i].Attack(Target));
                if (!spawnSoldiers[i].AttackSecond)
                    StartCoroutine(spawnSoldiers[i].Attack(Target));
                else
                {
                    spawnSoldiers[i].GetTargetDirection(Target);
                }
            }


        }
        UIAnimation.Instante.DestroyFinger();
        Target = null;
        ClearList();
    }
    void ClearList()
    {
        
        //for (int i = 0; i < spawnSoldiers.Count; i++)
        //{

        //    spawnSoldiers[i].GetComponent<SelectArea>().ResetLine();
        //}
        spawnSoldiers.RemoveRange(0, spawnSoldiers.Count);
        selectArea.RemoveRange(0, selectArea.Count);
    }
}
