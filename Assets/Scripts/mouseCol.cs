using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class mouseCol : MonoBehaviour
{

    public Vector2 mouseLocation;

    // Update is called once per frame
    void Update()
    {

        Vector3 newPos;

        newPos = Camera.main.ScreenToWorldPoint(new Vector3(mouseLocation.x, mouseLocation.y, 0));

        newPos.z = 0;

        transform.position = newPos;

    }

    void OnMouseLoc(InputValue input)
    {

        Vector2 inputVec = input.Get<Vector2>();

        mouseLocation = new Vector2(inputVec.x, inputVec.y);

    }

}
