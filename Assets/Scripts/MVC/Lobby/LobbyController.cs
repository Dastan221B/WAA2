using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;


public class LobbyController : ITryConnectToLobbySession , IParticipantPanelDependenciesContainer,
    IBroadCastChangedHeroIcon , IBroadcastChangedPlayerCastleRequest , IBroadcastChangeOrdinal, IBroadcastUpdatedFee, IChatSender , IStarGame
{
    private LobbyModel _lobbyModel;
    private ChatPanel _chatPanel;
    private LobbyMessageSender _lobbyMessageSender;
    private CreateNewLobbyProcess _createNewLobbyProcess;
    private AddJoiningParicipant _addJoiningParicipant;
    private ExitFromLobbySessionParticipant _exitFromLobbySessionParticipant;
    private UpdatedLobbySessions _updatedLobbySessions;
    private EnterInLobbySession _enterInLobbySession;
    private GetUpdatedHeroIcon _getUpdatedHeroIcon;
    private ExitFromLobbySession _exitFromLobbySession;
    private GetUpdatePlayerCastle _getUpdatePlayerCastle;
    private GetUpdatedParticipantSlotOrdinal _getUpdatedParticipantSlotOrdinal;
    private GetUpdatedFeeDictionary _getUpdatedFeeDictionary;
    private InfoMessagePanel _infoMessagePanel;
    private ParticipantSlotsPanel _participantSlotsPanel;
    private Heroes _heroes;
    private bool _isCanConnect = true;

    public IBroadCastChangedHeroIcon BroadCastChangedHeroIcon => this;

    public IBroadcastChangedPlayerCastleRequest BroadcastChangedPlayerCastleRequest => this;

    public IBroadcastChangeOrdinal BroadcastChangeOrdinal => this;

    public IBroadcastUpdatedFee BroadcastUpdatedFee => this;

    public LobbyController(Heroes heroes,ParticipantSlotsPanel participantSlotsPanel,LobbyModel lobbyModel , LobbyMessageSender lobbyMessageSender , InfoMessagePanel infoMessagePanel, ChatPanel chatPanel)
    {
        _heroes = heroes;
        _participantSlotsPanel = participantSlotsPanel;
        _chatPanel = chatPanel;
        _lobbyModel = lobbyModel;
        _lobbyMessageSender = lobbyMessageSender;
        _createNewLobbyProcess = new CreateNewLobbyProcess();
        _addJoiningParicipant = new AddJoiningParicipant();
        _exitFromLobbySessionParticipant = new ExitFromLobbySessionParticipant();
        _updatedLobbySessions = new UpdatedLobbySessions();
        _enterInLobbySession = new EnterInLobbySession(_lobbyModel);
        _getUpdatedHeroIcon = new GetUpdatedHeroIcon();
        _exitFromLobbySession = new ExitFromLobbySession();
        _getUpdatePlayerCastle = new GetUpdatePlayerCastle();
        _getUpdatedParticipantSlotOrdinal = new GetUpdatedParticipantSlotOrdinal();
        _getUpdatedFeeDictionary = new GetUpdatedFeeDictionary();
        _infoMessagePanel = infoMessagePanel;
        _lobbyModel.SetChatSender(this);
    }

    public void ChangeParticipantHero(SetPlayerHeroResult setPlayerHeroResult)
    {
        ParticipantSlot participantSlot = _participantSlotsPanel.ParticipantSlots.FirstOrDefault(item => item.SessionParticipant.Ordinal == setPlayerHeroResult.participantOrdinal);
        if(participantSlot != null)
        {
            participantSlot.SetHero(_heroes.GetHeroByIDOrNull(setPlayerHeroResult.heroId));
        }
    }

    public bool TryCreateNewLobbySessionRequest(LobbyCreateSettings lobbyCreateSettings)
    {
        string name = lobbyCreateSettings.LobbyName;
        if(name.Length == 0)
        {
            _infoMessagePanel.DisplayMessage("Enter room name");
            return false;
        }
        if (!CheckMessageHaveLatinSymbols(name))
        {
            _infoMessagePanel.DisplayMessage("Lobby name must not contain Cyrillic characters");
            return false;
        }
        int participants = lobbyCreateSettings.PlayerCount;
        int templateId = lobbyCreateSettings.TemplateId;
        int sizeId = lobbyCreateSettings.SizeId;
        int initTotalTime = lobbyCreateSettings.InitTotalTime;
        int turnTime = lobbyCreateSettings.TurnTime;
        CreateNewLobbySessionRequest request = new CreateNewLobbySessionRequest(name, participants,templateId, sizeId , initTotalTime, turnTime, lobbyCreateSettings.IsAllowBot);
        _lobbyMessageSender.MessageSender(OutputLobbyHeaders.CREATE_NEW_LOBBY_SESSION_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        return true;
    }

    public void ExitFromLobbySession()
    {
        ExitLobbySessionRequest request = new ExitLobbySessionRequest(_lobbyModel.CurrentLobbySession.Id);
        _lobbyMessageSender.MessageSender(OutputLobbyHeaders.EXIT_LOBBY_SESSION_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
    }
    public void GetLobbySessionListRequest()
    {
        EmptyObject request = new EmptyObject();
        _lobbyMessageSender.MessageSender(OutputLobbyHeaders.GET_LOBBY_SESSION_LIST_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
    }

    public void TryConnectBySession(LobbySession lobbySession)
    {
        if (!_isCanConnect)
            return;
        _isCanConnect = false;
        EnterLobbySessionRequest request = new EnterLobbySessionRequest(lobbySession.Id);
        _lobbyMessageSender.MessageSender(OutputLobbyHeaders.ENTER_LOBBY_SESSION_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
    }

    public void BroadcastChangedHeroIcon(int ordinal, string sessionID , int heroID)
    {
        SetPlayerHeroRequest request = new SetPlayerHeroRequest(sessionID, heroID, ordinal);
        _lobbyMessageSender.MessageSender(OutputLobbyHeaders.SET_PLAYER_HERO_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
    }

    public void BroadcastChangePlayerCastleRequest(string sessionId, int castleId, int ordinal)
    {
        Debug.Log("BroadCasted");
        SetPlayerCastleRequest request = new SetPlayerCastleRequest(sessionId, castleId, ordinal);
        _lobbyMessageSender.MessageSender(OutputLobbyHeaders.SET_PLAYER_CASTLE_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
    }

    public void BroadcastChangeParticipantOrdinalRequest(int currentOrdinal, int newOrdinal, string sessionId)
    {
        SetPlayerOrdinalRequest request = new SetPlayerOrdinalRequest(sessionId, currentOrdinal, newOrdinal);
        _lobbyMessageSender.MessageSender(OutputLobbyHeaders.SET_PLAYER_ORDINAL_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
    }

    public void BroadcastUpdatedFeeRequest(Dictionary<int, int> participantFeeMap, string sessionID)
    {
        SetParticipantFeeRequest request = new SetParticipantFeeRequest(sessionID, participantFeeMap);
        _lobbyMessageSender.MessageSender(OutputLobbyHeaders.SET_PARTICIPANT_FEE_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
    }

    public void SendMessageToChatRequest(string sessionId , string chatMessage)
    {
        if (!CheckMessageHaveLatinSymbols(chatMessage))
        {
            _chatPanel.DisplayErrorMessageToChat("Сообщение содержит символы кирилицы");
            return;
        }

        SendMessageToLobbySessionChatRequest request = new SendMessageToLobbySessionChatRequest(sessionId, chatMessage);
        _lobbyMessageSender.MessageSender(OutputLobbyHeaders.SEND_MESSAGE_TO_LOBBY_SESSION_CHAT_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
    }

    public void TryStartGemeRequest(string sessionId)
    {
        StartGameRequest request = new StartGameRequest(sessionId);
        _lobbyMessageSender.MessageSender(OutputLobbyHeaders.START_GAME_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
    }

    private bool CheckMessageHaveLatinSymbols(string message)
    {
        string pattern = @"\p{IsCyrillic}";
        if (Regex.Matches(message, pattern).Count > 0)
        {
            return false;
        }
        return true;
    }

    public void ProcessResponseFromServer(MessageInput messageInput)
    {
        Debug.Log((InputLobbyHeaders)messageInput.header);
        switch ((InputLobbyHeaders)messageInput.header)
        {
            case InputLobbyHeaders.CREATE_NEW_LOBBY_SESSION_RESULT:
                if (_createNewLobbyProcess.TryCreateNewLobbySession(messageInput, out LobbySession lobbySession))
                {
                    _lobbyModel.AddSession(lobbySession);
                    _lobbyModel.EnterSession(lobbySession);
                }
                else
                    _infoMessagePanel.DisplayMessage("Something went wrong :(");
                break;
            case InputLobbyHeaders.UPDATE_LOBBY_SESSION_LIST_INFO:
                var list = _updatedLobbySessions.GetLobbySessions(messageInput);
                _lobbyModel.UpdateLobbySessions(list);
                break;
            case InputLobbyHeaders.LOBBY_SESSION_JOIN_INFO:
                //if(_lobbyModel.SelfSessionParticipant != null)
                //{
                //    BroadcastChangedHeroIcon(_lobbyModel.SelfSessionParticipant.Ordinal, _lobbyModel.CurrentLobbySession.Id, _lobbyModel.SelfSessionParticipant.DicHeroId);
                //}
                //if (_lobbyModel.SelfSessionParticipant != null)
                //{
                //    BroadcastChangedPlayerCastleRequest.BroadcastChangePlayerCastleRequest(_lobbyModel.CurrentLobbySession.Id,
                //            _lobbyModel.SelfSessionParticipant.DicCastleId,
                //            _lobbyModel.SelfSessionParticipant.Ordinal);
                //}
                _lobbyModel.AddNewParticipantInCurrentLobbySession(_addJoiningParicipant.GetNewParticipant(messageInput));
                break;
            case InputLobbyHeaders.ENTER_LOBBY_SESSION_RESULT:
                if (_enterInLobbySession.TryGetEnteredLobbySession(messageInput, out SessionParticipant sessionParticipant, out EnterLobbySessionResult enterLobbySessionResult))
                    _lobbyModel.EnterSession(enterLobbySessionResult.sessionId, sessionParticipant);
                else          
                    _infoMessagePanel.DisplayMessage("Something went wrong :(");
                _isCanConnect = true;
                break;
            case InputLobbyHeaders.CLOSE_LOBBY_SESSION_INFO:
                    _lobbyModel.ExitFromCurrentLobbySesion();
                break;
            case InputLobbyHeaders.SEND_MESSAGE_TO_LOBBY_SESSION_CHAT_RESULT:
                SendMessageToLobbyChatResult sendMessageResult = Newtonsoft.Json.JsonConvert.DeserializeObject<SendMessageToLobbyChatResult>(messageInput.body);
                if (!sendMessageResult.result)
                    _infoMessagePanel.DisplayMessage(sendMessageResult.reason);

                break;
            case InputLobbyHeaders.NEW_LOBBY_SESSION_CHAT_MESSAGE_INFO:
                NewLobbySessionChatMessageInfo newLobbySessionChatMessageInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<NewLobbySessionChatMessageInfo>(messageInput.body);
                _lobbyModel.GettedMessageInChat(newLobbySessionChatMessageInfo.message);
                break;
            case InputLobbyHeaders.VALIDATE_LOBBY_SESSION_RESULT:
                ValidateLobbySessionResult validateLobbySessionResult = Newtonsoft.Json.JsonConvert.DeserializeObject<ValidateLobbySessionResult>(messageInput.body);
                if (!validateLobbySessionResult.result)
                    _infoMessagePanel.DisplayMessage(validateLobbySessionResult.reason);
                break;
            case InputLobbyHeaders.EXIT_LOBBY_SESSION_INFO:
                ExitLobbySessionInfo exitLobbySessionInfo = _exitFromLobbySessionParticipant.GetExitLobbySessionInfo(messageInput);
                _lobbyModel.ExitPariticipantFromLobbySession(exitLobbySessionInfo);
                break;
            case InputLobbyHeaders.EXIT_LOBBY_SESSION_RESULT:
                if (_exitFromLobbySession.TryExitFromLobbySession(messageInput))
                {
                    _lobbyModel.ExitFromCurrentLobbySesion();
                    GetLobbySessionListRequest();
                }

                else
                    _infoMessagePanel.DisplayMessage("Something went wrong :(");
                break;
            case InputLobbyHeaders.TOSS_A_COIN_INFO:
                break;
            case InputLobbyHeaders.SET_PLAYER_CASTLE_INFO:
                if (_getUpdatePlayerCastle.TryGetUpdatedPlayerCastle(messageInput, out SetPlayerCastleResult setPlayerCastleResult))
                    _lobbyModel.UpdateCastleInparticipantSlot(setPlayerCastleResult.participantCastles);
                else
                    _infoMessagePanel.DisplayMessage("Something went wrong :(");
                break;
            case InputLobbyHeaders.SET_PLAYER_ORDINAL_INFO:
                if (_getUpdatedParticipantSlotOrdinal.GetUpdatedOrdinal(messageInput, out SetPlayerOrdinalResult setPlayerOrdinalResult))
                    _lobbyModel.UpdateOrdinalPosition(setPlayerOrdinalResult);
                else
                    _infoMessagePanel.DisplayMessage("Something went wrong :(");
                break;
            case InputLobbyHeaders.SET_PARTICIPANT_FEE_INFO:
                if (_getUpdatedFeeDictionary.GetUpdatedFee(messageInput, out SetParticipantFeeInfo setParticipantFeeInfo))
                    _lobbyModel.UpdatedFeeParticipantsSlot(setParticipantFeeInfo.participantFeeMap);
                else
                    _infoMessagePanel.DisplayMessage("Something went wrong :(");
                break;
            case InputLobbyHeaders.SET_PLAYER_HERO_INFO:
                _lobbyModel.ChangeHeroesOnParicipantOnSession(_getUpdatedHeroIcon.GetUpdateHeroIconFromMessage(messageInput));
                break;

        }
    }
}
