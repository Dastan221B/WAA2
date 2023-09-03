using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IChatSender
{
    void SendMessageToChatRequest(string sessionId, string chatMessage);
}