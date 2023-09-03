using System.Collections;
using UnityEngine;
using System;
using System.Collections.Concurrent;

namespace Assets.Scripts.Client
{
    public class MessageServerHandlerDistrebutor : MonoBehaviour , IServerMessageDistrebutor
    {
        private ConcurrentQueue<Action> _runOnMainThread = new ConcurrentQueue<Action>();


        public event Action<MessageInput> OnGameMessageNotify;
        public event Action<MessageInput> OnLobbyMessageNotify;
        public event Action<MessageInput> OnCommonMessageNotify;


        public void Update()
        {
            if (!_runOnMainThread.IsEmpty)
            {
                Action action;
                while (_runOnMainThread.TryDequeue(out action))
                {
                    action.Invoke();
                }
            }

        }

        public void DistrebuteMessageFromServer(MessageInput messageInput)
        {
            _runOnMainThread.Enqueue(() =>
            {
                switch ((ClientHandlers)messageInput.cl)
                {

                    case ClientHandlers.GAME:
                        OnGameMessageNotify?.Invoke(messageInput);
                        break;
                    case ClientHandlers.LOBBY:
                        OnLobbyMessageNotify?.Invoke(messageInput);
                        break;
                    case ClientHandlers.COMMON:
                        OnCommonMessageNotify?.Invoke(messageInput);
                        break;
                }
            });
        }
    }
}