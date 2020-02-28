using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    private Slider volume;
    public AudioSource audio;
    void Start()
    {
        volume = GetComponent<Slider>();     
    }

    void Update()
    {
        audio.volume = volume.value;
    }
}
