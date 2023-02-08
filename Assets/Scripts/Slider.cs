using UnityEngine;
using System.Collections;

public class Slider : MonoBehaviour
{
    public float speed = 2;
    public int direction = -1;
    public float start;
    public float end;

    // Update is called once per frame
    void Update()
    {
       transform.Translate((direction * speed) * Time.deltaTime, 0, 0);
       if(transform.position.z >= end)
        {
            direction = 1;
        }

        if (transform.position.z <= start)
        {
            direction = -1;
        }
    }
}
