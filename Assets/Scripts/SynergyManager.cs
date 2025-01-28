using System.Collections.Generic;
using UnityEngine;

public class SynergyManager : MonoBehaviour
{
    public List<SynergyData> synergies;
    private List<Unit> deployedUnits = new List<Unit>();

    public void AddUnit(Unit unit)
    {
        deployedUnits.Add(unit);
        CheckSynergies();
    }

    void CheckSynergies()
    {
        foreach (SynergyData synergy in synergies)
        {
            int count = 0;
            foreach (Unit unit in deployedUnits)
            {
                if (synergy.unitsInSynergy.Contains(unit.data))
                {
                    count++;
                }
            }
            // Áp dụng hiệu ứng dựa trên count và thresholds
            for (int i = 0; i < synergy.activationThresholds.Length; i++)
            {
                if (count >= synergy.activationThresholds[i])
                {
                    ApplyEffect(synergy.effects[i]);
                }
            }
        }
    }

    void ApplyEffect(string effect)
    {
        // Logic áp dụng hiệu ứng (ví dụ: tăng sát thương)
    }
}