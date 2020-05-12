using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Turn_Script : MonoBehaviour
{
    public AudioClip Key_Turn_AudioClip;
    public AudioSource Key_Turn_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Key_Turn_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Item_Box_Open_AudioClip_Function()
    {
        Key_Turn_Script_Audio_Source.PlayOneShot(Key_Turn_AudioClip);
    }
}
