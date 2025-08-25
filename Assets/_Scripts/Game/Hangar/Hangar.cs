using System.Collections.Generic;
using UnityEngine;

public class Hangar : MonoBehaviour
{
    [Header("Sorted Boxes")]
    [Tooltip("Сортированный список коробок, первая коробка будет отображаться целиком, вторая неполностью. " +
        "Следующие не будут отображаться пока не придет их очередь")]
    [SerializeField] private List<Box> _sortedBoxesList;

    [Header("System refs")]
    [SerializeField] private Transform _storageTransform;
    [SerializeField] private Transform _secondBoxTransform;
    [SerializeField] private Transform _firstBoxTransform;

    private void Start()
    {
        RearrangeBoxesInHangar();
        SubscribeHangarToBoxesInList();
    }

    private void OnDestroy()
    {
        UnsubHangarFromBoxesList();
    }

    private void RearrangeBoxesInHangar()
    {
        
        _sortedBoxesList.RemoveAll(box => box == null);

        for (int i = 0; i < _sortedBoxesList.Count; i++)
        {
            var box = _sortedBoxesList[i];

            if (box == null) continue;

            if (i == 0)
            {
                box.transform.SetParent(_firstBoxTransform, true);
                box.transform.position = _firstBoxTransform.position;
                //var cardTrigger = box.GetComponentInChildren<CardTriggerZone>();
                //if (cardTrigger != null) cardTrigger.gameObject.SetActive(true);
                //else
                //{
                //    Debug.Log("F U");
                //}
            }
            else if (i == 1)
            {
                box.transform.SetParent(_secondBoxTransform, true);
                box.transform.position = _secondBoxTransform.position;
                //var cardTrigger = box.GetComponentInChildren<CardTriggerZone>();
                //if (cardTrigger != null) cardTrigger.gameObject.SetActive(false);
            }
            else
            {
                box.transform.SetParent(_storageTransform, true);
                box.transform.position = _storageTransform.position;
                //var cardTrigger = box.GetComponentInChildren<CardTriggerZone>();
                //if (cardTrigger != null) cardTrigger.gameObject.SetActive(false);
            }
        }
    }

    private void SubscribeHangarToBoxesInList()
    {
        foreach (var box in _sortedBoxesList)
        {
            if (box != null)
            {
                box.OnDestroyBox += Box_OnDestroyBox;
            }
        }
    }

    private void UnsubHangarFromBoxesList()
    {
        foreach (var box in _sortedBoxesList)
        {
            if (box != null)
            {
                box.OnDestroyBox -= Box_OnDestroyBox;
            }
        }
    }

    private void Box_OnDestroyBox(Box box)
    {
       
        if (box != null)
        {
            box.OnDestroyBox -= Box_OnDestroyBox;
        }

        _sortedBoxesList.Remove(box);
        Invoke(nameof(RearrangeBoxesInHangar),0.5f);
    }
}