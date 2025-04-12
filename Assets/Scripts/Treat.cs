using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Treat : MonoBehaviour, IPointerClickHandler
{
    public Action OnTreatClicked;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnTreatClicked?.Invoke();
        Debug.Log("Treat clicked");
    }
}