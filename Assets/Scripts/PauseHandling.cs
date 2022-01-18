using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandling : MonoBehaviour
{

    public bool paused = false;

    public GameObject player;

    Animator fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (paused)
        {
            fade.Play("PauseFade");
            player.GetComponent<playerMovement>().paused = true;
            player.GetComponent<playerAnimation>().paused = true;
            player.GetComponentInChildren<playerAttack>().paused = true;
        }
        else
        {
            fade.Play("PauseNoFade");
            player.GetComponent<playerMovement>().paused = false;
            player.GetComponent<playerAnimation>().paused = false;
            player.GetComponentInChildren<playerAttack>().paused = false;
        }

    }

    private void OnPause()
    {
        switch (paused)
        {
            case true:
                paused = false;
                break;
            case false:
                paused = true;
                break;
        }
    }

}
