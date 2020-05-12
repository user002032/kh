using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain_Script : MonoBehaviour
{
    public AudioClip Chain_AudioClip;
    AudioSource Chain_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Chain_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Chain_AudioClip_Function()
    {
        Chain_Script_Audio_Source.PlayOneShot(Chain_AudioClip);
    }
}
