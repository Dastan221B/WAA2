using System;

[Serializable]
public class TurnSecondsBasedBooleanInfo : SessionIdBasedBooleanResult
{
    public int turnSeconds { get; set; }
}
