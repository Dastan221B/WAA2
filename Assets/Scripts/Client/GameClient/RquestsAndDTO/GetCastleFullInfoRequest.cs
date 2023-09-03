using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Client.GameClient.RquestsAndDTO
{
    public class GetCastleFullInfoRequest : GameSessionIdBasedRequest
    {
        public string objectId;
        public string gameMapObjectType = "Castle";

        public GetCastleFullInfoRequest(string gameSessionId, string objectId) : base(gameSessionId)
        {
            this.gameSessionId = gameSessionId;
            this.objectId = objectId;
        }
    }

}