using Assets.Scripts.Client;
using Assets.Scripts.Client.CommonDTO;
using Assets.Scripts.Client.GameClient;
using Assets.Scripts.MVC.Lobby.View;
using UnityEngine;

public class LobbyCompositeRoot : CompositeRoot
{
    [SerializeField] private OnlineInGameView _onlineInGameView;
    [SerializeField] private CommonMessageSender _commonMessageSender;
    [SerializeField] private CommonData _commonData;
    [SerializeField] private GameMessageSender _gameMessageSender;
    [SerializeField] private GameBattleProcessResponseHandler _gameProcessResponseHandler;
    [SerializeField] private InfoMessagePanel _infoMessagePanel;
    [SerializeField] private ChatPanel _chatPanel;
    [SerializeField] private Transform _chatMessagesParent;
    [SerializeField] private ChatMessageItem _chatMessageItemPrefab;
    [SerializeField] private HeroIconPrefab _heroIconPrefab;
    [SerializeField] private ChooseHeroPanel _chooseHeroPanel;
    [SerializeField] private LobbySessionsPanel _lobbySessionsPanel;
    [SerializeField] private LobbySessionsSlots _lobbySessionsSlots;
    [SerializeField] private Transform _lobbySessioSlotParent;
    [SerializeField] private LobbySessionSlot _lobbySessionSlotPrefab;
    [SerializeField] private MessageServerHandlerDistrebutor _messageServerHandlerDistrebutor;
    [SerializeField] private LobbyMessageSender _lobbyMessageSender;
    [SerializeField] private LobbyView _lobbyView;
    [SerializeField] private RegisterPanel _registerPanel;
    [SerializeField] private CreateLobbyPanel _createLobbyPanel;
    [SerializeField] private ParticipantSlotsPanel _participantSlotsPanel;
    [SerializeField] private LobbyPanel _lobbyPanel;
    [SerializeField] private Transform _slotsParticipantLobbyParent;
    [SerializeField] private Heroes _heroes;
    [SerializeField] private ParticipantSlot _participantSlotPrefab;
    [SerializeField] private ClientRegister _clientRegister;
    private LobbyModel _lobbyModel;
    private LobbyController _lobbyController;
    private CoommonMessageHandler _coommonMessageHandler;
    public LobbyModel LobbyModel => _lobbyModel;
    public Heroes Heroes => _heroes;

    public override void Composite()
    {  
        _registerPanel.Init(_clientRegister);
        _chooseHeroPanel.Init(_heroIconPrefab, _heroes);
        _lobbyModel = new LobbyModel();
        _lobbyController = new LobbyController(_heroes, _participantSlotsPanel,_lobbyModel, _lobbyMessageSender , _infoMessagePanel, _chatPanel);
        _chatPanel.Init(_lobbyController, _chatMessagesParent, _chatMessageItemPrefab);
        _participantSlotsPanel.Init(_commonData, _heroes, _lobbyView,_lobbyModel,_slotsParticipantLobbyParent, _heroes, _participantSlotPrefab, _chooseHeroPanel, _lobbyController);
        _lobbySessionsSlots.Init(_lobbySessioSlotParent, _lobbySessionSlotPrefab, _lobbyController);
        _lobbyView.Init(_lobbyController, _lobbyModel, _participantSlotsPanel, _lobbyPanel, _createLobbyPanel, _registerPanel, _lobbySessionsSlots, _lobbySessionsPanel, _chatPanel, _onlineInGameView);
        _coommonMessageHandler = new CoommonMessageHandler(_commonData);
        ClientRegister.OnRegistred += _lobbyMessageSender.RegisterDTO;
        ClientRegister.OnRegistred += _gameMessageSender.RegisterDTO;
        ClientRegister.OnRegistred += _commonMessageSender.RegisterDTO;
        ClientRegister.OnRegistred += _commonMessageSender.SendAllRequestForCommonData;
        ClientRegister.OnRegistred += _lobbyModel.PlayerRegistredDTO;
        ClientRegister.OnRegistred += RegistredHandler;
        _lobbyModel.OnEnterInLobbySession += _lobbyView.EnterInLobbySession;
        _lobbyModel.OnJoinNewParticipant += _lobbyView.UpdateParticipantsList;
        _lobbyModel.OnExitFromCurrentLobbySession += _lobbyView.ExitFromLobby;
        _lobbyModel.OnExitParticipant += _lobbyView.ExitParticipantInfoFromLobby;
        _lobbyModel.OnUpdatedLobbySessions += _lobbyView.UpdateLobbySessions;
        _lobbyModel.OnUpdatedCastleOnSlot += _lobbyView.UpdatedCastleIDOnSlot;
        _lobbyModel.OnUpdatedPlayerOrdinal += _lobbyView.UpdateParticipantOrdinal;
        _lobbyModel.OnUpdatedFeeParticipantsSlot += _lobbyView.UpdateParticipantsFee;
        _lobbyModel.OnGettedMessageInChat += _lobbyView.DisplayMessage;
        _messageServerHandlerDistrebutor.OnLobbyMessageNotify += _lobbyController.ProcessResponseFromServer;
        _messageServerHandlerDistrebutor.OnGameMessageNotify += _gameProcessResponseHandler.ProccessResponseFromServer;
        _messageServerHandlerDistrebutor.OnCommonMessageNotify += _coommonMessageHandler.ProccessResponseFromServer;
        _lobbyModel.OnChangeHero += _lobbyController.ChangeParticipantHero;
    }

    //private void OnDisable()
    //{
    //    ClientRegister.OnRegistred -= RegistredHandler;
    //    ClientRegister.OnRegistred -= _lobbyMessageSender.RegisterDTO;
    //    ClientRegister.OnRegistred -= _lobbyModel.PlayerRegistredDTO;
    //    _lobbyModel.OnEnterInLobbySession -= _lobbyView.EnterInLobbySession;
    //    _lobbyModel.OnExitFromCurrentLobbySession -= _lobbyView.ExitFromLobby;
    //    _lobbyModel.OnJoinNewParticipant -= _lobbyView.UpdateParticipantsList;
    //    _lobbyModel.OnExitParticipant -= _lobbyView.ExitParticipantInfoFromLobby;
    //    _lobbyModel.OnUpdatedLobbySessions -= _lobbyView.UpdateLobbySessions;
    //    _lobbyModel.OnUpdatedCastleOnSlot -= _lobbyView.UpdatedCastleIDOnSlot;
    //    _lobbyModel.OnUpdatedPlayerOrdinal -= _lobbyView.UpdateParticipantOrdinal;
    //    _lobbyModel.OnUpdatedFeeParticipantsSlot -= _lobbyView.UpdateParticipantsFee;
    //    _lobbyModel.OnGettedMessageInChat -= _lobbyView.DisplayMessage;
    //}

    private void RegistredHandler(SessionUserDTO sessionUserDTO)
    {
        _lobbyView.CloseRegisterPanel();

    }

}