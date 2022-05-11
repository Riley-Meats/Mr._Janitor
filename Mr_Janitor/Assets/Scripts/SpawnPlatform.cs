using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatform : MonoBehaviour
{
    public float percent = 1.0f;
    public float percent2 = 1.0f;
    float hPercent;
    float pPercent;

    public Vector2 spawn;
    public Vector2 hSpawn;

    public GameObject platform;
    public GameObject healthUp2;
    public GameObject healthUp4;
    public GameObject healthUp6;
    public GameObject weakE;
    public GameObject sturdyE;
    public GameObject strongE;
    public GameObject powerE;

    void Chance()
    {
        percent = Random.value;

        if (percent <= .25f)
        {
            hPercent = Random.value;
        }
    }

    void Health()
    {
        percent2 = Random.value;
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

            if (hPercent >= 0f && hPercent < .50f)
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

        Health();
        if (percent2 > .0f && percent2 <= .40f)
        {
            Instantiate(weakE, hSpawn, Quaternion.identity);
        }
        else if (percent2 > .40f && percent2 <= .70f)
        {
            Instantiate(sturdyE, hSpawn, Quaternion.identity);
        }
        else if (percent2 > .70f && percent2 <= .90f)
        {
            Instantiate(strongE, hSpawn, Quaternion.identity);
        }
        else if (percent2 > .90f && percent2 <= 1.0f)
        {
            Instantiate(powerE, hSpawn, Quaternion.identity);
        }
    } 
}
