using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputFieldUtilites : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputField;


    private void Awake()
    {
        _inputField.onValueChanged.AddListener(OnValueChange);
    }

    private void OnValueChange(string value)
    {
        if(int.TryParse(value, out int result))
        {
            if (result < 0)
                _inputField.text = "0";
        }
    }

}
