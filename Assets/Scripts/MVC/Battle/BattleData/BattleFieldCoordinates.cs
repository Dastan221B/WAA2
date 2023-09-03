using System;
using Newtonsoft.Json;

[Serializable]
public class BattleFieldCoordinates
{
    public int x;
    public int y;

    [JsonConstructor]
    public BattleFieldCoordinates(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public float GetDistance(BattleFieldCoordinates first, BattleFieldCoordinates second)
    {
        return (float)Math.Sqrt(Math.Pow(Math.Abs(first.x - second.x), 2) + Math.Pow(Math.Abs(first.y - second.y), 2));
    }

    public static bool operator ==(BattleFieldCoordinates first, BattleFieldCoordinates sec) => first.x == sec.x && first.y == sec.y;

    public static bool operator !=(BattleFieldCoordinates first, BattleFieldCoordinates sec) => first.x != sec.x || first.y != sec.y;
}