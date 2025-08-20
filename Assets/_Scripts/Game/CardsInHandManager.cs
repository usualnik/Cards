using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsInHandManager : MonoBehaviour
{
    public static CardsInHandManager Instance { get; private set; }

    [SerializeField] private List<Card> _firstColumn;
    [SerializeField] private List<Card> _secondColumn;
    [SerializeField] private float _delayBetweenCards = 0.2f; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of Card in hand manager");           
        }
    }

    public void SendPackToConveyor(Card clickedCard)
    {
        string targetCardName = clickedCard.GetCardDataSO().name;

        if (_firstColumn.Contains(clickedCard))
        {
            StartCoroutine(SendMatchingCardsWithDelay(_firstColumn, targetCardName));
        }
        else if (_secondColumn.Contains(clickedCard))
        {
            StartCoroutine(SendMatchingCardsWithDelay(_secondColumn, targetCardName));
        }
        else
        {
            Debug.LogWarning("Clicked card not found in any column");
        }
    }

    private IEnumerator SendMatchingCardsWithDelay(List<Card> column, string targetName)
    {
        
        List<Card> cardsToSend = new List<Card>();

        foreach (var card in column)
        {
            if (card.GetCardDataSO().name == targetName)
            {
                cardsToSend.Add(card);
                card.ChangeCardState(Card.CardState.OnConveyor);
            }
        }
       
        foreach (var card in cardsToSend)
        {
            card.GetComponent<CardFromHandAnimation>().SendCardToConveyorAnimation();
            column.Remove(card);
           
            yield return new WaitForSeconds(_delayBetweenCards);
        }
    }
}