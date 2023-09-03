using System;

[Serializable]
public class MessageJSonOutput
{
    public int ID { get; private set; }
    public int ClientHandler { get; private set; }
    public int Header { get; private set; }
    public string Body { get; private set; }
    public string SessionID { get; private set; }

    public MessageJSonOutput(int id, int clientHandler, int header, string body, string sessionId)
    {
        ID = id;
        ClientHandler = clientHandler;
        Header = header;
        Body = body;
        SessionID = sessionId;
    }
}

[System.Serializable]
public class MessageOutput
{
    public int id { get; set; }
    public int cl { get; set; }
    public int header { get; set; }

    public string body, sessionId;

    public MessageOutput(int id, int cl, int header, string body, string sessionId)
    {
        this.id = id;
        this.cl = cl;
        this.header = header;
        this.body = body;
        this.sessionId = sessionId;
    }

}