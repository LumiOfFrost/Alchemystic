using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class envSpawner : MonoBehaviour
{

    GameObject tree;

    GameObject rock;

    // Start is called before the first frame update
    void Start()
    {
        tree = Resources.Load<GameObject>("tree");
        rock = Resources.Load<GameObject>("Rock");
        for (int i = 5; i > 0; i -= 1)
        {

            transform.position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            if (Random.value < 0.5)
            {
                Instantiate(tree, transform.position, new Quaternion(0, 0, 0, 0), transform.parent);
            } else
            {
                Instantiate(rock, transform.position, new Quaternion(0, 0, 0, 0), transform.parent);
            }

        }
        for (int i = 10; i > 0; i -= 1)
        {

            transform.position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            if (Random.value < 0.1)
            {
                Instantiate(tree, transform.position, new Quaternion(0, 0, 0, 0), transform.parent);
            }
            else
            {
                Instantiate(rock, transform.position, new Quaternion(0, 0, 0, 0), transform.parent);
            }

        }
        for (int i = 10; i > 0; i -= 1)
        {

            transform.position = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            if (Random.value < 0.9)
            {
                Instantiate(tree, transform.position, new Quaternion(0, 0, 0, 0), transform.parent);
            }
            else
            {
                Instantiate(rock, transform.position, new Quaternion(0, 0, 0, 0), transform.parent);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
