using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] playerControls;
    [SerializeField] private TextMeshProUGUI balanceText;
    [SerializeField] private TextMeshProUGUI betText;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform playerHandArea;
    [SerializeField] private Transform dealerHandArea;

    public void UpdateGameUI(PlayerController player, AIDealer dealer)
    {
        balanceText.text = $"Balance: ${player.balance}";
        betText.text = $"Current Bet: ${player.currentBet}";
        UpdateHandDisplay(player.hand, playerHandArea);
        UpdateHandDisplay(dealer.hand, dealerHandArea);
    }

    private void UpdateHandDisplay(List<Card> hand, Transform container)
    {
        foreach (Transform child in container) Destroy(child.gameObject);

        foreach (Card card in hand)
        {
            GameObject newCard = Instantiate(cardPrefab, container);
            newCard.GetComponent<Image>().sprite = card.isFaceUp ?
                card.data.frontSprite :
                card.data.backSprite;
        }
    }

    public void EnableControls(bool enable)
    {
        foreach (GameObject control in playerControls)
        {
            control.SetActive(enable);
        }
    }
}

