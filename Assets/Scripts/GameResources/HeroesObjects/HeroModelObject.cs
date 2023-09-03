using Assets.Scripts.GameResources;
using Assets.Scripts.MVC.HeroPanel;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroModelObject : GameMapObject
{
    [SerializeField] private GameObject _selectPlatform;
    [SerializeField] private SystemColors _color;
    public long DicHeroId { get; private set; }
    public int Attack { get; private set; }
    public int Defence { get; private set; }
    public int Knowledge { get; private set; }
    public int Power { get; private set; }
    public int Level { get; private set; }
    public int MovePointsLeft { get; private set; }
    public Cell LastCellStayed { get; private set; }
    public HeroObjectFullInfo HeroObjectFullInfo { get; private set; }
    public bool InCastle { get; private set; }
    private List<ArmySlotInfo> _armySlotInfos = new List<ArmySlotInfo>();
    private HeroPanelStatsWindow _inGameBarHeroPanelStatsWindow;
    public event Action<int> OnChangeMovePoints;
    public event Action<int> OnInitedMovePoints;
    public bool HeroInTurn { get; set; }
    public Hero Hero { get; private set; }

    public IReadOnlyList<ArmySlotInfo> ArmySlotInfos => _armySlotInfos;
    
    public void Init(HeroObject heroObject , Hero hero)
    {
        DicHeroId = heroObject.DicHeroId;
        Attack = heroObject.Attack;
        Defence = heroObject.Defence;
        Knowledge = heroObject.Knowledge;
        Power = heroObject.Power;
        Level = heroObject.Level;
        Hero = hero;
        _inGameBarHeroPanelStatsWindow = FindObjectOfType<HeroPanelStatsWindow>();
    }

    public void Init(HeroObjectFullInfo heroObject)
    {
        HeroObjectFullInfo = heroObject;
        DicHeroId = heroObject.dicHeroId;
        Attack = heroObject.attack;
        Defence = heroObject.defence;
        Knowledge = heroObject.knowledge;
        Power = heroObject.power;
        Level = heroObject.level;
        _armySlotInfos = heroObject.army;
        OnInitedMovePoints?.Invoke(heroObject.movePoints);
        SetMovePointsLeft(heroObject.movePoints);
    }

    public void SetArmySlots(List<ArmySlotInfo> armySlotInfos)
    {
        if(armySlotInfos != null)
            _armySlotInfos = armySlotInfos;
    }

    public void EnterInCastle()
    {
        InCastle = true;
    }

    public void ExitFromCastle()
    {
        InCastle = false;
    }

    public void SetCellStayed(Cell cell)
    {
        if(LastCellStayed != null)
            LastCellStayed.ResetHeroModelObject();
        LastCellStayed = cell;
        LastCellStayed.SetModelObject(this);
    }

    public void SelectHero()
    {
        _selectPlatform.SetActive(true);
    }

    public void UnselectHero()
    {
        _selectPlatform.SetActive(false);
    }

    public void SetMovePointsLeft(int movePoints)
    {
        Debug.Log("MovePointsLeft " + MovePointsLeft);
        if (movePoints < 0)
            movePoints = 0;
        MovePointsLeft = movePoints;
        OnChangeMovePoints?.Invoke(MovePointsLeft);
    }

    public virtual void Move()
    {
        Debug.Log("Move 1");

    }

    public virtual void Idle()
    {
    }
}