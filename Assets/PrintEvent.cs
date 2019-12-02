using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PrintEvent : MonoBehaviour
{
    private void OnMouseOver()
    {
        Debug.Log("mouse over " + Input.mousePosition);
    }
}
