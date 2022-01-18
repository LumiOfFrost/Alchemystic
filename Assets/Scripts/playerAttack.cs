using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerAttack : MonoBehaviour
{

    

    Vector3 mousePos;

    public bool shoot;

    GameObject spark;

    public bool hasShot;

    public float mana = 30;

    public float manaMax = 30;

    public float rechargeTimer = 0;

    public float visMan;

    public float shotDelay;

    public bool paused = false;

    Material orgMat;

    Vector2 aim;

    Vector2 mouseLocation;

    // Start is called before the first frame update
    void Start()
    {
        orgMat = GetComponentInParent<SpriteRenderer>().material;
        spark = Resources.Load<GameObject>("Spark");
        visMan = mana;
    }

    // Update is called once per frame
    void Update()
    {

        rechargeTimer -= Time.deltaTime;

        if (!paused)
        {
            if (PlayerPrefs.GetString("altAim") == "false")
            {
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseLocation.x, mouseLocation.y, transform.position.z));
                mousePos.z = 0;
            } else
            {
                mousePos = new Vector3(transform.position.x + aim.x, transform.position.y + aim.y, transform.position.z);
            }

            transform.up = mousePos - transform.position;

            

            if (mana < manaMax && rechargeTimer <= 0)
            {
                if (mana + (manaMax * 0.005f) < manaMax)
                {
                    mana += (manaMax * 0.005f);
                    visMan = mana;
                }
                else
                {
                    mana = manaMax;
                    visMan = mana;
                }

            }

            if (visMan > mana)
            {
                visMan -= (visMan - mana) / 20;
            }
        }

        



    }

    IEnumerator shotDelayReset()
    {
        yield return new WaitForSeconds(shotDelay);
        shotDelay = 0;
    }

    IEnumerator resetShot()
    {
        yield return new WaitForSeconds(0.25f);
        hasShot = false;
    }

    void OnAim(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        aim = new Vector2(inputVec.x, inputVec.y);
    }

    void OnMouseLoc(InputValue input)
    {

        Vector2 inputVec = input.Get<Vector2>();

        mouseLocation = new Vector2(inputVec.x, inputVec.y);

    }

    void OnAttack()
    {

        if (shotDelay == 0 && !hasShot && mana > 0 && transform.parent.GetComponentInChildren<playerCarry>().carryWeight == 0)
        {
            hasShot = true;
            GameObject sparkProj = Instantiate(spark, transform.position, transform.rotation);
            sparkProj.GetComponent<projectile>().boomTimer = 1;
            sparkProj.GetComponent<projectile>().boomTimerMax = 1;
            sparkProj.GetComponent<projectile>().owner = "Player";
            if (mana - 5 > 0) { mana -= 5; }
            else { mana = 0; }
            if (sparkProj != null) { this.GetComponent<ParticleSystem>().Play(); StartCoroutine(Flash(Resources.Load<Material>("Flash Material"))); StartCoroutine(resetShot()); rechargeTimer = 2.5f; shotDelay = 0.25f; StartCoroutine(shotDelayReset()); }
            else { OnAttack(); }
        }

    }

    IEnumerator Flash(Material fshMat)
    {

        GetComponentInParent<SpriteRenderer>().material = fshMat;
        yield return new WaitForSeconds(0.125f);
        GetComponentInParent<SpriteRenderer>().material = orgMat;

    }

}
