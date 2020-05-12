using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memo_Use_Script : MonoBehaviour
{
    public AudioClip Memo_Use_AudioClip;
    AudioSource Memo_Use_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Memo_Use_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Memo_Use_AudioClip_Function()
    {
        Memo_Use_Script_Audio_Source.PlayOneShot(Memo_Use_AudioClip);
    }
}
