using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Door_Script : MonoBehaviour
{
    public Animator Door_Script_Animator;

    private int Door_Open_Hash = Animator.StringToHash("Door_Open");

    // Start is called before the first frame update
    void Start()
    {
        Door_Script_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //▼ドアが開くアニメーターを管理するコルーチン。
    [System.Obsolete]
    public IEnumerator Door_Animator_Coroutine()
    {
        AnimatorStateInfo door_AnimatorStateInfo = Door_Script_Animator.GetCurrentAnimatorStateInfo(0);

        if (door_AnimatorStateInfo.IsName("Idol"))
        {
            var animator = GetComponent<Animator>();

            animator.Play(Door_Open_Hash);

            yield return null;

            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

        }
        else if (door_AnimatorStateInfo.IsName("Door_Open"))
        {
            Door_Script_Animator.SetTrigger("Door_Exit");
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


}
