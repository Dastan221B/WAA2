using System;

[Serializable]
public class GetCastleResult : TurnSecondsBasedBooleanInfo
{
    public CastleObjectFullInfo castleDTO { get; set; }
}
