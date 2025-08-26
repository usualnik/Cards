using System;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public event Action<Box> OnDestroyBox;

    [SerializeField] private Transform _headTransform;
    [SerializeField] private List<Card> cardsInBox = new List<Card>(6);

    private const int MaxCardsInBox = 6;
    private bool _isDestroyScheduled = false;

    private void FixedUpdate()
    {
       
        if (cardsInBox.Count >= MaxCardsInBox && !_isDestroyScheduled)
        {
            ScheduleDestruction();
        }
    }

    public void AddCardToBox(Card card)
    {
        if (card == null) return;

        
        if (cardsInBox.Count >= MaxCardsInBox || _isDestroyScheduled)
        {
            return; 
        }

        card.transform.Rotate(0, 90, 0);
        AudioManager.Instance.Play("SlotTouched");
        cardsInBox.Add(card);
        card.ChangeCardState(Card.CardState.InBox);

        
        if (cardsInBox.Count >= MaxCardsInBox && !_isDestroyScheduled)
        {
            ScheduleDestruction();
        }
    }

    private void ScheduleDestruction()
    {
        _isDestroyScheduled = true;
        Invoke(nameof(DestroyIfMaxCards), 0.5f);
    }

    private void DestroyIfMaxCards()
    {
        if (!_isDestroyScheduled) return;

        Debug.Log("Box destroyed");
        AudioManager.Instance.Play("SlotFilled");
       
        foreach (Card card in cardsInBox)
        {
            if (card != null && card.gameObject != null)
            {
                Destroy(card.gameObject);
            }
        }

        cardsInBox.Clear();
        gameObject.SetActive(false);
        gameObject.transform.SetParent(null);

        OnDestroyBox?.Invoke(this);
        _isDestroyScheduled = false;
    }

    public Transform GetBoxHeadTransform() => _headTransform;
    public int GetCardsInBoxCount() => cardsInBox.Count;

    public bool GetIsFull()
    {
        return cardsInBox.Count >= MaxCardsInBox;
    }
}