using System.Net;
using System.Net.Http;
using System.Text;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.UI;
using Assets.Scripts.JSONConfig;

public class ClientRegister : MonoBehaviour
{
    [SerializeField] private ConfigFile _configFile;

    public static event Action<SessionUserDTO> OnRegistred;
    private volatile SessionUserDTO _sessionUserDTO;


    public void FastReg(string email)
    {
        Register("123456", email);
    }

    public async void Register(string pass , string mail)
    {
        string defaultEmail = "dummy2@gmail.com";
        string data = "";
        if (mail.Length == 0 && pass.Length == 0)
        {
            data = "{\"email\":\"" + defaultEmail + "\",\"password\":\"123456\"}";
        }
        else
        {
            data = "{\"email\":\"" + mail + "\",\"password\":\"" + pass + "\"}";
        }

        using (var client = new HttpClient())
        {
            var response = await client.PostAsync(
                _configFile.Config.RequestURI,
                 new StringContent(data, Encoding.UTF8, "application/json"));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                string s = await response.Content.ReadAsStringAsync();
                _sessionUserDTO = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionUserDTO>(s);
                OnRegistred?.Invoke(_sessionUserDTO);
            }
            else
            {
                GameObject.Find("ErrorText").GetComponent<Text>().text = "Incorrect mail or password! " + response.StatusCode ;
            }

        }
    }
}