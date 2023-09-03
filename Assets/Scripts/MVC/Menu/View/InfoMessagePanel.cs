using System.Collections;
using UnityEngine;
using TMPro;

public class InfoMessagePanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _messageText;
    private bool _isOpen;

    public void DisplayMessage(string message)
    {
        _isOpen = true;
        _panel.gameObject.SetActive(true);
        _messageText.text = message;
    }

    public void Close()
    {
        _isOpen = false;
        _panel.gameObject.SetActive(false);
    }


    //private void Update()
    //{
    //    if (!_isOpen) return;
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        Close();
    //    }
    //}
}