using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stepladder_Manager_Script : MonoBehaviour
{

    //▼脚立と壁際のチェーンの3Dオブジェクトの箇所
    public GameObject Wall_Set_Chain_and_Dial_Lock_1_3D_Object;
    public GameObject Wall_Set_Chain_and_Dial_Lock_2_3D_Object;
    public GameObject Stepladder_3D_Object_6;
    public GameObject Stepladder_3D_Object_8;
    //▲脚立と壁際のチェーンの3Dオブジェクトの箇所


    SE_Manager_Script se_Manager_Script;
    public GameObject SE_Manager_Script_Object;

    Message_Box_Manager_Script message_Box_Manager_Script;
    public GameObject Message_Box_Manager_Script_Object;



    // Start is called before the first frame update
    void Start()
    {
        Stepladder_3D_Object_8.SetActive(true);
        Wall_Set_Chain_and_Dial_Lock_1_3D_Object.SetActive(true);
        Wall_Set_Chain_and_Dial_Lock_2_3D_Object.SetActive(false);

        se_Manager_Script = SE_Manager_Script_Object.GetComponent<SE_Manager_Script>();
        message_Box_Manager_Script = Message_Box_Manager_Script_Object.GetComponent<Message_Box_Manager_Script>();

    }

    // Update is called once per frame
    void Update()
    {

    }



    //▼脚立を金庫の前に設置する記述▼
    public void Stepladder_Set_For_Coroutine_Function()
    {
        StartCoroutine("Stepladder_Set_Coroutine");
    }


    public IEnumerator Stepladder_Set_Coroutine()
    {
        Stepladder_3D_Object_6.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        se_Manager_Script.SE_12_Stepladder_Set_AudioClip_Function();

        yield return new WaitForSeconds(0.6f);
        Static_variable_Manager_Script.text_change_Count_int = 3;
        message_Box_Manager_Script.Message_Box_Middle_Animator_For_Coroutine_Function();
    }

}
