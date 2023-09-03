using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class CastleParticipantSlot : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _castleDropdown;
    [SerializeField] public Sprite _randomSprite;
    [SerializeField] public Image _participantIcon;
    private IBroadcastChangedPlayerCastleRequest _broadcastChangedPlayerCastleRequest;
    private ParticipantSlot _participantSlot;
    private int _castleID = 0;

    private void Awake()
    {
        //_castleDropdown.onValueChanged.AddListener(SelectedNewCastle);
    }

    public void Init(IBroadcastChangedPlayerCastleRequest broadcastChangedPlayerCastleRequest, ParticipantSlot participantSlot)
    {
        _broadcastChangedPlayerCastleRequest = broadcastChangedPlayerCastleRequest;
        _participantSlot = participantSlot;
    }

    public void Disable()
    {
        _castleDropdown.enabled = false;
        _castleDropdown.GetComponent<Image>().color = Color.grey;
    }

    public void SelectedNewCastle()
    {
        Debug.Log("Changed");
        //Debug.Log("Value " + value);
        if(_castleDropdown.value == 0)
            _participantIcon.sprite = _randomSprite;
        _broadcastChangedPlayerCastleRequest.BroadcastChangePlayerCastleRequest(_participantSlot.SessionID, GetCastleID(), _participantSlot.Ordinal);
    }

    public void SetCastleID(int castleID)
    {
        if(castleID != _castleID)
        {
            _participantIcon.sprite = _randomSprite;
            _participantSlot.SetHero(null);
        }
        if (castleID < 0)
            castleID = 1;
        _castleID = castleID;
        var listAvailableStrings = _castleDropdown.options.Select(option => option.text).ToList();
        _castleDropdown.SetValueWithoutNotify(listAvailableStrings.IndexOf(castleID.ToString()));
    }

    private int GetCastleID()
    {
        if(int.TryParse(_castleDropdown.value.ToString(), out int result))
        {
            return result;
        }
        return 0;
    }
}