using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe_Box_Open_Script : MonoBehaviour
{
    public AudioClip Safe_Box_Open_AudioClip;
    AudioSource Safe_Box_Open_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Safe_Box_Open_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Safe_Box_Open_AudioClip_Function()
    {
        Safe_Box_Open_Script_Audio_Source.PlayOneShot(Safe_Box_Open_AudioClip);
    }
}
