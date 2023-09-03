using System;

[Serializable]
public class PickResourceInfo : SessionIdBasedBooleanResult
{
    public string resourceId { get; set; }
    public int turnSeconds { get; set; }
}
