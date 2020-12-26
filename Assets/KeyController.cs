using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InspectorVariables;
using System.Linq;
using UnityEngine.Events;
using NaughtyAttributes;
using System;

public class KeyController : MonoBehaviour
{
    [SerializeField]
    ScriptableFloatVariable[] TeethSizes;
    [SerializeField]
    CorrectToothSize[] answerKey;
    [SerializeField]
    GameObject toothPreab;
    [SerializeField]
    Transform container;
    [SerializeField]
    ScriptableFloatVariable Min, Max;

    internal float getCorrectCount()
    {
        return answerKey.Where(k => k.isCorrect()).Count();
    }

    [SerializeField]
    UnityEvent onValid;
    [SerializeField]
    UnityEvent onInvalid;
    private void Awake()
    {
        answerKey = TeethSizes.Select(t => new CorrectToothSize(
            Mathf.Floor(UnityEngine.Random.Range(Min.Value, Max.Value)),
            t,
            Instantiate(toothPreab, container))).ToArray();
        var i = 0;
        foreach (var key in answerKey)
        {
            key.tooth.transform.localPosition += Vector3.right * i++;
            var fevent = key.tooth.GetComponent<FloatVariableEvent>();
                fevent.setValueReff(key.currentSize);
            fevent.ValueOnChange.AddListener(f => { if (isValidKey()) onValid.Invoke(); else onInvalid.Invoke(); });
        }
    }
    public void scramble()
    {
        foreach (var a in answerKey)
        {
            a.size = Mathf.Floor(UnityEngine.Random.Range(Min.Value, Max.Value));
        }
    }
    public bool isValidKey()
    {
        return answerKey.All(t => t.isCorrect());
    }
}
[System.Serializable]
public class CorrectToothSize
{
    public float size;
    public ScriptableFloatVariable currentSize;
    public GameObject tooth;

    public CorrectToothSize(float size, ScriptableFloatVariable currentSize, GameObject tooth)
    {
        this.size = size;
        this.currentSize = currentSize;
        this.tooth = tooth;
    }

    public bool isCorrect() => size == currentSize.Value;
}
