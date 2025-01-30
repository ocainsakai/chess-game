using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Participant : MonoBehaviour
{
    public List<Card> hand = new List<Card>();
    protected DeckManager deckManager;

    public int currentScore;
    public int balance;
    public int currentBet;

    public virtual void InitializeHand()
    {
        deckManager = GetComponent<DeckManager>();

        hand.Clear();
        currentScore = 0;
    }

    public void AddCard(Card card)
    {
        hand.Add(card);
        CalculateScore();
    }

    protected void CalculateScore()
    {
        currentScore = 0;
        int aceCount = 0;

        foreach (Card card in hand)
        {
            int value = card.GetBlackjackValue();
            currentScore += value;
            if (value == 1) aceCount++;
        }

        while (aceCount > 0 && currentScore <= 11)
        {
            currentScore += 10;
            aceCount--;
        }
    }

    public bool HasBlackjack() => currentScore == 21 && hand.Count == 2;
    public bool IsBusted() => currentScore > 21;
}


