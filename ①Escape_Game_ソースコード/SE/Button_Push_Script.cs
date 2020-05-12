using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botton_Push_Script : MonoBehaviour
{
    public AudioClip Botton_Push_Open_AudioClip;
    AudioSource Botton_Push_Open_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Botton_Push_Open_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Botton_Push_AudioClip_Function()
    {
        Botton_Push_Open_Script_Audio_Source.PlayOneShot(Botton_Push_Open_AudioClip);
    }
}
