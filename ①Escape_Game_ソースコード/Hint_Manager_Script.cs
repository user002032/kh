using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hint_Manager_Script : MonoBehaviour
{
    public GameObject Hint_Object;
    public GameObject Message_Box_Manager_Script_Object;
    public GameObject UI_Manager_Script_Object;
    public GameObject SE_Manager_Script_Object;
    public GameObject Upper_Light_Icon_Manager_Script_Object;

    Hint_Script hint_Script;
    Message_Box_Manager_Script message_Box_Manager_Script;
    UI_Manager_Script ui_Manager_Script;
    SE_Manager_Script se_Manager_Script;
    Upper_Light_Icon_Manager_Script upper_Light_Icon_Manager_Script;

    private int Hint_1_IN_Hash = Animator.StringToHash("Hint_1_IN");
    private int Hint_2_IN_Hash = Animator.StringToHash("Hint_2_IN");
    private int Hint_Out_Hash = Animator.StringToHash("Hint_Out");


    // Start is called before the first frame update
    void Start()
    {
        hint_Script = Hint_Object.GetComponent<Hint_Script>(); 
        
        message_Box_Manager_Script = Message_Box_Manager_Script_Object.GetComponent<Message_Box_Manager_Script>();

        ui_Manager_Script = UI_Manager_Script_Object.GetComponent<UI_Manager_Script>();

        se_Manager_Script = SE_Manager_Script_Object.GetComponent<SE_Manager_Script>();

        upper_Light_Icon_Manager_Script = Upper_Light_Icon_Manager_Script_Object.GetComponent<Upper_Light_Icon_Manager_Script>();

        Hint_Object.SetActive(false);
    }

        // Update is called once per frame
    void Update()
    {
        
    }


    public void Hint_Animator_For_Coroutine_Function()
    {
        StartCoroutine("Hint_Animator_Coroutine");
    }


    //▼Hintを表示するアニメーターを管理するコルーチン
    [Obsolete]
    public IEnumerator Hint_Animator_Coroutine()
    {
        Hint_Object.SetActive(true);

        AnimatorStateInfo hint_AnimatorStateInfo = hint_Script.Hint_Script_Animator.GetCurrentAnimatorStateInfo(0);

        ui_Manager_Script.UI_Display_OFF_Function();

        upper_Light_Icon_Manager_Script.Upper_Light_Icon_Button_Object_OFF_Function();


        if (hint_AnimatorStateInfo.IsName("Idol"))
        {
            message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
            message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => Hint_Animator_For_Coroutine_Function());

            message_Box_Manager_Script.Message_Box_Button.enabled = false;

            var animator = Hint_Object.GetComponent<Animator>();

            se_Manager_Script.SE_7_Mini_Slide_AudioClip_Function();
            animator.Play(Hint_1_IN_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            message_Box_Manager_Script.Message_Box_Button.enabled = true;

        }
        else if (hint_AnimatorStateInfo.IsName("Hint_1_IN"))
        {
            message_Box_Manager_Script.Message_Box_Button.enabled = false;

            var animator = Hint_Object.GetComponent<Animator>();

            se_Manager_Script.SE_7_Mini_Slide_AudioClip_Function();
            animator.Play(Hint_2_IN_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            message_Box_Manager_Script.Message_Box_Button.enabled = true;
        }
        else if (hint_AnimatorStateInfo.IsName("Hint_2_IN"))
        {
            message_Box_Manager_Script.Message_Box_Button.enabled = false;

            var animator = Hint_Object.GetComponent<Animator>();

            animator.Play(Hint_Out_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            if (Static_variable_Manager_Script.event_Flag_No_3_bool == true)
            {
                Static_variable_Manager_Script.event_Flag_No_3_bool = false;
                message_Box_Manager_Script.Message_Box_Button.onClick.RemoveAllListeners();
                message_Box_Manager_Script.Message_Box_Button.onClick.AddListener(() => message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function());

                message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function();
            }
            else if(Static_variable_Manager_Script.event_Flag_No_3_bool == false)
            {
                upper_Light_Icon_Manager_Script.Upper_Light_Icon_Button_Object_ON_Function();
                ui_Manager_Script.UI_Display_ON_Function();
            }
        }
    }
    //▲Hintを表示するアニメーターを管理するコルーチン





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
