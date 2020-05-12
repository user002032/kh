using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Box_Open_Script : MonoBehaviour
{
    public AudioClip Item_Box_Open_AudioClip;
    AudioSource Item_Box_Open_Script_Audio_Source;


    // Start is called before the first frame update
    void Start()
    {
        Item_Box_Open_Script_Audio_Source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SE_Item_Box_Open_AudioClip_Function()
    {
        Item_Box_Open_Script_Audio_Source.PlayOneShot(Item_Box_Open_AudioClip);
    }
}
