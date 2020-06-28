using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Btn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        GameManager.instanse.SpeedGame /= 2;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        GameManager.instanse.SpeedGame *= 2;
    }
    
}
