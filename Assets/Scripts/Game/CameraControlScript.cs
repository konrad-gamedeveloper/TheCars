using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{
    public Transform car;
    public Vector3 offset_cam;


    public void Obracanko()
    {


        Vector3 obiekt_na_ktory_patrzy_kamera = car.position - this.transform.position;
        Quaternion rotacja_kamery_wzgledem_obiektu = Quaternion.LookRotation(obiekt_na_ktory_patrzy_kamera, new Vector3(0, 1, 0));
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotacja_kamery_wzgledem_obiektu, 10 * Time.deltaTime);
    }

    public void Sledzonko()
    {
        Vector3 target = car.position + car.forward * offset_cam.z + car.right * offset_cam.x + car.up * offset_cam.y;
        this.transform.position = Vector3.Lerp(transform.position, target, 10 * Time.deltaTime);
    }


    void FixedUpdate()
    {
        if (car.position.y > 110f)
        {
            Obracanko();
            Sledzonko();
        }

    }
}
