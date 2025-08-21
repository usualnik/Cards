using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	
	[SerializeField] private Transform _headTransform;

	private List<Card> cardsInBox = new List<Card>(6);

	private Vector3 _headTransformOffset = new Vector3(0, 0, -0.2f);

    private const int MaxCardsInBox = 6;
   
    public void AddCardToBox(Card card)
	{
        if (cardsInBox.Count >= MaxCardsInBox)
        {
            Invoke(nameof(DestroyIfMaxCards), 0.5f);
        }
        else
        {

            //if (cardsInBox.Count > 0)
            //{
            //    Card lastCard = cardsInBox[cardsInBox.Count - 1];
            //    card.transform.position = lastCard.transform.position + _headTransformOffset;
            //}
            //else
            //{
            //    card.transform.position = _headTransform.position;
            //}

            card.transform.Rotate(0, 90, 0);

            cardsInBox.Add(card);
            card.ChangeCardState(Card.CardState.InBox);
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

        Invoke(nameof(ReInitializeBox), 1f);
    }

    private void ReInitializeBox()
    {
        gameObject.SetActive(true);
    }

    public Transform GetBoxHeadTransform() => _headTransform;
    public int GetCardsInBoxCount() => cardsInBox.Count;
}
