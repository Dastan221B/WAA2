using System;
using UnityEngine;

namespace Assets.Scripts.JSONConfig
{
    [Serializable]
    public class JSonConfigFile
    {
        public string IP;
        public int PORT;
        public string RequestURI;
    }
}