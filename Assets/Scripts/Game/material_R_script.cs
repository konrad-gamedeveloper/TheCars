using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class material_R_script : MonoBehaviour
{
    public Material matR0;
    public Material matR1;
    // Start is called before the first frame update
    void Start()
    {

        Invoke("m1", 1.0f);

    }

    void m1()
    {
        GetComponent<Renderer>().material = matR0;

        Invoke("m2", 1.0f);

    }

    void m2()
    {
        GetComponent<Renderer>().material = matR1;

        Invoke("m1", 1.0f);

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
