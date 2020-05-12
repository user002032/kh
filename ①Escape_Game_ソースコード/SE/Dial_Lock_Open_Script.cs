using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial_Lock_Open_Script : MonoBehaviour
{
    public AudioClip Dial_Lock_Open_AudioClip;
    AudioSource Dial_Lock_Open_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Dial_Lock_Open_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Dial_Lock_Open_AudioClip_Function()
    {
        Dial_Lock_Open_Script_Audio_Source.PlayOneShot(Dial_Lock_Open_AudioClip);
    }
}
