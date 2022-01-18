using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class partSysDecay : MonoBehaviour
{

    public float decayTimer;

    // Start is called before the first frame update
    void Start()
    {
        decayTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (decayTimer  > 0 )
        {
            decayTimer -= Time.deltaTime;
        }
         if (decayTimer < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
