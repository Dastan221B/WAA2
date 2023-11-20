using Assets.Scripts.Client.GameClient;
using Assets.Scripts.Interfaces.Game;
using Assets.Scripts.MVC.Battle;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MVC.Game
{
    public class GameAndBattleCommandsSender : ISendStartTurnRequest
    {
        private GameMessageSender _gameMessageSender;
        private GameModel _gameModel;
        private BattleModel _battleModel;

        public GameAndBattleCommandsSender(GameMessageSender gameMessageSender, GameModel gameModel)
        {
            _gameMessageSender = gameMessageSender;
            _gameModel = gameModel;
        }

        public void Init(BattleModel battleModel)
        {
            _battleModel = battleModel;
        }
        public void SendStartTurnRequest()
        {
            StartEndTurnRequest request = new StartEndTurnRequest(_gameModel.GameSessionID);
            _gameMessageSender.SendMessage(OutputGameHeaders.START_TURN_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }

        public void SendEndTurnRequest()
        {
            StartEndTurnRequest request = new StartEndTurnRequest(_gameModel.GameSessionID);
            _gameMessageSender.SendMessage(OutputGameHeaders.END_TURN_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }
        public void SendMoveHeroRequestWithInteractable(Vector2Int position, string interactiveMapObjectId, GameMapObjectType gameMapObjectType)
        {
            
            _gameModel.HeroStartMove();
            MoveHeroRequestWithInteractive request = new MoveHeroRequestWithInteractive(
                                 _gameModel.GameSessionID, _gameModel.SelectedHero.MapObjectID, position.x,
                                 position.y, interactiveMapObjectId, gameMapObjectType.ToString());
            _gameMessageSender.SendMessage(OutputGameHeaders.MOVE_HERO_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }

        public void SendMoveHeroRequest(Vector2Int position)
        {
            _gameModel.HeroStartMove();
            MoveHeroRequest request = new MoveHeroRequest(_gameModel.GameSessionID,
                             _gameModel.SelectedHero.MapObjectID, position.x, position.y);
            _gameMessageSender.SendMessage(OutputGameHeaders.MOVE_HERO_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }

        public void SendMoveCreatureRequest(BattleFieldCoordinates battleFieldCoordinates)
        {
            var battleActionRequest = new BattleMoveRequest(_gameModel.GameSessionID, _battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldObjectId, battleFieldCoordinates);
            _gameMessageSender.SendMessage(OutputGameHeaders.BATTLE_ACTION_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(battleActionRequest));

        }

        public void SendMoveAndHitRequest(BattleFieldCoordinates battleFieldCoordinates ,int creatureId)
        {
            var battleActionRequest = new BattleMoveAndHitRequest(_gameModel.GameSessionID, _battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldObjectId, creatureId, battleFieldCoordinates);
            _gameMessageSender.SendMessage(OutputGameHeaders.BATTLE_ACTION_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(battleActionRequest));
        }

        public void SendHitMeleeRequest(BattleFieldCoordinates battleFieldCoordinates, int creatureId)
        {
            var battleActionRequest = new BattleHitMeleeRequest(_gameModel.GameSessionID, _battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldObjectId, creatureId, battleFieldCoordinates);
            _gameMessageSender.SendMessage(OutputGameHeaders.BATTLE_ACTION_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(battleActionRequest));
        }

        public void SendHitRangedRequest(int creatureId)
        {
            var battleActionRequest = new BattleHitRangeRequest(_gameModel.GameSessionID, _battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldObjectId, creatureId);
            _gameMessageSender.SendMessage(OutputGameHeaders.BATTLE_ACTION_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(battleActionRequest));
        }

        public void SendCreatureBlockActivationRequest()
        {
            BattleCreatureBlockRequest battleActionRequest = new BattleCreatureBlockRequest(_gameModel.GameSessionID, _battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldObjectId);
            _gameMessageSender.SendMessage(OutputGameHeaders.BATTLE_ACTION_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(battleActionRequest));
        }

        public void SendCreatureWaitActivationRequest()
        {
            BattleCreatureWaitRequest battleActionRequest = new BattleCreatureWaitRequest(_gameModel.GameSessionID, _battleModel.ActiveCreatureStackBattleObjectFullInfo.battleFieldObjectId);
            _gameMessageSender.SendMessage(OutputGameHeaders.BATTLE_ACTION_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(battleActionRequest));
        }
        public void SendCastleInfoRequest(string objectId)
        {
            GetCastleInfoRequest request = new GetCastleInfoRequest(_gameModel.GameSessionID, objectId);
            _gameMessageSender.SendMessage(OutputGameHeaders.GET_CASTLE_INFO_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(request));
        }


        public void SendSumbitTrade(string requestID, string recieverID , List<ArmySlotInfo> requestHeroArmyInfoSlots, List<ArmySlotInfo> recieverHeroArmyInfoSlots)
        {
            SubmitTradeRequest submit_trade_request = new SubmitTradeRequest(_gameModel.GameSessionID);
            submit_trade_request.objectId = requestID;
            submit_trade_request.receiverHeroObjectId = recieverID;
            submit_trade_request.receiverHeroArmy = recieverHeroArmyInfoSlots;
            submit_trade_request.requesterHeroArmy = requestHeroArmyInfoSlots;
            _gameMessageSender.SendMessage(OutputGameHeaders.SUBMIT_TRADE_REQUEST, Newtonsoft.Json.JsonConvert.SerializeObject(submit_trade_request));
        }
    }
}