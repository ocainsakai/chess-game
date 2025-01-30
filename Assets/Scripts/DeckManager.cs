using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private CardData[] cardConfigurations;
    [SerializeField] private int numberOfDecks = 6;
    [SerializeField] private int reshuffleThreshold = 20;

    private Queue<Card> activeDeck = new Queue<Card>();
    private List<Card> discardPile = new List<Card>();

    void Start() => InitializeNewDeck();

    private void InitializeNewDeck()
    {
        List<Card> newDeck = new List<Card>();

        for (int i = 0; i < numberOfDecks; i++)
        {
            foreach (CardData data in cardConfigurations)
            {
                newDeck.Add(new Card(data));
            }
        }

        ShuffleCards(newDeck);
        activeDeck = new Queue<Card>(newDeck);
        Debug.Log(activeDeck.Count);
    }

    private void ShuffleCards(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            int randomIndex = Random.Range(i, cards.Count);
            Card temp = cards[i];
            cards[i] = cards[randomIndex];
            cards[randomIndex] = temp;
        }
    }

    public Card DrawCard()
    {
        if (activeDeck.Count < reshuffleThreshold)
        {
            ReplenishDeck();
        }

        return activeDeck.Dequeue();
    }

    private void ReplenishDeck()
    {
        ShuffleCards(discardPile);
        foreach (Card card in discardPile)
        {
            activeDeck.Enqueue(card);
        }
        discardPile.Clear();
    }

    public void DiscardCards(IEnumerable<Card> cards) => discardPile.AddRange(cards);
}