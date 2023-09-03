using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Client.DTO;

[System.Serializable]
public class HeroPerCastle
{
    [SerializeField] private List<Hero> _heroes;

    public IList<Hero> Heroes => _heroes;
}

public class ParticipantSlotsPanel : MonoBehaviour, IChoosebaleHeroForParticipantSlot , IUpdateFeeMap
{
    private Transform _slotsParent;
    private Heroes _heroesIcons;
    private ParticipantSlot _participantSlotPrefab;
    private List<ParticipantSlot> _participantSlots = new List<ParticipantSlot>();
    private ChooseHeroPanel _chooseHeroPanel;
    private LobbyModel _lobbyModel;
    private LobbyView _lobbyView;
    private CommonData _commonData;
    private Heroes _heroes;
    private string _currentSessionID;
    private IBroadCastChangedHeroIcon _broadCastChangedHeroIcon;
    private IBroadcastChangedPlayerCastleRequest _broadcastChangedPlayerCastleRequest;
    private IBroadcastChangeOrdinal _broadcastChangeOrdinal;
    private IBroadcastUpdatedFee _broadcastUpdatedFeeRequest;
    private string _playerUserName;
    private int _maxPlayers;
    public IEnumerable<ParticipantSlot> ParticipantSlots => _participantSlots;

    public void Init(CommonData commonData, Heroes heroes,LobbyView lobbyView,LobbyModel lobbyModel,Transform slotsParent, Heroes heroesIcons, ParticipantSlot participantSlotPrefab , ChooseHeroPanel chooseHeroPanel 
        , IParticipantPanelDependenciesContainer participantPanelDependenciesContainer)
    {
        _commonData = commonData;
        _heroes = heroes;
        _lobbyView = lobbyView;
        _lobbyModel = lobbyModel;
        _slotsParent = slotsParent;
        _heroesIcons = heroesIcons;
        _participantSlotPrefab = participantSlotPrefab;
        _chooseHeroPanel = chooseHeroPanel;
        _broadCastChangedHeroIcon = participantPanelDependenciesContainer.BroadCastChangedHeroIcon;
        _broadcastChangedPlayerCastleRequest = participantPanelDependenciesContainer.BroadcastChangedPlayerCastleRequest;
        _broadcastChangeOrdinal = participantPanelDependenciesContainer.BroadcastChangeOrdinal;
        _broadcastUpdatedFeeRequest = participantPanelDependenciesContainer.BroadcastUpdatedFee;
    }

    public void SetSessionID(string sessionID)
    {
        _currentSessionID = sessionID;
    }

    public void InitParticipantsSessionsAdnDisplay(IEnumerable<SessionParticipant> sessionParticipants , string playerUserName, int playerCount)
    {
        _playerUserName = playerUserName;
        _maxPlayers = playerCount;
        SpawnParticipantsSlots(sessionParticipants, playerUserName, playerCount);
        ChangeOrdinal();
    }

    public void UpdateParticipants(IEnumerable<SessionParticipant> sessionParticipants, string playerUserName)
    {
        _playerUserName = playerUserName;
        SpawnParticipantsSlots(sessionParticipants, playerUserName, _maxPlayers);

        ChangeOrdinal();
    }


    private void SpawnParticipantsSlots(IEnumerable<SessionParticipant> sessionParticipants, string playerUserName , int playerCount)
    {
        List<SessionParticipant> participants = new List<SessionParticipant>(sessionParticipants);
        for(int i = 0; i < participants.Count; i++)
        {
            ParticipantSlot participantSlot = Instantiate(_participantSlotPrefab, Vector3.zero, Quaternion.identity, _slotsParent);

            bool isAI = false;
            if(participants[i].UserInfo.UserName == null || participants[i].UserInfo.UserName.Length == 0)
                isAI = true;

            participantSlot.InitParticipant(participants[i], _currentSessionID,
                _heroesIcons.GetHeroByID(participants[i].DicHeroId), this, _broadCastChangedHeroIcon, _broadcastChangedPlayerCastleRequest,
                _broadcastChangeOrdinal, playerUserName, this, isAI);
            //if (i < participants.Count)
            //{
            //    Debug.Log("Participant id " + participants[i].UserId);

            //    participantSlot.InitParticipant(participants[i], _currentSessionID,
            //        _heroesIcons.GetHeroByID(participants[i].DicHeroId), this, _broadCastChangedHeroIcon, _broadcastChangedPlayerCastleRequest,
            //        _broadcastChangeOrdinal, playerUserName, this, false);
            //}
            //else
            //{

            //    participantSlot.InitParticipant(new SessionParticipant("Bot","bot", i,0,0,i + 1), _currentSessionID,
            //        _heroesIcons.GetHeroByID(0), this, _broadCastChangedHeroIcon, _broadcastChangedPlayerCastleRequest,
            //        _broadcastChangeOrdinal, playerUserName, this, true);
            //}
            _participantSlots.Add(participantSlot);
        }
        ChangeOrdinal();
    }
        
    public void UpdateFeeMap(Dictionary<int,int> participantsFeeMap)
    {
        foreach(var item in participantsFeeMap)
        {
            if(TryGetParticipantSlotByOrdinal(item.Key, out ParticipantSlot participantSlot))
            {
                participantSlot.SetFEE(item.Value);
            }
        }
    }

