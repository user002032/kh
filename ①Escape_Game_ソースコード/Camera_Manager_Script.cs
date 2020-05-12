using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Camera_Manager_Script : MonoBehaviour
{

    private const int wall_No_1 = 1;
    private const int wall_No_2 = 2;
    private const int wall_No_3 = 3;
    private const int wall_No_4 = 4;
    private const int wall_No_5 = 5;
    private const int wall_No_6 = 6;
    private const int wall_No_7 = 7;
    private const int wall_No_8 = 8;


    public GameObject Canvas_UI_Object;
    public GameObject Canvas_UI_Zoom_6_Object;
    public GameObject Back_Wall_Cube_Object;


    //▼カメラの表示・非表示用のカメラオブジェクト
    public GameObject WALL_1_Distance_Camera_Object; // 後ろの行き止まり
    public GameObject WALL_2_Distance_Camera_Object; // 最初のシーン　　　　　　　
    public GameObject WALL_3_Distance_Camera_Object; // 手紙のあった場所
    public GameObject WALL_4_Distance_Camera_Object; // 
    public GameObject WALL_5_Distance_Camera_Object; //
    public GameObject WALL_6_Distance_Camera_Object; // 
    public GameObject WALL_7_Distance_Camera_Object; // 
    public GameObject WALL_8_Distance_Camera_Object; // 

    public GameObject WALL_4_Zoom_Camera_Object; // 近距離：机とアイテム箱のある場所
    public GameObject WALL_5_Zoom_Camera_Object; // 近距離：Hintの場所
    public GameObject WALL_6_Zoom_Camera_Object; // 近距離：手の届かない金庫の場所
    public GameObject WALL_7_Zoom_Camera_Object; // 近距離：鍵のかかったドアの場所
    public GameObject WALL_8_Zoom_Camera_Object; // 近距離：脚立の場所

    //▲カメラの表示・非表示用のカメラオブジェクト


    //▼UIへ代入するカメラ情報を格納するCameraデータ
    public Camera WALL_1_Distance_Camera; // 後ろの行き止まり
    public Camera WALL_2_Distance_Camera; // 最初のシーン　　　　　　　
    public Camera WALL_3_Distance_Camera; // 手紙のあった場所
    public Camera WALL_4_Distance_Camera; //
    public Camera WALL_5_Distance_Camera; //
    public Camera WALL_6_Distance_Camera; //
    public Camera WALL_7_Distance_Camera; //
    public Camera WALL_8_Distance_Camera; //

    public Camera WALL_4_Zoom_Camera; // 近距離：机と箱のある場所
    public Camera WALL_5_Zoom_Camera; // 近距離：Hintの場所
    public Camera WALL_6_Zoom_Camera; // 近距離：手の届かない金庫の場所
    public Camera WALL_7_Zoom_Camera; // 近距離：鍵のかかったドアの場所
    public Camera WALL_8_Zoom_Camera; // 近距離：脚立の場所

    

    Transform Camera_Transfrom;
    Vector3 Camera_Vector3_Position;
    Vector3 Camera_Vector3_LocalAngle;
    //▲UIへ代入するカメラ情報を格納するCameraデータ


    public GameObject Last_Scene_Camera_Object;
    public Camera Last_Scene_Camera;

    // Start is called before the first frame update
    void Start()
    {
        Back_Wall_Cube_Object.SetActive(true);

        Canvas_UI_Zoom_6_Object.GetComponent<Canvas>().enabled = false;

        WALL_2_Distance_Camera_Object.SetActive(true);
        WALL_1_Distance_Camera_Object.SetActive(false);
        WALL_3_Distance_Camera_Object.SetActive(false);
        WALL_4_Distance_Camera_Object.SetActive(false);
        WALL_5_Distance_Camera_Object.SetActive(false);
        WALL_6_Distance_Camera_Object.SetActive(false);
        WALL_7_Distance_Camera_Object.SetActive(false);
        WALL_8_Distance_Camera_Object.SetActive(false);

        WALL_4_Zoom_Camera_Object.SetActive(false);
        WALL_5_Zoom_Camera_Object.SetActive(false);
        WALL_6_Zoom_Camera_Object.SetActive(false);
        WALL_7_Zoom_Camera_Object.SetActive(false);
        WALL_8_Zoom_Camera_Object.SetActive(false);
        Last_Scene_Camera_Object.SetActive(false);

        Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_2_Distance_Camera; //　初期確認用。不要になったら消す。

        //▼WALL3_8のカメラの位置を移動する為の位置情報を取得する箇所
        Camera_Transfrom = WALL_3_Distance_Camera_Object.transform;
        Camera_Vector3_Position = Camera_Transfrom.position;
        Camera_Vector3_LocalAngle = Camera_Transfrom.localEulerAngles;
        //▲WALL3_8のカメラの位置を移動する為の位置情報を取得する箇所
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    //▼主体のカメラを、wallNoに合わせて指定する関数
    // 「Transition」は『変遷』の意味の英語。
    // 「Subject」は『主体』の意味の英語。
    public void Camera_Subject_Set_Function()
    {

        WALL_4_Zoom_Camera_Object.SetActive(false);
        WALL_5_Zoom_Camera_Object.SetActive(false);
        WALL_6_Zoom_Camera_Object.SetActive(false);
        WALL_7_Zoom_Camera_Object.SetActive(false);
        WALL_8_Zoom_Camera_Object.SetActive(false);

        if (Static_variable_Manager_Script.zoom_Scene_Flag_bool == false)
        {

            switch (Static_variable_Manager_Script.wall_Number_int)
            {
                case wall_No_1:
                    WALL_1_Distance_Camera_Object.SetActive(true);
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_1_Distance_Camera;
                    WALL_2_Distance_Camera_Object.SetActive(false);
                    WALL_3_Distance_Camera_Object.SetActive(false);
                    WALL_4_Distance_Camera_Object.SetActive(false);
                    WALL_5_Distance_Camera_Object.SetActive(false);
                    WALL_6_Distance_Camera_Object.SetActive(false);
                    WALL_7_Distance_Camera_Object.SetActive(false);
                    WALL_8_Distance_Camera_Object.SetActive(false);

                    break;

                case wall_No_2:
                    WALL_2_Distance_Camera_Object.SetActive(true);
                    Back_Wall_Cube_Object.SetActive(true); //カメラが２番目の時は表示する背景の壁のオブジェクト。
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_2_Distance_Camera;
                    WALL_1_Distance_Camera_Object.SetActive(false);
                    WALL_3_Distance_Camera_Object.SetActive(false);
                    WALL_4_Distance_Camera_Object.SetActive(false);
                    WALL_5_Distance_Camera_Object.SetActive(false);
                    WALL_6_Distance_Camera_Object.SetActive(false);
                    WALL_7_Distance_Camera_Object.SetActive(false);
                    WALL_8_Distance_Camera_Object.SetActive(false);

                    break;

                case wall_No_3:
                    WALL_3_Distance_Camera_Object.SetActive(true);
                    Back_Wall_Cube_Object.SetActive(false); // カメラを移動する際に「Back_Wall_Cube_Object」が邪魔になるので、WALL_3以降は非表示にする。
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_3_Distance_Camera;
                    WALL_1_Distance_Camera_Object.SetActive(false);
                    WALL_2_Distance_Camera_Object.SetActive(false);
                    WALL_4_Distance_Camera_Object.SetActive(false);
                    WALL_5_Distance_Camera_Object.SetActive(false);
                    WALL_6_Distance_Camera_Object.SetActive(false);
                    WALL_7_Distance_Camera_Object.SetActive(false);
                    WALL_8_Distance_Camera_Object.SetActive(false);

                    break;

                case wall_No_4:
                    WALL_4_Distance_Camera_Object.SetActive(true);
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_4_Distance_Camera;
                    WALL_1_Distance_Camera_Object.SetActive(false);
                    WALL_2_Distance_Camera_Object.SetActive(false);

                    WALL_3_Distance_Camera_Object.SetActive(false);
                    WALL_5_Distance_Camera_Object.SetActive(false);
                    WALL_6_Distance_Camera_Object.SetActive(false);
                    WALL_7_Distance_Camera_Object.SetActive(false);
                    WALL_8_Distance_Camera_Object.SetActive(false);

                    break;

                case wall_No_5:
                    WALL_5_Distance_Camera_Object.SetActive(true);
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_5_Distance_Camera;
                    WALL_1_Distance_Camera_Object.SetActive(false);
                    WALL_2_Distance_Camera_Object.SetActive(false);

                    WALL_4_Distance_Camera_Object.SetActive(false);
                    WALL_3_Distance_Camera_Object.SetActive(false);
                    WALL_6_Distance_Camera_Object.SetActive(false);
                    WALL_7_Distance_Camera_Object.SetActive(false);
                    WALL_8_Distance_Camera_Object.SetActive(false);

                    break;

                case wall_No_6:
                    WALL_6_Distance_Camera_Object.SetActive(true);
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_6_Distance_Camera;
                    WALL_1_Distance_Camera_Object.SetActive(false);
                    WALL_2_Distance_Camera_Object.SetActive(false);

                    WALL_4_Distance_Camera_Object.SetActive(false);
                    WALL_5_Distance_Camera_Object.SetActive(false);
                    WALL_3_Distance_Camera_Object.SetActive(false);
                    WALL_7_Distance_Camera_Object.SetActive(false);
                    WALL_8_Distance_Camera_Object.SetActive(false);

                    break;

                case wall_No_7:
                    WALL_7_Distance_Camera_Object.SetActive(true);
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_7_Distance_Camera;
                    WALL_1_Distance_Camera_Object.SetActive(false);
                    WALL_2_Distance_Camera_Object.SetActive(false);

                    WALL_4_Distance_Camera_Object.SetActive(false);
                    WALL_5_Distance_Camera_Object.SetActive(false);
                    WALL_6_Distance_Camera_Object.SetActive(false);
                    WALL_3_Distance_Camera_Object.SetActive(false);
                    WALL_8_Distance_Camera_Object.SetActive(false);

                    break;

                case wall_No_8:
                    WALL_8_Distance_Camera_Object.SetActive(true);
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_8_Distance_Camera;
                    WALL_1_Distance_Camera_Object.SetActive(false);
                    WALL_2_Distance_Camera_Object.SetActive(false);

                    WALL_4_Distance_Camera_Object.SetActive(false);
                    WALL_5_Distance_Camera_Object.SetActive(false);
                    WALL_6_Distance_Camera_Object.SetActive(false);
                    WALL_7_Distance_Camera_Object.SetActive(false);
                    WALL_3_Distance_Camera_Object.SetActive(false);

                    break;
            }
        }
        else if (Static_variable_Manager_Script.zoom_Scene_Flag_bool == true)
        {
            WALL_3_Distance_Camera_Object.SetActive(false);
            WALL_4_Distance_Camera_Object.SetActive(false);
            WALL_5_Distance_Camera_Object.SetActive(false);
            WALL_6_Distance_Camera_Object.SetActive(false);
            WALL_7_Distance_Camera_Object.SetActive(false);
            WALL_8_Distance_Camera_Object.SetActive(false);


            switch (Static_variable_Manager_Script.wall_Number_int)
            {
                case wall_No_1:

                    break;

                case wall_No_2:

                    break;

                case wall_No_3:

                    break;

                case wall_No_4:

                    WALL_4_Zoom_Camera_Object.SetActive(true);
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_4_Zoom_Camera;
                    break;

                case wall_No_5:
                    WALL_5_Zoom_Camera_Object.SetActive(true);
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_5_Zoom_Camera;
                    break;

                case wall_No_6:
                    WALL_6_Zoom_Camera_Object.SetActive(true);
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_6_Zoom_Camera;
                    break;

                case wall_No_7:
                    WALL_7_Zoom_Camera_Object.SetActive(true);
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_7_Zoom_Camera;
                    break;

                case wall_No_8:
                    WALL_8_Zoom_Camera_Object.SetActive(true);
                    Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_8_Zoom_Camera;
                    break;
            }
        }

    }
    //▲主体のカメラを、wallNoに合わせて指定する関数



    public void Camera_Canvas_UI_Zoom_6_Change_Function()
    {
        Canvas_UI_Object.GetComponent<Canvas>().enabled = false;
        Canvas_UI_Zoom_6_Object.GetComponent<Canvas>().enabled = true;
        Canvas_UI_Zoom_6_Object.GetComponent<Canvas>().worldCamera = WALL_6_Zoom_Camera;
    }

    public void Camera_Canvas_UI_Change_Function()
    {
        Canvas_UI_Object.GetComponent<Canvas>().enabled = true;
        Canvas_UI_Zoom_6_Object.GetComponent<Canvas>().enabled = false;
        Canvas_UI_Object.GetComponent<Canvas>().worldCamera = WALL_6_Zoom_Camera;
    }

    public void Camera_Last_Scene_Change_Function()
    {
        Last_Scene_Camera_Object.SetActive(true);
        Canvas_UI_Object.GetComponent<Canvas>().enabled = false;
        WALL_1_Distance_Camera_Object.SetActive(false);
        WALL_2_Distance_Camera_Object.SetActive(false);　
        WALL_3_Distance_Camera_Object.SetActive(false);
        WALL_4_Distance_Camera_Object.SetActive(false);
        WALL_5_Distance_Camera_Object.SetActive(false);
        WALL_6_Distance_Camera_Object.SetActive(false);
        WALL_7_Distance_Camera_Object.SetActive(false);
        WALL_8_Distance_Camera_Object.SetActive(false);

        WALL_4_Zoom_Camera_Object.SetActive(false);
        WALL_5_Zoom_Camera_Object.SetActive(false);
        WALL_6_Zoom_Camera_Object.SetActive(false);
        WALL_7_Zoom_Camera_Object.SetActive(false);
        WALL_8_Zoom_Camera_Object.SetActive(false);
    }

}
