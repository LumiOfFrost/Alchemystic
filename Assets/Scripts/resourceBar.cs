using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BarType
{
    Mana,
    Health,
    Stamina
}

public class resourceBar : MonoBehaviour
{

    public GameObject player;

    public GameObject sparkle;

    float barPerc;

    bool hasSparked = true;

    public BarType barType = new BarType();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 spriteScale = GetComponent<SpriteRenderer>().size;
        switch (barType.ToString()) {
            case "Mana":
                if (player != null)
                {
                    barPerc = player.GetComponentInChildren<playerAttack>().visMan / player.GetComponentInChildren<playerAttack>().manaMax;
                }
                else
                {
                    barPerc = 0;
                }
                spriteScale.y = barPerc * 0.875f;

                if (player.GetComponentInChildren<playerAttack>().visMan == player.GetComponentInChildren<playerAttack>().manaMax && !hasSparked)
                {
                    sparkle.GetComponent<Animator>().Play("ManaSpark");
                    hasSparked = true;
                }
                if (player.GetComponentInChildren<playerAttack>().visMan < player.GetComponentInChildren<playerAttack>().manaMax)
                {
                    hasSparked = false;
                }

                break;
            case "Health":
                if (player != null)
                {
                    barPerc = player.GetComponentInChildren<healthManagement>().visHp / player.GetComponentInChildren<healthManagement>().maxHp;
                } else
                {
                    barPerc = 0;
                }
                spriteScale.y = barPerc * 1.625f;
                break;
            case "Stamina":
                if (player != null)
                {
                    barPerc = player.GetComponent<playerMovement>().stamina / player.GetComponent<playerMovement>().maxStamina;
                }
                else
                {
                    barPerc = 0;
                }
                spriteScale.y = barPerc * 0.5f;
                if (barPerc < 0.25f)
                {
                    GetComponent<SpriteRenderer>().color = Color.red;
                } else
                {
                    GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
                }
                break;



        }   
        GetComponent<SpriteRenderer>().size = spriteScale;

    }
}
