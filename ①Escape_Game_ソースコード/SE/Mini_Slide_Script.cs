using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mini_Slide_Script : MonoBehaviour
{
    public AudioClip Mini_Slide_AudioClip;
    AudioSource Mini_Slide_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Mini_Slide_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Mini_Slide_AudioClip_Function()
    {
        Mini_Slide_Script_Audio_Source.PlayOneShot(Mini_Slide_AudioClip);
    }
}
