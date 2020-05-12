using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Memo_Manager_Script : MonoBehaviour
{
    public GameObject Background_Memo_Object;
    public GameObject Memo_Object;
    public GameObject Message_Box_Manager_Script_Object;
    public GameObject Game_Manager_Script_Object;
    public GameObject SE_Manager_Script_Object;

    Memo_Script memo_Script;
    Message_Box_Manager_Script message_Box_Manager_Script;
    GameManager game_Manager_Script;
    SE_Manager_Script se_Manager_Script;

    private int Memo_IN_Hash = Animator.StringToHash("Memo_IN");
    private int Memo_Out_Hash = Animator.StringToHash("Memo_Out");



    // Start is called before the first frame update
    void Start()
    {
        Background_Memo_Object.SetActive(true);
        Memo_Object.SetActive(false);

        memo_Script = Memo_Object.GetComponent<Memo_Script>();
        message_Box_Manager_Script = Message_Box_Manager_Script_Object.GetComponent<Message_Box_Manager_Script>();
        game_Manager_Script = Game_Manager_Script_Object.GetComponent<GameManager>();
        se_Manager_Script = SE_Manager_Script_Object.GetComponent<SE_Manager_Script>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void Memo_Animator_For_Coroutine_Function()
    {
        Memo_Object.SetActive(true);
        StartCoroutine("Memo_Animator_Coroutine");
    }


    //▼メモを表示するアニメーターを管理するコルーチン
    [Obsolete]
    public IEnumerator Memo_Animator_Coroutine()
    {
        AnimatorStateInfo memo_AnimatorStateInfo = memo_Script.Memo_Script_Animator.GetCurrentAnimatorStateInfo(0);
        

        if (memo_AnimatorStateInfo.IsName("Idol"))
        {
            if (Static_variable_Manager_Script.item_Get_Flag_Memo_bool == true)
            {
                Static_variable_Manager_Script.result_Hint_Check_Count_int = Static_variable_Manager_Script.result_Hint_Check_Count_int + 1;
            }

            se_Manager_Script.SE_2_Memo_Use_AudioClip_Function();

            message_Box_Manager_Script.Middle_item_display_select_Button.enabled = false;
            message_Box_Manager_Script.Message_Box_Button.enabled = false;
            var animator = Memo_Object.GetComponent<Animator>();
            animator.Play(Memo_IN_Hash);
            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Memo_Animator_For_Coroutine_Function() );

            message_Box_Manager_Script.Middle_item_display_select_Button.enabled = true;
            message_Box_Manager_Script.Message_Box_Button.enabled = true;
        }
        else if (memo_AnimatorStateInfo.IsName("Memo_IN"))
        {

            se_Manager_Script.SE_3_Memo_Close_AudioClip_Function();

            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();

            message_Box_Manager_Script.Middle_item_display_select_Button.enabled = false;
            message_Box_Manager_Script.Message_Box_Button.enabled = false;
            var animator = Memo_Object.GetComponent<Animator>();
            animator.Play(Memo_Out_Hash);
            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);


            if (Static_variable_Manager_Script.event_Flag_No_1_bool == true)
            {
                game_Manager_Script.Wait_While_Flag_Function();
            }
            else if(Static_variable_Manager_Script.event_Flag_No_2_bool == true)
            {

            }
            else if(Static_variable_Manager_Script.event_Flag_No_3_bool == true)
            {

            }
            else
            {
                message_Box_Manager_Script.Middle_item_display_select_Button.enabled = true;
                message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Bottom_Animator_For_Coroutine_Function());
                message_Box_Manager_Script.Message_Box_Button.enabled = true;
            }
        }
    }


    //▲メモを表示するアニメーターを管理するコルーチン



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