using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager_Script : MonoBehaviour
{

    private const int wall_No_1 = 1;
    private const int wall_No_2 = 2;
    private const int wall_No_3 = 3;
    private const int wall_No_4 = 4;
    private const int wall_No_5 = 5;
    private const int wall_No_6 = 6;
    private const int wall_No_7 = 7;
    private const int wall_No_8 = 8;


    public GameObject UI_Forward_Button_Object;
    public GameObject UI_Left_Button_Object;
    public GameObject UI_Right_Button_Object;
    public GameObject UI_Back_Button_Object;

    public Button UI_Forward_Button;
    public Button UI_Left_Button;
    public Button UI_Right_Button;
    public Button UI_Back_Button;

    public TextMeshProUGUI UI_Back_Button_Arrows_Text;

    public GameObject Memo_Manager_Script_Object;
    public GameObject Message_Box_Manager_Script_Object;
    public GameObject Camera_Manager_Script_Object;
    public GameObject Hint_Manager_Script_Object;


    Memo_Manager_Script memo_Manager_Script;
    Message_Box_Manager_Script message_Box_Manager_Script;
    Camera_Manager_Script camera_Manager_Script;
    Hint_Manager_Script hint_Manager_Script;

    void Start()
    {
        memo_Manager_Script = Memo_Manager_Script_Object.GetComponent<Memo_Manager_Script>();
        message_Box_Manager_Script = Message_Box_Manager_Script_Object.GetComponent<Message_Box_Manager_Script>();
        camera_Manager_Script = Camera_Manager_Script_Object.GetComponent<Camera_Manager_Script>();

        hint_Manager_Script = Hint_Manager_Script_Object.GetComponent<Hint_Manager_Script>();

        UI_Display_OFF_Function();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    //▼Static_variable_Manager_Script.wall_Number_intに応じてUIを表示する関数
    [Obsolete]
    public void UI_Display_ON_Function()
    {
        hint_Manager_Script.Hint_Object.SetActive(false);
        memo_Manager_Script.Memo_Object.SetActive(false);

        message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
        message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());


        if (Static_variable_Manager_Script.zoom_Scene_Flag_bool == false)
        {

            if (Static_variable_Manager_Script.wall_Number_int >= 4 && Static_variable_Manager_Script.zoom_Scene_Flag_bool == false)
            {
                UI_Back_Button_Arrows_Text.text = "▲";
            }
            else if (Static_variable_Manager_Script.wall_Number_int <= 3 && Static_variable_Manager_Script.zoom_Scene_Flag_bool == false)
            {
                UI_Back_Button_Arrows_Text.text = "▼";
            }



            switch (Static_variable_Manager_Script.wall_Number_int)
            {
                case wall_No_1:
                    message_Box_Manager_Script.Message_Box_Button.enabled = true;
                    UI_Forward_Button_Object.SetActive(false);
                    UI_Back_Button_Object.SetActive(true);
                    UI_Left_Button_Object.SetActive(false);
                    UI_Right_Button_Object.SetActive(false);
                    break;

                case wall_No_2:
                    message_Box_Manager_Script.Message_Box_Button.enabled = false;
                    UI_Forward_Button_Object.SetActive(true);
                    UI_Back_Button_Object.SetActive(true);
                    UI_Left_Button_Object.SetActive(false);
                    UI_Right_Button_Object.SetActive(false);
                    break;

                case wall_No_3:
                    message_Box_Manager_Script.Message_Box_Button.enabled = true;
                    UI_Forward_Button_Object.SetActive(false);
                    UI_Back_Button_Object.SetActive(false);
                    UI_Left_Button_Object.SetActive(true);
                    UI_Right_Button_Object.SetActive(true);
                    break;

                case wall_No_4:
                    message_Box_Manager_Script.Message_Box_Button.enabled = true;
                    UI_Forward_Button_Object.SetActive(false);
                    UI_Back_Button_Object.SetActive(true);
                    UI_Left_Button_Object.SetActive(true);
                    UI_Right_Button_Object.SetActive(true);
                    break;

                case wall_No_5:
                    message_Box_Manager_Script.Message_Box_Button.enabled = true;
                    UI_Forward_Button_Object.SetActive(false);
                    UI_Back_Button_Object.SetActive(true);
                    UI_Left_Button_Object.SetActive(true);
                    UI_Right_Button_Object.SetActive(true);
                    break;

                case wall_No_6:
                    message_Box_Manager_Script.Message_Box_Button.enabled = true;

                    UI_Forward_Button_Object.SetActive(false);
                    UI_Left_Button_Object.SetActive(true);
                    UI_Right_Button_Object.SetActive(true);

                    if (Static_variable_Manager_Script.item_Use_Flag_StepLadder_bool == false)
                    {
                        UI_Back_Button_Object.SetActive(false);

                    }
                    else if (Static_variable_Manager_Script.item_Use_Flag_StepLadder_bool == true)
                    {
                        UI_Back_Button_Object.SetActive(true);
                    }

                    break;

                case wall_No_7:
                    message_Box_Manager_Script.Message_Box_Button.enabled = true;
                    UI_Forward_Button_Object.SetActive(false);
                    UI_Back_Button_Object.SetActive(true);
                    UI_Left_Button_Object.SetActive(true);
                    UI_Right_Button_Object.SetActive(true);
                    break;

                case wall_No_8:
                    message_Box_Manager_Script.Message_Box_Button.enabled = true;
                    UI_Forward_Button_Object.SetActive(false);
                    UI_Back_Button_Object.SetActive(true);
                    UI_Left_Button_Object.SetActive(true);
                    UI_Right_Button_Object.SetActive(false);
                    break;
            }
        }
        else if (Static_variable_Manager_Script.zoom_Scene_Flag_bool == true)
        {
            message_Box_Manager_Script.Message_Box_Button.enabled = true;


            UI_Left_Button_Object.SetActive(false);
            UI_Right_Button_Object.SetActive(false);
            UI_Back_Button_Object.SetActive(true);

            if (Static_variable_Manager_Script.wall_Number_int >= 4 && Static_variable_Manager_Script.zoom_Scene_Flag_bool == true)
            {
                UI_Back_Button_Arrows_Text.text = "▼";
            }

        }
    }
    //▲Static_variable_Manager_Script.wall_Number_intに応じてUIを表示する関数




    //▼上下左右の移動ボタンを非表示にする関数
    public void UI_Display_OFF_Function()
    {
        UI_Forward_Button_Object.SetActive(false);
        UI_Left_Button_Object.SetActive(false);
        UI_Right_Button_Object.SetActive(false);
        UI_Back_Button_Object.SetActive(false);
    }

    //▲上下左右の移動ボタンを非表示にする関数





    //▼上下左右移動のUIボタンの箇所

    [Obsolete]
    public void Push_Right_Button()
    {

        if (Static_variable_Manager_Script.wall_Number_int < 9)
        {
            Static_variable_Manager_Script.wall_Number_int++;
        }

        UI_Display_ON_Function();

        camera_Manager_Script.Camera_Subject_Set_Function();
    }

    [Obsolete]
    public void Push_Left_Button()
    {

        if (Static_variable_Manager_Script.wall_Number_int == 3)
        {
            Static_variable_Manager_Script.wall_Number_int = 2;
        }
        else if (Static_variable_Manager_Script.wall_Number_int >= 4)
        {
            Static_variable_Manager_Script.wall_Number_int--;
        }

        UI_Display_ON_Function();

        camera_Manager_Script.Camera_Subject_Set_Function();
    }


    //カメラが３～８の箇所の際には、此方を表示して、尚且つ矢印の方角を↑の方へと変更し、尚且つzoomのフラグを記述していく。
    //並びに「Camera_Subject_Set_Function()」での記述箇所で３～８に、Upperの方のボタンが表示されるのをBottomの方のボタン（３～８の時は矢印は↑）を表示する。
    [Obsolete]
    public void Push_back_Button()
    {

        if (Static_variable_Manager_Script.wall_Number_int == 2)
        {
            Static_variable_Manager_Script.wall_Number_int = 1;
        }
        else if (Static_variable_Manager_Script.wall_Number_int == 1)
        {
            Static_variable_Manager_Script.wall_Number_int = 2;
            Static_variable_Manager_Script.zoom_Scene_Flag_bool = false;
        }
        else if (Static_variable_Manager_Script.wall_Number_int > 3 && Static_variable_Manager_Script.zoom_Scene_Flag_bool == false)
        {
            Static_variable_Manager_Script.zoom_Scene_Flag_bool = true;
        }
        else if (Static_variable_Manager_Script.wall_Number_int > 3 && Static_variable_Manager_Script.zoom_Scene_Flag_bool == true)
        {
            Static_variable_Manager_Script.zoom_Scene_Flag_bool = false;
        }

        UI_Display_ON_Function();

        camera_Manager_Script.Camera_Subject_Set_Function();
    }

    [Obsolete]
    public void Push_forward_Button()
    {

        if (Static_variable_Manager_Script.wall_Number_int == 2)
        {
            Static_variable_Manager_Script.wall_Number_int = 3;
        }

        UI_Display_ON_Function();

        camera_Manager_Script.Camera_Subject_Set_Function();
    }
    //▲上下左右移動のUIボタンの箇所

}
