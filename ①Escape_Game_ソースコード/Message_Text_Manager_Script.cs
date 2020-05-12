using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Message_Text_Manager_Script : MonoBehaviour
{

    private const int wall_No_1 = 1;
    private const int wall_No_2 = 2;
    private const int wall_No_3 = 3;
    private const int wall_No_4 = 4;
    private const int wall_No_5 = 5;
    private const int wall_No_6 = 6;
    private const int wall_No_7 = 7;
    private const int wall_No_8 = 8;

    public TextMeshProUGUI Message_Box_Text;

    bool wall_NO_4_Message_Text_Change_Flag = false;
    bool wall_NO_5_Message_Text_Change_Flag = false;
    bool wall_NO_7_Message_Text_Change_Flag = false;
    bool wall_NO_8_Message_Text_Change_Flag = false;

    public GameObject Message_Box_Manager_Script_Object;
    public GameObject Game_Manager_Object;
    public GameObject Hint_Manager_Script_Object;

    Hint_Manager_Script hint_Manager_Script;
    Message_Box_Manager_Script message_Box_Manager_Script;
    GameManager game_Manager_Script;

    

    void Start()
    {
        wall_NO_4_Message_Text_Change_Flag = false;
        wall_NO_5_Message_Text_Change_Flag = false;
        wall_NO_7_Message_Text_Change_Flag = false;
        wall_NO_8_Message_Text_Change_Flag = false;

        message_Box_Manager_Script = Message_Box_Manager_Script_Object.GetComponent<Message_Box_Manager_Script>();

        hint_Manager_Script = Hint_Manager_Script_Object.GetComponent<Hint_Manager_Script>();

        game_Manager_Script = Game_Manager_Object.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Message_Box_Text_Change_Function()
    {
        if (Static_variable_Manager_Script.zoom_Scene_Flag_bool == false)
        {
            switch (Static_variable_Manager_Script.wall_Number_int)
            {
                case wall_No_1:
                    Message_Box_Text.text = "此方は行き止まりだ。";
                    break;

                case wall_No_2:

                    if (Static_variable_Manager_Script.event_Flag_No_1_bool == true && Static_variable_Manager_Script.text_change_Count_int == 0)
                    {
                        Message_Box_Text.text = "ここは…？";
                        Static_variable_Manager_Script.text_change_Count_int = 1;

                        Static_variable_Manager_Script.event_While_Start_bool = true;
                    }
                    else if (Static_variable_Manager_Script.event_Flag_No_1_bool == true && Static_variable_Manager_Script.text_change_Count_int == 1)
                    {
                        Message_Box_Text.text = "何かがある。";
                        Static_variable_Manager_Script.text_change_Count_int = 2;
                        Static_variable_Manager_Script.item_Memo_Get_Flag_bool = true;
                    }

                    break;

                case wall_No_3:
                    Message_Box_Text.text = "メモが落ちていた場所だ。";
                    break;

                case wall_No_4:
                    if (Static_variable_Manager_Script.item_Get_Flag_Key_bool == false)
                    {
                        if (wall_NO_4_Message_Text_Change_Flag == false)
                        {
                            Message_Box_Text.text = "机の上に箱がある。";
                        }

                        if (wall_NO_4_Message_Text_Change_Flag == true)
                        {
                            Message_Box_Text.text = "机の上に金属の丸筒で\n閉じられた箱がある。";
                        }
                    }
                    else if (Static_variable_Manager_Script.item_Get_Flag_Key_bool == true)
                    {
                        Message_Box_Text.text = "箱の中には\nもう何も入っていない。";
                    }
                        break;

                case wall_No_5:
                    if (wall_NO_5_Message_Text_Change_Flag == false)
                    {
                        Message_Box_Text.text = "向こうに何かが、\n書かれている。";
                    }

                    if (wall_NO_5_Message_Text_Change_Flag == true)
                    {
                        Message_Box_Text.text = "向こうに恐らく\nヒントが書かれている。";
                    }
                    break;

                case wall_No_6:
                    if (Static_variable_Manager_Script.item_Use_Flag_StepLadder_bool == false)
                    {
                        if (Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool == false)
                        {
                            Message_Box_Text.text = "高い所に、\n金庫の様な物がある。\nジャンプしても、\n手は届きそうにない。";
                        }
                        else if (Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool == true){

                            if(Static_variable_Manager_Script.text_change_Count_int == 0) {
                                message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                                message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Message_Box_Text_Change_Function());
                                Message_Box_Text.text = "高い所に、\n金庫の様な物がある。\nジャンプしても、\n手は届きそうにない。";
                                Static_variable_Manager_Script.text_change_Count_int = 1;
                            }
                            else if (Static_variable_Manager_Script.text_change_Count_int == 1)
                            {
                                Message_Box_Text.text = "脚立を使えば届きそうだ。";
                                Static_variable_Manager_Script.text_change_Count_int = 2;
                            }
                            else if (Static_variable_Manager_Script.text_change_Count_int == 2)
                            {
                                Message_Box_Text.text = "脚立を使う？";
                                message_Box_Manager_Script.Message_Box_Button.enabled = false;
                                Static_variable_Manager_Script.choice_Set_Flag_bool = true;
                                Static_variable_Manager_Script.text_change_Count_int = 0;
                                message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function();
                                Static_variable_Manager_Script.event_Flag_No_5_bool = true;
                            }
                            else if (Static_variable_Manager_Script.text_change_Count_int == 3)
                            {
                                Static_variable_Manager_Script.item_Use_Flag_StepLadder_bool = true;
                                Static_variable_Manager_Script.event_Flag_No_5_bool = false;
                                Static_variable_Manager_Script.text_change_Count_int = 0;
                                Message_Box_Text.text = "脚立を置いた。";
                                message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                                message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());
                                Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool = false;
                            }
                        }
                    }
                    else if (Static_variable_Manager_Script.item_Use_Flag_StepLadder_bool == true)
                    {
                        if (Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool == false)
                        {
                            Message_Box_Text.text = "脚立を置いた。\n金庫のようなものに\n手が届きそうだ。";
                        }
                        else if (Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool == true)
                        {
                            Message_Box_Text.text = "金庫は空だ。\nもう何も無い。";
                        }
                    }
                        break;

                case wall_No_7:

                    if (Static_variable_Manager_Script.event_Flag_No_10_bool==false)
                    {
                        if (wall_NO_7_Message_Text_Change_Flag == false)
                        {
                            Message_Box_Text.text = "向こうに扉がある。";
                        }

                        if (wall_NO_7_Message_Text_Change_Flag == true)
                        {
                            Message_Box_Text.text = "向こうに、\n鍵のかかった扉がある。";
                        }
                    }
                    else if (Static_variable_Manager_Script.event_Flag_No_10_bool == true)
                    {
                        Message_Box_Text.text = "出口だ…！";
                    }


                    break;

                case wall_No_8:
                    if (Static_variable_Manager_Script.event_Flag_No_2_Text_bool == false)
                    {
                        if (wall_NO_8_Message_Text_Change_Flag == false)
                        {
                            Message_Box_Text.text = "向こうに脚立がある。";
                        }

                        if (wall_NO_8_Message_Text_Change_Flag == true)
                        {
                            Message_Box_Text.text = "向こうに鎖とダイヤル錠で\nつながれた脚立がある。";
                        }
                    }
                    else if (Static_variable_Manager_Script.event_Flag_No_2_Text_bool == true)
                    {
                        Message_Box_Text.text = "向こうにはもう何もない。";
                    }

                    break;

            }
        }
        else if(Static_variable_Manager_Script.zoom_Scene_Flag_bool == true)
        {


            switch (Static_variable_Manager_Script.wall_Number_int)
            {
                case wall_No_1:

                    break;
                case wall_No_2:

                    break;
                case wall_No_3:

                    break;
                case wall_No_4:

                    if(Static_variable_Manager_Script.item_Get_Flag_Hammer_bool == false)
                    {
                        if (Static_variable_Manager_Script.text_change_Count_int == 0)
                        {

                            Message_Box_Text.text = "鍵穴も無い箱がある。";
                            Static_variable_Manager_Script.text_change_Count_int = 1;
                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Message_Box_Text_Change_Function());

                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 1)
                        {
                            Message_Box_Text.text = "金属の丸棒で\n　固定されている。";
                            wall_NO_4_Message_Text_Change_Flag = true;
                            Static_variable_Manager_Script.text_change_Count_int = 2;
                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 2)
                        {
                            Message_Box_Text.text = "手で押したり、\n引っ張っても、\n手では固くて動かない。";
                            wall_NO_4_Message_Text_Change_Flag = true;
                            Static_variable_Manager_Script.text_change_Count_int = 0;
                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());
                        }
                    }
                    else if(Static_variable_Manager_Script.item_Get_Flag_Hammer_bool == true)
                    {
                        if(Static_variable_Manager_Script.item_Use_Flag_Hammer_bool == false)
                        {
                            if (Static_variable_Manager_Script.text_change_Count_int == 0)
                            {

                                Message_Box_Text.text = "鍵穴も無い箱がある。";
                                Static_variable_Manager_Script.text_change_Count_int = 1;
                                message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                                message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Message_Box_Text_Change_Function());

                            }
                            else if (Static_variable_Manager_Script.text_change_Count_int == 1)
                            {
                                Message_Box_Text.text = "金属の丸棒で\n　固定されている。";
                                Static_variable_Manager_Script.text_change_Count_int = 2;
                            }
                            else if (Static_variable_Manager_Script.text_change_Count_int == 2)
                            {
                                Message_Box_Text.text = "手で押したり、\n引っ張っても、\n手では固くて動かない。";
                                Static_variable_Manager_Script.text_change_Count_int = 3;
                            }
                            else if (Static_variable_Manager_Script.text_change_Count_int == 3)
                            {
                                Message_Box_Text.text = "金槌で金属の丸棒を\n押し込めるかもしれない。";
                                Static_variable_Manager_Script.text_change_Count_int = 4;
}
                            else if (Static_variable_Manager_Script.text_change_Count_int == 4)
                            {
                                Message_Box_Text.text = "金槌で叩いてみる？";
                                Static_variable_Manager_Script.choice_Set_Flag_bool = true;
                                Static_variable_Manager_Script.event_Flag_No_8_bool = true;
                                Static_variable_Manager_Script.text_change_Count_int = 5;
                                message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                                message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());
                                message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function();
                            }
                            else if (Static_variable_Manager_Script.text_change_Count_int == 5)
                            {
                                Message_Box_Text.text = "箱が開いた。";
                                Static_variable_Manager_Script.text_change_Count_int = 6;
                                message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                                message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Message_Box_Text_Change_Function());
                            }
                            else if (Static_variable_Manager_Script.text_change_Count_int == 6)
                            {
                                Message_Box_Text.text = "中に何か入っている。";
                                Static_variable_Manager_Script.event_Flag_No_8_bool = true;
                                Static_variable_Manager_Script.item_Use_Flag_Hammer_bool = true;
                                Static_variable_Manager_Script.text_change_Count_int = 0;
                                message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                                message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());
                            }
                        }
                        else if(Static_variable_Manager_Script.item_Use_Flag_Hammer_bool == true)
                        {
                            Message_Box_Text.text = "箱は空だ。";
                        }
                    }

                    break;


                case wall_No_5:

                    if (wall_NO_5_Message_Text_Change_Flag == false)
                    {
                        if (Static_variable_Manager_Script.text_change_Count_int == 0)
                        {
                            Static_variable_Manager_Script.text_change_Count_int = 1;
                            Message_Box_Text.text = "何か書かれている。";
                            Static_variable_Manager_Script.event_Flag_No_3_bool = true;
                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => game_Manager_Script.Event_Manager_Function());
                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 1)
                        {
                            Static_variable_Manager_Script.text_change_Count_int = 0;
                            Message_Box_Text.text = "これはもしかしたら\nヒントかもしれない。";
                            wall_NO_5_Message_Text_Change_Flag = true;
                        }
                    }
                    else if (wall_NO_5_Message_Text_Change_Flag == true)
                    {
                        if (Static_variable_Manager_Script.text_change_Count_int == 0)
                        {
                            Static_variable_Manager_Script.text_change_Count_int = 1;
                            Message_Box_Text.text = "恐らくヒントが\n書かれている。";
                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Message_Box_Text_Change_Function());
                            
                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 1)
                        {
                            Message_Box_Text.text = "もう一度確認する？";
                            message_Box_Manager_Script.Message_Box_Button.enabled = false;
                            Static_variable_Manager_Script.choice_Set_Flag_bool = true;
                            Static_variable_Manager_Script.event_Flag_No_4_bool = true;

                            Static_variable_Manager_Script.text_change_Count_int = 0; // メモ：ここでちゃんとcountを0にしておかないと、強制終了になるので注意。

                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());
                            message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function();
                        }
                    }
                    break;


                case wall_No_6:
                    if(Static_variable_Manager_Script.event_Flag_No_7_bool == false && Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool == false)
                    {
                        if (Static_variable_Manager_Script.text_change_Count_int == 0)
                        {
                            Message_Box_Text.text = "ボタンが三個、\n並んでいる。";
                            Static_variable_Manager_Script.text_change_Count_int = 1;
                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Message_Box_Text_Change_Function());
                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 1)
                        {
                            Message_Box_Text.text = "ボタンを押してみる？";
                            Static_variable_Manager_Script.event_Flag_No_6_bool = true;
                            Static_variable_Manager_Script.choice_Set_Flag_bool = true;

                            Static_variable_Manager_Script.text_change_Count_int = 0;

                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());
                            message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function();
                        }
                    }
                    if (Static_variable_Manager_Script.event_Flag_No_7_bool == true && Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool == false)
                    {
                        if (Static_variable_Manager_Script.text_change_Count_int == 0)
                        {
                            Message_Box_Text.text = "金庫が開いた。";
                            Static_variable_Manager_Script.text_change_Count_int = 1;
                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Message_Box_Text_Change_Function());
                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 1)
                        {
                            Message_Box_Text.text = "中に何か入っている。";
                            Static_variable_Manager_Script.text_change_Count_int = 2;
                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 2)
                        {
                            Message_Box_Text.text = "あれは…";
                            Static_variable_Manager_Script.text_change_Count_int = 3;
                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 3)
                        {
                            Static_variable_Manager_Script.text_change_Count_int = 0;
                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());
                            message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function();
                        }
                    }
                    if (Static_variable_Manager_Script.event_Flag_No_7_bool == false && Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool == true)
                    {
                        Message_Box_Text.text = "ここにはもう何も無い。";
                    }
                        break;

                case wall_No_7:

                    if (Static_variable_Manager_Script.item_Get_Flag_Key_bool == false)
                    {
                        if (Static_variable_Manager_Script.text_change_Count_int == 0)
                        {
                            Message_Box_Text.text = "扉には鍵がかかっていて\n開けられない。";
                            Static_variable_Manager_Script.text_change_Count_int = 1;
                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Message_Box_Text_Change_Function());

                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 1)
                        {
                            Message_Box_Text.text = "ここが出口かもしれない。";
                            wall_NO_7_Message_Text_Change_Flag = true;
                            Static_variable_Manager_Script.text_change_Count_int = 0;
                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());
                        }

                    }
                    if (Static_variable_Manager_Script.item_Get_Flag_Key_bool == true)
                    {
                        if (Static_variable_Manager_Script.text_change_Count_int == 0)
                        {
                            Message_Box_Text.text = "扉には鍵がかかっていて\n開けられない。";
                            Static_variable_Manager_Script.text_change_Count_int = 1;
                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Message_Box_Text_Change_Function());

                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 1)
                        {
                            Message_Box_Text.text = "鍵で開くかもしれない。";
                            Static_variable_Manager_Script.text_change_Count_int = 2;
                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 2)
                        {
                            Message_Box_Text.text = "鍵を使う？";
                            Static_variable_Manager_Script.text_change_Count_int = 0;

                            Static_variable_Manager_Script.event_Flag_No_9_bool = true;
                            Static_variable_Manager_Script.choice_Set_Flag_bool = true;

                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());
                            message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function();
                        }
                    }


                    break;

                case wall_No_8:
                    if (Static_variable_Manager_Script.event_Flag_No_2_Text_bool == false)
                    {
                        if (Static_variable_Manager_Script.text_change_Count_int == 0 && Static_variable_Manager_Script.event_Flag_No_2_bool == false)
                        {
                            wall_NO_8_Message_Text_Change_Flag = true;
                            Message_Box_Text.text = "脚立がある。";
                            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Message_Box_Text_Change_Function());
                            Static_variable_Manager_Script.text_change_Count_int = 1;
                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 1 && Static_variable_Manager_Script.event_Flag_No_2_bool == false)
                        {
                            Message_Box_Text.text = "だが、壁に備えられた鎖と";
                            Static_variable_Manager_Script.text_change_Count_int = 2;
                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 2 && Static_variable_Manager_Script.event_Flag_No_2_bool == false)
                        {
                            Message_Box_Text.text = "ダイヤル錠で、\n壁際に固定されている。";
                            Static_variable_Manager_Script.text_change_Count_int = 3;
                        }
                        else if (Static_variable_Manager_Script.text_change_Count_int == 3 && Static_variable_Manager_Script.event_Flag_No_2_bool == false)
                        {
                            Static_variable_Manager_Script.text_change_Count_int = 0;

                            //Static_variable_Manager_Script.choice_Ivent_Place_Hash_int = 100; 
                            // choiceの「いいえ」の個所を場所を判定する数字で条件分岐しようと考えたが保留（boolを一括でfalseにすればいいかもしれないため）。
                            //状況が変われば変更。使わなければ削除。

                            Static_variable_Manager_Script.event_Flag_No_2_bool = true;
                            Message_Box_Text.text = "ダイヤル錠を解いてみる？";
                            Static_variable_Manager_Script.choice_Set_Flag_bool = true;

                            message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function();
                        }
                    }
                    else if(Static_variable_Manager_Script.event_Flag_No_2_Text_bool == true)
                    {
                        Message_Box_Text.text = "ここにはもう何もない。";
                    }
                    break;
            }
        }
    }

}
