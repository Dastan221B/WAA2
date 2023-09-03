using System;

[Serializable]
public class NewLobbySessionChatMessageInfo : SessionIdBasedResponse
{
    public int senderOrdinal { get; set; }
    public string message { get; set; }
}
