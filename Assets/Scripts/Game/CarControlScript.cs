using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarControlScript : MonoBehaviour
{
  
    public float fromSlipBreak = 5;
    public float fromSlipBreak2 = 4;
    public Vector3 lastCheckpoint_position;
    public Vector3 lastCheckpoint_rotation;
    public bool flaga_first = false;
    public Rigidbody rigidbody;
    public WheelFrictionCurve poslizg_hamowane = new WheelFrictionCurve();
    public WheelFrictionCurve poslizg_normalne = new WheelFrictionCurve();
    public Vector3 center;
    public int nitro_fuel = 100;


    public GameObject text;


    private float m_horizontalImput;
    private float m_verticalImput;
    private float m_steeringAngle;


    public WheelCollider przednie_kiero_C, przednie_pas_C;
    public WheelCollider tylnie_kiero_C, tylnie_pas_C;

    public Transform przednie_kiero_T, przednie_pas_T;
    public Transform tylnie_kiero_T, tylnie_pas_T;
    public float maxSteerAngle = 10;
    public float motorForce = 1000;
    public float breakForce = 100;
    public float motorforceBOOST = 2500;
    public Vector3 startPosition;



    public Renderer breakLights;

    //public Material breakLightsON;
    //public Material breakLightsOFF;


    public int punkty = 0;


    private float predkosc;
    public Rigidbody samochodRigidbody;
    public Text predkoscTekst;

    public WheelFrictionCurve setPoslizg(float extremumSlip, float extremumValue, float asymptoteSlip, float asymptoteValue, float stiffness)
    {
        WheelFrictionCurve p = new WheelFrictionCurve();
        p.extremumSlip = extremumSlip;
        p.extremumValue = extremumValue;
        p.asymptoteSlip = asymptoteSlip;
        p.asymptoteValue = asymptoteValue;
        p.stiffness = stiffness;

        return p;
    }


    public void GetInput()
    {
        m_horizontalImput = Input.GetAxis("Horizontal");
        m_verticalImput = Input.GetAxis("Vertical");
        //  Debug.Log("m_horizontalImput :" + m_horizontalImput);
    }

    public void Steer()
    {
        m_horizontalImput *= maxSteerAngle;
        // Debug.Log("maxSteerAngle :" + maxSteerAngle);
        // Debug.Log("m_horizontalImput2 :" + m_horizontalImput);
        przednie_kiero_C.steerAngle = m_horizontalImput;
        przednie_pas_C.steerAngle = m_horizontalImput;
    }

    public void Accelerate()
    {

        // Debug.Log(przednie_kiero_C.motorTorque);
        przednie_kiero_C.motorTorque = m_verticalImput * motorForce;
        przednie_pas_C.motorTorque = m_verticalImput * motorForce;
        tylnie_kiero_C.motorTorque = m_verticalImput * motorForce;
        tylnie_pas_C.motorTorque = m_verticalImput * motorForce;

    }

    public void Hamowanko()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            fromSlipBreak = 5;
            fromSlipBreak = 4;

            tylnie_kiero_C.sidewaysFriction = setPoslizg(4, 1, 5, 1, 1);
            tylnie_pas_C.sidewaysFriction = setPoslizg(4, 1, 5, 1, 1);
            przednie_kiero_C.sidewaysFriction = setPoslizg(4, 1, 5, 1, 1);
            przednie_pas_C.sidewaysFriction = setPoslizg(4, 1, 5, 1, 1);



            przednie_kiero_C.brakeTorque += 0.0001f * breakForce;
            przednie_pas_C.brakeTorque += 0.0001f * breakForce;
            // tylnie_kiero_C.brakeTorque += 0.0003f * breakForce;
            // tylnie_pas_C.brakeTorque += 0.0003f * breakForce;

          //  breakLights.material = breakLightsON;
        }
        else
        {


            scaleSlipAfterBreak(fromSlipBreak, 1, fromSlipBreak2, 1);


            przednie_kiero_C.brakeTorque = 0;
            przednie_pas_C.brakeTorque = 0;
            tylnie_kiero_C.brakeTorque = 0;
            tylnie_pas_C.brakeTorque = 0;

          //  breakLights.material = breakLightsOFF;
        }

    }


    public void LadowankoNitra()
    {
        if ((Input.GetKey(KeyCode.Space))
            && ((Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D)))
            && przednie_kiero_C.motorTorque > motorForce - 10)
        {
            if (nitro_fuel < 100)
                nitro_fuel += 1;
        }
    }

    public void scaleSlipAfterBreak(float from_slip, float to_slip, float from_slip2, float to_slip2)
    {

        if (fromSlipBreak > to_slip) fromSlipBreak -= 5 * Time.deltaTime;

        if (fromSlipBreak2 > to_slip2) fromSlipBreak2 -= 5 * Time.deltaTime;

        tylnie_kiero_C.sidewaysFriction = setPoslizg(fromSlipBreak, 2, fromSlipBreak2, 1, 1);
        tylnie_pas_C.sidewaysFriction = setPoslizg(fromSlipBreak, 2, fromSlipBreak2, 1, 1);
        przednie_kiero_C.sidewaysFriction = setPoslizg(fromSlipBreak, 2, fromSlipBreak2, 1, 1);
        przednie_pas_C.sidewaysFriction = setPoslizg(fromSlipBreak, 2, fromSlipBreak2, 1, 1);

    }


    public void UpdateWheelPose(WheelCollider w, Transform t)
    {
        Vector3 pozycja;
        //  Debug.Log(pozycja);
        Quaternion rotacja;
        //   Debug.Log(rotacja);

        w.GetWorldPose(out pozycja, out rotacja);

        t.position = pozycja;
        t.rotation = rotacja;


    }

    public void Boost()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (nitro_fuel > 0)
            {
                motorForce = motorforceBOOST;
                nitro_fuel -= 1;
            }
        }
        else
        {
            motorForce = 1000;
        }
    }
    public void lastCheckpointJump()
    {
        if ((Input.GetKey("r")))
        {
            rigidbody.isKinematic = true;
            rigidbody.isKinematic = false;

            gameObject.transform.position = lastCheckpoint_position;
            gameObject.transform.eulerAngles = lastCheckpoint_rotation;
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "META")
        {
            text.SetActive(true);
        }
    }

    public void Predkosciomierz()
    {
        predkosc = Vector3.Dot(samochodRigidbody.velocity, transform.forward) * 10;

        if(predkosc > 0)
        {
            predkoscTekst.text = predkosc.ToString("0");
        }
        else
        {
            predkoscTekst.text = "0";
        }

    }


    public void UpdateWheelPoses()
    {
        UpdateWheelPose(przednie_kiero_C, przednie_kiero_T);
        UpdateWheelPose(przednie_pas_C, przednie_pas_T);
        UpdateWheelPose(tylnie_kiero_C, tylnie_kiero_T);
        UpdateWheelPose(tylnie_pas_C, tylnie_pas_T);
    }

    public void Start()
    {
        rigidbody.centerOfMass = center;
        startPosition = gameObject.transform.position;
        text = GameObject.FindGameObjectWithTag("Text");
        text.SetActive(false);
    }

    public void FixedUpdate()
    {
        //  Debug.Log(" nitro_fuel " + nitro_fuel + " przednie_kiero_C.motorTorque " + przednie_kiero_C.motorTorque);
        LadowankoNitra();
        Boost();
        Hamowanko();
        lastCheckpointJump();
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        Predkosciomierz();

    }


}
