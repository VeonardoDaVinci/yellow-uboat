using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonClicked : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public UnityEvent pointerDownEvent;
    public UnityEvent pointerUpEvent;
    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDownEvent?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerUpEvent?.Invoke();
    }
}
