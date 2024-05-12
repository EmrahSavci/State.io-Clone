using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindTargetEnemy : MonoBehaviour
{
    public List<Transform> TargetEnemy = new List<Transform>();

    public List<Transform> Deneme = new List<Transform>();
    LevelManager levelManager;
    
    public float Radius = 1f;
    float FirstColliderRadius = 0;
    CircleCollider2D circleCollider;
    private void Start()
    {   
        circleCollider = GetComponent<CircleCollider2D>();
        levelManager = GetComponentInParent<LevelManager>();
        FirstColliderRadius = circleCollider.radius;
        Radius = FirstColliderRadius;
       
       
    }
   
   
    private void Update()
    {  if(Deneme.Count<levelManager.Players.Count-1)
        {
            Radius += Time.deltaTime*5;
            circleCollider.radius = Radius;
            //colliders = Physics2D.OverlapCircleAll(transform.position, Radius, 1 <<layerMask);
            //if(Deneme.Count<levelManager.Players.Count)
            //    Deneme.Add(colliders[colliders.Length-1].gameObject.transform.parent);
        }
       else
        {
            circleCollider.radius = FirstColliderRadius;
        }
         
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {   
           
                Deneme.Add(collision.gameObject.transform.parent);

        }
    }
}
