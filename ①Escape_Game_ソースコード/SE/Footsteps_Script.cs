using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps_Script : MonoBehaviour
{
    public AudioClip Footsteps_AudioClip;
    AudioSource Footsteps_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Footsteps_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Footsteps_AudioClip_Function()
    {
        Footsteps_Script_Audio_Source.PlayOneShot(Footsteps_AudioClip);
    }
}
