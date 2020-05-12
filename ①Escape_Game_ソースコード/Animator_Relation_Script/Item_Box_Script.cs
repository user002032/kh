using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Item_Box_Script : MonoBehaviour
{

    public Animator Item_Box_Script_Animator;

    private int Item_Box_Open_Hash = Animator.StringToHash("Item_Box_Open");

    public GameObject Fastener_2_Object;

    //▼丸棒の留め具の移動関連のトランスフォームとVector3型の変数宣言箇所
    Transform Fastener_2_Object_Transfrom;
    Vector3 Fastener_2_Object_Vector3_Position;
    Vector3 Fastener_2_Object_Vector3_LocalAngle;
    //▲丸棒の留め具の移動関連のトランスフォームとVector3型の変数宣言箇所

    // Start is called before the first frame update
    void Start()
    {
        Item_Box_Script_Animator = GetComponent<Animator>();


        Fastener_2_Object_Transfrom = Fastener_2_Object.transform;
        Fastener_2_Object_Vector3_Position = Fastener_2_Object_Transfrom.position;
        Fastener_2_Object_Vector3_LocalAngle = Fastener_2_Object_Transfrom.localEulerAngles;

        Fastener_2_Object_Vector3_Position.x = 0f;
        Fastener_2_Object_Vector3_Position.y = 0f;
        Fastener_2_Object_Vector3_Position.z = 0f;

        Fastener_2_Object_Vector3_LocalAngle.x = -89.98f;
        Fastener_2_Object_Vector3_LocalAngle.y = 0f;
        Fastener_2_Object_Vector3_LocalAngle.z = -180f;

    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //▼アイテムボックスを開けるコルーチン
    [Obsolete]
    public IEnumerator Item_Box_Animator_Coroutine()
    {
        AnimatorStateInfo Item_Box_AnimatorStateInfo = Item_Box_Script_Animator.GetCurrentAnimatorStateInfo(0);


        if (Item_Box_AnimatorStateInfo.IsName("Idol"))
        {
            var animator = GetComponent<Animator>();

            animator.Play(Item_Box_Open_Hash);

            yield return null;
            yield return new Wait_For_Animation_Custom_Coroutine(animator, 0);

        }
    }
    //▲アイテムボックスを開けるコルーチン



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



    public void Fastener_2_Object_Move_Set_Function()
    {
        Fastener_2_Object_Vector3_Position.z = -0.00036f;

        Fastener_2_Object.transform.localPosition = Fastener_2_Object_Vector3_Position;
    }
}
