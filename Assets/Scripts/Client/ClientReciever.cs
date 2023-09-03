using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Assets.Scripts;

public class ClientReciever : MonoBehaviour
{
    [SerializeField] private ReloadScene _reloadScene;
    [SerializeField] private bool _detectingError;
    private int _bufferSize;
    private IServerMessageDistrebutor _serverMessageDistrebutor;
    private NetworkStream _networkStream;
    private volatile MessageInput _recivedMessageInput;
    private Thread _reciveDataThread;
    private volatile bool _isAbortedThread;

    public void Init(IServerMessageDistrebutor serverMessageDistrebutor , NetworkStream networkStream, int bufferSize)
    {
        if(serverMessageDistrebutor == null)
            throw new Exception("ServerMessageDistrebutor is null");
        if (networkStream == null)
            throw new Exception("Network strem is null");

        if (bufferSize <= 1024)
            bufferSize = 9192;

        _serverMessageDistrebutor = serverMessageDistrebutor;
        _networkStream = networkStream;
        _bufferSize = bufferSize;
        _reciveDataThread = new Thread(RecieveMessageFromServer);
        _reciveDataThread.Start();
    }

    public void OnApplicationQuit()
    {
        if (_reciveDataThread != null)
            _reciveDataThread.Abort();
    }

    private void Update()
    {
        if (_isAbortedThread)
        {
            _reloadScene.Reload();
            _isAbortedThread = false;
        }
    }

    public void RecieveMessageFromServer()
    {
        StreamReader reader = new StreamReader(_networkStream, Encoding.Default, true, _bufferSize);
        string message = "";

        while (true)
        {
            try
            {
                do
                {
                    message = reader.ReadLine();

                    if (message != null)
                    {
                        char[] messageArr = message.ToCharArray();

                        //for (int i = 0; i < messageArr.Length; i++)
                        //{
                        //    if (messageArr[i] != null)
                        //    {
                        //        if(messageArr[i] == '[')
                        //        {
                        //            var charArrayRebuild = new List<char>(messageArr);
                        //            charArrayRebuild.RemoveAt(0);
                        //            messageArr = charArrayRebuild.ToArray();
                        //        }
                        //        break;
                        //    }
                        //}

                        for (int i = 0; i < messageArr.Length; i++)
                        {

                            if (messageArr[i] == '{' || messageArr[i] == '[')
                            {
                                message = ConvertMessageCharToString(i, messageArr);
                                break;
                            }

                            //if(i != 0 && messageArr[i - 1] == '{'  && messageArr[i] == '{')
                            //{
                            //    Debug.Log("Error " + message);
                            //}
                            //else
                            //{
                            //    if (messageArr[i] == '{' || messageArr[i] == '[')
                            //    {
                            //        message = ConvertMessageCharToString(i, messageArr);
                            //        Debug.Log("message " + message);
                            //        break;
                            //    }
                            //}
                        }

                        if (message[0] == '{' && message[1] == '{')
                        {
                            message = message.Substring(1, message.Length - 1);
                        }
                        Debug.Log("Message " + message);
                        MessageInput messageInput = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageInput>(message);
                        _serverMessageDistrebutor.DistrebuteMessageFromServer(messageInput);
                    }
                }
                while (_networkStream.DataAvailable);
            }
            catch (SocketException e)
            {
                Debug.Log("message " + message);
                Debug.Log("SocketException: " + e.Message);
                _networkStream.Close();
                reader.Close();
                _isAbortedThread = true;
            }
            catch (Exception e)
            {
                Debug.Log("message " + message);
                Debug.Log("Exception: " + e.Message);
                Debug.Log("Exception: " + e.StackTrace);
                _networkStream.Close();
                reader.Close();
                _isAbortedThread = true;
            }
        }

    }


    private string ConvertMessageCharToString(int startBite, char[] messageArr)
    {
        string message = "";
        for (int i = startBite; i < messageArr.Length; i++)
        {
            message += messageArr[i];
        }
        return message;
    }
}
