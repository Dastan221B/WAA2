using System.Collections;
using UnityEngine;

[System.Serializable]
public class HeroObject : AbstractMapObject
{
    private long _dicHeroId;
    private int _attack;
    private int _defence;
    private int _knowledge;
    private int _power;
    private int _level;

    public long DicHeroId
    {
        get => _dicHeroId;
        set
        {
            if (value > 0)
                _dicHeroId = value;
        }
    }
    public int Attack
    {
        get => _attack;
        set
        {
            if (value > 0)
                _attack = value;
        }
    }
    public int Defence
    {
        get => _defence;
        set
        {
            if (value > 0)
                _defence = value;
        }
    }
    public int Knowledge
    {
        get => _knowledge;
        set
        {
            if (value > 0)
                _knowledge = value;
        }
    }
    public int Power
    {
        get => _power;
        set
        {
            if (value > 0)
                _power = value;
        }
    }
    public int Level
    {
        get => _level;
        set
        {
            if (value > 0)
                _level = value;
        }
    }
}