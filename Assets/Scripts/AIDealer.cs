using System.Collections;
using UnityEngine;


public class AIDealer : Participant
{
    public IEnumerator PlayTurn()
    {
        while (currentScore < 17)
        {
            yield return new WaitForSeconds(1f);
            AddCard(this.deckManager.DrawCard());
        }
    }

}
