using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public CarSelection cs;
    void Update()
    {
        //transform.eulerAngles = cs.rotacja_aktualna;
        transform.Rotate(new Vector3(0f, 30f, 0f)*Time.deltaTime);
        
    }
    //private void Start()
    //{
    //    cs.GetComponent<CarSelection>();
    //}
}
