
using System;

public class SessionUserDTO : UserDTO
{
    public string SessionID { get; private set; }

    public SessionUserDTO(int id, string userName, string email, string sessionId) : base(id,userName, email)
    {
        SessionID = sessionId;
    }

}