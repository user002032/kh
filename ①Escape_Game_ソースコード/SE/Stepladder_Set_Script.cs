using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stepladder_Set_Script : MonoBehaviour
{
    public AudioClip Stepladder_Set_AudioClip;
    AudioSource Stepladder_Set_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Stepladder_Set_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Stepladder_Set_AudioClip_Function()
    {
        Stepladder_Set_Script_Audio_Source.PlayOneShot(Stepladder_Set_AudioClip);
    }
}
