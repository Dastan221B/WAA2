using System;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class ClientSender : MonoBehaviour
{
    private readonly int _bufferSize;

    private NetworkStream _networkStream;
    private int _lastSendedMessageID;

    public ClientSender(NetworkStream networkStream , int bufferSize)
    {
        if (networkStream == null)
            throw new Exception("Network strem is null");
        if (bufferSize <= 1024)
            bufferSize = 9192;
        _networkStream = networkStream;
        _bufferSize = bufferSize;
    }

    public void SendMessageToServer(int clientHandler, int header, string body, string sessionId)
    {
        MessageOutput message = new MessageOutput(_lastSendedMessageID, clientHandler, header, body, sessionId);
        byte[] buffer = new byte[_bufferSize];
        string messageJson = Newtonsoft.Json.JsonConvert.SerializeObject(message);
        buffer = Encoding.UTF8.GetBytes(messageJson + "\n");
        _networkStream.Write(buffer, 0, messageJson.Length + 1);
        Debug.Log(messageJson);
        _lastSendedMessageID++;
    }
}