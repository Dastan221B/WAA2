using System.Collections;
using UnityEngine;

public interface IGameProcessHandler
{
    void ProccessResponseFromServer(MessageInput messageInput);
}