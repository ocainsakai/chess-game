using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public BattleManager battleManager;
    public Button attackButton;
    public Button defendButton;
    public Button healButton;

    private void Start()
    {
        attackButton.onClick.AddListener(() => battleManager.player.TakeDamage(battleManager.enemy.attackPower));
        defendButton.onClick.AddListener(() => battleManager.player.isDefending = true);
        healButton.onClick.AddListener(() => battleManager.player.Heal(20));
    }
}
