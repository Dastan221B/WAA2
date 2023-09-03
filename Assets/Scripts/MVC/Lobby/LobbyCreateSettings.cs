using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct LobbyCreateSettings
{
    public string LobbyName { get; private set; }
    public int PlayerCount { get; private set; }
    public int TemplateId { get; private set; }
    public int SizeId { get; private set; }

    public int InitTotalTime { get; private set; }
    public int TurnTime { get; private set; }
    public bool IsAllowBot { get; private set; }

    public LobbyCreateSettings(bool isAllowBot,string lobbyName , int playerCount , int templateId , int sizeID , int initTotalTime, int turnTime)
    {
        IsAllowBot = isAllowBot;
        LobbyName = lobbyName;
        PlayerCount = playerCount;
        TemplateId = templateId;
        SizeId = sizeID;
        InitTotalTime = initTotalTime;
        TurnTime = turnTime;
    }
}