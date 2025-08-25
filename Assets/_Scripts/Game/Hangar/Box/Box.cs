using System;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	
    public event Action<Box> OnDestroyBox;

	[SerializeField] private Transform _headTransform;

	[SerializeField] private List<Card> cardsInBox = new List<Card>(6);

	private Vector3 _headTransformOffset = new Vector3(0, 0, -0.2f);

    private const int MaxCardsInBox = 6;

    private bool _isFull;

    private void FixedUpdate()
    {
        if (cardsInBox.Count >= MaxCardsInBox)
        {
            Invoke(nameof(DestroyIfMaxCards), 0.5f);
        }
    }


    public void AddCardToBox(Card card)
	{
        if (cardsInBox.Count >= MaxCardsInBox)
        {
            Invoke(nameof(DestroyIfMaxCards), 0.5f);
        }
        else
        {
            card.transform.Rotate(0, 90, 0);

            if(card != null)
            {
                cardsInBox.Add(card);
                card.ChangeCardState(Card.CardState.InBox);
            }          
        }

    }

    private void DestroyIfMaxCards()
    {
        foreach (Card card in cardsInBox) 
        { 
            Destroy(card.gameObject);
        }

        cardsInBox.Clear();
        gameObject.SetActive(false);
        gameObject.transform.SetParent(null);

        OnDestroyBox?.Invoke(this);
        
    }

    public Transform GetBoxHeadTransform() => _headTransform;
    public int GetCardsInBoxCount() => cardsInBox.Count;
    public bool GetIsFull() => _isFull = cardsInBox.Count < MaxCardsInBox ? false : true;
}
