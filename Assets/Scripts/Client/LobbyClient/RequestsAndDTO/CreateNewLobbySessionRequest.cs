using System;

[Serializable]
public class CreateNewLobbySessionRequest
{
    public string name;
    public int maxPlayerCount, mapTemplateId, mapSizeId, initTotalTime, turnTime, totalParticipantAmount;
    public bool isAIAllowed;

    public CreateNewLobbySessionRequest(string name, int maxPlayerCount, int mapTemplateId, int mapSizeId, int initTotalTime, int turnTime, bool isAIAllowed)
    {
        this.name = name;
        this.maxPlayerCount = maxPlayerCount;
        this.mapTemplateId = mapTemplateId;
        this.mapSizeId = mapSizeId;
        this.isAIAllowed = isAIAllowed;
        this.totalParticipantAmount = maxPlayerCount;
        if (isAIAllowed && maxPlayerCount == 1)
        {
            this.totalParticipantAmount = 2;
        }
        if (isAIAllowed)
        {
            this.maxPlayerCount = 1;
        }
        this.initTotalTime = initTotalTime;
        this.turnTime = turnTime;
    }
}
