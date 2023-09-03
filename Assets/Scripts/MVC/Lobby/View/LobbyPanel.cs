using System.Collections;
using UnityEngine;
using TMPro;

public class LobbyPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _countPlayers;
    [SerializeField] private TMP_Text _lobbyName;

    public void SetPlayerCounts(int currentPlayers , int maxPlayers)
    {
        _countPlayers.text = $"PLAYERS : {currentPlayers}/{maxPlayers}";
    }

    public void SetLobbyName(string name)
    {
        _lobbyName.text = name;
    }

    public void OpenPanel()
    {
        _panel.SetActive(true);
    }

    public void ClosePanel()
    {
        _panel.SetActive(false);
    }
}