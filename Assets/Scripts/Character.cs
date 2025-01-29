using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public CharacterStats stats;
    public int MaxHealth => stats.GetStat(StatType.Health);
    public int Mana => stats.GetStat(StatType.Mana);
    public int attackPower => stats.GetStat(StatType.Attack);
    public int defense => stats.GetStat(StatType.Defense);
    public int currentHealth;
    public int mana = 50;

    public bool isDefending = false;

    void Awake()
    {
        stats = new CharacterStats()
        {

        };
        Debug.Log(MaxHealth);
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDefending)
        {
            damage = Mathf.Max(damage - defense, 0);
            isDefending = false;
        }

        currentHealth -= damage;
        Debug.Log($"{characterName} nhận {damage} sát thương! Máu còn lại: {currentHealth}");
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, MaxHealth);
        Debug.Log($"{characterName} hồi {amount} máu! Máu hiện tại: {currentHealth}");
    }
}