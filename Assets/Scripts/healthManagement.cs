using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthManagement : MonoBehaviour
{

    public float hp;

    public GameObject ptcSys;

    public GameObject dthSys;

    SpriteRenderer sprRdr;

    bool flashing;

    public int maxHp;

    public float visHp;

    public float invincibilityTimeMax = 0;

    float invTime = 0;

    // Start is called before the first frame update
    void Start()
    {

        invTime = 0;
        hp = maxHp;
        visHp = hp;
        sprRdr = GetComponent<SpriteRenderer>();
        StartCoroutine(Invincie());

    }

    // Update is called once per frame
    void Update()
    {

        invTime -= Time.deltaTime;

        if (visHp > hp)
        {
            visHp -= (visHp - hp) / 20;
        }

    }

    public void TakeDamage(float dmgVal)
    {

        if (invTime <= 0)
        {
            invTime = invincibilityTimeMax;

            hp -= dmgVal;

            if (!flashing)
            {
                StartCoroutine(flash());
            }
            if (hp <= 0)
            {
                if (GetComponent<DropTable>() != null)
                {
                    for (int i = 0; i < GetComponent<DropTable>().drops.Count; i++)
                    {
                        GameObject item = Instantiate(Resources.Load<GameObject>("Item"), new Vector3(transform.position.x + Random.Range(1, -1) * Random.value, transform.position.y + Random.Range(1, -1) * Random.value, transform.position.z), transform.rotation, transform.parent);
                        item.GetComponent<properties>().ID = GetComponent<DropTable>().drops[i];
                        item.GetComponent<properties>().weight = GetComponent<DropTable>().dropWeights[i];
                    }
                }
                dthSys.GetComponent<ParticleSystem>().Play();
                dthSys.transform.SetParent(null);
                dthSys.GetComponent<partSysDecay>().decayTimer = 1;
                Destroy(this.gameObject);
            }
            else
            {
                ptcSys.GetComponent<ParticleSystem>().Play();
            }
        } 

    }

    IEnumerator flash()
    {

        flashing = true;
        Material mat = sprRdr.material;
        sprRdr.material = Resources.Load<Material>("Flash Material");
        yield return new WaitForSeconds(0.1f);
        sprRdr.material = mat;
        flashing = false;

    }

    IEnumerator Invincie()
    {
        while(true)
        {
            if (invTime > 0)
            {
                Color col = sprRdr.color;
                sprRdr.color = new Color(col.r, col.g, col.b, 0.75f);
                yield return new WaitForSeconds(0.25f);
                sprRdr.color = col;
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                yield return new WaitForSeconds(0);
            }
        }
    }

}
