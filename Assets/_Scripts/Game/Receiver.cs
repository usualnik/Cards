
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Receiver : MonoBehaviour
{
    public static Receiver Instance { get; private set; }

    private List<Card> _cardsOnConveyor = new List<Card>(20);
    [SerializeField] private TextMeshProUGUI _receiverCardsAmountText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of Receiver");
        }


    }

    private void Start()
    {
        
        CardsInHandManager.Instance.OnSendCardsToConveyor += CardsInHandManager_OnSendCardsToConveyor;

    }
    private void OnDestroy()
    {
        CardsInHandManager.Instance.OnSendCardsToConveyor -= CardsInHandManager_OnSendCardsToConveyor;

    }

    private void CardsInHandManager_OnSendCardsToConveyor(List<Card> sendedCards)
    {
        StartCoroutine(UpdateReceiverText(sendedCards));
      
    }    

    private IEnumerator UpdateReceiverText(List<Card> sendedCards)
    {
        foreach (Card card in sendedCards)
        {
            if (!_cardsOnConveyor.Contains(card))
            {
                _cardsOnConveyor.Add(card);
                _receiverCardsAmountText.text = string.Format("{0} / {1}", _cardsOnConveyor.Count, _cardsOnConveyor.Capacity);
                yield return new WaitForSeconds(0.1f);
            }
            
        }       
       
    }
}
