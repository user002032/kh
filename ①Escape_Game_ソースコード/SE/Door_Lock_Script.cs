using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Lock_Script : MonoBehaviour
{
    public AudioClip Door_Lock_AudioClip;
    public AudioSource Door_Lock_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Door_Lock_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SE_Door_Lock_AudioClip_Function()
    {
        Door_Lock_Script_Audio_Source.PlayOneShot(Door_Lock_AudioClip);
    }

}
