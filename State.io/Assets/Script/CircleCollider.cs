using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollider : MonoBehaviour
{
    // Start is called before the first frame update
    Transform parent;
    private void Start()
    {
        parent = transform.parent;
    }
    SoldierAttack soldierAttack;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        
        
        if(collision.gameObject.GetComponent<SoldierAttack>())
        {
            soldierAttack = collision.gameObject.GetComponent<SoldierAttack>();

            Debug.Log("objenin adý: " + collision.gameObject.name);
            if (soldierAttack.target == parent)
            {
                soldierAttack.collect = true;

                collision.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                UIManager.Instante.iHaveMovingPlayersCounts[soldierAttack.soldier.PlayerIndex].movingPlayers.Remove(collision.gameObject);
                LeanTween.move(collision.gameObject, parent, 0.3f).setOnComplete(() =>
                {
                    
                    collision.gameObject.SetActive(false);
                    
                        if (parent.GetComponent<EmptyArea>() != null)
                        {
                            parent.GetComponent<EmptyArea>().EnemySoldier(collision.gameObject);
                        }
                        else if (parent.GetComponent<SpawnSoldier>() != null)
                        {
                            parent.GetComponent<SpawnSoldier>().EnemyPlayerFunc(collision.gameObject);
                        }
                   

                    // Destroy(collision.gameObject);
                });
            }
        }
        
        
    }
}
