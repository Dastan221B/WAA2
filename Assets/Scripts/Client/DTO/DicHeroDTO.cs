using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Client.DTO
{
    public class DicHeroDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public int attack { get; set; }
        public int defence { get; set; }
        public int power { get; set; }
        public int knowledge { get; set; }
        public int mapObjectId { get; set; }
        public int castleId { get; set; }
    }
}