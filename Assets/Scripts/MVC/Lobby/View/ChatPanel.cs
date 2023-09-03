using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChatPanel : MonoBehaviour
{
    [SerializeField] private TMP_InputField _chatInputField;
    [SerializeField] private Button _buttonSend;
    private List<ChatMessageItem> _chatMessageItems = new List<ChatMessageItem>();
    private Transform _chatMessagesParent;
    private ChatMessageItem _chatMessageItemPrefab;
    private IChatSender _chatSender;
    private string _userName;
    private string _sessionID;

    private void Awake()
    {
        _buttonSend.onClick.AddListener(SendMessageToChat);
        _chatInputField.onSubmit.AddListener(SendMessageToChat);
    }

    public void Init(IChatSender chatSender, Transform parent, ChatMessageItem chatMessageItemPrefab)
    {
        _chatSender = chatSender;
        _chatMessagesParent = parent;
        _chatMessageItemPrefab = chatMessageItemPrefab;
    }

    public void InitChatData(string userName, string sessionID)
    {
        _userName = userName;
        _sessionID = sessionID;
    }

    public void SendMessageToChat(string message)
    {
        SendMessageToChat();
    }

    public void SendMessageToChat()
    {
        string message = $"{_userName}: {_chatInputField.text}";
        _chatSender.SendMessageToChatRequest(_sessionID, message);
        _chatInputField.text = "";
    }

    public void DispalyMessage(string message)
    {
        var messageItem = Instantiate(_chatMessageItemPrefab, _chatMessagesParent);
        messageItem.SetText(message);
        _chatMessageItems.Add(messageItem);
    }

    public void DisplayErrorMessageToChat(string message)
    {
        var messageItem = Instantiate(_chatMessageItemPrefab, _chatMessagesParent);
        messageItem.SetText(message,Color.red);
        _chatMessageItems.Add(messageItem);
    }

    public void ClearChat()
    {
        foreach (var item in _chatMessageItems)
            Destroy(item.gameObject);
        _chatMessageItems.Clear();
    }

}