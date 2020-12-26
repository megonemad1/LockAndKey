using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InspectorVariables;
using TMPro;

public class NumInput : MonoBehaviour
{
    [SerializeField]
    TextMeshPro t;
    [SerializeField]
    ScriptableFloatVariable min, max, currentValue;
    public void inc()
    {
        if (currentValue.Value <= max.Value)
            currentValue.Value++;
        show();
    }
    public void dec()
    {
        if (currentValue.Value > min.Value)
            currentValue.Value--;
        show();
    }
    void show()
    {
        t.text = currentValue.Value.ToString();
    }
    // Start is called before the first frame update
    void Awake()
    {
        show();
    }

}
