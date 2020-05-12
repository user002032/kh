using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Manager_Script : MonoBehaviour
{

    public GameObject Fade_Black_Renderer_Image_Object;
    public GameObject Circle_Sprite_Mask_Object;

    Fade_Black_Renderer_Image_Script fade_Black_Renderer_Image_Script;


    private int Fade_Out_Hash = Animator.StringToHash("Fade_Out");
    private int Fade_Gaze_Circle_IN_Hash = Animator.StringToHash("Fade_Gaze_Circle_IN");
    private int Fade_Gaze_Circle_Out_Hash = Animator.StringToHash("Fade_Gaze_Circle_Out");
    private int Fade_IN_Hash = Animator.StringToHash("Fade_IN");

    // Start is called before the first frame update
    void Start()
    {
        Circle_Sprite_Mask_Object.SetActive(false);
        Fade_Black_Renderer_Image_Object.SetActive(true);
        fade_Black_Renderer_Image_Script = Fade_Black_Renderer_Image_Object.GetComponent<Fade_Black_Renderer_Image_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fade_Animator_Event_1_For_Coroutine_Function()
    {
        StartCoroutine("Fade_Animator_Event_1_Coroutine");
    }


    //▼イベント１のフェードイン・アウト、丸型の穴の空いた半黒色の画面のアニメーターを管理するコルーチン。
    [System.Obsolete]
    public IEnumerator Fade_Animator_Event_1_Coroutine()
    {
        AnimatorStateInfo fade_AnimatorStateInfo = fade_Black_Renderer_Image_Script.Fade_Black_Renderer_Image_Script_Animator.GetCurrentAnimatorStateInfo(0);

        if (fade_AnimatorStateInfo.IsName("Idol"))
        {

            var animator = Fade_Black_Renderer_Image_Object.GetComponent<Animator>();

            animator.Play(Fade_Out_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

        }
        else if (fade_AnimatorStateInfo.IsName("Fade_Out"))
        {
            Circle_Sprite_Mask_Object.SetActive(true);
            var animator = Fade_Black_Renderer_Image_Object.GetComponent<Animator>();

            animator.Play(Fade_Gaze_Circle_IN_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);
        }
        else if (fade_AnimatorStateInfo.IsName("Fade_Gaze_Circle_IN"))
        {
            var animator = Fade_Black_Renderer_Image_Object.GetComponent<Animator>();

            animator.Play(Fade_Gaze_Circle_Out_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);
            yield return null;
        }
    }
    //▲イベント１のフェードイン・アウト、丸型の穴の空いた半黒色の画面のアニメーターを管理するコルーチン。


    //▼イベント８のフェードイン・アウトを管理するコルーチン。
    [System.Obsolete]
    public IEnumerator Fade_Animator_Event_8_Coroutine()
    {
        AnimatorStateInfo fade_AnimatorStateInfo = fade_Black_Renderer_Image_Script.Fade_Black_Renderer_Image_Script_Animator.GetCurrentAnimatorStateInfo(0);

        if (fade_AnimatorStateInfo.IsName("Fade_not_Waiting"))
        {
            Circle_Sprite_Mask_Object.SetActive(false);

            var animator = Fade_Black_Renderer_Image_Object.GetComponent<Animator>();

            animator.Play(Fade_IN_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

        }
        else if (fade_AnimatorStateInfo.IsName("Fade_IN"))
        {
            var animator = Fade_Black_Renderer_Image_Object.GetComponent<Animator>();

            animator.Play(Fade_Out_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);
        }
    }
    //▲イベント８のフェードイン・アウトを管理するコルーチン。


    //▼イベント９のフェードイン・アウトを管理するコルーチン。
    [System.Obsolete]
    public IEnumerator Fade_Animator_Event_9_Coroutine()
    {
        AnimatorStateInfo fade_AnimatorStateInfo = fade_Black_Renderer_Image_Script.Fade_Black_Renderer_Image_Script_Animator.GetCurrentAnimatorStateInfo(0);

        if (fade_AnimatorStateInfo.IsName("Fade_Out"))
        {
            Circle_Sprite_Mask_Object.SetActive(false);

            var animator = Fade_Black_Renderer_Image_Object.GetComponent<Animator>();

            animator.Play(Fade_IN_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

        }
        else if (fade_AnimatorStateInfo.IsName("Fade_IN"))
        {
            var animator = Fade_Black_Renderer_Image_Object.GetComponent<Animator>();

            animator.Play(Fade_Out_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);
        }
    }
    //▲イベント９のフェードイン・アウトを管理するコルーチン。



    //▼各種アニメーターで用いるアニメーションの終了判定のコルーチンに用いるカスタムコルーチン
    public class Wait_For_Animation_Custom_Coroutine : CustomYieldInstruction
    {
        Animator wait_Check_Animator;
        int wait_Check_Last_State_Hash = 0;
        int wait_Check_Layer_No = 0;

        [System.Obsolete]
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
