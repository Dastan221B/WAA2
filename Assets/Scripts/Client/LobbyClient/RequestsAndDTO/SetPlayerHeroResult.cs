using System;

[Serializable]
public class SetPlayerHeroResult : SessionIdBasedBooleanResult
{
    public int participantOrdinal { get; set; }
    public int heroId { get; set; }
}
