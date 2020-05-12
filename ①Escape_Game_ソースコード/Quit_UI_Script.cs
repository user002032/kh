using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

public class Quit_UI_Script : MonoBehaviour
{
    public Animator Quit_UI_Script_Animator;

    public Button Quit_Button;
    public Button Quit_Yes_Button;
    public Button Quit_No_Button;

    public GameObject Quit_Back_Object;

    private int Quit_Message_Box_IN_Hash = Animator.StringToHash("Quit_Message_Box_IN");
    private int Quit_Message_Box_Out_Hash = Animator.StringToHash("Quit_Message_Box_Out");


    // Start is called before the first frame update
    void Start()
    {
        Quit_Back_Object.SetActive(false);
        Quit_UI_Script_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Game_Quit();
        }
    }


    public void Game_Quit()
    {
        // プラットフォームがアンドロイドかチェック
        if (Application.platform == RuntimePlatform.Android)
        {
            // アプリケーション終了
            Application.Quit();
            return;
        }

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
    }

    public void Select_Game_Quit()
    {
        Quit_Button.enabled = false;

        Quit_Back_Object.SetActive(true);

        Quit_Select_Message_Box_Middle_Animator_For_Coroutine_Function();
    }




    //▼強制終了用メッセージボックスを表示するアニメーターを管理するコルーチンを呼び出す関数
    public void Quit_Select_Message_Box_Middle_Animator_For_Coroutine_Function()
    {
        StartCoroutine("Quit_Message_Box_Middle_Animator_Coroutine");
    }
    //▲強制終了用メッセージボックスを表示するアニメーターを管理するコルーチンを呼び出す関数


    //▼強制終了用メッセージボックスを表示するアニメーターを管理するコルーチン
    [Obsolete]
    public IEnumerator Quit_Message_Box_Middle_Animator_Coroutine()
    {
        AnimatorStateInfo quit_UI_Object_AnimatorStateInfo = Quit_UI_Script_Animator.GetCurrentAnimatorStateInfo(0);


        if (quit_UI_Object_AnimatorStateInfo.IsName("Idol"))
        {
            var animator = GetComponent<Animator>();


            animator.Play(Quit_Message_Box_IN_Hash);

            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);
            Quit_Yes_Button.enabled = true;
            Quit_No_Button.enabled = true;

        }
        else if (quit_UI_Object_AnimatorStateInfo.IsName("Quit_Message_Box_IN"))
        {
            Quit_Yes_Button.enabled = false;
            Quit_No_Button.enabled = false;

            var animator = GetComponent<Animator>();

            animator.Play(Quit_Message_Box_Out_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            Quit_Back_Object.SetActive(false);
            Quit_Button.enabled = true;
        }
    }


    //▲強制終了用メッセージボックスを表示するアニメーターを管理するコルーチン




    //▼「はい」「いいえ」の選択肢の内、『はい』のボタンを選らばれた場合に呼び出す関数。
    public void Choice_Animator_For_Coroutine_YES_Flag_Function()
    {
        Game_Quit();
    }
    //▲「はい」「いいえ」の選択肢の内、『はい』のボタンを選らばれた場合に呼び出す関数。




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
