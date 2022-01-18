using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckBox : MonoBehaviour
{

    bool mouseOver = false;

    public Sprite checkOn;

    public Sprite checkOff;

    public string type = "";


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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlayerPrefs.GetString(type) == "true")
        {
            GetComponent<SpriteRenderer>().sprite = checkOn;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = checkOff;
        }

    }

    void OnAttack()
    {

        if (mouseOver)
        {
            switch (PlayerPrefs.GetString(type))
            {

                case "true":
                    PlayerPrefs.SetString(type, "false");
                    break;
                case "false":
                    PlayerPrefs.SetString(type, "true");
                    break;
                default:
                    PlayerPrefs.SetString(type, "true");
                    break;

            }
        }

    }

}
