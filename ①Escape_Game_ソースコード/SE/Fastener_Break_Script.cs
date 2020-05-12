using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fastener_Break_Script : MonoBehaviour
{
    public AudioClip Fastener_Break_AudioClip;
    public AudioSource Fastener_Break_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Fastener_Break_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Fastener_Break_AudioClip_Function()
    {
        Fastener_Break_Script_Audio_Source.PlayOneShot(Fastener_Break_AudioClip);
    }
}
