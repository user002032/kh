using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Last_Scene_Script : MonoBehaviour
{

    public GameObject Last_scene_fade_Image_Object;
    public GameObject Black_fade_in_Image_Object;

    public GameObject SE_Manager_Script_Object;
    SE_Manager_Script se_Manager_Script;

    public Animator Last_Scene_Script_Animator;

    private int Last_Scene_Hash = Animator.StringToHash("Last_Scene");
    private int Result_Scene_Hash = Animator.StringToHash("Result_Scene");
    private int Skip_Result_Scene_Hash = Animator.StringToHash("Skip_Result_Scene");
    private int Black_Fade_IN_Hash = Animator.StringToHash("Black_Fade_IN");
    private int Last_Scene_Message_Box_IN_Hash = Animator.StringToHash("Last_Scene_Message_Box_IN");
    private int Last_Scene_Message_Box_Out_Hash = Animator.StringToHash("Last_Scene_Message_Box_Out");

    public float play_Time = 0f;
    public bool play_Start_Flag_bool = false;
    public int result_Play_Time = 0;

    public TextMeshProUGUI result_Time_Count_Text;
    public TextMeshProUGUI result_Hint_Check_Count_Text;
    public TextMeshProUGUI result_Rank_Text;

    int total_Point = 0;

    public Button Last_Scene_Message_Box_Button;
    public GameObject Last_Scene_Message_Box_Button_Object;

    public TextMeshProUGUI Last_Scene_Message_Box_Text;

    [Obsolete]
    void Start()
    {
        Last_scene_fade_Image_Object.SetActive(false);
        Black_fade_in_Image_Object.SetActive(false);

        play_Time = 0f;
        play_Start_Flag_bool = false;
        result_Play_Time = 0;

        total_Point = 0;

        Last_Scene_Message_Box_Button.onClick.AddListener(() => Last_Scene_Animator_For_Coroutine_Function());

        Last_Scene_Message_Box_Button.enabled = false;
        Last_Scene_Message_Box_Button_Object.SetActive(false);

        play_Start_Flag_bool = false;
        Last_Scene_Script_Animator = GetComponent<Animator>();
        se_Manager_Script = SE_Manager_Script_Object.GetComponent<SE_Manager_Script>();

        result_Time_Count_Text.text = "";
        result_Hint_Check_Count_Text.text = "";
        result_Rank_Text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(play_Start_Flag_bool == true)
        {
            play_Time += Time.deltaTime;

            if(play_Time > 999.9f)
            {
                play_Start_Flag_bool = false;
                play_Time = 999.0f;
            }
        }
    }


    public void Last_Scene_Animator_For_Coroutine_Function()
    {

        Last_scene_fade_Image_Object.SetActive(true);
        Black_fade_in_Image_Object.SetActive(true);

        StartCoroutine("Last_Scene_Animator_Coroutine");
    }


    //▼ドアが開くアニメーターを管理するコルーチン。
    [System.Obsolete]
    public IEnumerator Last_Scene_Animator_Coroutine()
    {
        AnimatorStateInfo Last_Scene_AnimatorStateInfo = Last_Scene_Script_Animator.GetCurrentAnimatorStateInfo(0);

        if (Last_Scene_AnimatorStateInfo.IsName("Idol"))
        {

            var animator = GetComponent<Animator>();

            animator.Play(Last_Scene_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

        }
        else if (Last_Scene_AnimatorStateInfo.IsName("Last_Scene"))
        {
            Last_Scene_Message_Box_Button_Object.SetActive(true);


            Last_Scene_Message_Box_Button.onClick.RemoveAllListeners();
            Last_Scene_Message_Box_Button.onClick.AddListener(() => Last_Scene_Animator_For_Coroutine_Function());

            Last_Scene_Message_Box_Button.enabled = true;
            

            var animator = GetComponent<Animator>();

            animator.Play(Result_Scene_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);
        }
        else if (Last_Scene_AnimatorStateInfo.IsName("Result_Scene"))
        {
            Last_Scene_Message_Box_Button.enabled = false;

            var animator = GetComponent<Animator>();

            animator.Play(Skip_Result_Scene_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            yield return new WaitForSeconds(0.7f);

            Last_Scene_Message_Box_Button.enabled = true;
        }
        else if (Last_Scene_AnimatorStateInfo.IsName("Skip_Result_Scene"))
        {
            Last_Scene_Text_Change_Function();

            Last_Scene_Message_Box_Button.enabled = false;

            var animator = GetComponent<Animator>();

            animator.Play(Black_Fade_IN_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            yield return new WaitForSeconds(1.9f);

            animator.Play(Last_Scene_Message_Box_IN_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            Static_variable_Manager_Script.text_change_Count_int = 0;
            Last_Scene_Message_Box_Button.onClick.RemoveAllListeners();
            Last_Scene_Message_Box_Button.onClick.AddListener(() => Last_Scene_Text_Change_Function());

            Last_Scene_Message_Box_Button.enabled = true;
        }
        else if (Last_Scene_AnimatorStateInfo.IsName("Last_Scene_Message_Box_IN"))
        {
            Last_Scene_Message_Box_Button.enabled = false;

            var animator = GetComponent<Animator>();

            animator.Play(Last_Scene_Message_Box_Out_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            yield return new WaitForSeconds(1.2f);

            SceneManager.LoadScene("Title_Scene");
        }
    }
    //▲ドアが開くアニメーターを管理するコルーチン。


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



    public void Last_Scene_Animator_Event_SE_Function()
    {
        se_Manager_Script.SE_1_Footsteps_AudioClip_Function();
    }



    public void Result_Text_Set_Function()
    {
        play_Start_Flag_bool = false;

        int hint_Count = Static_variable_Manager_Script.result_Hint_Check_Count_int;

        result_Play_Time = (int)play_Time;

        result_Hint_Check_Count_Text.text = Static_variable_Manager_Script.result_Hint_Check_Count_int.ToString();


        result_Time_Count_Text.text = result_Play_Time.ToString();



        if(0 <= result_Play_Time && result_Play_Time <= 300)
        {
            total_Point = 1;
        }
        else if (301 <= result_Play_Time && result_Play_Time <= 480)
        {
            total_Point = 2;
        }
        else if (481 <= result_Play_Time && result_Play_Time <= 660)
        {
            total_Point = 3;
        }
        else if (661 <= result_Play_Time && result_Play_Time <= 840)
        {
            total_Point = 4;
        }
        else if (841 <= result_Play_Time)
        {
            total_Point = 5;
        }



        if(0 <= hint_Count && hint_Count <= 4)
        {
            total_Point = total_Point + 1;
        }
        else if (5 <= hint_Count && hint_Count <= 7)
        {
            total_Point = total_Point + 2;
        }
        else if (8 <= hint_Count && hint_Count <= 9)
        {
            total_Point = total_Point + 3;
        }
        else if (10 <= hint_Count && hint_Count <= 11)
        {
            total_Point = total_Point + 4;
        }
        else if (12 <= hint_Count)
        {
            total_Point = total_Point + 5;
        }


        if (total_Point == 2)
        {
            result_Rank_Text.text = "A";
        }
        else if(total_Point == 3 || total_Point == 4)
        {
            result_Rank_Text.text = "B";
        }
        else if (total_Point == 5 || total_Point == 6)
        {
            result_Rank_Text.text = "C";
        }
        else if (total_Point == 7 || total_Point == 8)
        {
            result_Rank_Text.text = "D";
        }
        else if (total_Point == 9  || total_Point == 10)
        {
            result_Rank_Text.text = "E";
        }
    }


    public void Last_Scene_Text_Change_Function()
    {
        if (Static_variable_Manager_Script.text_change_Count_int == 0)
        {
            Static_variable_Manager_Script.text_change_Count_int = 1;
            Last_Scene_Message_Box_Text.text = "このゲームは\nここで終了です";
        }
        else if (Static_variable_Manager_Script.text_change_Count_int == 1)
        {
            Static_variable_Manager_Script.text_change_Count_int = 2;
            Last_Scene_Message_Box_Text.text = "最後まで\nプレイしていただき\nありがとうございました";
        }
        else if (Static_variable_Manager_Script.text_change_Count_int == 2)
        {
            Static_variable_Manager_Script.text_change_Count_int = 3;
            Last_Scene_Message_Box_Text.text = "画面をタッチ\n(またはクリック)すると\nスタート画面に戻ります";

            Last_Scene_Message_Box_Button.onClick.RemoveAllListeners();
            Last_Scene_Message_Box_Button.onClick.AddListener(() => Last_Scene_Animator_For_Coroutine_Function());
        }
    }

}
