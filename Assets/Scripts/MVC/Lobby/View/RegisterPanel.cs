using UnityEngine;
using TMPro;

public class RegisterPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_InputField _email;
    [SerializeField] private TMP_InputField _password;
    private ClientRegister _clientRegister;

    public void Init(ClientRegister clientRegister)
    {
        _clientRegister = clientRegister;
    }

    public void Register()
    {
        _clientRegister.Register(_email.text, _password.text);
    }

    public void OpenPanel()
    {
        _panel.SetActive(true);
    }

    public void ClosePanel()
    {
        _panel.SetActive(false);
    }

}