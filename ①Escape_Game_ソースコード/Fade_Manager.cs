using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade_Manager : MonoBehaviour
{

    //▼フェードイン・アウトの役割の「Fade_Black_Renderer_Image」と「Circle_Sprite_Mask」をGameObject型に入れ込み、表示のON、OFFの役割を担わせる箇所。
    /*

    public GameObject Fade_Black_Renderer_Image_Object;
    public GameObject Circle_Sprite_Mask_Object;

    Fade_Black_Renderer_Image_Script fade_Black_Renderer_Image_Script;
    */
    //▲フェードイン・アウトの役割の「Fade_Black_Renderer_Image」と「Circle_Sprite_Mask」をGameObject型に入れ込み、表示のON、OFFの役割を担わせる箇所。

    // Start is called before the first frame update
    void Start()
    {
        /*
        fade_Black_Renderer_Image_Script = Fade_Black_Renderer_Image_Object.GetComponent<Fade_Black_Renderer_Image_Script>(); //　フェードイン・アウトのアニメーターの読み込み。
        Circle_Sprite_Mask_Object.SetActive(false); // フェードアウトの後の、手紙を注視させるCircle型Maskを最初はOFFにし、最初のスタートのイベントの時にSetActive(true)とする。
        Fade_Black_Renderer_Image_Object.SetActive(true); // 作業時は黒画面をOFFにする為、起動時に真っ黒の画面から開始する。
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    //▼フェードイン・フェードアウトを行うアニメーターを管理する関数
    public void Fade_Animator_Function()
    {
        //▼Play時、なぜか足音のSEがなる不具合対策の為、AudioListenerをstartでミュートにしている状態から解除する為の記述箇所
        AudioListener.volume = 1f;
        //▲Play時、なぜか足音のSEがなる不具合対策の為、AudioListenerをstartでミュートにしている状態から解除する為の記述箇所

        AnimatorStateInfo fade_AnimatorStateInfo = fade_Black_Renderer_Image_Script.Fade_Black_Renderer_Image_Script_Animator.GetCurrentAnimatorStateInfo(0);

        if (fade_AnimatorStateInfo.IsName("Idol"))
        {
            fade_Black_Renderer_Image_Script.Fade_Black_Renderer_Image_Script_Animator.SetTrigger("Fade_Out");

        }
        else if (fade_AnimatorStateInfo.IsName("Fade_Out"))
        {
            fade_Black_Renderer_Image_Script.Fade_Black_Renderer_Image_Script_Animator.SetTrigger("Fade_Gaze_Circle_IN");
        }
        else if (fade_AnimatorStateInfo.IsName("Fade_Gaze_Circle_IN"))
        {
            fade_Black_Renderer_Image_Script.Fade_Black_Renderer_Image_Script_Animator.SetTrigger("Fade_Gaze_Circle_Out");
        }
    }
    //▲フェードイン・フェードアウトを行うアニメーターを管理する関数
    */
}
