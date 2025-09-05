using JetBrains.Annotations;
using UnityEditor.Recorder;
using UnityEngine;

public class BoxTriggerHandler : MonoBehaviour
{    
    public enum WaitingForCard
    {
        Red,
        Blue,
        Green,
        Pink,
        Yellow,
        Cyan
    }
    [SerializeField] private WaitingForCard _boxType;
   
    private Box box;
    

    private void Start()
    {
        box = GetComponentInParent<Box>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Card>(out Card card))
        {
            switch (_boxType)
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
               
                case WaitingForCard.Green:
                    if (card.GetCardDataSO().Title == "GreenCard")
                    {
                        card.GetComponent<CardFromConveyorAnimation>().SendCardToBoxAnimation(box);
                        box.AddCardToBox(card);

                        if (!box.GetIsFull())
                            Receiver.Instance.RemoveCardFromConveyor(card);

                    }
                    break;
               
                case WaitingForCard.Pink:
                    if (card.GetCardDataSO().Title == "PinkCard")
                    {
                        card.GetComponent<CardFromConveyorAnimation>().SendCardToBoxAnimation(box);
                        box.AddCardToBox(card);

                        if (!box.GetIsFull())
                            Receiver.Instance.RemoveCardFromConveyor(card);

                    }
                    break;
               
                case WaitingForCard.Yellow:
                    if (card.GetCardDataSO().Title == "YellowCard")
                    {
                        card.GetComponent<CardFromConveyorAnimation>().SendCardToBoxAnimation(box);
                        box.AddCardToBox(card);

                        if (!box.GetIsFull())
                            Receiver.Instance.RemoveCardFromConveyor(card);

                    }
                    break;
               
                case WaitingForCard.Cyan:
                    if (card.GetCardDataSO().Title == "CyanCard")
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

   public WaitingForCard GetWaitingForCardType() => _boxType;
}
