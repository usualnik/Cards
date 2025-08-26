using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Splines;

public class Receiver : MonoBehaviour
{
    public static Receiver Instance { get; private set; }

    [SerializeField] private List<Card> _cardsOnConveyor = new List<Card>(20);
    [SerializeField] private TextMeshProUGUI _receiverCardsAmountText;

    private const int MAX_CARDS_ON_CONVEYOR = 20;

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
        Buffer.Instance.OnSendCardsToConveyor += Buffer_OnSendCardsToConveyor;
        Dumper.Instance.OnDumpCard += Dumper_OnDumpCard;
    }

    private void OnDestroy()
    {
        CardsInHandManager.Instance.OnSendCardsToConveyor -= CardsInHandManager_OnSendCardsToConveyor;
        Buffer.Instance.OnSendCardsToConveyor -= Buffer_OnSendCardsToConveyor;
        Dumper.Instance.OnDumpCard -= Dumper_OnDumpCard;
    }

    private void FixedUpdate()
    {       
       CleanNullCardsFromConveyor();
    }

    private void Dumper_OnDumpCard(Card card)
    {
        RemoveCardFromConveyor(card);
    }

    private void CardsInHandManager_OnSendCardsToConveyor(List<Card> sendedCards)
    {
        AddCardsToConveyor(sendedCards);
        StartCoroutine(UpdateReceiverText(sendedCards));
    }

    private void Buffer_OnSendCardsToConveyor(List<Card> sendedCards)
    {
        AddCardsToConveyor(sendedCards);
        StartCoroutine(UpdateReceiverText(sendedCards));
    }

    private void AddCardsToConveyor(List<Card> sendedCards)
    {
        foreach (Card card in sendedCards)
        {
           
            if (card != null && card.gameObject != null && !_cardsOnConveyor.Contains(card))
            {
                _cardsOnConveyor.Add(card);

                card.OnCardDestroyed += HandleCardDestroyed;
            }
        }
    }

    private IEnumerator UpdateReceiverText(List<Card> sendedCards)
    {
        yield return new WaitForSeconds(0.1f);
        UpdateReceiverText();
    }

    private void UpdateReceiverText()
    {
        
        CleanNullCardsFromConveyor();
        _receiverCardsAmountText.text = string.Format("{0} / {1}", _cardsOnConveyor.Count, MAX_CARDS_ON_CONVEYOR);
    }

    
    private void CleanNullCardsFromConveyor()
    {
        for (int i = _cardsOnConveyor.Count - 1; i >= 0; i--)
        {
            if (_cardsOnConveyor[i] == null || _cardsOnConveyor[i].gameObject == null)
            {
                _cardsOnConveyor.RemoveAt(i);
            }
        }
    }
        
    private void HandleCardDestroyed(Card card)
    {
        RemoveCardFromConveyor(card);
    }

    public bool GetCanAcceptCards()
    {
        CleanNullCardsFromConveyor();
        return _cardsOnConveyor.Count < MAX_CARDS_ON_CONVEYOR;
    }

    public bool GetCanAcceptMore(int cardsToBeAdded)
    {
        CleanNullCardsFromConveyor();
        return _cardsOnConveyor.Count + cardsToBeAdded <= MAX_CARDS_ON_CONVEYOR;
    }

    public void RemoveCardFromConveyor(Card card)
    {
        if (card != null)
        {
            // Отписываемся от события
            card.OnCardDestroyed -= HandleCardDestroyed;

            if (_cardsOnConveyor.Contains(card))
            {
                _cardsOnConveyor.Remove(card);
            }
        }
        UpdateReceiverText();
    }
}