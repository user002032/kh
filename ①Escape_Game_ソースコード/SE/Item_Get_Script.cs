using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Get_Script : MonoBehaviour
{
    public AudioClip Item_Get_AudioClip;
    AudioSource Item_Get_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Item_Get_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Item_Get_AudioClip_Function()
    {
        Item_Get_Script_Audio_Source.PlayOneShot(Item_Get_AudioClip);
    }
}
