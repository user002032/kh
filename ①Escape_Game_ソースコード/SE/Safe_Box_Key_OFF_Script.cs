using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe_Box_Key_OFF_Script : MonoBehaviour
{
    public AudioClip Safe_Box_Key_OFF_AudioClip;
    public AudioSource Safe_Box_Key_OFF_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Safe_Box_Key_OFF_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SE_Safe_Box_Key_OFF_AudioClip_Function()
    {
        Safe_Box_Key_OFF_Script_Audio_Source.PlayOneShot(Safe_Box_Key_OFF_AudioClip);
    }
}
