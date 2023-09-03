using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interfaces.Game
{
    interface IMap
    {
        float[,] Generate();
    }
}