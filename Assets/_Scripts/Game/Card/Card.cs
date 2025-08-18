using System;
using UnityEngine;

public class Card : MonoBehaviour
{
    public event Action<CardState> OnCardStateChanged;
    public enum CardState
    {
        None,
        OnConveyor,
        InBuffer,
        InBox
    }

    [SerializeField] private CardState _cardState = CardState.None;
  
    [SerializeField] private CardDataSo cardData;

    private void Start()
    {
        ChangeCardState(CardState.OnConveyor); // Init card on coveyor
    }

    public void ChangeCardState(CardState cardState)
    {      
        this._cardState = cardState;
        OnCardStateChanged?.Invoke(_cardState);
    }

    public CardDataSo GetCardDataSO() => cardData;
}
