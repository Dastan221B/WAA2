using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class ParticipantSlot : MonoBehaviour , IChangebleHero
{
    [SerializeField] private HeroPerCastle[] _heroPerCastles;

    private const int MAX_FEE = 10000;
    [SerializeField] private FeeSlotPanel _feeSlotPanel;
    [SerializeField] private OrdinalSlotChanger _ordinalSlotChanger;
    [SerializeField] private PointerClickNotifier _pointerClickNotifier;
    [SerializeField] private CastleParticipantSlot _castleParticipantSlot;
    [SerializeField] private Image _heroIcon;
    [SerializeField] private TMP_Text _userName;
    private SessionParticipant _sessionParticipant;
    private IChoosebaleHeroForParticipantSlot _choosebaleHeroForPariticipantSlot;
    private IBroadCastChangedHeroIcon _broadCastChangedHeroIcon;
    private IBroadcastChangeOrdinal _broadcastChangeOrdinal;
    private Hero _currentHero;
    private string _sessionID;
    private int _fee;
    public int CastleID { get; private set; }

    public SessionParticipant SessionParticipant => _sessionParticipant;
    public int FEE => _fee;
    public int Ordinal => _sessionParticipant.Ordinal;
    public Hero CurrentHero => _currentHero;
    public string SessionID => _sessionID;
    public bool IsAI { get; private set; }
   
    public OrdinalSlotChanger OrdinalSlotChanger => _ordinalSlotChanger;
    private void OnEnable()
    {
        _pointerClickNotifier.OnClicked += OpenChoosePanel;
    }

    private void OnDisable()
    {
        _pointerClickNotifier.OnClicked -= OpenChoosePanel;
    }

    public void InitParticipant(SessionParticipant sessionParticipant, string sessionID,Hero hero ,
        IChoosebaleHeroForParticipantSlot choosebaleHeroForParticipantSlot , IBroadCastChangedHeroIcon broadCastChangedHeroIcon, 
        IBroadcastChangedPlayerCastleRequest broadcastChangedPlayerCastleRequest , IBroadcastChangeOrdinal broadcastChangeOrdinal
        , string playerUserName, IUpdateFeeMap updateFeeMap , bool isAI)
    {
        _currentHero = null;
        _sessionID = sessionID;
        if (FindObjectOfType<CastleParticipantSlot>()._participantIcon.sprite == FindObjectOfType<CastleParticipantSlot>()._randomSprite)
        {
            _heroIcon.sprite = FindObjectOfType<CastleParticipantSlot>()._randomSprite;
        }
        else
        {
            _heroIcon.sprite = _currentHero.Icon;
        }
        if (sessionParticipant != null)
        {
            _userName.text = sessionParticipant.UserInfo.UserName;
        }
        _sessionParticipant = sessionParticipant;
        _choosebaleHeroForPariticipantSlot = choosebaleHeroForParticipantSlot;
        _broadCastChangedHeroIcon = broadCastChangedHeroIcon;
        _castleParticipantSlot.Init(broadcastChangedPlayerCastleRequest, this);
        _broadcastChangeOrdinal = broadcastChangeOrdinal;
        _ordinalSlotChanger.Init(this, _broadcastChangeOrdinal);
        _feeSlotPanel.Init(updateFeeMap, this);
        IsAI = isAI;
        if (playerUserName != _sessionParticipant.UserInfo.UserName && !isAI)
        {
            _castleParticipantSlot.Disable();
        }
    }

    public void SetHero(Hero hero)
    {
        _currentHero = hero;
        if(_currentHero != null)
            _heroIcon.sprite = _currentHero.Icon;
    }

    public void SetFEE(int newFee)
    {
        //if (newFee < 0 || newFee > MAX_FEE)
        //    return;
        _fee = newFee;
        _feeSlotPanel.UpdateFeeText(_fee);
    }

    public void SetOrdinal(int ordinal)
    {
        if(_sessionParticipant != null)
            _sessionParticipant.ChangeOrdinal(ordinal);
    }

    public void ChangeHero(Hero hero)
    {
        //SetChangedHero(hero);
        _broadCastChangedHeroIcon.BroadcastChangedHeroIcon(Ordinal,_sessionID, hero.HeroID);
    }

    public void ChangeCastleID(int castleID)
    {
        CastleID = castleID;
        _castleParticipantSlot.SetCastleID(castleID);
       _sessionParticipant.DicCastleId = castleID;
        
    }

    public void SetChangedHero(Hero hero)
    {
        _currentHero = hero;
        _heroIcon.sprite = _currentHero.Icon;
        _sessionParticipant.DicHeroId = hero.HeroID;
    }

    public void OpenChoosePanel()
    {
        if(CastleID > 0)
            _choosebaleHeroForPariticipantSlot.OpenChoosepanelForParticipant(this , _heroPerCastles[CastleID - 1].Heroes.ToList());
    }
}