using UnityEngine;

public class CardClick : MonoBehaviour
{
    private Card _card;

    private void Awake()
    {
        _card = GetComponent<Card>();        
    }

    private void OnMouseDown()
    {
        if (_card != null && CardsInHandManager.Instance != null)
        {
            CardsInHandManager.Instance.SendPackToConveyor(_card);
        }
    }
}