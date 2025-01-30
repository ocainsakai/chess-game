using System.Collections;
using UnityEngine;

public enum GameState { Betting, Dealing, PlayerTurn, DealerTurn, Payout }

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private AIDealer dealer;

    private GameState currentState;
    [SerializeField] private DeckManager deckManager;
    [SerializeField] private UIManager uiManager;

    void Start()
    {
        deckManager = GetComponent<DeckManager>();
        uiManager = GetComponent<UIManager>();
        TransitionToState(GameState.Betting);
        NewGame();
    }

    private void NewGame()
    {
        dealer.AddCard(deckManager.DrawCard());
        player.AddCard(deckManager.DrawCard());
        dealer.AddCard(deckManager.DrawCard());
        player.AddCard(deckManager.DrawCard());
        uiManager.UpdateGameUI(player,dealer);
        Debug.Log(player.hand.Count);
    }
    private void TransitionToState(GameState newState)
    {
        currentState = newState;

        switch (newState)
        {
            case GameState.Betting:
                HandleBettingState();
                break;

            case GameState.Dealing:
                StartCoroutine(HandleDealingState());
                break;

            case GameState.PlayerTurn:
                EnablePlayerControls(true);
                break;

            case GameState.DealerTurn:
                StartCoroutine(HandleDealerTurn());
                break;

            case GameState.Payout:
                CalculatePayout();
                break;
        }
    }
    private void EnablePlayerControls(bool enable)
    {

    }
    private void HandleBettingState()
    {
        player.InitializeHand();
        dealer.InitializeHand();
    }

    private IEnumerator HandleDealingState()
    {
        // Initial deal
        player.AddCard(deckManager.DrawCard());
        dealer.AddCard(deckManager.DrawCard().SetFaceUp(false));
        player.AddCard(deckManager.DrawCard());
        dealer.AddCard(deckManager.DrawCard());

        yield return new WaitForSeconds(1f);

        // Check for natural blackjack
        if (player.HasBlackjack() || dealer.HasBlackjack())
        {
            TransitionToState(GameState.Payout);
        }
        else
        {
            TransitionToState(GameState.PlayerTurn);
        }
    }

    private IEnumerator HandleDealerTurn()
    {
        dealer.hand[0].SetFaceUp(true);
        yield return StartCoroutine(dealer.PlayTurn());
        TransitionToState(GameState.Payout);
    }

    private void CalculatePayout()
    {
        if (player.IsBusted())
        {
            // Player loses bet
        }
        else if (dealer.IsBusted())
        {
            player.balance += player.currentBet * 2;
        }
        else if (player.currentScore > dealer.currentScore)
        {
            player.balance += player.currentBet * 2;
        }
        else if (player.currentScore == dealer.currentScore)
        {
            player.balance += player.currentBet;
        }

        TransitionToState(GameState.Betting);
    }

    // ------------ Player Actions ------------
    public void OnPlayerHit()
    {
        player.AddCard(deckManager.DrawCard());
        if (player.IsBusted()) TransitionToState(GameState.Payout);
    }

    public void OnPlayerStand() => TransitionToState(GameState.DealerTurn);

    public void OnPlayerDoubleDown()
    {
        player.DoubleDown();
        OnPlayerHit();
        OnPlayerStand();
    }
}