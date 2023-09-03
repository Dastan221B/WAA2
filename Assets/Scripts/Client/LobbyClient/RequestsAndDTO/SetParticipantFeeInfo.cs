using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class SetParticipantFeeInfo : SessionIdBasedBooleanResult
{
    public Dictionary<int, int> participantFeeMap { get; set; }
}
