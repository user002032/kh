using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upper_Light_Icon_Manager_Script : MonoBehaviour
{


    //↓右上のアイコン画像を表示しているボタンの表示のON/OFFを行う為のオブジェクト。
    public GameObject Upper_Light_Upper_Icon_Object;
    public GameObject Upper_Light_Middle_Icon_Object;
    public GameObject Upper_Light_Bottom_Icon_Object;

    //↓UIマスクイメージを入れている右上アイコンの上にかぶせているボタンの表示のON/OFFを行う為のオブジェクト。
    public GameObject Upper_Light_Upper_Button_Object;
    public GameObject Upper_Light_Middle_Button_Object;
    public GameObject Upper_Light_Bottom_Button_Object;

    public Button Upper_Light_Upper_Icon_Button;
    public Button Upper_Light_Middle_Icon_Button;
    public Button Upper_Light_Bottom_Icon_Button;

    public Sprite Memo_Sprite;
    public Sprite Stepladder_Sprite;
    public Sprite Hammer_Sprite;
    public Sprite Key_Sprite;

    public GameObject Middle_Item_Display_Image_Object;

    public GameObject Message_Text_Manager_Script_Object;

    Message_Text_Manager_Script message_Text_Manager_Script;


    public GameObject Message_Box_Manager_Script_Object;

    Message_Box_Manager_Script message_Box_Manager_Script;




    void Start()
    {
        Upper_Light_Icon_Button_Object_OFF_Function();

        message_Text_Manager_Script = Message_Text_Manager_Script_Object.GetComponent<Message_Text_Manager_Script>();

        message_Box_Manager_Script = Message_Box_Manager_Script_Object.GetComponent<Message_Box_Manager_Script>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }




    //▼右上のアイコンを非表示にする関数
    public void Upper_Light_Icon_Button_Object_OFF_Function()
    {
        Upper_Light_Upper_Button_Object.SetActive(false);
        Upper_Light_Middle_Button_Object.SetActive(false);
        Upper_Light_Bottom_Button_Object.SetActive(false);

        Upper_Light_Upper_Icon_Object.SetActive(false);
        Upper_Light_Middle_Icon_Object.SetActive(false);
        Upper_Light_Bottom_Icon_Object.SetActive(false);
    }
    //▲右上のアイコンを非表示にする関数


    //▼右上のアイコンを「Item_～～_Get_Flag」で管理し表示する関数
    public void Upper_Light_Icon_Button_Object_ON_Function()
    {
        if (Static_variable_Manager_Script.upper_Light_Upper_Icon_Item_Get_Flag_bool == true)
        {
            Upper_Light_Upper_Icon_Object.SetActive(true);
            Upper_Light_Upper_Button_Object.SetActive(true);
        }

        if (Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool == true)
        {
            Upper_Light_Middle_Icon_Object.SetActive(true);
            Upper_Light_Middle_Button_Object.SetActive(true);
        }

        if (Static_variable_Manager_Script.upper_Light_Bottom_Icon_Item_Get_Flag_bool == true)
        {
            Upper_Light_Bottom_Icon_Object.SetActive(true);
            Upper_Light_Bottom_Button_Object.SetActive(true);
        }
    }
    //▲右上のアイコンを「Item_～～_Get_Flag」で管理し表示する関数




    //▼右上上段のアイコンを押した際の挙動の管理
    public void Upper_Light_Upper_Icon_Button_Push_Function()
    {
        Middle_Item_Display_Image_Object.GetComponent<Image>().sprite = Memo_Sprite;

        message_Text_Manager_Script.Message_Box_Text.text = "メモだ。\nヒントが書かれている。";

        Static_variable_Manager_Script.message_Box_Bottom_Animator_Select_Flag_bool = true;
        message_Box_Manager_Script.Message_Box_Bottom_Animator_For_Coroutine_Function();
    }
    //▲右上上段のアイコンを押した際の挙動の管理




    //▼右上中段のアイコンを押した際の挙動の管理（脚立時）
    public void Upper_Light_Middle_Icon_Button_Push_Function()
    {
        if (Static_variable_Manager_Script.item_Use_Flag_StepLadder_bool == false)
        {
            Middle_Item_Display_Image_Object.GetComponent<Image>().sprite = Stepladder_Sprite;
            message_Text_Manager_Script.Message_Box_Text.text = "脚立だ。\n高い所に手が\n届くかもしれない。";
        }else if(Static_variable_Manager_Script.item_Use_Flag_StepLadder_bool == true)
        {
            Middle_Item_Display_Image_Object.GetComponent<Image>().sprite = Hammer_Sprite;
            message_Text_Manager_Script.Message_Box_Text.text = "頑丈な金槌だ。\n何か硬いものを\n叩けるかもしれない。";
        }

        message_Box_Manager_Script.Message_Box_Bottom_Animator_For_Coroutine_Function();
    }
    //▲右上中段のアイコンを押した際の挙動の管理（脚立時）



    //▼右上上段のアイコンを押した際の挙動の管理
    public void Upper_Light_Buttom_Icon_Button_Push_Function()
    {
        Middle_Item_Display_Image_Object.GetComponent<Image>().sprite = Key_Sprite;

        message_Text_Manager_Script.Message_Box_Text.text = "金色の鍵だ。\nどこかを開ける事が\nできるかもしれない。";

        message_Box_Manager_Script.Message_Box_Bottom_Animator_For_Coroutine_Function();
    }
    //▲右上上段のアイコンを押した際の挙動の管理

}
