using System.Collections;
using UnityEngine;

public class UserDTO
{
    public int Id { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }

    public UserDTO(int id , string userName, string email)
    {
        UserName = userName;
        Email = email;
        Id = id;
    }
}