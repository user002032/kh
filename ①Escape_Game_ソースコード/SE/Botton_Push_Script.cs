using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button_Push_Script : MonoBehaviour
{
    public AudioClip Button_Push_Open_AudioClip;
    AudioSource Button_Push_Open_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Button_Push_Open_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Button_Push_AudioClip_Function()
    {
        Button_Push_Open_Script_Audio_Source.PlayOneShot(Button_Push_Open_AudioClip);
    }
}
