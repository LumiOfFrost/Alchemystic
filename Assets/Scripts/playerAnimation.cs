using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerAnimation : MonoBehaviour
{

    Animator ani;

    public bool busy = false;

    Vector3 mousePos;

    public bool paused = false;

    Vector2 aim;

    Vector2 movement;

    Vector2 mouseLocation;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!paused)
        {
            if (PlayerPrefs.GetString("altAim") == "false")
            {
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseLocation.x, mouseLocation.y, transform.position.z));
                mousePos.z = 0;
            }
            else
            {
                mousePos = new Vector3(transform.position.x + aim.x, transform.position.y + aim.y, transform.position.z);
            }

            if (movement.x != 0 || movement.y != 0 && !busy)
            {
                if (GetComponentInChildren<playerCarry>().carryWeight != 0)
                {
                    ani.Play("RunCarry");
                } else {
                    ani.Play("Run");
                }
                
            }
            else if ( mousePos.y > transform.position.y + 3 && GetComponentInChildren<playerCarry>().carryWeight == 0 && !busy)
            {
                ani.Play("LookUp");
            }
            else if (mousePos.y < transform.position.y - 3 && GetComponentInChildren<playerCarry>().carryWeight == 0 && !busy)
            {
                ani.Play("LookDown");
            }
            else if (!busy)
            {
                if (GetComponentInChildren<playerCarry>().carryWeight == 0)
                {
                    ani.Play("IdleCarry");
                } else
                {
                    ani.Play("Idle");
                }

                
            }
        }
    }

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();

        movement = new Vector2(inputVec.x, inputVec.y);
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

}
