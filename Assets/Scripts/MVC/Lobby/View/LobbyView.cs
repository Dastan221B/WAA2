using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using Assets.Scripts.MVC.Lobby.View;
using System.Linq;


public class LobbyView : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private Image _backGroundImage;
    [SerializeField] private Sprite _monolitBackground;
    [SerializeField] private Sprite _pictureBackground;

    [Header("Buttons")]
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _refreshLobbyList;
    [SerializeField] private Button _registerButton;
    [SerializeField] private Button _openCreateLobbyButton;
    [SerializeField] private Button _closeCreateLobbyButton;
    [SerializeField] private Button _createLobbyButton;
    [SerializeField] private Button _exitFromLobbySession;

    [Header("Texts")]
    [SerializeField] private TMP_Text _usernameTMP;

    private LobbyController _lobbyController;
    private LobbyModel _lobbyModel;

    private OnlineInGameView _onlineInGameView;
    private ParticipantSlotsPanel _participantSlotsPanel;
    private LobbySessionsSlots _lobbySessionsSlots;
    private LobbyPanel _lobbyPanel;
    private CreateLobbyPanel _createLobbyPanel;
    private RegisterPanel _registerPanel;
    private LobbySessionsPanel _lobbySessionsPanel;
    private ChatPanel _chatPanel;
    private IStarGame _starGame;


    private void Awake()
    {
        _registerButton.onClick.AddListener(Register);
        _openCreateLobbyButton.onClick.AddListener(OpenCreateLobbyPanel);
        _closeCreateLobbyButton.onClick.AddListener(CloseCreateLobbyPanel);
        _createLobbyButton.onClick.AddListener(CreateLobby);
        _exitFromLobbySession.onClick.AddListener(CallExitFromLobby);
        _refreshLobbyList.onClick.AddListener(RefreshSessionLobbyList);
        _startGameButton.onClick.AddListener(StartGame);
    }

    public void Init(LobbyController lobbyController , LobbyModel lobbyModel ,
        ParticipantSlotsPanel participantSlotsPanel , LobbyPanel lobbyPanel , CreateLobbyPanel createLobbyPanel , RegisterPanel registerPanel,
        LobbySessionsSlots lobbySessionsSlots, LobbySessionsPanel lobbySessionsPanel, ChatPanel chatPanel,
        OnlineInGameView onlineInGameView)
    {
        _lobbyController = lobbyController;
        _lobbyModel = lobbyModel;
        _participantSlotsPanel = participantSlotsPanel;
        _lobbyPanel = lobbyPanel;
        _createLobbyPanel = createLobbyPanel;
        _registerPanel = registerPanel;
        _lobbySessionsSlots = lobbySessionsSlots;
        _lobbySessionsPanel = lobbySessionsPanel;
        _chatPanel = chatPanel;
        _starGame = lobbyController;
        _onlineInGameView = onlineInGameView;
    }

    #region Lobby

    public void StartGame()
    {
        _starGame.TryStartGemeRequest(_lobbyModel.CurrentLobbySession.Id);
    }

    public void UpdateParticipantsFee(Dictionary<int,int> pariticipantFeeMap)
    {
        _participantSlotsPanel.UpdateFeeMap(pariticipantFeeMap);
    }

    public void RefreshSessionLobbyList()
    {
        _lobbyController.GetLobbySessionListRequest();
    }

    public void DisplayMessage(string message)
    {
        _chatPanel.DispalyMessage(message);
    }

    public void UpdateLobbySessions()
    {
        _lobbySessionsSlots.DestroyAllSlots();
        foreach (var lobby in _lobbyModel.LobbySessions)
        {
            _lobbySessionsSlots.AddLobbySession(lobby);
        }
    }

    public void UpdatedCastleIDOnSlot(Dictionary<int, int> participantCastles)
    {
        _participantSlotsPanel.ChangeCastleIDOnSlot(participantCastles);
    }

    public void UpdateParticipantOrdinal(SetPlayerOrdinalResult setPlayerOrdinalResult)
    {
        _participantSlotsPanel.ChangePlayerSlotOrdinal(setPlayerOrdinalResult);
    }

    public void EnterInLobbySession()
    {
        CloseCreateLobbyPanel();
        _lobbyPanel.SetPlayerCounts(_lobbyModel.CurrentLobbySession.SessionParticipants.Count, _lobbyModel.CurrentLobbySession.MaxPlayerCount);
        _lobbyPanel.SetLobbyName(_lobbyModel.CurrentLobbySession.Name);
        _lobbyPanel.OpenPanel();
        _lobbySessionsPanel.Close();
        _participantSlotsPanel.SetSessionID(_lobbyModel.CurrentLobbySession.Id);
        _participantSlotsPanel.InitParticipantsSessionsAdnDisplay(_lobbyModel.CurrentLobbySession.SessionParticipants , _lobbyModel.UserDTO.UserName, _lobbyModel.CurrentLobbySession.MaxPlayerCount);
        _chatPanel.InitChatData(_lobbyModel.UserDTO.UserName, _lobbyModel.CurrentLobbySession.Id);
        _openCreateLobbyButton.gameObject.SetActive(true);
        _backGroundImage.sprite = _pictureBackground;
        if (_lobbyModel.CurrentLobbySession.Creator == _lobbyModel.UserDTO.UserName)
        {
            _startGameButton.gameObject.SetActive(true);
        }
        else
        {
            _startGameButton.gameObject.SetActive(false);
        }
    }

    public void CallExitFromLobby()
    {
        _lobbyController.ExitFromLobbySession();
    }

    public void ExitFromLobby()
    {
        _participantSlotsPanel.DestroyAllSlots();
        _lobbyPanel.ClosePanel();
        _lobbySessionsPanel.Open();
        _chatPanel.ClearChat();
        _backGroundImage.sprite = _monolitBackground;
        _openCreateLobbyButton.gameObject.SetActive(true);
    }

    public void ExitParticipantInfoFromLobby(ExitLobbySessionInfo exitLobbySessionInfo)
    {
        _participantSlotsPanel.RemoveParticipantSlotByOrdinal(exitLobbySessionInfo.participantOrdinal);
    }

    public void ChangeIcon(SetPlayerHeroResult setPlayerHeroResult)
    {
        _participantSlotsPanel.SetNewIconByOrdinalInSession(setPlayerHeroResult.participantOrdinal, setPlayerHeroResult.heroId);
    }

    public void UpdateParticipantsList()
    {
        //_participantSlotsPanel.DestroyAllSlots();
        List<SessionParticipant> participantsInSlots = new List<SessionParticipant>
            (_participantSlotsPanel.ParticipantSlots.Select(item => item.SessionParticipant));
        List<SessionParticipant> sessionParticipantsToCreate = new List<SessionParticipant>();

        foreach(var item in _lobbyModel.CurrentLobbySession.SessionParticipants)
        {
            if(!participantsInSlots.Contains(item))
            {
                sessionParticipantsToCreate.Add(item);
            }
        }    

        _participantSlotsPanel.UpdateParticipants(sessionParticipantsToCreate, _lobbyModel.UserDTO.UserName);
        UpdateParticipantsCount();
    }

    public void UpdateParticipantsCount()
    {
        _lobbyPanel.SetPlayerCounts(_lobbyModel.CurrentLobbySession.SessionParticipants.Count, _lobbyModel.CurrentLobbySession.MaxPlayerCount);
    }

    #endregion

    #region Registration

    public void Register()
    {
        _registerPanel.Register();
    }

    public void CloseRegisterPanel()
    {
        _lobbySessionsPanel.Open();
        _registerPanel.ClosePanel();
        _openCreateLobbyButton.gameObject.SetActive(true);
        _backGroundImage.sprite = _monolitBackground;
        _usernameTMP.text = _lobbyModel.UserDTO.UserName;
        _onlineInGameView.InitPlayer(_lobbyModel.UserDTO);
    }
    #endregion

    #region CreateLobbRegion
    public void OpenCreateLobbyPanel()
    {
        _createLobbyPanel.OpenPanel();
        _openCreateLobbyButton.gameObject.SetActive(false);
    }

    public void CloseCreateLobbyPanel()
    {
        _lobbySessionsPanel.Open();
        _createLobbyPanel.ClosePanel();
        _openCreateLobbyButton.gameObject.SetActive(true);
    }


    public void CreateLobby()
    {
        LobbyCreateSettings lobbyCreateSettings = _createLobbyPanel.GetLobbyCreateSettings();
        if (_lobbyController.TryCreateNewLobbySessionRequest(lobbyCreateSettings))
        {
            _createLobbyPanel.ClosePanel();
        }
    }
    #endregion
}