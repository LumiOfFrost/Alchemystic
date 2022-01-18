using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    public string owner;

    public float dmgValue;

    public float speed = 3;

    public float boomTimer;

    public float boomTimerMax;

    bool slowed = false;

    public GameObject partSys;

    private void Start()
    {
        slowed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        boomTimer -= Time.deltaTime;
        transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
        if (boomTimer < 0)
        {

            partSys.transform.SetParent(null);

            partSys.GetComponent<partSysDecay>().decayTimer = 0.2f;

            partSys.GetComponent<ParticleSystem>().Stop();

            Object.Destroy(this.gameObject);
            
        } else if (boomTimer < boomTimerMax / 2 && !slowed)
        {
            slowed = true;
            speed /= 2;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.tag != owner && collision.transform.tag != "PlayerMovement" && !collision.CompareTag("DetectionCollider"))
        {
            if (collision.GetComponent<healthManagement>() != null)
            {
                collision.GetComponent<healthManagement>().TakeDamage(dmgValue);
            }
            Object.Destroy(this.gameObject);
        }
    }
}
