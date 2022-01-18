using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class playerMovement : MonoBehaviour
{

    Rigidbody2D rb;

    public float baseSpeed = 5;

    float speed = 5;

    Vector2 veloc;

    Vector3 mousePos;

    float staminaRecharge = 0;

    Animator ani;

    [HideInInspector]

    public bool busy;

    [HideInInspector]

    public bool paused = false;

    [HideInInspector]

    public float stamina = 100;

    [HideInInspector]

    public float maxStamina = 100;

    Vector2 movement;

    Vector2 mouseLocation;

    Vector2 aim;



    // Start is called before the first frame update
    void Start()
    {
        stamina = 100;
        speed = baseSpeed;
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!paused)
        {
            veloc = rb.velocity;
            if (movement.x > 0 && !busy)
            {
                veloc.x = speed;
            }
            else if (movement.x < 0)
            {
                veloc.x = speed * -1;
            } else
            {
                veloc.x = 0;
            }
            if (movement.y > 0 && !busy)
            {
                veloc.y = speed;
            }
            else if (movement.y < 0)
            {
                veloc.y = speed * -1;
            }
            else
            {
                veloc.y = 0;
            }
            rb.velocity = veloc;
        }
        
    }

    private void Update()
    {

        

        if (!paused)
        {
            if (PlayerPrefs.GetString("altAim") == "false")
            {
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mouseLocation.x, mouseLocation.y, transform.position.z));
            }
            else
            {
                mousePos = new Vector3(transform.position.x + aim.x, transform.position.y + aim.y, transform.position.z);
            }

            Vector2 veccy = transform.localScale;

            if (mousePos.x >= transform.position.x)
            {

                veccy.x = -1;

            }

            if (mousePos.x < transform.position.x)
            {

                veccy.x = 1;

            }

            transform.localScale = veccy;


        
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
