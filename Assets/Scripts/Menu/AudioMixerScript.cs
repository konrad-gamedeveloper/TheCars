using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerScript : MonoBehaviour
{
    public AudioMixer mastermixer;

    public void SetSfxLvl(float SfxLvl)
    {
        mastermixer.SetFloat("sfxVol", SfxLvl);

    }
    public void SetVolLvl(float MusicLvl)
    {
        mastermixer.SetFloat("vol", MusicLvl);
    }
}
