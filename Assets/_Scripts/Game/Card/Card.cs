using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    public event Action<CardState> OnCardStateChanged;
    public enum CardState
    {
        InHand,
        OnConveyor,
        InBuffer,
        InBox
    }

    [SerializeField] private CardState _cardState = CardState.InHand;
  
    [SerializeField] private CardDataSo cardData;

    private void Start()
    {
        ChangeCardState(CardState.InHand); // Init card in hand
    }

    public void ChangeCardState(CardState cardState)
    {      
        this._cardState = cardState;
        OnCardStateChanged?.Invoke(_cardState);
    }

    public CardDataSo GetCardDataSO() => cardData;
    public CardState GetCardState() => _cardState;
}
