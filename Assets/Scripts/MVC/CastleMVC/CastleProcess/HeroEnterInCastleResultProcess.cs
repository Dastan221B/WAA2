using Assets.Scripts.MVC.Game.GameProcces;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MVC.CastleMVC.CastleProcess
{
    public class HeroEnterInCastleResultProcess
    {
        private CastleCommandsSender _castleCommandsSender;
        private MoveHeroInfoWithMovePointsProcess _moveHeroInfoWithMovePointsProcess;

        public HeroEnterInCastleResultProcess(CastleCommandsSender castleCommandsSender, MoveHeroInfoWithMovePointsProcess moveHeroInfoWithMovePointsProcess)
        {
            _castleCommandsSender = castleCommandsSender;
            _moveHeroInfoWithMovePointsProcess = moveHeroInfoWithMovePointsProcess;
        }

        public void EnterInCastle(MessageInput message)
        {
            HeroEnteredCastleResult heroEnteredCastle = Newtonsoft.Json.JsonConvert.DeserializeObject<HeroEnteredCastleResult>(message.body);
            _moveHeroInfoWithMovePointsProcess.MoveHeroAndAndActionBeforeEndingMove(heroEnteredCastle.movementPath, heroEnteredCastle.heroId, heroEnteredCastle.movePointsLeft,
                () => _castleCommandsSender.SendCastleInfoRequest(heroEnteredCastle.castleId));
            Debug.Log("heroEnteredCastle.castleId " + heroEnteredCastle.castleId + " heroEnteredCastle.heroId " + heroEnteredCastle.heroId);
        }
    }
}