    public void UpdateFeeMap(int ordinalTargetChangedFee , DecreaseIncreaseMode decreaseIncreaseMode = DecreaseIncreaseMode.Increase)
    {
        Debug.Log("Feeupdate");
        int fee;      
        Dictionary<int, int> participants = new Dictionary<int, int>();
        foreach (var item in _participantSlots)
        {

            Debug.Log("FeeupdateCheck");
            if (item.Ordinal == ordinalTargetChangedFee)
                fee = item.FEE + (100 * (int)decreaseIncreaseMode);
            else
                fee = item.FEE;
            participants.Add(item.Ordinal, fee);
        }
        _broadcastUpdatedFeeRequest.BroadcastUpdatedFeeRequest(participants, _currentSessionID);
    }

    public void UpdateFeeMap(int ordinalTargetChangedFee , int feeNew)
    {

        Debug.Log("Feeupdate123");
        int fee;
        Dictionary<int, int> participants = new Dictionary<int, int>();
        foreach (var item in _participantSlots)
        {

            Debug.Log("Feeupdate222");
            if (item.Ordinal == ordinalTargetChangedFee)
                fee = feeNew;
            else
                fee = item.FEE;
            participants.Add(item.Ordinal, fee);
        }
        _broadcastUpdatedFeeRequest.BroadcastUpdatedFeeRequest(participants, _currentSessionID);
    }

    public void ChangePlayerSlotOrdinal(SetPlayerOrdinalResult setPlayerOrdinalResult)
    {
        if(TryGetParticipantSlotByOrdinal(setPlayerOrdinalResult.oldValue, out ParticipantSlot participantSlotOld))
        {
            if (TryGetParticipantSlotByOrdinal(setPlayerOrdinalResult.newValue, out ParticipantSlot participantSlotNew))
            {
                participantSlotNew.SetOrdinal(setPlayerOrdinalResult.oldValue);
                participantSlotOld.SetOrdinal(setPlayerOrdinalResult.newValue);
                _participantSlots.OrderBy(item => item.Ordinal);
                var gridLayout = _slotsParent.GetComponent<GridLayoutGroup>();
                gridLayout.enabled = false;
                foreach (var item in _participantSlots)
                {
                    item.transform.SetSiblingIndex(item.Ordinal);
                }
                gridLayout.enabled = true;
                ChangeOrdinal();
            }
        }
    }

    public void ChangeOrdinal()
    {
        foreach(var slot in _participantSlots)
        {
            if(slot.Ordinal - 1 < 0)
                slot.OrdinalSlotChanger.DisableUpperButton();
            else
                slot.OrdinalSlotChanger.EnableUpperButton();
            if(slot.Ordinal + 1 >= _participantSlots.Count)
                slot.OrdinalSlotChanger.DisableDownerButton();
            else
                slot.OrdinalSlotChanger.EnableDownerButton();
        }
    }

    public void ChangeCastleIDOnSlot(Dictionary<int, int> participantCastles)
    {
        foreach(var item in participantCastles)
        {
            if (TryGetParticipantSlotByOrdinal(item.Key, out ParticipantSlot participantSlot))
            {
                participantSlot.ChangeCastleID(item.Value);
            }
        }
    }

    public void RemoveParticipantSlotByOrdinal(int ordinal)
    {
        if(TryGetParticipantSlotByOrdinal(ordinal, out ParticipantSlot participantSlot))
        {
            Destroy(participantSlot.gameObject);
            _participantSlots.Remove(participantSlot);
            _lobbyModel.CurrentLobbySession.RemoveParticipant(participantSlot.SessionParticipant);
            _lobbyView.UpdateParticipantsCount();
        }
        
    }

    public void SetNewIconByOrdinalInSession(int ordinal , int id)
    {
        if (TryGetParticipantSlotByOrdinal(ordinal, out ParticipantSlot participantSlot))
        {
            participantSlot.SetChangedHero(_heroesIcons.GetHeroByID(id));
        }
    }

    private bool TryGetParticipantSlotByOrdinal(int ordinal, out ParticipantSlot participantSlot)
    {
        participantSlot = _participantSlots.FirstOrDefault(item => item.Ordinal == ordinal);
        if (participantSlot != null)
        {
            return true;
        }
        participantSlot = null;
        return false;
    }


    public void DestroyAllSlots()
    {
        foreach (var slot in _participantSlots)
        {
            Destroy(slot.gameObject);
        }
        _participantSlots.Clear();
    }

    public void OpenChoosepanelForParticipant(ParticipantSlot participantSlot , IReadOnlyCollection<Hero> heroes)
    {
        if (_lobbyModel.UserDTO.UserName != participantSlot.SessionParticipant.UserInfo.UserName && !_lobbyModel.CurrentLobbySession.IsAiAllowed)
            return;
        List<Hero> list = new List<Hero>();
        List<DicHeroDTO> dicHeroDTOs = _commonData.HeroesDictianory.Values.Where(item => item.castleId == participantSlot.CastleID).ToList();
        List<Hero> ids = new List<Hero>();


        ids = _participantSlots.Select(item => item.CurrentHero).ToList();

        foreach (var item in dicHeroDTOs)
        {
            Debug.Log("Hero id " + item.id);
            Hero hero = _heroes.GetHeroByIDOrNull(item.id);
            if (!ids.Contains(hero))
                list.Add(_heroes.GetHeroByID(item.id));
        }
        _chooseHeroPanel.OpenPanelToSelectHeroForParitipantSlot(participantSlot , list);
    }
}