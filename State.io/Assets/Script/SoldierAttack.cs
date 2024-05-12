using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SoldierAttack : MonoBehaviour
{

    public Transform target;
    [SerializeField] float AttackSpeed = 5f;
    public string tag = "";
    public Vector3 direction;
    public Soldier soldier;
    Rigidbody2D rigidbody2D;
    float starttime = 0f;
    void Start()
    {

        rigidbody2D = GetComponent<Rigidbody2D>();
        Invoke("TagSelect", 0.3f);
    }
    void TagSelect()
    {
        gameObject.tag = tag;
        GetComponent<Collider2D>().enabled = true;
        //if (gameObject.tag != "Player")
        //{
        //    UIManager.Instante.IsLastSoldier = true;
        //}
    }
    Vector3 v_diff;
    float atan2;
    // Update is called once per frame
    void Update()
    {
        if (!UIManager.Instante.SoldierStop)
        {
            if (target != null)
            {
                //rigidbody2D.AddForce(direction * Time.deltaTime * AttackSpeed);
                transform.position += direction.normalized * Time.deltaTime * AttackSpeed;
                v_diff = (target.position - transform.position);
                atan2 = Mathf.Atan2(v_diff.x, v_diff.y);
                transform.rotation = Quaternion.Euler(0f, 0f, -atan2 * Mathf.Rad2Deg);
            }

            else
                gameObject.SetActive(false);
           
        }
        
        //touchedtime += Time.deltaTime;
        //if(touchedtime>=1f)
        //{
        //    touchedtime = 0;
        //    touched = false;
        //}

    }
    float touchedtime = 0;
   public int touched;
    public bool collect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.gameObject.layer == this.gameObject.layer && collision.gameObject.tag != gameObject.tag)
        {
            if (collect)
            {
                return;
            }

            if (collision.GetComponent<SoldierAttack>().touched != 0)
            {
                touched = 0;
                return;
            }
            collision.GetComponent<SoldierAttack>().touched = gameObject.GetInstanceID();

            UIManager.Instante.iHaveMovingPlayersCounts[soldier.PlayerIndex].movingPlayers.Remove(gameObject);
           // UIManager.Instante.BarFillAmount(soldier.PlayerIndex, -1);
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }

    }
}
[System.Serializable]
public class Soldier
{
    public int NumberEnemyAttacking = 0;
    public int MaxSoldierCount = 0;
    public float SpawnTime = 0.5f;
    public int PlayerIndex = 0;
    public GameObject soldier;
    public LayerMask soldierLayer;
    public string tag = "";
    public Sprite PlayerIcon;
    public Sprite SoldierIcon;
    public Color LightColor;
    public Color DarkColor;
    public Color IconColor;
    //public MapColor PlayerMap;
}
