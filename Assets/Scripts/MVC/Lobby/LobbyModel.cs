using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LobbyModel : IUserDTO , ISessionParticipants , ISessionID 
{
    public event Action OnEnterInLobbySession;
    public event Action OnExitFromCurrentLobbySession;
    public event Action OnJoinNewParticipant;
    public event Action<ExitLobbySessionInfo> OnExitParticipant;
    public event Action OnUpdatedLobbySessions;
    public event Action<SetPlayerHeroResult> OnChangeHero;
    public event Action<Dictionary<int, int>> OnUpdatedCastleOnSlot;
    public event Action<SetPlayerOrdinalResult> OnUpdatedPlayerOrdinal;
    public event Action<Dictionary<int,int>> OnUpdatedFeeParticipantsSlot;
    public event Action<string> OnGettedMessageInChat;

    public List<LobbySession> _lobbySessions = new List<LobbySession>();
    public LobbySession CurrentLobbySession { get; private set; }
    public UserDTO UserDTO { get; private set; }
    public IReadOnlyList<LobbySession> LobbySessions => _lobbySessions;
    public SessionParticipant SelfSessionParticipant { get; private set; }

    public IEnumerable<SessionParticipant> SessionParticipants => CurrentLobbySession.SessionParticipants;

    public string SessionID => CurrentLobbySession.Id;

    private IChatSender _chatSender;


    public void SetChatSender(IChatSender chatSender)
    {
        _chatSender = chatSender;
    }


    public void AddSession(LobbySession lobbySession)
    {
        if (lobbySession == null)
            throw new Exception("Lobby Session is null");

        _lobbySessions.Add(lobbySession);
    }

    public void PlayerRegistredDTO(UserDTO userDTO)
    {
        UserDTO = userDTO;
    }

    public void ExitFromCurrentLobbySesion()
    {
        if(CurrentLobbySession != null)
        {
            _lobbySessions.Remove(CurrentLobbySession);
            //if (CurrentLobbySession.Creator != UserDTO.UserName)
            //{
            //    _chatSender.SendMessageToChatRequest(CurrentLobbySession.Id, $"{UserDTO.UserName}: Left ");
            //}
            CurrentLobbySession = null;
            OnExitFromCurrentLobbySession?.Invoke();
        }
    }

    public void UpdatedFeeParticipantsSlot(Dictionary<int,int> participantsSlots)
    {
        OnUpdatedFeeParticipantsSlot?.Invoke(participantsSlots);
    }

    public void UpdateOrdinalPosition(SetPlayerOrdinalResult setPlayerOrdinalResult)
    {
        OnUpdatedPlayerOrdinal?.Invoke(setPlayerOrdinalResult);
    }

    public void UpdateCastleInparticipantSlot(Dictionary<int, int> participantCastles)
    {
        OnUpdatedCastleOnSlot?.Invoke(participantCastles);
    }

    public void UpdateLobbySessions(List<LobbySession> lobbySessions)
    {
        _lobbySessions = lobbySessions;
        OnUpdatedLobbySessions?.Invoke();
    }

    public void GettedMessageInChat(string message)
    {
        OnGettedMessageInChat?.Invoke(message);
    }

    public void EnterSession(LobbySession lobbySession)
    {
        SelfSessionParticipant = lobbySession.SessionParticipants.FirstOrDefault(item => item.UserInfo.UserName == UserDTO.UserName);
        CurrentLobbySession = lobbySession;
        _chatSender.SendMessageToChatRequest(CurrentLobbySession.Id, $"{UserDTO.UserName}: Joined ");
        OnEnterInLobbySession?.Invoke();
    }

    public void EnterSession(string sessionID , SessionParticipant sessionParticipant)
    {
        if(TryGetLobbySessionBySessionID(sessionID, out LobbySession lobbySession))
        {
            lobbySession.AddParticipant(sessionParticipant);
            CurrentLobbySession = lobbySession;
            _chatSender.SendMessageToChatRequest(CurrentLobbySession.Id, $"{sessionParticipant.UserInfo.UserName}: Joined ");
            OnEnterInLobbySession?.Invoke();
        }
    }

    public void ExitPariticipantFromLobbySession(ExitLobbySessionInfo exitLobbySessionInfo)
    {
        SessionParticipant sessionParticipant = SessionParticipants.SingleOrDefault(item => item.Ordinal == exitLobbySessionInfo.participantOrdinal);
        if(sessionParticipant != null)
            _chatSender.SendMessageToChatRequest(exitLobbySessionInfo.sessionId, $"{sessionParticipant.UserInfo.UserName}: Left ");
        OnExitParticipant?.Invoke(exitLobbySessionInfo);
    }

    public void AddNewParticipantInCurrentLobbySession(SessionParticipant sessionParticipant)
    {
        CurrentLobbySession.AddParticipant(sessionParticipant);
        
        OnJoinNewParticipant?.Invoke();
    }

    public bool TryGetLobbySessionBySessionID(string sessionID , out LobbySession lobbySession)
    {
        var session = _lobbySessions.FirstOrDefault(item => item.Id == sessionID);
        if(session != null)
        {
            lobbySession = session;
            return true;
        }
        lobbySession = null;
        return false;
    }

    public void ChangeHeroesOnParicipantOnSession(SetPlayerHeroResult setPlayerHeroResult)
    {
        OnChangeHero?.Invoke(setPlayerHeroResult);
    }

}
