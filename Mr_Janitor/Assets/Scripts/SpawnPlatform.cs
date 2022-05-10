using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public float percent = 1.0f;
    float hPercent;

    public Vector2 spawn;

    public GameObject platform;
    public GameObject healthUp2;
    public GameObject healthUp4;
    public GameObject healthUp6;

    void Chance()
    {
        percent = Random.value;

        if (percent <= .25f)
        {
            hPercent = Random.value;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Chance();
        Debug.Log(percent);

        if (percent <= .25f)
        {
            Instantiate(platform);
        }

        if (percent <= .25f)
        {
            Debug.Log("your percent was " + hPercent);

            if (hPercent >= 0.00001f && hPercent < .50f)
            {
                Instantiate(healthUp2, spawn, Quaternion.identity);
            }
            else if (hPercent > .50f && hPercent <= .80f)
            {
                Instantiate(healthUp4, spawn, Quaternion.identity);
            }
            else if (hPercent > .80 && hPercent <= 1.0f)
            {
                Instantiate(healthUp6, spawn, Quaternion.identity);
            }
        }
    } 
}
