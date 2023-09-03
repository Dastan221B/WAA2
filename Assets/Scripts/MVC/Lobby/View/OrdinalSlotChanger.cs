using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OrdinalSlotChanger : MonoBehaviour
{
    [SerializeField] private Button _upButton;
    [SerializeField] private Button _downButton;

    private ParticipantSlot _participantSlot;
    private IBroadcastChangeOrdinal _broadcastChangeOrdinal;

    public void Init(ParticipantSlot participantSlot, IBroadcastChangeOrdinal broadcastChangeOrdinal)
    {
        _participantSlot = participantSlot;
        _broadcastChangeOrdinal = broadcastChangeOrdinal;
    }

    public void Increase()
    {
        int currentOridnal = _participantSlot.Ordinal;
        _broadcastChangeOrdinal.BroadcastChangeParticipantOrdinalRequest(currentOridnal, currentOridnal + 1, _participantSlot.SessionID);
    }

    public void Decrease()
    {
        int currentOridnal = _participantSlot.Ordinal;
        _broadcastChangeOrdinal.BroadcastChangeParticipantOrdinalRequest(currentOridnal, currentOridnal - 1, _participantSlot.SessionID);
    }

    public void DisableUpperButton()
    {
        DisableButton(_upButton);
    }

    public void DisableDownerButton()
    {
        DisableButton(_downButton);
    }

    public void EnableUpperButton()
    {
        EnableButton(_upButton);
    }

    public void EnableDownerButton()
    {
        EnableButton(_downButton);
    }

    private void DisableButton(Button button)
    {
        button.enabled = false;
        button.GetComponent<Image>().color = Color.gray;
    }

    private void EnableButton(Button button)
    {
        button.enabled = true;
        button.GetComponent<Image>().color = Color.green;
    }
}