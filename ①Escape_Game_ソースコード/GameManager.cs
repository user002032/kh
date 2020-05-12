using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class GameManager : MonoBehaviour
{
    /*
     * ・定数は頭文字を大文字。
     * ・配列は頭文字を大文字。
     * ・変数は頭文字を小文字。
     * ・boolは頭文字を小文字。
     * 
     * ・オブジェクトは頭文字を大文字。 
     * ・ボタン、カメラの頭文字は大文字。
     * ・TextMeshProUGUIは頭文字を大文字。
     * 
     * ・Transform は頭文字を大文字。
     * ・Vector3　 は頭文字を大文字。
     * 
     * ・外部スクリプトを読み込む際には、外部スクリプトの名前を頭文字のみ小文字にした名前とする。
     * 
     */



    public GameObject Message_Box_Manager_Script_Object;
    public GameObject Fade_Manager_Script_Object;
    public GameObject Memo_Manager_Script_Object;
    public GameObject Message_Text_Manager_Script_Object;
    public GameObject UI_Manager_Script_Object;
    public GameObject Camera_Manager_Script_Object;
    public GameObject SE_Manager_Script_Object;
    public GameObject Upper_Light_Icon_Manager_Script_Object;
    public GameObject Dial_Lock_Manager_Script_Object;
    public GameObject Stepladder_Manager_Script_Object;
    public GameObject Hint_Manager_Script_Object;
    public GameObject Safe_Box_Script_Object;
    public GameObject Safe_Box_Cancel_Button_Script_Object;
    public GameObject Item_Box_Script_Object;
    public GameObject Door_Script_Object;
    public GameObject Last_Scene_Script_Object;

    Safe_Box_Cancel_Button_Script safe_Box_Cancel_Button_Script;
    Fade_Manager_Script fade_Manager_Scirpt;
    Message_Box_Manager_Script message_Box_Manager_Script;
    Memo_Manager_Script memo_Manager_Script;
    Message_Text_Manager_Script message_Text_Manager_Script;
    UI_Manager_Script ui_Manager_Script;
    SE_Manager_Script se_Manager_Script;
    Upper_Light_Icon_Manager_Script upper_Light_Icon_Manager_Script;
    Dial_Lock_Manager_Script dial_Lock_Manager_Script;
    Stepladder_Manager_Script stepladder_Manager_Script;
    Hint_Manager_Script hint_Manager_Script;
    Safe_Box_Script safe_Box_Script;
    Camera_Manager_Script camera_Manager_Script;
    Item_Box_Script item_Box_Script;
    Door_Script door_Script;
    Last_Scene_Script last_Scene_Script;

    private bool event_While_bool = false;



    void Start()
    {
        event_While_bool = false;

        message_Box_Manager_Script = Message_Box_Manager_Script_Object.GetComponent<Message_Box_Manager_Script>();

        fade_Manager_Scirpt = Fade_Manager_Script_Object.GetComponent<Fade_Manager_Script>();

        memo_Manager_Script = Memo_Manager_Script_Object.GetComponent<Memo_Manager_Script>();

        message_Text_Manager_Script = Message_Text_Manager_Script_Object.GetComponent<Message_Text_Manager_Script>();

        ui_Manager_Script = UI_Manager_Script_Object.GetComponent<UI_Manager_Script>();

        se_Manager_Script = SE_Manager_Script_Object.GetComponent<SE_Manager_Script>();


        upper_Light_Icon_Manager_Script = Upper_Light_Icon_Manager_Script_Object.GetComponent<Upper_Light_Icon_Manager_Script>();

        dial_Lock_Manager_Script = Dial_Lock_Manager_Script_Object.GetComponent<Dial_Lock_Manager_Script>();

        stepladder_Manager_Script = Stepladder_Manager_Script_Object.GetComponent<Stepladder_Manager_Script>();

        hint_Manager_Script = Hint_Manager_Script_Object.GetComponent<Hint_Manager_Script>();

        safe_Box_Script = Safe_Box_Script_Object.GetComponent<Safe_Box_Script>();

        safe_Box_Cancel_Button_Script = Safe_Box_Cancel_Button_Script_Object.GetComponent<Safe_Box_Cancel_Button_Script>();

        camera_Manager_Script = Camera_Manager_Script_Object.GetComponent<Camera_Manager_Script>();

        item_Box_Script = Item_Box_Script_Object.GetComponent<Item_Box_Script>();

        door_Script = Door_Script_Object.GetComponent<Door_Script>();

        last_Scene_Script = Last_Scene_Script_Object.GetComponent<Last_Scene_Script>();

        //↓イベント１を開始。
        Static_variable_Manager_Script.wall_Number_int = 2; // 開始時点のシーンは２なので入れておく。
        Static_variable_Manager_Script.text_change_Count_int = 0;

        Static_variable_Manager_Script.message_Box_Event_bool = false;

        Static_variable_Manager_Script.item_Use_Flag_StepLadder_bool = false;
        Static_variable_Manager_Script.item_Use_Flag_Hammer_bool = false;
        Static_variable_Manager_Script.item_Get_Flag_Memo_bool = false;
        Static_variable_Manager_Script.item_Get_Flag_Hammer_bool = false;
        Static_variable_Manager_Script.item_Get_Flag_Key_bool = false;

        Static_variable_Manager_Script.zoom_Scene_Flag_bool = false;

        Static_variable_Manager_Script.choice_Start_Flag_bool = false;
        Static_variable_Manager_Script.choice_Set_Flag_bool = false;
        Static_variable_Manager_Script.choice_Flag_bool = false;

        Static_variable_Manager_Script.message_Box_Manager_dummy_argument_bool = true;

        Static_variable_Manager_Script.event_Flag_No_1_bool = true;
        Static_variable_Manager_Script.event_Flag_No_2_bool = false;
        Static_variable_Manager_Script.event_Flag_No_3_bool = false;
        Static_variable_Manager_Script.event_Flag_No_4_bool = false;
        Static_variable_Manager_Script.event_Flag_No_5_bool = false;
        Static_variable_Manager_Script.event_Flag_No_6_bool = false;
        Static_variable_Manager_Script.event_Flag_No_7_bool = false;
        Static_variable_Manager_Script.event_Flag_No_8_bool = false;
        Static_variable_Manager_Script.event_Flag_No_9_bool = false;
        Static_variable_Manager_Script.event_Flag_No_10_bool = false;

        Static_variable_Manager_Script.event_Flag_No_2_Text_bool = false;

        Static_variable_Manager_Script.item_Memo_Get_Flag_bool = false;

        Static_variable_Manager_Script.event_While_Start_bool = false;

        Static_variable_Manager_Script.upper_Light_Upper_Icon_Item_Get_Flag_bool = false;
        Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool = false;
        Static_variable_Manager_Script.upper_Light_Bottom_Icon_Item_Get_Flag_bool = false;

        Static_variable_Manager_Script.message_Box_Bottom_Animator_Select_Flag_bool = false;

        Static_variable_Manager_Script.result_time_Int = 0;
        Static_variable_Manager_Script.result_Hint_Check_Count_int = 0;

        Event_Manager_Function();
    }




    // Update is called once per frame
    void Update()
    {

    }





    public void Event_Manager_Function()
    {
        StartCoroutine("Event_Manager_Coroutine");
    }


    [Obsolete]
    public IEnumerator Event_Manager_Coroutine()
    {
        //↓最初のフェードアウト→メモ入手のイベント
        if(Static_variable_Manager_Script.event_Flag_No_1_bool == true)
        {
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(fade_Manager_Scirpt.Fade_Animator_Event_1_Coroutine());
            yield return null;

            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Middle_Animator_Coroutine(Static_variable_Manager_Script.wall_Number_int));
            yield return null;

            yield return new WaitWhile(() => !event_While_bool);
            event_While_bool = false;
            yield return new WaitForSeconds(0.3f);

            yield return StartCoroutine(fade_Manager_Scirpt.Fade_Animator_Event_1_Coroutine());
            yield return null;
            yield return new WaitForSeconds(0.5f);
            message_Box_Manager_Script.Message_Box_Button.enabled = true;
            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Middle_Animator_Coroutine(Static_variable_Manager_Script.wall_Number_int));
            yield return null;

            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => fade_Manager_Scirpt.Fade_Animator_Event_1_For_Coroutine_Function() );
            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());
            message_Box_Manager_Script.Message_Box_Button.enabled = true;
            Static_variable_Manager_Script.event_While_Start_bool = true;
            yield return null;
            yield return new WaitWhile(() => !event_While_bool);
            event_While_bool = false;
            memo_Manager_Script.Memo_Object.SetActive(true);
            yield return new WaitForSeconds(0.3f);


            Static_variable_Manager_Script.event_While_Start_bool = true;

            memo_Manager_Script.Background_Memo_Object.SetActive(false);
            se_Manager_Script.SE_7_Mini_Slide_AudioClip_Function();
            yield return new WaitForSeconds(1.2f);

            yield return StartCoroutine(memo_Manager_Script.Memo_Animator_Coroutine());
            yield return null;
            yield return new WaitWhile(() => !event_While_bool);

            event_While_bool = false;

            Static_variable_Manager_Script.event_While_Start_bool = true;
            yield return new WaitForSeconds(0.4f);
            message_Text_Manager_Script.Message_Box_Text.text = "メモを手に入れた。";
            Static_variable_Manager_Script.text_change_Count_int = 0;
            upper_Light_Icon_Manager_Script.Middle_Item_Display_Image_Object.GetComponent<Image>().sprite = upper_Light_Icon_Manager_Script.Memo_Sprite;
            Static_variable_Manager_Script.upper_Light_Upper_Icon_Item_Get_Flag_bool = true;


            se_Manager_Script.SE_4_Item_Get_AudioClip_Function();
            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Bottom_Animator_Coroutine());
            yield return null;

            yield return new WaitWhile(() => !event_While_bool);

            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());

            event_While_bool = false;
            yield return null;

            Static_variable_Manager_Script.event_Flag_No_1_bool = false;

            upper_Light_Icon_Manager_Script.Upper_Light_Icon_Button_Object_ON_Function();
            ui_Manager_Script.UI_Display_ON_Function();
            Static_variable_Manager_Script.item_Get_Flag_Memo_bool = true;
            last_Scene_Script.play_Start_Flag_bool = true;
        }




        //↓脚立のダイヤル錠のイベント
        if (Static_variable_Manager_Script.event_Flag_No_2_bool == true)
        {
            message_Box_Manager_Script.Message_Box_Object.SetActive(true);
            message_Box_Manager_Script.Message_Box_Button_Object.SetActive(true);
            message_Text_Manager_Script.Message_Box_Text.text = "ダイヤル錠が解けた。";
            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Middle_Animator_Coroutine(Static_variable_Manager_Script.wall_Number_int));
            yield return null;
            Static_variable_Manager_Script.event_While_Start_bool = true;
            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => dial_Lock_Manager_Script.Dial_Lock_Animator_For_Coroutine_Function());
            message_Box_Manager_Script.Message_Box_Button.enabled = true;
            event_While_bool = false;
            
            yield return new WaitWhile(() => !event_While_bool);
            yield return null;
            Static_variable_Manager_Script.event_While_Start_bool = true;
            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();

            yield return new WaitForSeconds(0.3f);

            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Middle_Animator_Coroutine(Static_variable_Manager_Script.wall_Number_int));
            yield return null;

            message_Text_Manager_Script.Message_Box_Text.text = "脚立を手に入れた。";

            upper_Light_Icon_Manager_Script.Middle_Item_Display_Image_Object.GetComponent<Image>().sprite = upper_Light_Icon_Manager_Script.Stepladder_Sprite;
            Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool = true;


            
            se_Manager_Script.SE_4_Item_Get_AudioClip_Function();
            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Bottom_Animator_Coroutine());
            yield return null;

            yield return new WaitWhile(() => !event_While_bool);
            yield return null;

            Static_variable_Manager_Script.event_Flag_No_2_Text_bool = true;
            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Bottom_Animator_For_Coroutine_Function());
            message_Box_Manager_Script.Message_Box_Button.enabled = true;

            upper_Light_Icon_Manager_Script.Upper_Light_Middle_Icon_Button.onClick.AddListener(() => upper_Light_Icon_Manager_Script.Upper_Light_Middle_Icon_Button_Push_Function() );

            stepladder_Manager_Script.Stepladder_3D_Object_8.SetActive(false);
            
            Static_variable_Manager_Script.event_Flag_No_2_bool = false;

        }





        //↓ヒントのイベント1
        if (Static_variable_Manager_Script.event_Flag_No_3_bool == true)
        {
            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Middle_Animator_Coroutine(Static_variable_Manager_Script.wall_Number_int));
            yield return null;

            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => hint_Manager_Script.Hint_Animator_For_Coroutine_Function());

            yield return StartCoroutine(hint_Manager_Script.Hint_Animator_Coroutine());
        }



        //↓脚立を置いた後、金庫の謎解きのイベント
        if (Static_variable_Manager_Script.event_Flag_No_7_bool == true)
        {
            Static_variable_Manager_Script.choice_Set_Flag_bool = false;
            safe_Box_Script.Safe_Box_Cancel_Button.enabled = false;
            safe_Box_Script.Safe_Box_Button_enabled_OFF_Function();
            var cancel_Message_animator = safe_Box_Cancel_Button_Script.Safe_Box_Cancel_Button_Script_Animator.GetComponent<Animator>();
            safe_Box_Script.Safe_Box_Animator.SetTrigger("Safe_Box_Button_Flashing_Out");
            cancel_Message_animator.Play(safe_Box_Script.Safe_Box_Cancel_Message_Box_Out_Hash);
            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(cancel_Message_animator, 0);

            yield return new WaitForSeconds(0.3f);
            yield return StartCoroutine(se_Manager_Script.Wall_NO_5_SE_Coroutine());
            yield return new WaitForSeconds(0.6f);

            var safe_Box_animator = safe_Box_Script.GetComponent<Animator>();
            safe_Box_animator.Play(safe_Box_Script.Safe_Box_Door_Open_Hash);
            yield return null;
            se_Manager_Script.SE_13_Safe_Box_Open_AudioClip_Function();
            yield return new Wait_For_Animation_Custom_Coroutine(safe_Box_animator, 0);

            camera_Manager_Script.Camera_Canvas_UI_Change_Function();

            Static_variable_Manager_Script.event_Flag_No_6_bool = false;
            Static_variable_Manager_Script.event_Flag_No_7_bool = true;

            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Middle_Animator_Coroutine(Static_variable_Manager_Script.wall_Number_int));
            yield return null;

            event_While_bool = false;

            yield return new WaitWhile(() => !event_While_bool);
            yield return null;

            message_Text_Manager_Script.Message_Box_Text.text = "金槌を手に入れた。";
            Static_variable_Manager_Script.upper_Light_Middle_Icon_Item_Get_Flag_bool = true;

            upper_Light_Icon_Manager_Script.Upper_Light_Middle_Icon_Object.GetComponent<Image>().sprite = upper_Light_Icon_Manager_Script.Hammer_Sprite;
            upper_Light_Icon_Manager_Script.Middle_Item_Display_Image_Object.GetComponent<Image>().sprite = upper_Light_Icon_Manager_Script.Hammer_Sprite;

            se_Manager_Script.SE_4_Item_Get_AudioClip_Function();
            Static_variable_Manager_Script.event_Flag_No_7_bool = false;
            Static_variable_Manager_Script.item_Get_Flag_Hammer_bool = true;
            safe_Box_Script.Hammer_Object.SetActive(false);
            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Bottom_Animator_Coroutine());
        }

        //↓丸い留め具の箱を金槌で開けて、鍵を手に入れるイベント
        if (Static_variable_Manager_Script.event_Flag_No_8_bool == true)
        {
            yield return StartCoroutine(fade_Manager_Scirpt.Fade_Animator_Event_8_Coroutine());
            yield return StartCoroutine(se_Manager_Script.Wall_NO_3_SE_Coroutine());
            yield return new WaitForSeconds(0.1f);
            item_Box_Script.Fastener_2_Object_Move_Set_Function();
            yield return StartCoroutine(fade_Manager_Scirpt.Fade_Animator_Event_8_Coroutine());

            se_Manager_Script.SE_14_Item_Box_Open_AudioClip_Function();
            yield return new WaitForSeconds(0.3f);
            yield return StartCoroutine(item_Box_Script.Item_Box_Animator_Coroutine());

            Static_variable_Manager_Script.event_While_Start_bool = true;
            event_While_bool = false;
            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Middle_Animator_Coroutine(Static_variable_Manager_Script.wall_Number_int));


            Static_variable_Manager_Script.event_Flag_No_8_bool = false;
            yield return new WaitWhile(() => !event_While_bool);

            Static_variable_Manager_Script.item_Get_Flag_Key_bool = true;
            message_Text_Manager_Script.Message_Box_Text.text = "鍵を手に入れた。";
            Static_variable_Manager_Script.upper_Light_Bottom_Icon_Item_Get_Flag_bool = true;
            upper_Light_Icon_Manager_Script.Middle_Item_Display_Image_Object.GetComponent<Image>().sprite = upper_Light_Icon_Manager_Script.Key_Sprite;
            se_Manager_Script.SE_4_Item_Get_AudioClip_Function();
            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Bottom_Animator_Coroutine());
            Static_variable_Manager_Script.event_Flag_No_8_bool = false;
        }

        //↓ドアを開けてゲームクリアのイベント
        if (Static_variable_Manager_Script.event_Flag_No_9_bool == true)
        {
            yield return StartCoroutine(fade_Manager_Scirpt.Fade_Animator_Event_9_Coroutine());
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(se_Manager_Script.Wall_NO_Key_Trun_SE_Coroutine());

            Static_variable_Manager_Script.zoom_Scene_Flag_bool = false;
            camera_Manager_Script.Camera_Subject_Set_Function();


            yield return new WaitForSeconds(0.3f);
            yield return StartCoroutine(fade_Manager_Scirpt.Fade_Animator_Event_9_Coroutine());

            se_Manager_Script.SE_15_Door_Open_AudioClip_Function();
            yield return StartCoroutine(door_Script.Door_Animator_Coroutine());

            Static_variable_Manager_Script.event_Flag_No_10_bool = true;
            Static_variable_Manager_Script.event_Flag_No_9_bool = false;
            event_While_bool = false;
            Static_variable_Manager_Script.event_While_Start_bool = true;

            yield return StartCoroutine(message_Box_Manager_Script.Message_Box_Middle_Animator_Coroutine(Static_variable_Manager_Script.wall_Number_int));

           
            yield return new WaitWhile(() => !event_While_bool);

            yield return new WaitForSeconds(0.3f);
            

            camera_Manager_Script.Camera_Last_Scene_Change_Function();
            yield return new WaitForSeconds(0.1f);
            last_Scene_Script.Result_Text_Set_Function();
            message_Box_Manager_Script.Message_Box_Button_Object.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(last_Scene_Script.Last_Scene_Animator_Coroutine());
            yield return StartCoroutine(door_Script.Door_Animator_Coroutine());

            yield return new WaitForSeconds(0.1f);
            yield return StartCoroutine(last_Scene_Script.Last_Scene_Animator_Coroutine());
            fade_Manager_Scirpt.Fade_Black_Renderer_Image_Object.GetComponent<Animator>().SetTrigger("Fade_Exit");
        }

    }


    public void Wait_While_Flag_Function()
    {
        if (Static_variable_Manager_Script.event_While_Start_bool == true)
        {
            Static_variable_Manager_Script.event_While_Start_bool = false;
            event_While_bool = true;
        }
    }




    //▼各種アニメーターで用いるアニメーションの終了判定のコルーチンに用いるカスタムコルーチン
    public class Wait_For_Animation_Custom_Coroutine : CustomYieldInstruction
    {
        Animator wait_Check_Animator;
        int wait_Check_Last_State_Hash = 0;
        int wait_Check_Layer_No = 0;

        [Obsolete]
        public Wait_For_Animation_Custom_Coroutine(Animator animator, int layer_No)
        {
            Init_Subordinate_Function(animator, layer_No, animator.GetCurrentAnimatorStateInfo(layer_No).nameHash);
        }

        void Init_Subordinate_Function(Animator animator, int layer_No, int hash)
        {
            wait_Check_Layer_No = layer_No;
            wait_Check_Animator = animator;
            wait_Check_Last_State_Hash = hash;
        }

        public override bool keepWaiting
        {
            get
            {
                var current_Animator_State = wait_Check_Animator.GetCurrentAnimatorStateInfo(wait_Check_Layer_No);

                return current_Animator_State.fullPathHash == wait_Check_Last_State_Hash && (current_Animator_State.normalizedTime < 1);
            }
        }
    }
    //▲各種アニメーターで用いるアニメーションの終了判定のコルーチンに用いるカスタムコルーチン
}
