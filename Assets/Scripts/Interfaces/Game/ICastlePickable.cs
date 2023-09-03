using Assets.Scripts.MVC.Game.Views.UI;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Interfaces.Game
{
    public interface ICastlePickable
    {
        void PickCastle(out CastleIcon castleIcon);
    }
}