using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramState : MonoBehaviour
{
    public StatesOfProgram StatesOfProgram { get; private set; }

    public void GameStartHandler()
    {
        StatesOfProgram = StatesOfProgram.Game;
    }


    public void MenuStartHandler()
    {
        StatesOfProgram = StatesOfProgram.Menu;
    }
    public void CastleUIWindowsStartHandler()
    {
        StatesOfProgram = StatesOfProgram.CastleUIWindows;
    }

    public void BattleStartHandler()
    {
        StatesOfProgram = StatesOfProgram.Battle;
    }

    public void CastleStartHandler()
    {
        StatesOfProgram = StatesOfProgram.Castle;
    }

    public void HeroPanelStartHandler()
    {
        StatesOfProgram = StatesOfProgram.HeroPanel;
    }

    public void SetStatesOfProgram(StatesOfProgram statesOfProgram)
    {
        if(statesOfProgram == StatesOfProgram.Battle)
        {
            Invoke(nameof(EnterInGameState), 0.3f);
        }
        else
        {
            StatesOfProgram = statesOfProgram;
        }
    }

    public void LoadingStartHandler()
    {
        StatesOfProgram = StatesOfProgram.Loading;
    }


    private void EnterInGameState()
    {
        StatesOfProgram = StatesOfProgram.Battle;
    }

}


public enum StatesOfProgram
{
    Menu,
    Game,
    Battle,
    Loading,
    Castle,
    CastleUIWindows,
    HeroPanel,
}