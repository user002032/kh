using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Dial_Lock_Manager_Script : MonoBehaviour
{

    Dial_Lock_Script dial_Lock_Script;
    bool dial_Lock_Clear_Flag_bool = false;

    public GameObject Dial_Lock_3D_Object;

    int[] Dial_Lock_Upper_Number_Array = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int[] Dial_Lock_Middle_Number_Array = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    int[] Dial_Lock_Bottom_Number_Array = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    public TextMeshProUGUI Dial_Lock_Upper_Number_Middle_Text;
    public TextMeshProUGUI Dial_Lock_Middle_Number_Middle_Text;
    public TextMeshProUGUI Dial_Lock_Bottom_Number_Middle_Text;

    public TextMeshProUGUI Dial_Lock_Upper_Number_Left_Text;
    public TextMeshProUGUI Dial_Lock_Middle_Number_Left_Text;
    public TextMeshProUGUI Dial_Lock_Bottom_Number_Left_Text;

    public TextMeshProUGUI Dial_Lock_Upper_Number_Right_Text;
    public TextMeshProUGUI Dial_Lock_Middle_Number_Right_Text;
    public TextMeshProUGUI Dial_Lock_Bottom_Number_Right_Text;

    int dial_Lock_Upper_Number_int = 0;
    int dial_Lock_Middle_Number_int = 0;
    int dial_Lock_Bottom_Number_int = 0;

    public Button Dial_Lock_Upper_Number_Change_Right_Button;
    public Button Dial_Lock_Middle_Number_Change_Right_Button;
    public Button Dial_Lock_Bottom_Number_Change_Right_Button;

    public Button Dial_Lock_Upper_Number_Change_Left_Button;
    public Button Dial_Lock_Middle_Number_Change_Left_Button;
    public Button Dial_Lock_Bottom_Number_Change_Left_Button;

    public Button Dial_Lock_Cancel_Button;


    private int Dial_Lock_IN_Hash = Animator.StringToHash("Dial_Lock_IN");
    private int Dial_Lock_Out_Hash = Animator.StringToHash("Dial_Lock_Out");
    private int Dial_Lock_Key_Out_1_Hash = Animator.StringToHash("Dial_Lock_Key_Out_1");
    private int Dial_Lock_Key_Out_2_Hash = Animator.StringToHash("Dial_Lock_Key_Out_2");


    public GameObject SE_Manager_Script_Object;
    SE_Manager_Script se_Manager_Script;

    public GameObject Message_Box_Manager_Script_Object;
    Message_Box_Manager_Script message_Box_Manager_Script;

    public GameObject UI_Manager_Script_Object;
    UI_Manager_Script ui_Manager_Script;

    public GameObject Upper_Light_Icon_Manager_Script_Object;
    Upper_Light_Icon_Manager_Script upper_Light_Icon_Manager_Script;

    public GameObject Game_Manager_Object;
    GameManager game_Manager_Script;

    public GameObject Stepladder_Manager_Script_Object;
    Stepladder_Manager_Script stepladder_Manager_Script;

    // Start is called before the first frame update
    void Start()
    {
        Dial_Lock_Upper_Number_Change_Right_Button.enabled = false;
        Dial_Lock_Middle_Number_Change_Right_Button.enabled = false;
        Dial_Lock_Bottom_Number_Change_Right_Button.enabled = false;
        Dial_Lock_Upper_Number_Change_Left_Button.enabled = false;
        Dial_Lock_Middle_Number_Change_Left_Button.enabled = false;
        Dial_Lock_Bottom_Number_Change_Left_Button.enabled = false;
        Dial_Lock_Cancel_Button.enabled = false;

        dial_Lock_Clear_Flag_bool = false;
        dial_Lock_Upper_Number_int = 0;
        dial_Lock_Middle_Number_int = 0;
        dial_Lock_Bottom_Number_int = 0;

        dial_Lock_Script = Dial_Lock_3D_Object.GetComponent<Dial_Lock_Script>();//  ダイヤル錠のアニメーターの読み込み。
        Dial_Lock_3D_Object.SetActive(false);

        Dial_Lock_Upper_Number_Left_Text.text = "9";
        Dial_Lock_Middle_Number_Left_Text.text = "9";
        Dial_Lock_Bottom_Number_Left_Text.text = "9";

        Dial_Lock_Upper_Number_Right_Text.text = "2";
        Dial_Lock_Middle_Number_Right_Text.text = "2";
        Dial_Lock_Bottom_Number_Right_Text.text = "2";

        se_Manager_Script = SE_Manager_Script_Object.GetComponent<SE_Manager_Script>();
        message_Box_Manager_Script = Message_Box_Manager_Script_Object.GetComponent<Message_Box_Manager_Script>();
        ui_Manager_Script = UI_Manager_Script_Object.GetComponent<UI_Manager_Script>();
        upper_Light_Icon_Manager_Script = Upper_Light_Icon_Manager_Script_Object.GetComponent<Upper_Light_Icon_Manager_Script>();
        game_Manager_Script = Game_Manager_Object.GetComponent<GameManager>();

        stepladder_Manager_Script = Stepladder_Manager_Script_Object.GetComponent<Stepladder_Manager_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Dial_Lock_Animator_For_Coroutine_Function()
    {
        StartCoroutine("Dial_Lock_Animator_Coroutine");
    }


    //▼ダイヤル錠が画面の左から入り、出る時は右へ移動するアニメーターを管理する関数
    [Obsolete]
    public IEnumerator Dial_Lock_Animator_Coroutine()
    {
        Dial_Lock_3D_Object.SetActive(true);
        yield return null;

        AnimatorStateInfo Dial_Lock_AnimatorStateInfo = dial_Lock_Script.Dial_Lock_Script_Animator.GetCurrentAnimatorStateInfo(0);

        message_Box_Manager_Script.Message_Box_Button.enabled = false;

        if (Dial_Lock_AnimatorStateInfo.IsName("Idol"))
        {

            Dial_Lock_Upper_Number_Change_Right_Button.enabled = false;
            Dial_Lock_Middle_Number_Change_Right_Button.enabled = false;
            Dial_Lock_Bottom_Number_Change_Right_Button.enabled = false;
            Dial_Lock_Upper_Number_Change_Left_Button.enabled = false;
            Dial_Lock_Middle_Number_Change_Left_Button.enabled = false;
            Dial_Lock_Bottom_Number_Change_Left_Button.enabled = false;
            Dial_Lock_Cancel_Button.enabled = false;


            message_Box_Manager_Script.Message_Box_Button_Object.SetActive(false);
            message_Box_Manager_Script.Message_Box_Object.SetActive(false);

            var animator = Dial_Lock_3D_Object.GetComponent<Animator>();
            se_Manager_Script.SE_8_Chain_AudioClip_Function();
            animator.Play(Dial_Lock_IN_Hash);
            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            Dial_Lock_Upper_Number_Change_Right_Button.enabled = true;
            Dial_Lock_Middle_Number_Change_Right_Button.enabled = true;
            Dial_Lock_Bottom_Number_Change_Right_Button.enabled = true;
            Dial_Lock_Upper_Number_Change_Left_Button.enabled = true;
            Dial_Lock_Middle_Number_Change_Left_Button.enabled = true;
            Dial_Lock_Bottom_Number_Change_Left_Button.enabled = true;
            Dial_Lock_Cancel_Button.enabled = true;
        }
        else if (Dial_Lock_AnimatorStateInfo.IsName("Dial_Lock_IN") && dial_Lock_Clear_Flag_bool == false)
        {
            Dial_Lock_Upper_Number_Change_Right_Button.enabled = false;
            Dial_Lock_Middle_Number_Change_Right_Button.enabled = false;
            Dial_Lock_Bottom_Number_Change_Right_Button.enabled = false;
            Dial_Lock_Upper_Number_Change_Left_Button.enabled = false;
            Dial_Lock_Middle_Number_Change_Left_Button.enabled = false;
            Dial_Lock_Bottom_Number_Change_Left_Button.enabled = false;
            Dial_Lock_Cancel_Button.enabled = false;

            Static_variable_Manager_Script.event_Flag_No_2_bool = false;
            Dial_Lock_Cancel_Button.enabled = false;

            var animator = Dial_Lock_3D_Object.GetComponent<Animator>();
            se_Manager_Script.SE_8_Chain_AudioClip_Function();
            animator.Play(Dial_Lock_Out_Hash);
            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);
            yield return null;
            Dial_Lock_3D_Object.SetActive(false);

            message_Box_Manager_Script.Message_Box_Object.SetActive(true);
            message_Box_Manager_Script.Message_Box_Button_Object.SetActive(true);
            upper_Light_Icon_Manager_Script.Upper_Light_Icon_Button_Object_ON_Function();
            ui_Manager_Script.UI_Display_ON_Function();

        }
        else if (Dial_Lock_AnimatorStateInfo.IsName("Dial_Lock_IN") && dial_Lock_Clear_Flag_bool == true)
        {
            Dial_Lock_Upper_Number_Change_Right_Button.enabled = false;
            Dial_Lock_Middle_Number_Change_Right_Button.enabled = false;
            Dial_Lock_Bottom_Number_Change_Right_Button.enabled = false;
            Dial_Lock_Upper_Number_Change_Left_Button.enabled = false;
            Dial_Lock_Middle_Number_Change_Left_Button.enabled = false;
            Dial_Lock_Bottom_Number_Change_Left_Button.enabled = false;
            Dial_Lock_Cancel_Button.enabled = false;

            var animator = Dial_Lock_3D_Object.GetComponent<Animator>();
            se_Manager_Script.SE_11_Dial_Lock_Open_AudioClip_Function();
            animator.Play(Dial_Lock_Key_Out_1_Hash);
            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);
            yield return null;
            yield return new WaitForSeconds(1.0f);
            Static_variable_Manager_Script.event_Flag_No_2_bool = true;

            //ここにイベントコルーチン呼び出し。
            StartCoroutine(game_Manager_Script.Event_Manager_Coroutine());

        }
        else if (Dial_Lock_AnimatorStateInfo.IsName("Dial_Lock_Key_Out_1") && dial_Lock_Clear_Flag_bool == true)
        {
            
            var animator = Dial_Lock_3D_Object.GetComponent<Animator>();

            se_Manager_Script.SE_8_Chain_AudioClip_Function();

            animator.Play(Dial_Lock_Key_Out_2_Hash);

            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);
            yield return null;

            Dial_Lock_3D_Object.SetActive(false);
            game_Manager_Script.Wait_While_Flag_Function();
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








    //▼ダイヤル錠の数字を変更、並びに数字が合えば開錠する関数の箇所

    public void Dial_Lock_Upper_Number_Change_Right_Button_Function()
    {
        se_Manager_Script.SE_10_Turn_The_Dial_AudioClip_Function();

        if (dial_Lock_Upper_Number_int == 8)
        {
            dial_Lock_Upper_Number_int = 0;
            Dial_Lock_Upper_Number_Left_Text.text = "9";
            Dial_Lock_Upper_Number_Right_Text.text = "2";
        }
        else
        {
            dial_Lock_Upper_Number_int++;
            Dial_Lock_Upper_Number_Left_Text.text = Convert.ToString(Dial_Lock_Upper_Number_Array[dial_Lock_Upper_Number_int - 1]);

            if (dial_Lock_Upper_Number_int == 8)
            {
                Dial_Lock_Upper_Number_Right_Text.text = "1";
            }
            else
            {
                Dial_Lock_Upper_Number_Right_Text.text = Convert.ToString(Dial_Lock_Upper_Number_Array[dial_Lock_Upper_Number_int + 1]);
            }
        }

        Unlocked_Dial_Lock_Function();
        Dial_Lock_Upper_Number_Middle_Text.text = Convert.ToString(Dial_Lock_Upper_Number_Array[dial_Lock_Upper_Number_int]);
    }




    public void Dial_Lock_Middle_Number_Change_Right_Button_Function()
    {
        se_Manager_Script.SE_10_Turn_The_Dial_AudioClip_Function();

        if (dial_Lock_Middle_Number_int == 8)
        {
            dial_Lock_Middle_Number_int = 0;
            Dial_Lock_Middle_Number_Left_Text.text = "9";
            Dial_Lock_Middle_Number_Right_Text.text = "2";
        }
        else
        {
            dial_Lock_Middle_Number_int++;
            Dial_Lock_Middle_Number_Left_Text.text = Convert.ToString(Dial_Lock_Middle_Number_Array[dial_Lock_Middle_Number_int - 1]);

            if (dial_Lock_Middle_Number_int == 8)
            {
                Dial_Lock_Middle_Number_Right_Text.text = "1";
            }
            else
            {
                Dial_Lock_Middle_Number_Right_Text.text = Convert.ToString(Dial_Lock_Middle_Number_Array[dial_Lock_Middle_Number_int + 1]);
            }
        }
        Unlocked_Dial_Lock_Function();
        Dial_Lock_Middle_Number_Middle_Text.text = Convert.ToString(Dial_Lock_Middle_Number_Array[dial_Lock_Middle_Number_int]);

    }





    public void Dial_Lock_Bottom_Number_Change_Right_Button_Function()
    {
        se_Manager_Script.SE_10_Turn_The_Dial_AudioClip_Function();

        if (dial_Lock_Bottom_Number_int == 8)
        {
            dial_Lock_Bottom_Number_int = 0;
            Dial_Lock_Bottom_Number_Left_Text.text = "9";
            Dial_Lock_Bottom_Number_Right_Text.text = "2";
        }
        else
        {
            dial_Lock_Bottom_Number_int++;
            Dial_Lock_Bottom_Number_Left_Text.text = Convert.ToString(Dial_Lock_Bottom_Number_Array[dial_Lock_Bottom_Number_int - 1]);

            if (dial_Lock_Bottom_Number_int == 8)
            {
                Dial_Lock_Bottom_Number_Right_Text.text = "1";
            }
            else
            {
                Dial_Lock_Bottom_Number_Right_Text.text = Convert.ToString(Dial_Lock_Bottom_Number_Array[dial_Lock_Bottom_Number_int + 1]);
            }
        }

        Unlocked_Dial_Lock_Function();
        Dial_Lock_Bottom_Number_Middle_Text.text = Convert.ToString(Dial_Lock_Bottom_Number_Array[dial_Lock_Bottom_Number_int]);
    }



    public void Dial_Lock_Upper_Number_Change_Left_Button_Function()
    {
        se_Manager_Script.SE_10_Turn_The_Dial_AudioClip_Function();

        if (dial_Lock_Upper_Number_int == 0)
        {
            dial_Lock_Upper_Number_int = 8;
            Dial_Lock_Upper_Number_Left_Text.text = "8";
            Dial_Lock_Upper_Number_Right_Text.text = "1";
        }
        else
        {
            dial_Lock_Upper_Number_int--;
            Dial_Lock_Upper_Number_Right_Text.text = Convert.ToString(Dial_Lock_Upper_Number_Array[dial_Lock_Upper_Number_int + 1]);


            if (dial_Lock_Upper_Number_int == 0)
            {
                Dial_Lock_Upper_Number_Left_Text.text = "9";
            }
            else
            {
                Dial_Lock_Upper_Number_Left_Text.text = Convert.ToString(Dial_Lock_Upper_Number_Array[dial_Lock_Upper_Number_int - 1]);
            }
        }
        Unlocked_Dial_Lock_Function();
        Dial_Lock_Upper_Number_Middle_Text.text = Convert.ToString(Dial_Lock_Upper_Number_Array[dial_Lock_Upper_Number_int]);

    }



    public void Dial_Lock_Middle_Number_Change_Left_Button_Function()
    {
        se_Manager_Script.SE_10_Turn_The_Dial_AudioClip_Function();

        if (dial_Lock_Middle_Number_int == 0)
        {
            dial_Lock_Middle_Number_int = 8;
            Dial_Lock_Middle_Number_Left_Text.text = "8";
            Dial_Lock_Middle_Number_Right_Text.text = "1";
        }
        else
        {
            dial_Lock_Middle_Number_int--;
            Dial_Lock_Middle_Number_Right_Text.text = Convert.ToString(Dial_Lock_Middle_Number_Array[dial_Lock_Middle_Number_int + 1]);


            if (dial_Lock_Middle_Number_int == 0)
            {
                Dial_Lock_Middle_Number_Left_Text.text = "9";
            }
            else
            {
                Dial_Lock_Middle_Number_Left_Text.text = Convert.ToString(Dial_Lock_Middle_Number_Array[dial_Lock_Middle_Number_int - 1]);
            }
        }
        Unlocked_Dial_Lock_Function();
        Dial_Lock_Middle_Number_Middle_Text.text = Convert.ToString(Dial_Lock_Middle_Number_Array[dial_Lock_Middle_Number_int]);
    }



    public void Dial_Lock_Bottom_Number_Change_Left_Button_Function()
    {
        se_Manager_Script.SE_10_Turn_The_Dial_AudioClip_Function();

        if (dial_Lock_Bottom_Number_int == 0)
        {
            dial_Lock_Bottom_Number_int = 8;
            Dial_Lock_Bottom_Number_Left_Text.text = "8";
            Dial_Lock_Bottom_Number_Right_Text.text = "1";
        }
        else
        {
            dial_Lock_Bottom_Number_int--;
            Dial_Lock_Bottom_Number_Right_Text.text = Convert.ToString(Dial_Lock_Bottom_Number_Array[dial_Lock_Bottom_Number_int + 1]);


            if (dial_Lock_Bottom_Number_int == 0)
            {
                Dial_Lock_Bottom_Number_Left_Text.text = "9";
            }
            else
            {
                Dial_Lock_Bottom_Number_Left_Text.text = Convert.ToString(Dial_Lock_Bottom_Number_Array[dial_Lock_Bottom_Number_int - 1]);
            }
        }

        Unlocked_Dial_Lock_Function();
        Dial_Lock_Bottom_Number_Middle_Text.text = Convert.ToString(Dial_Lock_Bottom_Number_Array[dial_Lock_Bottom_Number_int]);
    }


    public void Unlocked_Dial_Lock_Function()
    {
        if (dial_Lock_Upper_Number_int == 4 && dial_Lock_Middle_Number_int == 2 && dial_Lock_Bottom_Number_int == 8)
        {
            dial_Lock_Clear_Flag_bool = true;
            stepladder_Manager_Script.Wall_Set_Chain_and_Dial_Lock_1_3D_Object.SetActive(false);
            stepladder_Manager_Script.Wall_Set_Chain_and_Dial_Lock_2_3D_Object.SetActive(true);
            Dial_Lock_Animator_For_Coroutine_Function();
        }
    }
    //▲ダイヤル錠の数字を変更、並びに数字が合えば開錠する関数の箇所

}
