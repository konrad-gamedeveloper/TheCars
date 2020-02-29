using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBoxScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float ts = Time.fixedDeltaTime;

    public void OnTriggerEnter(Collider other)
    {
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
    public void OnTriggerExit(Collider other)
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("TIMESCALE" + Time.fixedDeltaTime);
    }
}
