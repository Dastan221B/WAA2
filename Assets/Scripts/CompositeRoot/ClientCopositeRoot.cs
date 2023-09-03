using Assets.Scripts.Client;
using Assets.Scripts.Client.CommonDTO;
using Assets.Scripts.Client.GameClient;
using Assets.Scripts.JSONConfig;
using System.Collections;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class ClientCopositeRoot : CompositeRoot
{
    private const int BUFFER_SIZE = 9192;
    [SerializeField] private ConfigFile _configFile;
    [SerializeField] private CommonMessageSender _commonMessageSender;
    [SerializeField] private GameMessageSender _gameMessageSender;
    [SerializeField] private LobbyMessageSender _lobbyMessageSender;
    [SerializeField] private MessageServerHandlerDistrebutor _messageServerHandlerDistrebutor;
    [SerializeField] private ClientReciever _clientReciever;

    private ClientSender _clientSender;
    
    private TcpClient _tcpClient;
    private NetworkStream _stream;

    public override void Composite()
    {
        _configFile.LoadJSonConfigData();
        _tcpClient = new TcpClient();
        _tcpClient.Connect(_configFile.Config.IP, _configFile.Config.PORT);
        _stream = _tcpClient.GetStream();
        _clientSender = new ClientSender(_stream, BUFFER_SIZE);
        _gameMessageSender.Init(_clientSender);
        _lobbyMessageSender.Init(_clientSender);
        _commonMessageSender.Init(_clientSender);
        _clientReciever.Init(_messageServerHandlerDistrebutor,_stream, BUFFER_SIZE);
    }

}