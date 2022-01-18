using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class properties : MonoBehaviour
{

    public string ID = "";
    public float weight = 1;
    public void Start()
    {

        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/item/" + ID);

    }

}
