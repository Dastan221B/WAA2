using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySessionsSlots : MonoBehaviour
{
    private Transform _slotsParent;
    private LobbySessionSlot _lobbySessionSlotPrefab;
    private List<LobbySessionSlot> _lobbySessionSlots = new List<LobbySessionSlot>();
    private ITryConnectToLobbySession _connectToLobbySession;

    public void Init(Transform parent, LobbySessionSlot lobbySessionSlotPrefab , ITryConnectToLobbySession connectToLobbySession)
    {
        _connectToLobbySession = connectToLobbySession;
        _slotsParent = parent;
        _lobbySessionSlotPrefab = lobbySessionSlotPrefab;
    }

    public void AddLobbySession(LobbySession lobbySession)
    {
        var slot = Instantiate(_lobbySessionSlotPrefab, Vector3.zero, Quaternion.identity,_slotsParent);
        slot.Init(lobbySession, _connectToLobbySession);
        _lobbySessionSlots.Add(slot);
    }

    public void DestroyAllSlots()
    {
        foreach (var slot in _lobbySessionSlots)
            if(slot != null)
                Destroy(slot.gameObject);

        _lobbySessionSlots.Clear();
    }

}