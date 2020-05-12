using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo_Close_Script : MonoBehaviour
{
    public AudioClip Memo_Close_AudioClip;
    AudioSource Memo_Close_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Memo_Close_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Memo_Close_AudioClip_Function()
    {
        Memo_Close_Script_Audio_Source.PlayOneShot(Memo_Close_AudioClip);
    }
}
