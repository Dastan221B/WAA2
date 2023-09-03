using TMPro;
using UnityEngine;

public class ChatMessageItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _message;

    public void SetText(string message)
    {
        _message.text = message;
        _message.color = Color.white;
    }

    public void SetText(string message, Color color)
    {
        _message.text = message;
        _message.color = color;
    }
}