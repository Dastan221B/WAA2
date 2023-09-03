using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerCastleResult : SessionIdBasedBooleanResult
{
    public Dictionary<int, int> participantCastles { get; set; }
}

