using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Start_Scene_Script : MonoBehaviour
{

    public Animator Start_Scene_Script_Animator;

    private int Fade_Out_Hash = Animator.StringToHash("Fade_Out");
    private int Start_IN_Hash = Animator.StringToHash("Start_IN");
    private int Start_Waiting_Hash = Animator.StringToHash("Start_Waiting");
    private int Push_Start_Button_Hash = Animator.StringToHash("Push_Start_Button");

    public GameObject Start_Scene_Button_Object;
    public Button Start_Scene_Button;

    public AudioClip Start_Scene_Button_Push_Fade_Out_SE_AudioClip;
    AudioSource Start_Scene_Button_Push_Fade_Out_SE_Audio_Source;

    // Start is called before the first frame update
    void Start()
    {
        Start_Scene_Button_Push_Fade_Out_SE_Audio_Source = GetComponent<AudioSource>();

        Start_Scene_Script_Animator = GetComponent<Animator>();
        Start_Scene_Button_Object.SetActive(false);
        Start_Scene_Button.enabled = false;
        
        Start_Scene_Animator_For_Coroutine_Function();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Game_Quit();
        }
    }


    void Game_Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
                    UnityEngine.Application.Quit();
        #endif
    }


    public void Start_Scene_Animator_For_Coroutine_Function()
    {
        StartCoroutine("Start_Scene_Animator_Coroutine");
    }


    //▼ドアが開くアニメーターを管理するコルーチン。
    [System.Obsolete]
    public IEnumerator Start_Scene_Animator_Coroutine()
    {
        AnimatorStateInfo Start_Scene_AnimatorStateInfo = Start_Scene_Script_Animator.GetCurrentAnimatorStateInfo(0);

        if (Start_Scene_AnimatorStateInfo.IsName("Idol"))
        {
            Start_Scene_Button_Object.SetActive(true);
            Start_Scene_Button.enabled = true;

            var animator = GetComponent<Animator>();

            animator.Play(Fade_Out_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            yield return null;

            animator.Play(Start_IN_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            Start_Scene_Animator_For_Coroutine_Function();
        }
        else if (Start_Scene_AnimatorStateInfo.IsName("Start_IN"))
        {
            Start_Scene_Button.enabled = true;

            var animator = GetComponent<Animator>();

            animator.Play(Start_Waiting_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

            yield return new WaitForSeconds(0.7f);

        }
        else if (Start_Scene_AnimatorStateInfo.IsName("Start_Waiting"))
        {
            Start_Scene_Button.enabled = false;

            var animator = GetComponent<Animator>();

            Start_Scene_Button_Push_Fade_Out_SE_AudioClip_Function();
            animator.Play(Push_Start_Button_Hash);
            
            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);
            animator.SetTrigger("Start_Scene_Exit");
            yield return new WaitForSeconds(1.2f);

            Start_Scene_Button_Object.SetActive(false);
            SceneManager.LoadScene("Game_Scene");
        }
    }
    //▲ドアが開くアニメーターを管理するコルーチン。


    //▼各種アニメーターで用いるアニメーションの終了判定のコルーチンに用いるカスタムコルーチン
    public class Wait_For_Animation_Custom_Coroutine : CustomYieldInstruction
    {
        Animator wait_Check_Animator;
        int wait_Check_Start_State_Hash = 0;
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
            wait_Check_Start_State_Hash = hash;
        }


        public override bool keepWaiting
        {
            get
            {
                var current_Animator_State = wait_Check_Animator.GetCurrentAnimatorStateInfo(wait_Check_Layer_No);
                return current_Animator_State.fullPathHash == wait_Check_Start_State_Hash && (current_Animator_State.normalizedTime < 1);
            }
        }
    }
    //▲各種アニメーターで用いるアニメーションの終了判定のコルーチンに用いるカスタムコルーチン



    public void Start_Scene_Button_Push_Fade_Out_SE_AudioClip_Function()
    {
        Start_Scene_Button_Push_Fade_Out_SE_Audio_Source.PlayOneShot(Start_Scene_Button_Push_Fade_Out_SE_AudioClip);
    }

}
