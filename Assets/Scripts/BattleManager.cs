using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public Character player;
    public Character enemy;

    public GameObject actionPanel;
    public TextMeshProUGUI battleLog;

    private bool isPlayerTurn = true;

    void Start()
    {
        StartCoroutine(BattleLoop());
    }

    IEnumerator BattleLoop()
    {
        while (player.currentHealth > 0 && enemy.currentHealth > 0)
        {
            if (isPlayerTurn)
            {
                ShowPlayerActions();
                yield return new WaitUntil(() => !isPlayerTurn);
            }
            else
            {
                EnemyTurn();
                yield return new WaitForSeconds(1.5f);
                isPlayerTurn = true;
            }
        }

        EndBattle();
    }

    void ShowPlayerActions()
    {
        actionPanel.SetActive(true);
    }

    public void PlayerAttack()
    {
        int damage = player.attackPower;
        enemy.TakeDamage(damage);
        battleLog.text += $"\nBạn tấn công gây {damage} sát thương!";
        EndPlayerTurn();
    }

    public void PlayerDefend()
    {
        player.isDefending = true;
        battleLog.text += "\nBạn phòng thủ!";
        EndPlayerTurn();
    }

    public void PlayerHeal()
    {
        player.Heal(20);
        battleLog.text += "\nBạn hồi 20 máu!";
        EndPlayerTurn();
    }

    void EnemyTurn()
    {
        int randomAction = Random.Range(0, 3);

        switch (randomAction)
        {
            case 0:
                int damage = enemy.attackPower;
                player.TakeDamage(damage);
                battleLog.text += $"\nKẻ địch tấn công gây {damage} sát thương!";
                break;

            case 1:
                enemy.isDefending = true;
                battleLog.text += "\nKẻ địch phòng thủ!";
                break;

            case 2:
                enemy.Heal(15);
                battleLog.text += "\nKẻ địch hồi 15 máu!";
                break;
        }
    }

    void EndPlayerTurn()
    {
        actionPanel.SetActive(false);
        isPlayerTurn = false;
    }

    void EndBattle()
    {
        actionPanel.SetActive(false);
        battleLog.text += player.currentHealth > 0 ?
            "\n\nBạn đã chiến thắng!" :
            "\n\nBạn đã thua cuộc!";
    }
}

