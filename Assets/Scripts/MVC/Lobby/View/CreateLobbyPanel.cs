using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateLobbyPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_InputField _nameTextField;
    [SerializeField] private TMP_Dropdown _playerCountDropdown;
    [SerializeField] private TMP_Dropdown _templateIdDropdown;
    [SerializeField] private TMP_Dropdown _sizeIdDropdown;
    [SerializeField] private TMP_InputField _initTotalTimeDropdown;
    [SerializeField] private TMP_InputField _turnTimeDropdown;
    [SerializeField] private Toggle _allowBotToggle;

    private Dictionary<string, int> _sizePerID = new Dictionary<string, int>()
    {
        ["XS"] = 1,
        ["S"] = 2,
        ["M"] = 3,
        ["L"] = 4,
    };

    public string _lobbyName;
    public int _playerCount;
    public int _templateId;
    public int _sizeId;
    public int _initTotalTime;
    public int _turnTime;

    private void Awake()
    {
        _lobbyName = LobbyName;
        _playerCount = PlayerCount;
        _templateId = TemplateId;
        _sizeId = SizeId;
        _initTotalTime = InitTotalTime;
        _turnTime = TurnTime;
    }

    public void OnEnable()
    {
        _templateIdDropdown.onValueChanged.AddListener(ChangeTemplateHandle);
        _sizeIdDropdown.options.Clear();
        _sizeIdDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "XS" });
        _sizeIdDropdown.value = 0;
        _sizeIdDropdown.RefreshShownValue();
        _playerCountDropdown.onValueChanged.AddListener(AllowBotHandler);
        _allowBotToggle.isOn = false;
        _allowBotToggle.interactable = false;
    }

    private void AllowBotHandler(int value)
    {
        if(value == 0)
        {
            _allowBotToggle.isOn = false;
            _allowBotToggle.interactable = false;
        }
        else
        {
            _allowBotToggle.isOn = true;
            _allowBotToggle.interactable = true;
        }
    }

    private void ChangeTemplateHandle(int currentOption)
    {
        if (_templateIdDropdown.options[currentOption].text == "1")
        {
            _sizeIdDropdown.options.Clear();
            _sizeIdDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "XS" });
            _sizeIdDropdown.value = 0;
            _sizeIdDropdown.RefreshShownValue();
        }
        else if (_templateIdDropdown.options[currentOption].text == "2")
        {
            _sizeIdDropdown.options.Clear();
            _sizeIdDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "S" });
            _sizeIdDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "M" });
            _sizeIdDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "L" });
            _sizeIdDropdown.value = 0;
            _sizeIdDropdown.RefreshShownValue();
        }
    }

    #region Filed Settings
    public string LobbyName
    {
        get
        {
            if (_nameTextField.text.Length > 0)
                return _nameTextField.text;
            return "";
        }
    }
    public int PlayerCount
    {
        get
        {
            if (int.TryParse(_playerCountDropdown.options[_playerCountDropdown.value].text, out int result))
            {
                return result;
            }
            return 1;
        }
    }
    public int TemplateId
    {
        get
        {
            if (int.TryParse(_templateIdDropdown.options[_templateIdDropdown.value].text, out int result))
            {
                return result;
            }
            return 1;
        }
    }
    public int SizeId
    {
        get
        {
            if (_sizePerID.TryGetValue(_sizeIdDropdown.options[_sizeIdDropdown.value].text, out int result))
            {
                return result;
            }
            return 1;
        }
    }
    public int InitTotalTime
    {
        get
        {
            if (int.TryParse(_initTotalTimeDropdown.text, out int result))
            {
                return result;
            }
            return 60;
        }
    }
    public int TurnTime
    {
        get
        {
            if (int.TryParse(_turnTimeDropdown.text, out int result))
            {
                return result;
            }
            return 60;
        }
    }
    #endregion


    public void ResetLobbyPanel()
    {
        _playerCountDropdown.value = 0;
        _templateIdDropdown.value = 0;
        _sizeIdDropdown.value = 0;
        _nameTextField.text = _lobbyName;
        _initTotalTimeDropdown.text = _initTotalTime.ToString();
        _turnTimeDropdown.text = _turnTime.ToString();
        _allowBotToggle.isOn = false;
    }

    public LobbyCreateSettings GetLobbyCreateSettings()
    {
        LobbyCreateSettings lobbyCreateSettings = new LobbyCreateSettings(_allowBotToggle.isOn, LobbyName, PlayerCount, TemplateId, SizeId, InitTotalTime, TurnTime);

        return lobbyCreateSettings;
    }

    public void OpenPanel()
    {
        ResetLobbyPanel();
        _panel.SetActive(true);
    }

    public void ClosePanel()
    {
        ResetLobbyPanel();
        _panel.SetActive(false);        
    }
}