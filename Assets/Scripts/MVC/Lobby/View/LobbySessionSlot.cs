using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LobbySessionSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _creator;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _playerCount;

    private LobbySession _lobbySession;
    private ITryConnectToLobbySession _tryConnectToLobbySession;

    public void Init(LobbySession lobbySession , ITryConnectToLobbySession tryConnectToLobbySession)
    {
        _tryConnectToLobbySession = tryConnectToLobbySession;
        _lobbySession = lobbySession;
        _name.text = _lobbySession.Name;
        _creator.text = lobbySession.Creator;
        _playerCount.text = $"{_lobbySession.SessionParticipants.Count}/{_lobbySession.MaxPlayerCount}";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _tryConnectToLobbySession.TryConnectBySession(_lobbySession);
    }

}