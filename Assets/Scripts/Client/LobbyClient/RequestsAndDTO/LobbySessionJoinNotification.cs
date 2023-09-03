using System;

[Serializable]
public class LobbySessionJoinNotification : SessionIdBasedResponse
{
    public string userName { get; set; }
    public string email { get; set; }
    public int ordinal { get; set; }
    public int userId { get; set; }
}
