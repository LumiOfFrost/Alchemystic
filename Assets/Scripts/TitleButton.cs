using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.InputSystem;

public class TitleButton : MonoBehaviour
{

    bool swapping = false;

    public Animator exitScreen;

    bool mouseOver = false;

    public string type;

    public string subtype;

    Sprite[] buttonSpr;

    public Sprite buttonOn;

    public Sprite button;

    public Animator menu;
    private void Start()
    {

        buttonSpr = Resources.LoadAll<Sprite>(type + "Button");
        button = buttonSpr.Single(s => s.name == type + "Button");
        buttonOn = buttonSpr.Single(s => s.name == type + "ButtonLit");
        if (menu != null)
        {
            menu.Play("Title");
        }
        
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Mouse")
        { 
            mouseOver = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D col)
    {

        if (col.transform.tag == "Mouse")
        {
            mouseOver = false;
        }

    }

    void Update()
    {
        if (!mouseOver)
        {
            GetComponent<SpriteRenderer>().sprite = button;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = buttonOn;
        }
    
    }

    private IEnumerator StartGame()
    {

        swapping = true;

        exitScreen.Play("TitleOut");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Main");

        swapping = false;

    }

    private IEnumerator TitleScreen()
    {

        swapping = true;

        exitScreen.Play("FadeOut");

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene("Title");

        swapping = false;

    }

    private IEnumerator ExitGame()
    {

        swapping = true;

        exitScreen.Play("TitleOut");

        yield return new WaitForSeconds(1);

        Application.Quit();

        swapping = false;

    }

    IEnumerator OptionsMenu()
    {

        swapping = true;

        exitScreen.Play("menu swap");

        yield return new WaitForSeconds(1);

        menu.Play("OptionsMenu");

        swapping = false;

    }

    IEnumerator TitleFromOptions()
    {

        swapping = true;

        exitScreen.Play("menu swap");

        yield return new WaitForSeconds(1);

        menu.Play("Title");

        swapping = false;

    }

    void OnAttack()
    {
        if (mouseOver && !swapping)
        {
            switch (type)
            {
                case "play":
                    StartCoroutine(StartGame());
                    break;
                case "exit":
                    switch(subtype)
                    {
                        case "quit":
                            StartCoroutine(ExitGame());
                            break;
                        case "title":
                            StartCoroutine(TitleScreen());
                            break;
                    }
                    break;
                case "resume":
                    transform.parent.GetComponentInParent<PauseHandling>().paused = false;
                    break;
                case "options":
                    StartCoroutine(OptionsMenu());
                    break;
                case "back":
                    StartCoroutine(TitleFromOptions());
                    break;
            }
        }
    }

}
