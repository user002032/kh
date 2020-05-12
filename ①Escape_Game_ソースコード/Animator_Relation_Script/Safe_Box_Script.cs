using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Safe_Box_Script : MonoBehaviour
{
    public Animator Safe_Box_Animator;


    public int Safe_Box_Button_Flashing_Hash = Animator.StringToHash("Safe_Box_Button_Flashing");
    public int Safe_Box_Door_Open_Hash = Animator.StringToHash("Armature|Safe_Box_Door_Open");

    public int Safe_Box_Cancel_Message_Box_IN_Hash = Animator.StringToHash("Safe_Box_Cancel_Message_Box_IN");
    public int Safe_Box_Cancel_Message_Box_Out_Hash = Animator.StringToHash("Safe_Box_Cancel_Message_Box_Out");


    public const int color_White = 0;
    public const int color_Red = 1;
    public const int color_Blue = 2;
    public const int color_Green = 3;

    public GameObject[] lamp_Button_Image_Object = new GameObject[3];

    public SpriteRenderer[] lamp_Button_Image_SpriteRenderer_Object = new SpriteRenderer[3];

    public Sprite[] lamp_Button_Image_Sprite = new Sprite[4];

    public Button[] lamp_Array_Button = new Button[3];

    public Button Safe_Box_Cancel_Button;

    public GameObject Hammer_Object;

    private int right_Button_Count_int = 0;
    private int center_Button_Count_int = 0;
    private int left_Button_Count_int = 0;



    Safe_Box_Cancel_Button_Script safe_Box_Cancel_Button_Script;
    Camera_Manager_Script camera_Manager_Script;
    UI_Manager_Script ui_Manager_Script;
    SE_Manager_Script se_Manager_Script;
    Message_Box_Manager_Script message_Box_Manager_Script;
    GameManager game_Manager_Script;


    public GameObject Safe_Box_Cancel_Button_Script_Object;
    public GameObject Camera_Manager_Script_Object;
    public GameObject UI_Manager_Script_Object;
    public GameObject SE_Manager_Script_Object;
    public GameObject Message_Box_Manager_Script_Object;
    public GameObject Game_Manager_Script_Object;


    // Start is called before the first frame update
    void Start()
    {
        right_Button_Count_int = 0;
        center_Button_Count_int = 0;
        left_Button_Count_int = 0;

        Hammer_Object.SetActive(true);
        Safe_Box_Cancel_Button.enabled = false;
        Safe_Box_Animator = GetComponent<Animator>();

        lamp_Array_Button[0].enabled = false;
        lamp_Array_Button[1].enabled = false;
        lamp_Array_Button[2].enabled = false;

        safe_Box_Cancel_Button_Script = Safe_Box_Cancel_Button_Script_Object.GetComponent<Safe_Box_Cancel_Button_Script>();
        camera_Manager_Script  = Camera_Manager_Script_Object.GetComponent<Camera_Manager_Script>();
        ui_Manager_Script = UI_Manager_Script_Object.GetComponent<UI_Manager_Script>();
        se_Manager_Script = SE_Manager_Script_Object.GetComponent<SE_Manager_Script>();
        message_Box_Manager_Script = Message_Box_Manager_Script_Object.GetComponent<Message_Box_Manager_Script>();

        game_Manager_Script = Game_Manager_Script_Object.GetComponent<GameManager>();

        lamp_Button_Image_SpriteRenderer_Object[0].GetComponent<SpriteRenderer>().sprite = lamp_Button_Image_Sprite[color_White];
        lamp_Button_Image_SpriteRenderer_Object[1].GetComponent<SpriteRenderer>().sprite = lamp_Button_Image_Sprite[color_White];
        lamp_Button_Image_SpriteRenderer_Object[2].GetComponent<SpriteRenderer>().sprite = lamp_Button_Image_Sprite[color_White];
    }

    // Update is called once per frame
    void Update()
    {
        
    }






    public void Safe_Box_Cancel_Button_Set_Animator_For_Coroutine_Function()
    {
        StartCoroutine("Safe_Box_Cancel_Button_Set_Animator_Coroutine");
    }

    [Obsolete]
    public IEnumerator Safe_Box_Cancel_Button_Set_Animator_Coroutine()
    {
        AnimatorStateInfo Safe_Box_Cancel_Button_Set_AnimatorStateInfo = safe_Box_Cancel_Button_Script.Safe_Box_Cancel_Button_Script_Animator.GetCurrentAnimatorStateInfo(0);

        if (Safe_Box_Cancel_Button_Set_AnimatorStateInfo.IsName("Idol"))
        {
            var animator = safe_Box_Cancel_Button_Script.Safe_Box_Cancel_Button_Script_Animator.GetComponent<Animator>();

            animator.Play(Safe_Box_Cancel_Message_Box_IN_Hash);

            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);
            Safe_Box_Animator.Play(Safe_Box_Button_Flashing_Hash);
            Safe_Box_Button_enabled_Set_Function();
            Safe_Box_Cancel_Button.enabled = true;
        }
        else if (Safe_Box_Cancel_Button_Set_AnimatorStateInfo.IsName("Safe_Box_Cancel_Message_Box_IN"))
        {
            Static_variable_Manager_Script.event_Flag_No_6_bool = false;
            Safe_Box_Button_enabled_OFF_Function();
            Safe_Box_Cancel_Button.enabled = false;
            var animator = safe_Box_Cancel_Button_Script.Safe_Box_Cancel_Button_Script_Animator.GetComponent<Animator>();

            Safe_Box_Animator.SetTrigger("Safe_Box_Button_Flashing_Out");
            animator.Play(Safe_Box_Cancel_Message_Box_Out_Hash);

            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            camera_Manager_Script.Camera_Canvas_UI_Change_Function();
            ui_Manager_Script.UI_Display_ON_Function();
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


    public void Safe_Box_Button_enabled_Set_Function()
    {
        lamp_Array_Button[0].enabled = true;
        lamp_Array_Button[1].enabled = true;
        lamp_Array_Button[2].enabled = true;
    }

    public void Safe_Box_Button_enabled_OFF_Function()
    {
        lamp_Array_Button[0].enabled = false;
        lamp_Array_Button[1].enabled = false;
        lamp_Array_Button[2].enabled = false;
    }



    public void Push_Button_Lamp_1_Function()
    {
        left_Button_Count_int++;

        if (left_Button_Count_int >3)
        {
            left_Button_Count_int = 0;
        }

        lamp_Button_Image_SpriteRenderer_Object[0].GetComponent<SpriteRenderer>().sprite =
            lamp_Button_Image_Sprite[left_Button_Count_int];

        Safe_Box_Open_Flag_ON_Function();
    }

    public void Push_Button_Lamp_2_Function()
    {
        center_Button_Count_int++;

        if (center_Button_Count_int > 3)
        {
            center_Button_Count_int = 0;
        }

        lamp_Button_Image_SpriteRenderer_Object[1].GetComponent<SpriteRenderer>().sprite =
            lamp_Button_Image_Sprite[center_Button_Count_int];

        Safe_Box_Open_Flag_ON_Function();
    }

    public void Push_Button_Lamp_3_Function()
    {
        right_Button_Count_int++;

        if (right_Button_Count_int > 3)
        {
            right_Button_Count_int = 0;
        }

        lamp_Button_Image_SpriteRenderer_Object[2].GetComponent<SpriteRenderer>().sprite =
            lamp_Button_Image_Sprite[right_Button_Count_int];

        Safe_Box_Open_Flag_ON_Function();
    }

    public void Safe_Box_Open_Flag_ON_Function()
    {
        //Debug.Log("確認2222");

        if (left_Button_Count_int == 3 && center_Button_Count_int == 2 && right_Button_Count_int ==1 )
        {
            Safe_Box_Open_Animator_For_Coroutine_Function();
        }
    }

    public void Safe_Box_Open_Animator_For_Coroutine_Function()
    {
        Static_variable_Manager_Script.event_Flag_No_6_bool = false;
        Static_variable_Manager_Script.event_Flag_No_7_bool = true;
        game_Manager_Script.Event_Manager_Function();

        //StartCoroutine("Safe_Box_Open_Animator_Coroutine");
    }


    ////▼金庫扉を開くコルーチン
    //[Obsolete]
    //public IEnumerator Safe_Box_Open_Animator_Coroutine()
    //{
    //    AnimatorStateInfo Safe_Box_Open_AnimatorStateInfo = Safe_Box_Animator.GetCurrentAnimatorStateInfo(0);

    //    if (Safe_Box_Open_AnimatorStateInfo.IsName("Safe_Box_Button_Flashing"))
    //    {
    //        //Safe_Box_Cancel_Button.enabled = false;
    //        //Safe_Box_Button_enabled_OFF_Function();

    //        //var cancel_Message_animator = safe_Box_Cancel_Button_Script.Safe_Box_Cancel_Button_Script_Animator.GetComponent<Animator>();
    //        //Safe_Box_Animator.SetTrigger("Safe_Box_Button_Flashing_Out");
    //        //cancel_Message_animator.Play(Safe_Box_Cancel_Message_Box_Out_Hash);
    //        //yield return null;
    //        //yield return new Wait_For_Animation_Custom_Coroutine(cancel_Message_animator, 0);

    //        //yield return new WaitForSeconds(0.3f);
    //        //yield return StartCoroutine(se_Manager_Script.Wall_NO_5_SE_Coroutine());
    //        //yield return new WaitForSeconds(0.6f);

    //        //var safe_Box_animator = GetComponent<Animator>();
    //        //safe_Box_animator.Play(Safe_Box_Door_Open_Hash);
    //        //yield return null;
    //        //se_Manager_Script.SE_13_Safe_Box_Open_AudioClip_Function();
    //        //yield return new Wait_For_Animation_Custom_Coroutine(safe_Box_animator, 0);


    //        //camera_Manager_Script.Camera_Canvas_UI_Change_Function();

    //        //Static_variable_Manager_Script.event_Flag_No_6_bool = false;
    //        //Static_variable_Manager_Script.event_Flag_No_7_bool = true;

    //        //message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function();

    //        ////ここにイベント呼び出し。


    //        
    //    }
    //}
    //▲金庫扉を開くコルーチン

}

