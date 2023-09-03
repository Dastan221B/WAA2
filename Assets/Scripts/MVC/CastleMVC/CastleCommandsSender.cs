using Assets.Scripts.Client.GameClient;
using Assets.Scripts.Client.GameClient.RquestsAndDTO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC
{
    public class CastleCommandsSender
    {
        private GameMessageSender _gameMessageSender;
        private GameModel _gameModel;


        public CastleCommandsSender(GameMessageSender gameMessageSender, GameModel gameModel)
        {
            _gameMessageSender = gameMessageSender;
            _gameModel = gameModel;
        }

        public void SendAddCastleBuildingRequest(string objectId, int buildingId)
        {
            AddCastleBuildingRequest request = new AddCastleBuildingRequest(_gameModel.GameSessionID, objectId, buildingId);
            _gameMessageSender.SendMessage(OutputGameHeaders.ADD_CASTLE_BUILDING_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }

        public void SendHireCastleCreatureRequest(string objectId, int creatureId, int amount,
            List<ArmySlotInfo> castleCreatures, List<ArmySlotInfo> armyInGarrison)
        {
            HireCastleCreatureRequest request = new HireCastleCreatureRequest(_gameModel.GameSessionID, objectId, creatureId, amount, castleCreatures, armyInGarrison);
            _gameMessageSender.SendMessage(OutputGameHeaders.HIRE_CASTLE_CREATURE_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }
        
        public void SendCastleInfoRequest(string objectId)
        {
            GetCastleInfoRequest request = new GetCastleInfoRequest(_gameModel.GameSessionID, objectId);
            _gameMessageSender.SendMessage(OutputGameHeaders.GET_CASTLE_INFO_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }

        public void SendCastleFullInfoRequest(string objectId)
        {
            GetCastleFullInfoRequest request = new GetCastleFullInfoRequest(_gameModel.GameSessionID, objectId);
            _gameMessageSender.SendMessage(OutputGameHeaders.GET_CASTLE_FULL_INFO_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }

        public void SendHeroMoveToCastleRequest(string objectId, List<ArmySlotInfo> castleCreatures, List<ArmySlotInfo> armyInGarrison)
        {
            MoveHeroToCastleRequest request = new MoveHeroToCastleRequest(_gameModel.GameSessionID, objectId, castleCreatures, armyInGarrison);
            _gameMessageSender.SendMessage(OutputGameHeaders.MOVE_HERO_TO_CASTLE_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }

        public void SendHeroMoveToGarissonRequest(string objectId, List<ArmySlotInfo> castleCreatures, List<ArmySlotInfo> armyInGarrison)
        {
            MoveHeroToGarrisonRequest request = new MoveHeroToGarrisonRequest(_gameModel.GameSessionID, objectId, castleCreatures, armyInGarrison);
            _gameMessageSender.SendMessage(OutputGameHeaders.MOVE_HERO_TO_GARRISON_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }

        public void SendLeaveCastleRequest(string objectId, string heroObjectId, List<ArmySlotInfo> castleCreatures,
                List<ArmySlotInfo> armyInGarrison)
        {
            LeaveCastleRequest request = new LeaveCastleRequest(_gameModel.GameSessionID, objectId, castleCreatures, heroObjectId, armyInGarrison);
            _gameMessageSender.SendMessage(OutputGameHeaders.LEAVE_CASTLE_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }

    }
}