using System;


[Serializable]
public class ServerMessage
{
    public int Id { get; private set; }
    public int Header { get; private set; }
    public int ClientHandler { get; private set; }
    public string Body { get; private set; }

    public ServerMessage(int id, int clientHandler, int header, string body)
    {
        Id = id;
        ClientHandler = clientHandler;
        Header = header;
        Body = body;
    }
}

[System.Serializable]
public class MessageInput
{
    public int id, cl, header;
    public string body;

    public MessageInput(int id, int cl, int header, string body)
    {
        this.id = id;
        this.cl = cl;
        this.header = header;
        this.body = body;
    }
}
