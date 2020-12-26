using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class AttemptOutput : MonoBehaviour
{
    [SerializeField]
    KeyController KeyController;
    [SerializeField]
    TextMeshPro output;
    [SerializeField]
    UnityEngine.Events.UnityEvent onReset;
    [SerializeField]
    int max_attempts = 10;
    [SerializeField]
    int current_attempts = 0;
    // Start is called before the first frame update
    void Awake()
    {
        output = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    public void UpdateDisplay()
    {
        if (!output)
            output = GetComponent<TextMeshPro>();
        
        float CorrectCount = KeyController.getCorrectCount();
        current_attempts++;
        if (current_attempts >= max_attempts)
        {
            current_attempts = 0;
            KeyController.scramble();
            onReset.Invoke();
            output.text = $"Key Reset";
        }
        else
            output.text = $"Correct: {CorrectCount}";
    }
}
