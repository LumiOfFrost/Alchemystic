using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

    GameObject enemy;
    GameObject plr;
    public int zoffset;
    GameObject plrPrf;
    public GameObject manaBar;
    public GameObject hpBar;
    bool spawning = false;
    public GameObject fadeObj;
    Animator fadeAnim;
    public GameObject pauseFade;
    public GameObject staminaBar;
    int x;

    // Start is called before the first frame update
    void Start()
    {

        fadeAnim = fadeObj.GetComponent<Animator>();
        enemy = Resources.Load<GameObject>("Slime");
        plrPrf = Resources.Load<GameObject>("Player");
        plr = Instantiate(plrPrf, new Vector3(0,0,0), new Quaternion(0,0,0,0));
        plr.GetComponent<playerMovement>().baseSpeed = 5;
        manaBar.GetComponent<resourceBar>().player = plr;
        hpBar.GetComponent<resourceBar>().player = plr;
        pauseFade.GetComponent<PauseHandling>().player = plr;
        staminaBar.GetComponent<resourceBar>().player = plr;
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

        if (plr == null && !spawning)
        {
            StartCoroutine(Respawn());
        }
        
        if (!spawning)
        {
            Vector3 newPos = transform.position;
            if (plr.transform.position.x >= newPos.x + 0.25f) 
            {

                newPos.x = plr.transform.position.x - 0.25f;

            } else if (plr.transform.position.x <= newPos.x - 0.25f)
            {

                newPos.x = plr.transform.position.x + 0.25f;

            }
            if (plr.transform.position.y >= newPos.y + 0.25f)
            {

                newPos.y = plr.transform.position.y - 0.25f;

            }
            else if (plr.transform.position.y <= newPos.y - 0.25f)
            {

                newPos.y = plr.transform.position.y + 0.25f;

            }

            newPos.z = zoffset;
            transform.position = newPos;

        }
        


    }


    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);
            if (Random.value > 0.4)
            {
                x = -1;
            } else { x = 1; }
            GameObject enemySpawned = Instantiate(enemy, new Vector3(transform.position.x + ((Random.Range(1,10) + 15) * x), transform.position.y, 0), transform.rotation);
            enemySpawned.GetComponent<PlaceholderEnemy>().target = plr;
        }
    }

    IEnumerator Respawn()
    {
        fadeAnim.Play("FadeOut");
        spawning = true;
        yield return new WaitForSeconds(2);
        fadeAnim.Play("FadeIn");
        plr = Instantiate(plrPrf, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        staminaBar.GetComponent<resourceBar>().player = plr;
        manaBar.GetComponent<resourceBar>().player = plr;
        hpBar.GetComponent<resourceBar>().player = plr;
        pauseFade.GetComponent<PauseHandling>().player = plr;
        spawning = false;
    }


}
