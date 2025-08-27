using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class TeleportIN : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;
    private Queue<Card> _teleportQueue = new Queue<Card>();
    private bool _isQueueProcessing = false;
    [SerializeField] private float _teleportDelay = 0.2f;
    [SerializeField] private float speedAfterTeleport = 0.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Card card))
        {
            _teleportQueue.Enqueue(card);
            card.GetComponent<CardMovement>().enabled = false;

            if (!_isQueueProcessing)
            {
                StartCoroutine(StartProcessQueue());
            }
        }
    }

    private IEnumerator StartProcessQueue()
    {
        _isQueueProcessing = true;

        while (_teleportQueue.Count > 0)
        {
            Card card = _teleportQueue.Dequeue();
            if (card != null)
            {
                CardMovement cardMovement = card.GetComponent<CardMovement>();
                if (cardMovement != null)
                {
                    
                    cardMovement.SetSplineContainer(splineContainer, speedAfterTeleport);
                    cardMovement.enabled = true;
                }
            }
            yield return new WaitForSeconds(_teleportDelay); 
        }

        _isQueueProcessing = false;
    }
}