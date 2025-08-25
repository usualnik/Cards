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
        switch (_card.GetCardState())
        {
            case Card.CardState.InHand:
                if (_card != null && CardsInHandManager.Instance != null && Receiver.Instance.GetCanAcceptCards())
                {
                    CardsInHandManager.Instance.SendPackToConveyor(_card);                    
                }
                break;
            case Card.CardState.OnConveyor:
                break;
            case Card.CardState.InBuffer:
                if (_card != null && Buffer.Instance != null && Receiver.Instance.GetCanAcceptCards())
                {
                    Buffer.Instance.SendPackToConveyor(_card);                    
                }
                break;
            case Card.CardState.InBox:
                break;
        }

       
    }
}