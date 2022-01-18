using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carriedItem : MonoBehaviour
{

    public string ID = "";
    int dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Const());
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("sprites/item/" + ID);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector2(transform.localPosition.x + ((0.125f * Time.deltaTime) / 2) * dir, transform.localPosition.y);
    }

    IEnumerator Const()
    {

        while(true)
        {

            dir = 1;
            yield return new WaitForSeconds(2);
            dir = -1;
            yield return new WaitForSeconds(2);

        }

    }

}
