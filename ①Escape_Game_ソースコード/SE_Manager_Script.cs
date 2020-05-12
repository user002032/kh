using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SE_Manager_Script : MonoBehaviour
{
    Dial_Lock_Open_Script dial_Lock_Open_Script;
    Door_Lock_Script door_Lock_Script;
    Fastener_Break_Script fastener_Break_Script;
    Footsteps_Script footsteps_Script;
    Item_Get_Script item_Get_Script;
    Memo_Close_Script memo_Close_Script;
    Memo_Use_Script memo_Use_Script;
    Safe_Box_Open_Script safe_Box_Open_Script;
    Chain_Script chain_Script;
    Mini_Slide_Script mini_Slide_Script;
    Stepladder_Set_Script stepladder_Set_Script;
    Button_Push_Script button_Push_Script;
    Turn_The_Dial_Script turn_The_Dial_Script;
    Safe_Box_Key_OFF_Script safe_Box_Key_OFF_Script;
    Item_Box_Open_Script item_Box_Open_Script;
    Key_Turn_Script key_turn_Script;
    Door_Open_Script door_Open_Script;

    public GameObject Dial_Lock_Open_SE_Only_Object;
    public GameObject Door_Lock_SE_Only_Object;
    public GameObject Fastener_Break_SE_Only_Object;
    public GameObject Footsteps_SE_Only_Object;
    public GameObject Item_Get_SE_Only_Object;
    public GameObject Memo_Close_SE_Only_Object;
    public GameObject Memo_Use_SE_Only_Object;
    public GameObject Safe_Box_Open_SE_Only_Object;
    public GameObject Chain_SE_Only_Object;
    public GameObject Mini_Slide_SE_Only_Object;
    public GameObject Stepladder_Set_SE_Only_Object;
    public GameObject Button_Push_SE_Only_Object;
    public GameObject Turn_The_Dial_SE_Only_Object;
    public GameObject Safe_Box_Key_OFF_SE_Only_Object;
    public GameObject Item_Box_Open_SE_Only_Object;
    public GameObject Key_Turn_Script_SE_Only_Object;
    public GameObject Door_Open_Script_SE_Only_Object;


    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        Door_Lock_SE_Only_Object = GameObject.Find("Door_Lock");


        door_Lock_Script = Door_Lock_SE_Only_Object.GetComponent<Door_Lock_Script>();
        dial_Lock_Open_Script = Dial_Lock_Open_SE_Only_Object.GetComponent<Dial_Lock_Open_Script>();
        door_Lock_Script = Door_Lock_SE_Only_Object.GetComponent<Door_Lock_Script>();
        fastener_Break_Script = Fastener_Break_SE_Only_Object.GetComponent<Fastener_Break_Script>();
        footsteps_Script = Footsteps_SE_Only_Object.GetComponent<Footsteps_Script>();
        item_Get_Script = Item_Get_SE_Only_Object.GetComponent<Item_Get_Script>();
        memo_Close_Script = Memo_Close_SE_Only_Object.GetComponent<Memo_Close_Script>();
        memo_Use_Script = Memo_Use_SE_Only_Object.GetComponent<Memo_Use_Script>();
        safe_Box_Open_Script = Safe_Box_Open_SE_Only_Object.GetComponent<Safe_Box_Open_Script>();
        chain_Script = Chain_SE_Only_Object.GetComponent<Chain_Script>();
        mini_Slide_Script = Mini_Slide_SE_Only_Object.GetComponent<Mini_Slide_Script>();
        stepladder_Set_Script = Stepladder_Set_SE_Only_Object.GetComponent<Stepladder_Set_Script>();
        button_Push_Script = Button_Push_SE_Only_Object.GetComponent<Button_Push_Script>();
        turn_The_Dial_Script = Turn_The_Dial_SE_Only_Object.GetComponent<Turn_The_Dial_Script>();
        safe_Box_Key_OFF_Script = Safe_Box_Key_OFF_SE_Only_Object.GetComponent<Safe_Box_Key_OFF_Script>();
        item_Box_Open_Script = Item_Box_Open_SE_Only_Object.GetComponent<Item_Box_Open_Script>();
        key_turn_Script = Key_Turn_Script_SE_Only_Object.GetComponent<Key_Turn_Script>();
        door_Open_Script = Door_Open_Script_SE_Only_Object.GetComponent<Door_Open_Script>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void SE_1_Footsteps_AudioClip_Function()
    {
        footsteps_Script.SE_Footsteps_AudioClip_Function();
    }

    public void SE_2_Memo_Use_AudioClip_Function()
    {
        memo_Use_Script.SE_Memo_Use_AudioClip_Function();
    }

    public void SE_3_Memo_Close_AudioClip_Function()
    {
        memo_Close_Script.SE_Memo_Close_AudioClip_Function();
    }


    public void SE_4_Item_Get_AudioClip_Function()
    {
        item_Get_Script.SE_Item_Get_AudioClip_Function();
    }

    public void SE_5_Safe_Box_Key_OFF_AudioClip_Function()
    {
        safe_Box_Key_OFF_Script.SE_Safe_Box_Key_OFF_AudioClip_Function();
    }

    public void SE_6_Door_Lock_AudioClip_Function()
    {
        door_Lock_Script.SE_Door_Lock_AudioClip_Function();
    }

    public void SE_7_Mini_Slide_AudioClip_Function()
    {
        mini_Slide_Script.SE_Mini_Slide_AudioClip_Function();
    }
    public void SE_8_Chain_AudioClip_Function()
    {
        chain_Script.SE_Chain_AudioClip_Function();
    }
    public void SE_9_button_Push_AudioClip_Function()
    {
        button_Push_Script.SE_Button_Push_AudioClip_Function();
    }
    public void SE_10_Turn_The_Dial_AudioClip_Function()
    {
        turn_The_Dial_Script.SE_Turn_The_Dial_AudioClip_Function();
    }
    public void SE_11_Dial_Lock_Open_AudioClip_Function()
    {
        dial_Lock_Open_Script.SE_Dial_Lock_Open_AudioClip_Function();
    }

    public void SE_12_Stepladder_Set_AudioClip_Function()
    {
        stepladder_Set_Script.SE_Stepladder_Set_AudioClip_Function();
    }

    public void SE_13_Safe_Box_Open_AudioClip_Function()
    {
         safe_Box_Open_Script.SE_Safe_Box_Open_AudioClip_Function();
    }

    public void SE_14_Item_Box_Open_AudioClip_Function()
    {
        item_Box_Open_Script.SE_Item_Box_Open_AudioClip_Function();
    }

    public void SE_15_Door_Open_AudioClip_Function()
    {
        door_Open_Script.SE_Door_Open_AudioClip_Function();
    }


    //▼ドアのＳＥの音の出力と、ＳＥの終了判定を併用した関数。
    public IEnumerator Wall_NO_7_SE_Coroutine()
    {
        Door_Lock_SE_Only_Object = GameObject.Find("Door_Lock");
        door_Lock_Script = Door_Lock_SE_Only_Object.GetComponent<Door_Lock_Script>();


        door_Lock_Script.Door_Lock_Script_Audio_Source.PlayOneShot(door_Lock_Script.Door_Lock_AudioClip);

        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!door_Lock_Script.Door_Lock_Script_Audio_Source.isPlaying)
            {
                yield return new WaitForSeconds(0.8f);
                break;
            }
        }
    }
    //▲ドアのＳＥの音の出力と、ＳＥの終了判定を併用した関数。


    //▼金庫の鍵が開くＳＥの音の出力と、ＳＥの終了判定を併用した関数。
    public IEnumerator Wall_NO_5_SE_Coroutine()
    {
        Safe_Box_Key_OFF_SE_Only_Object = GameObject.Find("Safe_Box_Key_OFF");
        safe_Box_Key_OFF_Script = Safe_Box_Key_OFF_SE_Only_Object.GetComponent<Safe_Box_Key_OFF_Script>();


        safe_Box_Key_OFF_Script.Safe_Box_Key_OFF_Script_Audio_Source.PlayOneShot(safe_Box_Key_OFF_Script.Safe_Box_Key_OFF_AudioClip);

        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!safe_Box_Key_OFF_Script.Safe_Box_Key_OFF_Script_Audio_Source.isPlaying)
            {
                yield return new WaitForSeconds(0.8f);
                break;
            }
        }
    }
    //▲金庫の鍵が開くＳＥの音の出力と、ＳＥの終了判定を併用した関数。

    //▼金槌で丸棒を叩くＳＥの音の出力と、ＳＥの終了判定を併用した関数。
    public IEnumerator Wall_NO_3_SE_Coroutine()
    {
        Fastener_Break_SE_Only_Object = GameObject.Find("Fastener_Break");
        fastener_Break_Script = Fastener_Break_SE_Only_Object.GetComponent<Fastener_Break_Script>();


        fastener_Break_Script.Fastener_Break_Script_Audio_Source.PlayOneShot(fastener_Break_Script.Fastener_Break_AudioClip);

        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!fastener_Break_Script.Fastener_Break_Script_Audio_Source.isPlaying)
            {
                yield return new WaitForSeconds(0.8f);
                break;
            }
        }
    }
    //▲金槌で丸棒を叩くＳＥの音の出力と、ＳＥの終了判定を併用した関数。

    //▼鍵を鍵穴に通して回すＳＥの音の出力と、ＳＥの終了判定を併用した関数。
    public IEnumerator Wall_NO_Key_Trun_SE_Coroutine()
    {
        Key_Turn_Script_SE_Only_Object = GameObject.Find("Key_Turn");
        key_turn_Script = Key_Turn_Script_SE_Only_Object.GetComponent<Key_Turn_Script>();


        key_turn_Script.Key_Turn_Script_Audio_Source.PlayOneShot(key_turn_Script.Key_Turn_AudioClip);

        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!key_turn_Script.Key_Turn_Script_Audio_Source.isPlaying)
            {
                yield return new WaitForSeconds(0.8f);
                break;
            }
        }
    }
    //▲鍵を鍵穴に通して回すＳＥの音の出力と、ＳＥの終了判定を併用した関数。
}
