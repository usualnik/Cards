using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "CardDataSO")]
public class CardDataSo : ScriptableObject
{
    public string Title;
    public GameObject Prefab;
}
