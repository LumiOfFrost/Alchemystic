using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    Constant,
    Burst,
    Ranger
}

public class PlaceholderEnemy : MonoBehaviour
{

    //External

    [Header("Basic")]

    public float detectionRange = 10;

    public float loseRange = 20;

    public MovementType movementType = new MovementType();

    public float movementSpeed = 1;

    public float maxBurstTime = 1;

    [Header("Attack")]

    public bool dealContactDamage = true;

    public float contactDamage = 0;

    public bool hasMeleeAttack = false;

    public float attackRange = 1;

    public bool pursue = true;

    public bool detectTag = false;

    public string tagToDetect = "";

    //Internal

    float burstTime = 1;

    bool targetSeen= false;

    [HideInInspector]
    public float moveAmount = 0;

    public GameObject target;

    //Methods

    private void Start()
    {

        burstTime = maxBurstTime;

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.GetComponent<healthManagement>() != null && dealContactDamage)
        {
            if (detectTag)
            {
                if (collision.gameObject.tag == tagToDetect)
                {
                    collision.gameObject.GetComponent<healthManagement>().TakeDamage(contactDamage);
                }
            } else
            {
                collision.gameObject.GetComponent<healthManagement>().TakeDamage(contactDamage);
            }
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            targetSeen = false;
            target = GameObject.FindWithTag("Player");
        }
        if (Vector2.Distance(transform.position, target.transform.position) < detectionRange)
        {
            targetSeen = true;
        } 
        if (Vector2.Distance(transform.position, target.transform.position) > loseRange)
        {
            targetSeen = false;
        }
        if (target.transform.position.x > transform.position.x && target)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        moveAmount -= (moveAmount / 40);
        if (moveAmount < 0.05)
        {
            moveAmount = 0;
        }
        
    }
    private void FixedUpdate()
    {

        switch (movementType.ToString())
        {

            case "Burst":

                if (burstTime <= 0.0625)
                {
                    moveAmount = 1;
                    burstTime = maxBurstTime;
                }
                
                burstTime -=  Time.fixedDeltaTime;
                moveAmount -= Time.fixedDeltaTime;
                if (moveAmount < 0)
                {
                    moveAmount = 0;
                }

                break;

            case "Constant":
                {

                    if (pursue)
                    {

                        moveAmount = 1;

                    } else
                    {

                        if (Vector2.Distance(transform.position, new Vector2(target.transform.position.x, target.transform.position.y + 0.5f)) > attackRange)
                        {
                            moveAmount = 1;
                        } else 
                        { 
                            moveAmount = 0;
                        }
                    
                    }         
                
                }
                break;

        }

        if (targetSeen && moveAmount > 0 && target != null)
        {

            Vector3 direction = (target.transform.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().MovePosition(transform.position + direction * (movementSpeed * moveAmount) * Time.fixedDeltaTime);
        
        }

    }

}
