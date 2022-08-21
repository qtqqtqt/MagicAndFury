using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public bool pause;

    public bool IsPressed
    {
        get;
        private set;
    }

    

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!GameManagement.instance.isPaused)
             IsPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsPressed = false;
    }
}