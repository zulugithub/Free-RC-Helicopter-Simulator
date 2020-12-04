using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    float time = 0; // [sec]
    const float time_on = 0.5f; // [sec]
    const float time_off = 1.0f; // [sec]
    bool light_status_flag = false;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > time)
        {
            light_status_flag ^= true;
            if (light_status_flag == true)
                time = Time.time + time_on;
            else
                time = Time.time + time_off;
            this.gameObject.GetComponent<Light>().enabled = light_status_flag;
        }
    }

}
