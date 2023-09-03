using System;

[Serializable]
public class SessionIdBasedBooleanResult : SessionIdBasedResponse
{
    public bool result { get; set; }
    public string reason { get; set; }
}
