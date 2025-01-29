using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public Dictionary<StatType, int> stats = new Dictionary<StatType, int>
    {
        { StatType.Health, 100 },
        { StatType.Mana, 50 },
        { StatType.Attack, 10 },
        { StatType.Defense, 5 },
        { StatType.Speed, 3 },
        { StatType.CriticalChance, 10 }
    };

    public int GetStat(StatType type) => stats.ContainsKey(type) ? stats[type] : 0;

    public void ModifyStat(StatType type, int amount)
    {
        if (stats.ContainsKey(type))
            stats[type] += amount;
    }
}

public enum StatType
{
    Health,
    Mana,
    Attack,
    Defense,
    Speed,
    CriticalChance
}
