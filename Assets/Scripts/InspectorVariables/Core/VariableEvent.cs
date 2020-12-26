using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class VariableEvent<T> : MonoBehaviour
{

    [SerializeField]
    protected ScriptableVariable<T> value;
    public void setValueReff(ScriptableVariable<T> value)
    {
        this.value = value;
        register();
    }

    public T Value { get => value.Value; set => this.value.Value = value; }

    [SerializeField]
    public UnityEvent<T> ValueOnChange;
    [SerializeField]
    public UnityEvent<ScriptableVariable<T>> RefOnChange;
    Action unregister;
    private void handler(T s)
    {
        ValueOnChange.Invoke(s);
        RefOnChange.Invoke(value);
    }
    public void register()
    {
        this.unregister?.Invoke();
        var lastvalue = value;
        if (value != null)
        {
            value.onChange.AddListener(handler);
            LocalTrigger();
        }
        Action unregister = () =>
        {
            if (lastvalue != null)
                lastvalue.onChange.RemoveListener(handler);
        };
        this.unregister = unregister;
    }
    private void Awake() => register();
    private void OnValidate() => register();
    private void OnDestroy()
    {
        unregister();
    }

    public void Trigger()
    {
        value.trigger();
    }
    public void LocalTrigger()
    {
        ValueOnChange.Invoke(value.Value);
        RefOnChange.Invoke(value);
    }
}
