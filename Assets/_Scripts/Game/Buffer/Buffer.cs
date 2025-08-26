using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffer : MonoBehaviour
{
   
    public static Buffer Instance { get; private set; }
    public event Action<List<Card>> OnSendCardsToConveyor;
    public event Action OnBufferFull;

    [SerializeField] private Transform _headPos;
    [SerializeField] private float _delayBetweenCards = 0.2f;

    private List<Card> _cardsInBuffer = new List<Card>(); // MAX capacity - 20 card
    private Vector3 _bufferCardOffset = new Vector3(0.2f,0,0);


    private const int MAX_CARDS_IN_BUFFER = 20;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("More than one instance of Buffer");
        }
    }

    void Start()
    {
        Dumper.Instance.OnDumpCard += Dumper_OnDumpCard;
    }
    private void OnDestroy()
    {
        Dumper.Instance.OnDumpCard -= Dumper_OnDumpCard;
    }

    private void Dumper_OnDumpCard(Card card)
    {
        PutCardInBuffer(card);
    }

    private void PutCardInBuffer(Card card)
    {
        if (_cardsInBuffer.Count <= MAX_CARDS_IN_BUFFER)
        {
            _cardsInBuffer.Add(card);
            card.ChangeCardState(Card.CardState.InBuffer);
            card.GetComponent<CardMovement>().SetSplineContainer(Conveyor.Instance.GetMainSplineContainer());
        }
        else 
        {
            OnBufferFull?.Invoke();
        }
        
    }

    public void SendPackToConveyor(Card clickedCard)
    {
        string targetCardName = clickedCard.GetCardDataSO().name;

        if (_cardsInBuffer.Contains(clickedCard))
        {
            StartCoroutine(SendMatchingCardsWithDelay(targetCardName));
        }
        else
        {
            Debug.LogWarning("Clicked card not found in any column");
        }
    }

    private IEnumerator SendMatchingCardsWithDelay(string targetName)
    {
        List<Card> cardsToSend = new List<Card>();

        foreach (var card in _cardsInBuffer)
        {
            if (card.GetCardDataSO().name == targetName && Receiver.Instance.GetCanAcceptMore(cardsToSend.Count))
            {
                cardsToSend.Add(card);
            }else
                break;
        }

        OnSendCardsToConveyor?.Invoke(cardsToSend);

        foreach (var card in cardsToSend)
        {
            card.GetComponent<CardFromBufferAnimation>().SendCardToConveyorAnimation();
            _cardsInBuffer.Remove(card);
            yield return new WaitForSeconds(_delayBetweenCards);
        }

        yield return new WaitForSeconds(_delayBetweenCards);

        // Анимируем перемещение оставшихся карт с учетом индекса
        for (int i = 0; i < _cardsInBuffer.Count; i++)
        {
            var card = _cardsInBuffer[i];

            // Рассчитываем позицию с offset
            Vector3 targetPosition = _headPos.position + CalculateCardOffset(i);
            Quaternion targetRotation = _headPos.rotation;

            // Получаем или добавляем компонент анимации
            var animationComponent = card.GetComponent<RepositionCardInBufferAnimation>();
            if (animationComponent == null)
            {
                animationComponent = card.gameObject.AddComponent<RepositionCardInBufferAnimation>();
            }

            // Настраиваем параметры анимации
            animationComponent.animationDuration = 0.6f;
            animationComponent.jumpHeight = 1.2f;
            animationComponent.horizontalOffset = 0f;

            // Запускаем анимацию
            animationComponent.AnimateToPosition(targetPosition, targetRotation, i);

            yield return new WaitForSeconds(_delayBetweenCards);
        }

        yield return new WaitUntil(AllBufferAnimationsComplete);
    }

    private Vector3 CalculateCardOffset(int index)
    {
        // Например, смещение по оси X для каждой следующей карты
        return new Vector3(index * 0.2f, 0, 0);
    }

    private bool AllBufferAnimationsComplete()
    {
        foreach (var card in _cardsInBuffer)
        {
            var animationComponent = card.GetComponent<RepositionCardInBufferAnimation>();
            if (animationComponent != null && animationComponent.IsAnimating())
            {
                return false;
            }
        }
        return true;
    }

   
    public int GetBufferCardsListCount() => _cardsInBuffer.Count;
    public List<Card> GetBufferCardsList() => _cardsInBuffer;

}
