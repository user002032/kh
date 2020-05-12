using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Open_Script : MonoBehaviour
{
    public AudioClip Door_Open_AudioClip;
    AudioSource Door_Open_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Door_Open_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Door_Open_AudioClip_Function()
    {
        Door_Open_Script_Audio_Source.PlayOneShot(Door_Open_AudioClip);
    }
}
