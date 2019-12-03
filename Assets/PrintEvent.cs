using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class PrintEvent : MonoBehaviour, 
    IMoveHandler,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    private void OnMouseOver()
    {
        Debug.Log("mouse over " + Input.mousePosition);
    }

    public void OnMove(AxisEventData eventData)
    {
        Debug.Log("move");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("pointer click @" + eventData.position);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("pointer enter @" + eventData.position);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("pointer exit @" + eventData.position);
    }
}
