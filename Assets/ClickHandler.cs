using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour
{
    [SerializeField]
    UnityEvent onClick;
    private void OnMouseUpAsButton()
    {
        onClick.Invoke();
    }
}
