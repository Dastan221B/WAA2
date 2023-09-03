using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.JSONConfig
{
    public class ConfigFile : MonoBehaviour
    {
        public JSonConfigFile Config { get; private set; }

        public void LoadJSonConfigData()
        {
            string path = "";
#if UNITY_EDITOR
            path = Path.Combine(Application.streamingAssetsPath, "config.json");
#else
            path = Path.Combine(Application.streamingAssetsPath, "config.json");
#endif
            var json = File.ReadAllText(path);
            Config = JsonConvert.DeserializeObject<JSonConfigFile>(json);
            UnityEngine.Debug.Log(Config.IP);
        }

        private void CreateConfigFile()
        {
            string path = "";
#if UNITY_EDITOR
            path = Path.Combine(Application.streamingAssetsPath, "config.json");
#else
            path = Path.Combine(Application.dataPath, "config.json");
#endif
            JSonConfigFile file = new JSonConfigFile()
            {
                IP = "31.43.180.52",
                PORT = 9001,
                RequestURI = "http://176.100.14.170:9000/users/login"
            };
            File.WriteAllText(path, JsonConvert.SerializeObject(file));
        }
    }
}