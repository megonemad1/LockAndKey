using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InspectorVariables;

public class ToothControler : MonoBehaviour
{
    public void setSize(float height)
    {
        height /= 2;
        transform.localScale = Vector3.one + Vector3.forward * (height);
        var p = transform.localPosition;
        transform.localPosition = new Vector3(p.x,p.y,(height)/2) ;
    }

}