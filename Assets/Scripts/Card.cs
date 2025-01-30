using UnityEngine;

[System.Serializable]
public struct CardData
{
    public string identifier;
    public int value;
    public Suit suit;
    public Sprite frontSprite;
    public Sprite backSprite;
}

public enum Suit { Hearts, Diamonds, Clubs, Spades }

public class Card
{
    public CardData data;
    public bool isFaceUp;

    public Card(CardData data)
    {
        this.data = data;
        isFaceUp = true;
    }
    public Card SetFaceUp(bool value)
    {
        this.isFaceUp = value;
        return this;
    }
    public int GetBlackjackValue() => data.value > 10 ? 10 : data.value;
}

