using UnityEngine;
public class PlayerController : Participant
{
    public void PlaceBet(int amount)
    {
        currentBet = Mathf.Min(amount, balance);
        balance -= currentBet;
    }

    public void DoubleDown()
    {
        if (balance >= currentBet)
        {
            balance -= currentBet;
            currentBet *= 2;
        }
    }
}