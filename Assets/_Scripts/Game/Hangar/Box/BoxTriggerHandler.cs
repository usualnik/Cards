using JetBrains.Annotations;
using UnityEngine;

public class BoxTriggerHandler : MonoBehaviour
{    
    public enum WaitingForCard
    {
        Red,
        Blue
    }
    [SerializeField] private WaitingForCard boxType;

    private Box box;
    

    private void Start()
    {
        box = GetComponentInParent<Box>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Card>(out Card card))
        {
            switch (boxType)
            {
                case WaitingForCard.Red:
                    if (card.GetCardDataSO().Title == "RedCard")
                    {
                        card.GetComponent<CardFromConveyorAnimation>().SendCardToBoxAnimation(box);
                        box.AddCardToBox(card);

                        if(!box.GetIsFull())
                            Receiver.Instance.RemoveCardFromConveyor(card);
                    }
                    break;
                case WaitingForCard.Blue:
                    if (card.GetCardDataSO().Title == "BlueCard")
                    {
                        card.GetComponent<CardFromConveyorAnimation>().SendCardToBoxAnimation(box);
                        box.AddCardToBox(card);

                        if (!box.GetIsFull())
                            Receiver.Instance.RemoveCardFromConveyor(card);
                        
                    }
                    break;
                default:
                    break;
            }
        }

        
    }

   
}
