using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<GameObject> TargetEnemyList = new List<GameObject>();
    [SerializeField] Transform TargetEnemy;
    public LevelManager levelManager;
    [SerializeField] FindTargetEnemy targetEnemy;
    public Transform SpawnPoints;
    public int EnemyCount;

    public GameObject RedCircle;

    private void OnMouseOver()
    {
        MyArea.Instante.Target = this.transform;

    }
    private void OnMouseExit()
    {
        RedCircle.SetActive(false);
    }

    void Start()
    {
        RedCircle = transform.GetChild(5).gameObject;
        SpawnPoints = transform.GetChild(3);
        levelManager = gameObject.GetComponentInParent<LevelManager>();
        targetEnemy = GetComponent<FindTargetEnemy>();
        GetComponentInChildren<LineRenderer>().enabled = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            RedCircle.SetActive(false);
    }
    public void TargetEnemyFind()
    {   //Clear Enemy List
        if (TargetEnemyList.Count >= 1)
            TargetEnemyList.RemoveRange(0, TargetEnemyList.Count);

        //Find Enemy Players
        EnemyCount = targetEnemy.Deneme.Count;

        for (int i = 0; i < EnemyCount; i++)
        {
            if (targetEnemy.Deneme[i].tag != this.gameObject.tag)
            {

                TargetEnemy = targetEnemy.Deneme[i].transform;
                SpawnPoints.LookAt(TargetEnemy.position);
                if(!UIManager.Instante.IsGameFinish)
                StartCoroutine(GetComponent<SpawnSoldier>().Attack(TargetEnemy));
                break;


            }
        }



    }

}
