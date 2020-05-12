using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Static_variable_Manager_Script : MonoBehaviour
{

    public static int wall_Number_int = 2; // 開始時点のシーンは２なので入れておく。
    public static int text_change_Count_int = 0;

    public static bool message_Box_Event_bool = false;

    public static bool item_Use_Flag_StepLadder_bool = false;
    public static bool item_Use_Flag_Hammer_bool = false;
    public static bool item_Get_Flag_Memo_bool = false;
    public static bool item_Get_Flag_Hammer_bool = false;
    public static bool item_Get_Flag_Key_bool = false;

    public static bool zoom_Scene_Flag_bool = false;

    public static bool choice_Start_Flag_bool = false;
    public static bool choice_Set_Flag_bool = false;
    public static bool choice_Flag_bool = false;

    public static bool message_Box_Manager_dummy_argument_bool = true;

    public static bool event_Flag_No_1_bool = true;
    public static bool event_Flag_No_2_bool = false;
    public static bool event_Flag_No_3_bool = false;
    public static bool event_Flag_No_4_bool = false;
    public static bool event_Flag_No_5_bool = false;
    public static bool event_Flag_No_6_bool = false;
    public static bool event_Flag_No_7_bool = false;
    public static bool event_Flag_No_8_bool = false;
    public static bool event_Flag_No_9_bool = false;
    public static bool event_Flag_No_10_bool = false;

    public static bool event_Flag_No_2_Text_bool = false;

    public static bool item_Memo_Get_Flag_bool = false;

    public static bool event_While_Start_bool = false;

    public static bool upper_Light_Upper_Icon_Item_Get_Flag_bool = false;
    public static bool upper_Light_Middle_Icon_Item_Get_Flag_bool = false;
    public static bool upper_Light_Bottom_Icon_Item_Get_Flag_bool = false;

    public static bool message_Box_Bottom_Animator_Select_Flag_bool = false;


    public static int result_time_Int = 0;
    public static int result_Hint_Check_Count_int = 0;


    



    // Start is called before the first frame update
    void Start()
    {
        wall_Number_int = 2; // 開始時点のシーンは２なので入れておく。
        text_change_Count_int = 0;

        message_Box_Event_bool = false;

        item_Use_Flag_StepLadder_bool = false;
        item_Use_Flag_Hammer_bool = false;
        item_Get_Flag_Memo_bool = false;
        item_Get_Flag_Hammer_bool = false;
        item_Get_Flag_Key_bool = false;

        zoom_Scene_Flag_bool = false;

        choice_Start_Flag_bool = false;
        choice_Set_Flag_bool = false;
        choice_Flag_bool = false;

        message_Box_Manager_dummy_argument_bool = true;

        event_Flag_No_1_bool = true;
        event_Flag_No_2_bool = false;
        event_Flag_No_3_bool = false;
        event_Flag_No_4_bool = false;
        event_Flag_No_5_bool = false;
        event_Flag_No_6_bool = false;
        event_Flag_No_7_bool = false;
        event_Flag_No_8_bool = false;
        event_Flag_No_9_bool = false;
        event_Flag_No_10_bool = false;

        event_Flag_No_2_Text_bool = false;

        item_Memo_Get_Flag_bool = false;

        event_While_Start_bool = false;

        upper_Light_Upper_Icon_Item_Get_Flag_bool = false;
        upper_Light_Middle_Icon_Item_Get_Flag_bool = false;
        upper_Light_Bottom_Icon_Item_Get_Flag_bool = false;

        message_Box_Bottom_Animator_Select_Flag_bool = false;

        result_time_Int = 0;
        result_Hint_Check_Count_int = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
