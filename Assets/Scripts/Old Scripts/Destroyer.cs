using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Destroyer : MonoBehaviour
{

    public float timerLimit;

    float timer = 0;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= timerLimit)
        {
            Destroy(gameObject);
        }
    }
}
