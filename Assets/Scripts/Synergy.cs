using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Synergy", menuName = "Synergy System/Synergy")]
public class SynergyData : ScriptableObject
{
    public string synergyName;
    public List<UnitData> unitsInSynergy;
    public int[] activationThresholds; // Ví dụ: [2, 4, 6]
    public string[] effects; // Hiệu ứng tương ứng
}

