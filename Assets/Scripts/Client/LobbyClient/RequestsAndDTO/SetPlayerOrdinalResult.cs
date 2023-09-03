using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SetPlayerOrdinalResult : SessionIdBasedBooleanResult
{
    public int oldValue { get; set; }
    public int newValue { get; set; }
}
