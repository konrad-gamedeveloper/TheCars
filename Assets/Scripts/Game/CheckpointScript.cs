using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public CarControlScript cars;

    public Vector3 fixedRotation;
    public Vector3 fixedPosition;

    public void OnTriggerEnter(Collider other)
    {
        cars.lastCheckpoint_position = gameObject.transform.position;
        cars.lastCheckpoint_rotation = fixedRotation;

        cars.flaga_first = true;
    }

    private void Start()
    {
        cars = GameObject.Find("CAR_1").GetComponent<CarControlScript>();
    }


}