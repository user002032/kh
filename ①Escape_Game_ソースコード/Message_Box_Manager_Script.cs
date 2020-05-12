using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Message_Box_Manager_Script : MonoBehaviour
{

    public GameObject Message_Box_Button_Object;

    public Button Message_Box_Button;
    public Button Yes_Button;
    public Button No_Button;
    public Button Middle_item_display_select_Button;
    public GameObject Bottom_select_button_Object;
    Bottom_select_button_Script bottom_select_button_Script;

    //↓このスクリプト外でマネージャー管理用として宣言してあるEmptyのオブジェクトと、Addされたスクリプト。
    Message_Box_Object_Script message_Box_Object_Script;
    UI_Manager_Script ui_Manager_Script;
    Upper_Light_Icon_Manager_Script upper_Light_Icon_Manager_Script;
    Message_Text_Manager_Script message_Text_Manager_Script;
    SE_Manager_Script se_Manager_Script;
    GameManager game_Manager_Script;
    Dial_Lock_Manager_Script dial_Lock_Manager_Script;
    Hint_Manager_Script hint_Manager_Script;
    Stepladder_Manager_Script stepladder_Manager_Script;
    Camera_Manager_Script camera_Manager_Script;
    Safe_Box_Script safe_Box_Script;

    public GameObject Message_Text_Manager_Script_Object;
    public GameObject SE_Manager_Script_Object;
    public GameObject Message_Box_Object;
    public GameObject Game_Manager_Object;
    public GameObject UI_Manager_Script;
    public GameObject Upper_Light_Icon_Manager_Script_Object;
    public GameObject Dial_Lock_Manager_Script_Object;
    public GameObject Hint_Manager_Script_Object;
    public GameObject Stepladder_Manager_Script_Object;
    public GameObject Camera_Manager_Script_Object;
    public GameObject Safe_Box_Script_Object;

    //↓アニメーターのハッシュ値の生成
    private int Message_Box_Object_Middle_IN_Hash = Animator.StringToHash("Message_Box_Object_Middle_IN");
    private int Message_Box_Object_Middle_Out_Hash = Animator.StringToHash("Message_Box_Object_Middle_Out");

    private int Select_IN_Hash = Animator.StringToHash("Select_IN");
    private int Select_Out_Hash = Animator.StringToHash("Select_Out");

    private int Message_Box_Object_Bottom_IN_Hash = Animator.StringToHash("Message_Box_Object_Bottom_IN");
    private int Message_Box_Object_Bottom_Out_Hash = Animator.StringToHash("Message_Box_Object_Bottom_Out");

    private int Message_Box_Object_Bottom_and_Select_IN_Hash = Animator.StringToHash("Message_Box_Object_Bottom_and_Select_IN");
    private int Message_Box_Object_Bottom_and_Select_Out_Hash = Animator.StringToHash("Message_Box_Object_Bottom_and_Select_Out");


    // Start is called before the first frame update
    void Start()
    {
        Middle_item_display_select_Button.enabled = false;

        Message_Box_Button.onClick.AddListener( () => Message_Box_Middle_Animator_For_Coroutine_Function()); //ボタンにイベントリスナーを設定していない為、その設定。（動的にイベントを切り替える為）


        Message_Box_Button.enabled = false;
        Yes_Button.enabled = false;
        No_Button.enabled = false;


        message_Box_Object_Script = Message_Box_Object.GetComponent<Message_Box_Object_Script>(); // メッセージボックスのアニメーターの読み込み用
        se_Manager_Script = SE_Manager_Script_Object.GetComponent<SE_Manager_Script>();           // ＳＥを管理するスクリプトの読み込み用
        bottom_select_button_Script = Bottom_select_button_Object.GetComponent<Bottom_select_button_Script>();                              // 選択肢のアニメーターの読みこみ用
        message_Text_Manager_Script = Message_Text_Manager_Script_Object.GetComponent<Message_Text_Manager_Script>(); // メッセージボックス内のテキストの変更を管理するスクリプトを読み込む用
        game_Manager_Script = Game_Manager_Object.GetComponent<GameManager>();

        ui_Manager_Script = UI_Manager_Script.GetComponent<UI_Manager_Script>();

        upper_Light_Icon_Manager_Script = Upper_Light_Icon_Manager_Script_Object.GetComponent<Upper_Light_Icon_Manager_Script>();

        dial_Lock_Manager_Script = Dial_Lock_Manager_Script_Object.GetComponent<Dial_Lock_Manager_Script>();

        hint_Manager_Script = Hint_Manager_Script_Object.GetComponent<Hint_Manager_Script>();

        stepladder_Manager_Script = Stepladder_Manager_Script_Object.GetComponent<Stepladder_Manager_Script>();

        camera_Manager_Script = Camera_Manager_Script_Object.GetComponent<Camera_Manager_Script>();

        safe_Box_Script = Safe_Box_Script_Object.GetComponent<Safe_Box_Script>();
    }




    // Update is called once per frame
    void Update()
    {

    }



    //▼中段にメッセージボックスを表示するアニメーターを管理するコルーチンを呼び出す関数
    public void Message_Box_Middle_Animator_For_Coroutine_Function()
    {
        StartCoroutine("Message_Box_Middle_Animator_Coroutine", Static_variable_Manager_Script.wall_Number_int);
    }
    //▲中段にメッセージボックスを表示するアニメーターを管理するコルーチンを呼び出す関数


    //▼中段にメッセージボックスを表示するアニメーターを管理するコルーチン
    [Obsolete]
    public IEnumerator Message_Box_Middle_Animator_Coroutine(int wall_Number_int)
    {
        ui_Manager_Script.UI_Display_OFF_Function();
        AnimatorStateInfo massage_Box_AnimatorStateInfo = message_Box_Object_Script.Message_Box_Object_Script_Animator.GetCurrentAnimatorStateInfo(0);
        upper_Light_Icon_Manager_Script.Upper_Light_Icon_Button_Object_OFF_Function();

        if (massage_Box_AnimatorStateInfo.IsName("Idol"))
        {

            Message_Box_Button.enabled = false;
            message_Text_Manager_Script.Message_Box_Text_Change_Function();
            var animator = Message_Box_Object.GetComponent<Animator>();


            //↓イベント・ＳＥチェック用
            switch (wall_Number_int)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:

                    break;
                case 7:
                    if (Static_variable_Manager_Script.zoom_Scene_Flag_bool == true) {
                        yield return StartCoroutine(se_Manager_Script.Wall_NO_7_SE_Coroutine()); // ← コルーチンの待機状態の同期用に「yield return」を付ける事。
                                                                                                 // 　　ドアの前のＳＥを先に鳴らす。
                        yield return null;
                    }
                    break;
                case 8:

                    break;
            }
            //↑イベント・ＳＥチェック用

            animator.Play(Message_Box_Object_Middle_IN_Hash);

            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            Message_Box_Button.enabled = true;


        }
        else if (massage_Box_AnimatorStateInfo.IsName("Message_Box_Object_Middle_IN") && Static_variable_Manager_Script.choice_Set_Flag_bool == true)
        {
            Message_Box_Button.enabled = false;
            yield return null;
            yield return StartCoroutine("Choice_Animator_Coroutine");
            Static_variable_Manager_Script.choice_Set_Flag_bool = false;
        }
        else if (massage_Box_AnimatorStateInfo.IsName("Message_Box_Object_Middle_IN") && Static_variable_Manager_Script.choice_Set_Flag_bool == false)
        {

            Message_Box_Button.enabled = false;
            
            var animator = Message_Box_Object.GetComponent<Animator>();

            animator.Play(Message_Box_Object_Middle_Out_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);


            //↓Static_variable_Manager_Script.choice_Flag_boolをfalseへ。
            if (Static_variable_Manager_Script.choice_Flag_bool == true)
            {
                Static_variable_Manager_Script.choice_Flag_bool = false;
            }


            if (Static_variable_Manager_Script.event_Flag_No_1_bool == true)
            {
                game_Manager_Script.Wait_While_Flag_Function();
            }
            else if (Static_variable_Manager_Script.event_Flag_No_2_bool == true)
            {
                if (Static_variable_Manager_Script.event_While_Start_bool == false)
                {
                    yield return null;
                    dial_Lock_Manager_Script.Dial_Lock_Animator_For_Coroutine_Function();
                }

            }
            else if (Static_variable_Manager_Script.event_Flag_No_3_bool == true)
            {
                yield break;
            }
            else if (Static_variable_Manager_Script.event_Flag_No_4_bool == true)
            {
                Static_variable_Manager_Script.result_Hint_Check_Count_int = Static_variable_Manager_Script.result_Hint_Check_Count_int + 1;
                Static_variable_Manager_Script.event_Flag_No_4_bool = false;
                hint_Manager_Script.Hint_Animator_For_Coroutine_Function();
            }
            else if (Static_variable_Manager_Script.event_Flag_No_5_bool == true)
            {
                stepladder_Manager_Script.Stepladder_Set_For_Coroutine_Function();
            }
            else if (Static_variable_Manager_Script.event_Flag_No_6_bool == true)
            {
                camera_Manager_Script.Camera_Canvas_UI_Zoom_6_Change_Function();
                safe_Box_Script.Safe_Box_Cancel_Button_Set_Animator_For_Coroutine_Function();
            }
            else if (Static_variable_Manager_Script.event_Flag_No_7_bool == true)
            {
                game_Manager_Script.Wait_While_Flag_Function();
            }
            else if (Static_variable_Manager_Script.event_Flag_No_8_bool == true)
            {
                if (Static_variable_Manager_Script.item_Use_Flag_Hammer_bool == false)
                {
                    game_Manager_Script.Event_Manager_Function();
                }
                else if (Static_variable_Manager_Script.item_Use_Flag_Hammer_bool == true)
                {
                    game_Manager_Script.Wait_While_Flag_Function();
                }
            }
            else if (Static_variable_Manager_Script.event_Flag_No_9_bool == true)
            {
                game_Manager_Script.Event_Manager_Function();
            }
            else if (Static_variable_Manager_Script.event_Flag_No_10_bool == true)
            {
                Static_variable_Manager_Script.event_Flag_No_10_bool = false;
                game_Manager_Script.Wait_While_Flag_Function();
            }
            else
            {
                ui_Manager_Script.UI_Display_ON_Function();
                upper_Light_Icon_Manager_Script.Upper_Light_Icon_Button_Object_ON_Function();
            }

        }
    }


    //▲中段にメッセージボックスを表示するアニメーターを管理するコルーチン




    //▼「はい」「いいえ」の選択肢の内、『はい』のボタンを選らばれた場合に呼び出す関数。
    public void Choice_Animator_For_Coroutine_YES_Flag_Function()
    {
        Static_variable_Manager_Script.choice_Flag_bool = true;
        StartCoroutine("Choice_Animator_Coroutine");
    }
    //▲「はい」「いいえ」の選択肢の内、『はい』のボタンを選らばれた場合に呼び出す関数。





    //▼「はい」「いいえ」の選択肢の内、『いいえ』のボタンを選らばれた場合に呼び出す関数。
    public void Choice_Animator_For_Coroutine_NO_Flag_Function()
    {   
        Static_variable_Manager_Script.event_Flag_No_2_bool = false;
        Static_variable_Manager_Script.event_Flag_No_3_bool = false;
        Static_variable_Manager_Script.event_Flag_No_4_bool = false;
        Static_variable_Manager_Script.event_Flag_No_5_bool = false;
        Static_variable_Manager_Script.event_Flag_No_6_bool = false;
        Static_variable_Manager_Script.event_Flag_No_7_bool = false;
        Static_variable_Manager_Script.event_Flag_No_8_bool = false;
        Static_variable_Manager_Script.event_Flag_No_9_bool = false;
        Static_variable_Manager_Script.text_change_Count_int = 0;
        StartCoroutine("Choice_Animator_Coroutine");
    }
    //▲「はい」「いいえ」の選択肢の内、『いいえ』のボタンを選らばれた場合に呼び出す関数。





    //▼「はい」「いいえ」の選択肢を表示するコルーチン
    [Obsolete]
    public IEnumerator Choice_Animator_Coroutine()
    {
        AnimatorStateInfo Choice_AnimatorStateInfo = bottom_select_button_Script.Bottom_select_button_Script_Animator.GetCurrentAnimatorStateInfo(0);
        

        if (Choice_AnimatorStateInfo.IsName("Idol"))
        {
            var animator = Bottom_select_button_Object.GetComponent<Animator>();

            animator.Play(Select_IN_Hash);

            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            Yes_Button.enabled = true;
            No_Button.enabled = true;

        }
        else if (Choice_AnimatorStateInfo.IsName("Select_IN"))
        {
            Yes_Button.enabled = false;
            No_Button.enabled = false;

            var animator = Bottom_select_button_Object.GetComponent<Animator>();

            animator.Play(Select_Out_Hash);
            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            bottom_select_button_Script.Bottom_select_button_Script_Animator.SetTrigger("Select_Exit");
            Message_Box_Middle_Animator_For_Coroutine_Function();
        }
    }
    //▲「はい」「いいえ」の選択肢を表示するコルーチン




    //▼下段にメッセージボックスを表示するアニメーターを管理するコルーチンを呼び出す関数
    public void Message_Box_Bottom_Animator_For_Coroutine_Function()
    {
        StartCoroutine("Message_Box_Bottom_Animator_Coroutine");
    }
    //▲下段にメッセージボックスを表示するアニメーターを管理するコルーチンを呼び出す関数



    //▼下段にメッセージボックスを表示するアニメーターを管理するコルーチン
    [Obsolete]
    public IEnumerator Message_Box_Bottom_Animator_Coroutine()
    {
        AnimatorStateInfo massage_Box_AnimatorStateInfo = message_Box_Object_Script.Message_Box_Object_Script_Animator.GetCurrentAnimatorStateInfo(0);


        ui_Manager_Script.UI_Display_OFF_Function();


        upper_Light_Icon_Manager_Script.Upper_Light_Icon_Button_Object_OFF_Function();


        if (massage_Box_AnimatorStateInfo.IsName("Idol"))
        {

            Message_Box_Button.enabled = false;

            var animator = Message_Box_Object.GetComponent<Animator>();


            if (Static_variable_Manager_Script.message_Box_Bottom_Animator_Select_Flag_bool == true)
            {
                animator.Play(Message_Box_Object_Bottom_and_Select_IN_Hash);
            }
            else
            {
                animator.Play(Message_Box_Object_Bottom_IN_Hash);
            }

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);


            Message_Box_Button.onClick.RemoveAllListeners();
            Message_Box_Button.onClick.AddListener(() => Message_Box_Bottom_Animator_For_Coroutine_Function() );

            Message_Box_Button.enabled = true;

            if (Static_variable_Manager_Script.message_Box_Bottom_Animator_Select_Flag_bool == true)
            {
                Middle_item_display_select_Button.enabled = true;
            }

        }
        else if (massage_Box_AnimatorStateInfo.IsName("Message_Box_Object_Bottom_IN") || massage_Box_AnimatorStateInfo.IsName("Message_Box_Object_Bottom_and_Select_IN"))
        {
            Message_Box_Button.onClick.RemoveAllListeners();

            Message_Box_Button.enabled = false;
            Middle_item_display_select_Button.enabled = false;

            var animator = Message_Box_Object.GetComponent<Animator>();

            if (Static_variable_Manager_Script.message_Box_Bottom_Animator_Select_Flag_bool == true)
            {
                animator.Play(Message_Box_Object_Bottom_and_Select_Out_Hash);
                Static_variable_Manager_Script.message_Box_Bottom_Animator_Select_Flag_bool = false;
            }
            else
            {
                animator.Play(Message_Box_Object_Bottom_Out_Hash);
            }

            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            if (Static_variable_Manager_Script.event_Flag_No_1_bool == true)
            {
                game_Manager_Script.Wait_While_Flag_Function();
            }
            else if (Static_variable_Manager_Script.event_Flag_No_2_bool == true)
            {
                game_Manager_Script.Wait_While_Flag_Function();
            }
            else if (Static_variable_Manager_Script.event_Flag_No_3_bool == true)
            {

            }
            else if (Static_variable_Manager_Script.event_Flag_No_7_bool == true)
            {
                game_Manager_Script.Wait_While_Flag_Function();
            }
            else
            {
                ui_Manager_Script.UI_Display_ON_Function();
                upper_Light_Icon_Manager_Script.Upper_Light_Icon_Button_Object_ON_Function();
            }
        }
    }
    //▲下段にメッセージボックスを表示するアニメーターを管理するコルーチン



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

                return current_Animator_State.fullPathHash == wait_Check_Last_State_Hash && ( current_Animator_State.normalizedTime < 1 );
            }
        }
    }
    //▲各種アニメーターで用いるアニメーションの終了判定のコルーチンに用いるカスタムコルーチン
}
