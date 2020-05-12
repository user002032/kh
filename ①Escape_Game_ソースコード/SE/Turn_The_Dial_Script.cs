using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_The_Dial_Script : MonoBehaviour
{
    public AudioClip Turn_The_Dial_AudioClip;
    AudioSource Turn_The_Dial_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Turn_The_Dial_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Turn_The_Dial_AudioClip_Function()
    {
        Turn_The_Dial_Script_Audio_Source.PlayOneShot(Turn_The_Dial_AudioClip);
    }
}
