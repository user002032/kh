using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Quit_Mouse_over_Script : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image Quit_Close_Button_Image { get { return GetComponent<Image>(); } }
    Color change_Color = new Color();

    // Start is called before the first frame update
    void Start()
    {
        change_Color.a = 0.2f;
        change_Color.g = 255f;
        change_Color.b = 255f;
        change_Color.r = 255f;

        Quit_Close_Button_Image.color = change_Color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        change_Color.a = 1.0f;
        change_Color.g = 255f;
        change_Color.b = 255f;
        change_Color.r = 255f;
        Quit_Close_Button_Image.color = change_Color;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        change_Color.a = 0.2f;
        change_Color.g = 255f;
        change_Color.b = 255f;
        change_Color.r = 255f;
        Quit_Close_Button_Image.color = change_Color;
    }
}
